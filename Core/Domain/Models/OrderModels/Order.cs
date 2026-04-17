using Domain.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.OrderModels
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }

        public Order(string userEmail, Address shippingAddress, ICollection<OrderItem> orderItems, DeliveryMethod deliveryMethod, decimal subTotal, string paymentIntentId)
        {
            Id = Guid.NewGuid();
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DeliveryMethod = deliveryMethod;
            SubTotal = subTotal;
            PaymentIntentId = paymentIntentId;
        }

        public string UserEmail { get; set; }

        public Address ShippingAddress { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>(); // Navigatiional Property

        public DeliveryMethod  DeliveryMethod { get; set; }


        public int? DeliveryMethodId { get; set; } // FK

        // Payment Status

        public OrderPaymentStatus PaymentStatus { get; set; }= OrderPaymentStatus.Pending;

        public decimal SubTotal { get; set; }

        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;

        // Payment
        public string PaymentIntentId { get; set; }
    }
}
