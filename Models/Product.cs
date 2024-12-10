using System;
using Models;

namespace Models
{
    public class Product
    {
        public int Id { get; set;}
        public string Name { get; set; }
        public decimal Price { get; set; }
        public override string ToString()
        {
            return $"{Name} - {Price}zł";
        }

        public Product(int id, string name, decimal price){
            Id = id;
            Name = name;
            Price = price;
        }
    }
}