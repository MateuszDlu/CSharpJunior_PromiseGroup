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
            Console.Clear();
            var productChoice = "";
            while(productChoice != "q"){
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
        }
        private static void removeProduct(){
            
        }
        private static void checkTotal(){
            
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