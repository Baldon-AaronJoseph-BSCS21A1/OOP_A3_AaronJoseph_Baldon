using System;
using System.Collections.Generic;

namespace CoffeeShopApp
{
    public class Product
    {
        public string Name { get; }
        public double Price { get; }

        public Product(string name, double price)
        {
            Name = name;
            Price = price;
        }

        public override string ToString()
        {
            return $"{Name} - ${Price:F2}";
        }
    }

    public class ProductCatalog
    {
        private List<Product> products = new List<Product>();

        public void AddProduct(Product product)
        {
            products.Add(product);
            Console.WriteLine("Product added successfully!");
        }

        public void DisplayProducts()
        {
            Console.WriteLine("Menu:");
            for (int i = 0; i < products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {products[i]}");
            }
        }

        public Product? GetProduct(int index)
        {
            if (index >= 1 && index <= products.Count)
            {
                return products[index - 1];
            }
            return null;
        }
    }

    public class Order
    {
        private List<Product> items = new List<Product>();
        public Buyer? Buyer { get; private set; } // Nullable property

        public void SetBuyer(Buyer? buyer)
        {
            Buyer = buyer;
        }

        public void AddProduct(Product? product)
        {
            if (product != null)
            {
                items.Add(product);
                Console.WriteLine("Item added to order!");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        public void DisplayOrder()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Your order is empty!");
                return;
            }

            Console.WriteLine("Your Order:");
            if (Buyer != null)
            {
                Console.WriteLine(Buyer); // Display the buyer's name
            }
            foreach (var product in items)
            {
                Console.WriteLine(product);
            }
        }

        public double CalculateTotal()
        {
            double total = 0;
            foreach (var product in items)
            {
                total += product.Price;
            }
            return total;
        }
    }

    public class Buyer
    {
        public string Name { get; }

        public Buyer(string name)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name)); // Throw if name is null
        }

        public override string ToString()
        {
            return $"Buyer: {Name}";
        }
    }

    public class CoffeeShop
    {
        private ProductCatalog productCatalog = new ProductCatalog();
        private Order order = new Order();

        public void ShowMainMenu()
        {
            Console.WriteLine("\nWelcome to the Coffee Shop!");
            Console.WriteLine("1. Add Menu Item");
            Console.WriteLine("2. View Menu");
            Console.WriteLine("3. Place Order");
            Console.WriteLine("4. View Order");
            Console.WriteLine("5. Calculate Total");
            Console.WriteLine("6. Exit");
        }

        public void Run()
        {
            while (true)
            {
                ShowMainMenu();
                Console.Write("Select an option: ");
                if (int.TryParse(Console.ReadLine(), out int choice))
                {
                    switch (choice)
                    {
                        case 1:
                            AddProduct();
                            break;

                        case 2:
                            productCatalog.DisplayProducts();
                            break;

                        case 3:
                            PlaceOrder();
                            break;

                        case 4:
                            order.DisplayOrder();
                            break;

                        case 5:
                            ShowTotal();
                            break;

                        case 6:
                            Console.WriteLine("Thank you for visiting the Coffee Shop!");
                            return;

                        default:
                            Console.WriteLine("Invalid option. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }

        private void AddProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine() ?? string.Empty; // Handle possible null

            Console.Write("Enter product price: ");
            if (double.TryParse(Console.ReadLine(), out double price))
            {
                productCatalog.AddProduct(new Product(name, price));
            }
            else
            {
                Console.WriteLine("Invalid price. Please enter a numeric value.");
            }
        }

        private void PlaceOrder()
        {
            Console.Write("Enter buyer's name: ");
            string? buyerName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(buyerName))
            {
                Console.WriteLine("Name cannot be null or empty.");
                return;
            }

            Buyer buyer = new Buyer(buyerName);
            order.SetBuyer(buyer);

            productCatalog.DisplayProducts();
            Console.Write("Enter the product number to order: ");
            if (int.TryParse(Console.ReadLine(), out int productNumber))
            {
                Product? product = productCatalog.GetProduct(productNumber);
                order.AddProduct(product);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void ShowTotal()
        {
            double total = order.CalculateTotal();
            Console.WriteLine($"Total Amount Payable: ${total:F2}");
        }
    }

    class Program
    {
        static void Main()
        {
            CoffeeShop coffeeShop = new CoffeeShop();
            coffeeShop.Run();
        }
    }
}

