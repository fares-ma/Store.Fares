using Shared.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IOrderService
    {
        // GetOrderById

       Task<OrderResultDto> GetOrderByIdAsync(Guid id);

        Task<IEnumerable<OrderResultDto>> GetOrderByUserEmailAsync(string userEmail);


        

     Task<OrderResultDto>   CreateOrderAsync(OrderRequestDto orderRequest, string userEmail);


       Task<IEnumerable<DeliveryMethodDto>> GetAllDeliveryMethods();


    }
}
