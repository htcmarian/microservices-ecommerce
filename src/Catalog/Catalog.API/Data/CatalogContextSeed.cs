using System.Collections.Generic;
using Catalog.API.Entities;
using Faker.Resources;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            var existsProduct = productCollection.Find(p => true).Any();

            if (!existsProduct)
            {
                productCollection.InsertManyAsync(GetPreconfiguredProducts());    
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "IPhone X",
                    Summary = "This iphone is the company's biggest change to its flagship smartphone in years",
                    Description = Faker.Lorem.Paragraph(2),
                    ImageFile = "product-1.png",
                    Category = "Smart Phone",
                    Price = Faker.RandomNumber.Next(5200,7000)
                },
                new Product
                {
                    Name = "Macbook Pro 16'",
                    Summary = "Macbook Pro 16' with Retina display",
                    Description = Faker.Lorem.Paragraph(2),
                    ImageFile = "product-2.png",
                    Category = "Laptop",
                    Price = Faker.RandomNumber.Next(10000, 12000)
                },
                new Product
                {
                    Name = "Macbook Air 13'",
                    Summary = "Macbook Air 13' with Retina display",
                    Description = Faker.Lorem.Paragraph(2),
                    ImageFile = "product-2.png",
                    Category = "Laptop",
                    Price = Faker.RandomNumber.Next(8000,10000)
                },
                new Product
                {
                    Name = "IPad Mini",
                    Summary = "Company's latest medium-sized tabled with top generation hardware",
                    Description = Faker.Lorem.Paragraph(2),
                    ImageFile = "product-3.png",
                    Category = "Tablet",
                    Price = Faker.RandomNumber.Next(1000,2000)
                }
            };
        }
    }
}