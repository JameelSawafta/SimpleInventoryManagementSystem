using SimpleInventoryManagementSystem.Models;

namespace SimpleInventoryManagementSystem.Repostories;

public interface IInventoryRepository
{
    public void AddProduct(Product product);
    public List<Product>? ViewAllProducts();
    public bool EditProduct(string productName, Product updatedProduct);
    public bool DeletProduct(string productName);
    public Product SearchProductByName(string productName);
}