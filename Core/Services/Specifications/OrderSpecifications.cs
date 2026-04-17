using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderSpecifications(Guid orderId) 
            : base(o => o.Id == orderId)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }

        public OrderSpecifications(string userEmail) 
            : base(o => o.UserEmail == userEmail)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
            AddOrderByDescending(o => o.OrderDate);
        }
    }
}
