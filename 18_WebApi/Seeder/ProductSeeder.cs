using _18_WebApi.DataContext;
using _18_WebApi.Models;

namespace _18_WebApi.Seeder
{
    public static class ProductSeeder
    {
        public static void Seed(ProductContext context)
        {
            // Veritabanında veri olup olmadığını kontrol et
            if (context.Products.Any())
            {
                return; // Veriler zaten mevcut
            }

            var products = new List<Product>
            {
                new Product
                {
                    Title = "Product1",
                    Description = "Description for Product 1",
                    Category = "Category 1",
                    Price = 10.99m,
                    DiscountPercentage = 5.0m,
                    Rating = 4.5,
                    Stock = 20,
                    Tags = new List<string> { "tag1", "tag2" },
                    Brand = "Brand A",
                    Sku = "SKU1",
                    Weight = 0.5m,
                    WarrantyInformation = "1 year warranty",
                    ShippingInformation = "Ships in 3 days",
                    AvailabilityInformation = "In Stock",
                    ReturnPolicy = "30 days return policy",
                    MinimumOrderQuantity = 1,
                    Thumbnail = "thumbnail1.jpg",
                    Images = new List<string> { "image1.jpg", "image2.jpg" }
                },
                new Product
                {
                    Title = "Product2",
                    Description = "Description for Product 2",
                    Category = "Category 2",
                    Price = 25.50m,
                    DiscountPercentage = 10.0m,
                    Rating = 4.0,
                    Stock = 50,
                    Tags = new List<string> { "tag3", "tag4" },
                    Brand = "Brand B",
                    Sku = "SKU2",
                    Weight = 1.2m,
                    WarrantyInformation = "6 months warranty",
                    ShippingInformation = "Ships in 5 days",
                    AvailabilityInformation = "Limited Stock",
                    ReturnPolicy = "14 days return policy",
                    MinimumOrderQuantity = 2,
                    Thumbnail = "thumbnail2.jpg",
                    Images = new List<string> { "image3.jpg", "image4.jpg" }
                },
                new Product
                {
                    Title = "Product3",
                    Description = "Description for Product 3",
                    Category = "Category 3",
                    Price = 35.75m,
                    DiscountPercentage = 15.0m,
                    Rating = 4.8,
                    Stock = 30,
                    Tags = new List<string> { "tag5", "tag6" },
                    Brand = "Brand C",
                    Sku = "SKU3",
                    Weight = 0.8m,
                    WarrantyInformation = "2 years warranty",
                    ShippingInformation = "Ships in 1 day",
                    AvailabilityInformation = "In Stock",
                    ReturnPolicy = "60 days return policy",
                    MinimumOrderQuantity = 1,
                    Thumbnail = "thumbnail3.jpg",
                    Images = new List<string> { "image5.jpg", "image6.jpg" }
                },
                new Product
                {
                    Title = "Product4",
                    Description = "Description for Product 4",
                    Category = "Category 1",
                    Price = 45.00m,
                    DiscountPercentage = 20.0m,
                    Rating = 4.2,
                    Stock = 15,
                    Tags = new List<string> { "tag7", "tag8" },
                    Brand = "Brand D",
                    Sku = "SKU4",
                    Weight = 0.9m,
                    WarrantyInformation = "1 year warranty",
                    ShippingInformation = "Ships in 7 days",
                    AvailabilityInformation = "Pre-order",
                    ReturnPolicy = "30 days return policy",
                    MinimumOrderQuantity = 3,
                    Thumbnail = "thumbnail4.jpg",
                    Images = new List<string> { "image7.jpg", "image8.jpg" }
                },
                new Product
                {
                    Title = "Product5",
                    Description = "Description for Product 5",
                    Category = "Category 2",
                    Price = 55.99m,
                    DiscountPercentage = 25.0m,
                    Rating = 4.7,
                    Stock = 40,
                    Tags = new List<string> { "tag9", "tag10" },
                    Brand = "Brand E",
                    Sku = "SKU5",
                    Weight = 1.5m,
                    WarrantyInformation = "6 months warranty",
                    ShippingInformation = "Ships in 2 days",
                    AvailabilityInformation = "In Stock",
                    ReturnPolicy = "45 days return policy",
                    MinimumOrderQuantity = 1,
                    Thumbnail = "thumbnail5.jpg",
                    Images = new List<string> { "image9.jpg", "image10.jpg" }
                },
                new Product
                {
                    Title = "Product6",
                    Description = "Description for Product 6",
                    Category = "Category 3",
                    Price = 65.30m,
                    DiscountPercentage = 12.0m,
                    Rating = 4.6,
                    Stock = 25,
                    Tags = new List<string> { "tag11", "tag12" },
                    Brand = "Brand F",
                    Sku = "SKU6",
                    Weight = 1.1m,
                    WarrantyInformation = "1 year warranty",
                    ShippingInformation = "Ships in 4 days",
                    AvailabilityInformation = "In Stock",
                    ReturnPolicy = "30 days return policy",
                    MinimumOrderQuantity = 2,
                    Thumbnail = "thumbnail6.jpg",
                    Images = new List<string> { "image11.jpg", "image12.jpg" }
                },
                new Product
                {
                    Title = "Product7",
                    Description = "Description for Product 7",
                    Category = "Category 1",
                    Price = 75.00m,
                    DiscountPercentage = 18.0m,
                    Rating = 4.3,
                    Stock = 60,
                    Tags = new List<string> { "tag13", "tag14" },
                    Brand = "Brand G",
                    Sku = "SKU7",
                    Weight = 2.0m,
                    WarrantyInformation = "2 years warranty",
                    ShippingInformation = "Ships in 3 days",
                    AvailabilityInformation = "Pre-order",
                    ReturnPolicy = "30 days return policy",
                    MinimumOrderQuantity = 1,
                    Thumbnail = "thumbnail7.jpg",
                    Images = new List<string> { "image13.jpg", "image14.jpg" }
                },
                new Product
                {
                    Title = "Product8",
                    Description = "Description for Product 8",
                    Category = "Category 2",
                    Price = 85.20m,
                    DiscountPercentage = 10.0m,
                    Rating = 4.9,
                    Stock = 35,
                    Tags = new List<string> { "tag15", "tag16" },
                    Brand = "Brand H",
                    Sku = "SKU8",
                    Weight = 1.8m,
                    WarrantyInformation = "6 months warranty",
                    ShippingInformation = "Ships in 2 days",
                    AvailabilityInformation = "In Stock",
                    ReturnPolicy = "60 days return policy",
                    MinimumOrderQuantity = 2,
                    Thumbnail = "thumbnail8.jpg",
                    Images = new List<string> { "image15.jpg", "image16.jpg" }
                },
                new Product
                {
                    Title = "Product9",
                    Description = "Description for Product 9",
                    Category = "Category 3",
                    Price = 95.75m,
                    DiscountPercentage = 22.0m,
                    Rating = 4.4,
                    Stock = 20,
                    Tags = new List<string> { "tag17", "tag18" },
                    Brand = "Brand I",
                    Sku = "SKU9",
                    Weight = 1.3m,
                    WarrantyInformation = "1 year warranty",
                    ShippingInformation = "Ships in 5 days",
                    AvailabilityInformation = "Limited Stock",
                    ReturnPolicy = "30 days return policy",
                    MinimumOrderQuantity = 1,
                    Thumbnail = "thumbnail9.jpg",
                    Images = new List<string> { "image17.jpg", "image18.jpg" }
                },
                new Product
                {
                    Title = "Product10",
                    Description = "Description for Product 10",
                    Category = "Category 1",
                    Price = 105.00m,
                    DiscountPercentage = 30.0m,
                    Rating = 4.6,
                    Stock = 45,
                    Tags = new List<string> { "tag19", "tag20" },
                    Brand = "Brand J",
                    Sku = "SKU10",
                    Weight = 1.7m,
                    WarrantyInformation = "1 year warranty",
                    ShippingInformation = "Ships in 3 days",
                    AvailabilityInformation = "In Stock",
                    ReturnPolicy = "30 days return policy",
                    MinimumOrderQuantity = 1,
                    Thumbnail = "thumbnail10.jpg",
                    Images = new List<string> { "image19.jpg", "image20.jpg" }
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();


        }
    }
}
