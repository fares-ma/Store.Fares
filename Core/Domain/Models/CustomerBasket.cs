﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class CustomerBasket
    {
        public string id { get; set; }
        public IEnumerable<BasketItem> Items { get; set; }
    }
}   
