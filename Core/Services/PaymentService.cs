using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Services.Abstractions;
using Shared;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderProduct = Domain.Models.Product;

namespace Services
{
    public class PaymentService(IBasketRepository basketRepository, IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketid)
        {
            var basket = await basketRepository.GetBasketAsync(basketid);

            if(basket is null)
            {
                throw new BasketNotFoundException(basketid);
            }

            foreach(var item in basket.Items)
            {
              var product = await unitOfWork.GetRepository<OrderProduct, int>().GetAsync(item.Id);

                if(product is null)
                {
                    throw new ProductNotFoundExceptions(item.Id);
                }

                item.Price = product.Price;

            }

            if(!basket.DeliveryMethodId.HasValue) throw new  Exception("Delivery method not set for the basket.");

        var deliveryMethod = await  unitOfWork.GetRepository<DeliveryMethod, int>().GetAsync(basket.DeliveryMethodId.Value);

            if(deliveryMethod is null)
            {
                throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);
            }

            basket.ShippingPrice = deliveryMethod.Cost;

            // Amount 



            var amount = (long)(basket.Items.Sum(item => item.Price * item.Quantity) + basket.ShippingPrice) * 100;

            StripeConfiguration.ApiKey = configuration["StripeSettings:SecretKey"];

            var service = new PaymentIntentService();

            if (string.IsNullOrEmpty(basket.PaymentIntentId))
            {
                //Create a new payment intent

                var createOptions = new PaymentIntentCreateOptions
                {
                    Amount = amount, // Stripe expects the amount in cents
                    Currency = "usd", // Change to your desired currency
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var paymentIntent = await service.CreateAsync(createOptions);

                basket.PaymentIntentId = paymentIntent.Id;
                basket.ClientSecret = paymentIntent.ClientSecret;
            }
            else
            {
                //Update the payment intent

                var updateOptions = new PaymentIntentUpdateOptions
                {
                    Amount = amount, // Stripe expects the amount in cents
                };

                var paymentIntent = await service.UpdateAsync(basket.PaymentIntentId, updateOptions);
            }

           
           await basketRepository.UpdateBasketAsync(basket);

          var result = mapper.Map<BasketDto>(basket);

            return result;

        }
    }
}
