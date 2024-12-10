using System;
using System.Collections.Generic;
using Models;

namespace Models
{
    public class OrderItem
    {
        public Product product { get; set;}
        public Order order { get; set;}
        public OrderItem(Product product, Order order){
            this.product = product;
            this.order = order;
        }
    }
}