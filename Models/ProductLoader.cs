using System;
using System.Collections.Generic;
using System.IO;
using Models;

public static class ProductLoader
{
    public static List<Product> LoadProductsFromCsv()
    {
        var products = new List<Product>();

        try
        {
            using (var reader = new StreamReader("./Data/Products.csv"))
            {
                // Skip the header line (if present)
                var header = reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    var values = line.Split(',');

                    var product = new Product(int.Parse(values[0].Trim()), values[1].Trim(), decimal.Parse(values[2].Trim()));

                    products.Add(product);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred while loading the products: {ex.Message}");
        }

        return products;
    }
}
