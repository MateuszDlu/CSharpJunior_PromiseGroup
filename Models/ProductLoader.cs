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

                    var product = new Product
                    {
                        Id = int.Parse(values[0].Trim()),
                        Name = values[1].Trim(),
                        Price = decimal.Parse(values[2].Trim())
                    };

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
