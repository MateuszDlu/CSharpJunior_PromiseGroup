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
            var productChoice = ""; // Two variables for loop
            while(productChoice != "q"){
                Console.Clear();
                Console.WriteLine("Wybierz produkt do dodania do zamówienia");

                foreach (var product in productList) // Show products to user
                {
                    int productQuantity = orderItemList.Count(o => o.product.Id == product.Id);
                    Console.WriteLine($"[{product.Id}] ({productQuantity} szt.) {product.ToString()}");
                }
                Console.WriteLine("[q] Zakończ");
                productChoice = Console.ReadLine();

                if (productChoice == "q") break;

                foreach (var product in productList){ // Match user's choice to object
                    if (productChoice == product.Id.ToString()){
                        orderItemList.Add(new OrderItem(product, order)); // Make OrderItem object
                    }
                }
            }
            Console.Clear();
        }
        private static void removeProduct(){
            string productChoice = "";
            while (productChoice != "q")
            {
                Console.Clear();
                Console.WriteLine("Wybierz produkt do usunięcia z zamówienia");
                
                // Show products to user with current order quantities
                foreach (var product in productList)
                {
                    int productQuantity = orderItemList.Count(o => o.product.Id == product.Id);
                    if (productQuantity > 0)  // Only show products that are in the order
                    {
                        Console.WriteLine($"[{product.Id}] {product.ToString()} ({productQuantity} szt.)");
                    }
                }

                Console.WriteLine("[q] Zakończ");
                productChoice = Console.ReadLine();

                if (productChoice == "q") break;

                foreach (var product in productList)
                {
                    if (productChoice == product.Id.ToString())
                    {
                        var orderItemsToRemove = orderItemList.FindAll(o => o.product.Id == product.Id);
                        if (orderItemsToRemove.Count > 0)
                        {
                            orderItemList.Remove(orderItemsToRemove[orderItemsToRemove.Count - 1]); // Remove last item of this product
                        }
                    }
                }
            }
            Console.Clear();
        }
        private static void checkTotal(){
            Console.Clear();
            // Dictionary to hold product counts
            Dictionary<Product, int> productCounts = new Dictionary<Product, int>();
            decimal totalAmount = 0;
            decimal discount = 0;
            string discountText = "";
            Product discountedProduct = null;

            // Add item if key exists and make key if not
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

            // Discount system (understood as discount per item only counted when 2 or 3 items in order, more does not comply and there is no choice)
            if (orderItemList.Count == 2){
                discount = 0.9M;
                discountText = "10%";
                discountedProduct = productCounts.Keys.OrderBy(p => p.Price).First();
            }else if (orderItemList.Count == 3){
                discount = 0.8M;
                discountText = "20%";
                discountedProduct = productCounts.Keys.OrderBy(p => p.Price).First();
            }
            Console.WriteLine(productCounts.Keys.OrderBy(p => p.Price));
            // Display the items in the order with their quantities
            Console.WriteLine("Produkty w zamówieniu:");
            foreach (var entry in productCounts){
                var product = entry.Key;
                int quantity = entry.Value;

                decimal productTotal = 0;

                // Calculate total price for each product including discounts
                if (product == discountedProduct){
                    if (quantity > 1)
                    {
                        // Apply discount to one unit, and normal price to the rest
                        productTotal = product.Price * discount;
                        totalAmount += productTotal;
                        Console.WriteLine($"{product.Name} {product.Price}zł [1 szt.] - {productTotal}zł ({discountText})");
                        quantity--;

                        productTotal = product.Price * quantity;
                        totalAmount += productTotal;
                        Console.WriteLine($"{product.Name} {product.Price}zł [{quantity} szt.] - {productTotal}zł");
                    }
                    else{
                        productTotal = product.Price * discount;
                        totalAmount += productTotal;
                        Console.WriteLine($"{product.Name} {product.Price}zł [1 szt.] - {productTotal}zł ({discountText})");
                    }
                }else{
                    productTotal = product.Price * quantity;
                    totalAmount += productTotal;
                    Console.WriteLine($"{product.Name} {product.Price}zł [{quantity} szt.] - {productTotal}zł");
                }
            }

            // Apply additional discount for total > 5000 PLN
            if (totalAmount > 5000)
            {
                decimal discountForLargeOrder = totalAmount * 0.05M;
                totalAmount *= 0.95M;
                Console.WriteLine($"\nDodatkowa zniżka 5% za zamówienie powyżej 5000zł: -{discountForLargeOrder}zł");
            }

            // Display the final total
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
            // Menu loop
            while (!exitFlag){
                Console.WriteLine("Wybierz akcję którą chcesz wykonać:");
                Console.Write("[1] Dodaj produkt do zamówienia \n" +
                              "[2] Usuń produkt z zamówienia \n" +
                              "[3] Zobacz sumę zamówienia \n" +
                              "[q] Wyjdź \n");
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
                    case "q":{
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