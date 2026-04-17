using Domain.Models.OrderModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class OrderWithPaymentIntentSpecifications : BaseSpecifications<Order, Guid>
    {
        public OrderWithPaymentIntentSpecifications(string paymentIntentId) 
            : base(o => o.PaymentIntentId == paymentIntentId)
        {
            AddInclude(o => o.DeliveryMethod);
            AddInclude(o => o.OrderItems);
        }
    }
}
