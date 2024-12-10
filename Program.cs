using System;
using System.Collections.Generic;
using Models;

namespace CSharpJunior_PromiseGroup
{
    public class Program
    {
        static List<Product> productList = ProductLoader.LoadProductsFromCsv();
        static Order order = new Order();
        static List<OrderItem> orderItemList = new List<OrderItem>(); 
        private static void addProduct(){
            Product chosenProduct = null;
            var productChoice = "";
            while(productChoice != "q"){
                Console.Clear();
                Console.WriteLine("Wybierz produkt do dodania do zamówienia");
                foreach (var product in productList)
                {
                    Console.WriteLine("[" + product.Id + "] " + product.ToString());
                }
                Console.WriteLine("[q] Zakończ");
                productChoice = Console.ReadLine();
                foreach (var product in productList){
                    if (productChoice == product.Id.ToString()){
                        orderItemList.Add(new OrderItem(product, order));
                    }
                }
            }
            Console.Clear();
        }
        private static void removeProduct(){
            
        }
        private static void checkTotal(){
            Console.Clear();
            // Dictionary to hold product counts
            Dictionary<Product, int> productCounts = new Dictionary<Product, int>();

            foreach (var orderItem in orderItemList)
            {
                if (productCounts.ContainsKey(orderItem.product))
                {
                    productCounts[orderItem.product]++;
                }
                else
                {
                    productCounts[orderItem.product] = 1;
                }
            }

            // Display the items in the order with their quantities
            Console.WriteLine("Produkty w zamówieniu:");
            decimal totalAmount = 0;
            foreach (var entry in productCounts)
            {
                var product = entry.Key;
                int quantity = entry.Value;

                // Calculate total price for each product
                decimal productTotal = product.Price * quantity;
                totalAmount += productTotal;

                // Display the product name, quantity, and total price for that product
                Console.WriteLine($"{product.Name} {product.Price}zł [{quantity} szt.] - {productTotal}zł");
            }

            // Display the total sum to pay
            Console.WriteLine($"\nCałkowita suma do zapłaty: {totalAmount}zł");
            Console.Write("Aby wrócić do menu wciśnij ENTER");
            Console.ReadLine();
            Console.Clear();
        }

        public static void Main(string[] args)
        {
            Console.Clear();
            var exitFlag = false;
            Console.WriteLine("Witamy w aplikacji konsolowej do składania zamówień!");
            
            while (!exitFlag){
                Console.WriteLine("Wybierz akcję którą chcesz wykonać:");
                Console.Write("[1] Dodaj produkt do zamówienia \n" +
                              "[2] Usuń produkt z zamówienia \n" +
                              "[3] Zobacz sumę zamówienia \n" +
                              "[4] Wyjdź \n");
                var menuChoice = Console.ReadLine();
                Console.Clear();
                switch (menuChoice){
                    case "1":{
                        addProduct();
                        break;
                    }
                    case "2":{
                        removeProduct();
                        break;
                    }
                    case "3":{
                        checkTotal();
                        break;
                    }
                    case "4":{
                        exitFlag = true;
                        break;
                    }
                    default:{
                        Console.WriteLine("Proszę wybrać numer zawarty w '[]' jako opcję");
                        break;
                    }
                }
            }
        }
    }
}