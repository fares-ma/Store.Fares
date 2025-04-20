using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public class BasketDto
    {
        public int Id { get; set; }
        public IEnumerator<BasketItemDto> Items { get; set; }
    }
}
