using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderModels
{
    public class OrderRequestDto
    {
        public string BasketId { get; set; }

        public int DeliveryMethod { get; set; }
        public AddressDto ShipToAddress { get; set; }

        public int DeliveryMethodId { get; set; }

    }
}
