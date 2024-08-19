using SimpleInventoryManagementSystem.Models;
using SimpleInventoryManagementSystem.Repostories;

namespace SimpleInventoryManagementSystem.utalis;

public class MenuHandler
{
       private readonly IInventoryRepository _repository;

        public MenuHandler(IInventoryRepository repository)
        {
            _repository = repository;
        }

        public void DisplayMenu()
        {
            while (true)
            {
                Console.WriteLine("\nInventory Management System");
                Console.WriteLine("1. Add a product");
                Console.WriteLine("2. View all products");
                Console.WriteLine("3. Edit a product");
                Console.WriteLine("4. Delete a product");
                Console.WriteLine("5. Search for a product");
                Console.WriteLine("6. Exit");
                Console.Write("Select an option: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddProduct();
                        break;
                    case "2":
                        ViewAllProducts();
                        break;
                    case "3":
                        EditProduct();
                        break;
                    case "4":
                        DeleteProduct();
                        break;
                    case "5":
                        SearchProduct();
                        break;
                    case "6":
                        ExitApplication();
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please select a valid option.");
                        break;
                }
            }
        }

        private void AddProduct()
        {
            Console.Write("Enter product name: ");
            string name = Console.ReadLine();

            Console.Write("Enter product price: ");
            decimal price = decimal.Parse(Console.ReadLine());

            Console.Write("Enter product quantity: ");
            long quantity = long.Parse(Console.ReadLine());

            var product = new Product (name, price, quantity );
            _repository.AddProduct(product);

            Console.WriteLine("Product added successfully.");
        }

        private void ViewAllProducts()
        {
            var products = _repository.ViewAllProducts();
            Console.WriteLine("\nProduct List:");
            foreach (var product in products)
            {
                Console.WriteLine($"Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            }
        }

        private void EditProduct()
        {
            Console.Write("Enter the product name to edit: ");
            string name = Console.ReadLine();

            var product = _repository.SearchProductByName(name);
            if (product != null)
            {
                Console.WriteLine($"Editing product: {product.Name}");

                Console.Write("Enter new name (leave blank to keep current name): ");
                string newName = Console.ReadLine();
                if (!string.IsNullOrEmpty(newName))
                {
                    product.Name = newName;
                }

                Console.Write("Enter new price (leave blank to keep current price): ");
                string newPriceStr = Console.ReadLine();
                if (decimal.TryParse(newPriceStr, out decimal newPrice))
                {
                    product.Price = newPrice;
                }

                Console.Write("Enter new quantity (leave blank to keep current quantity): ");
                string newQuantityStr = Console.ReadLine();
                if (long.TryParse(newQuantityStr, out long newQuantity))
                {
                    product.Quantity = newQuantity;
                }

                _repository.EditProduct(name, product);
                Console.WriteLine("Product updated successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        private void DeleteProduct()
        {
            Console.Write("Enter the product name to delete: ");
            string name = Console.ReadLine();

            var deleted = _repository.DeletProduct(name);
            if (deleted)
            {
                Console.WriteLine("Product deleted successfully.");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        private void SearchProduct()
        {
            Console.Write("Enter the product name to search: ");
            string name = Console.ReadLine();

            var product = _repository.SearchProductByName(name);
            if (product != null)
            {
                Console.WriteLine($"Product found: Name: {product.Name}, Price: {product.Price}, Quantity: {product.Quantity}");
            }
            else
            {
                Console.WriteLine("Product not found.");
            }
        }

        private void ExitApplication()
        {
            Console.WriteLine("Exiting application. Goodbye!");
        }
}