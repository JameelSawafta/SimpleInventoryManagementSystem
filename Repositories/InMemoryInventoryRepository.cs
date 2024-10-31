using SimpleInventoryManagementSystem.Models;

namespace SimpleInventoryManagementSystem.Repositories;

public class InMemoryInventoryRepository : IInventoryRepository
{
    private readonly Inventory _inventory;

    public InMemoryInventoryRepository(Inventory inventory)
    {
        _inventory = inventory;
    }


    public void AddProduct(Product product)
    {
        _inventory.Products.Add(product);
    }

    public List<Product>? ViewAllProducts()
    {
        return _inventory.Products;
    }

    public bool EditProduct(string productName, Product updatedProduct)
    {
        var product = _inventory.Products.Find(p => p.Name == productName);
        if (product != null)
        {
            product.Name = updatedProduct.Name;
            product.Quantity = updatedProduct.Quantity;
            product.Price = updatedProduct.Price;
            return true;
        }
        return false;
    }

    public bool DeleteProduct(string productName)
    {
        var product = _inventory.Products.Find(p => p.Name == productName);
        if (product != null)
        {
            _inventory.Products.Remove(product);
            return true;
        }
        return false;
    }

    public Product SearchProductByName(string productName)
    {
        var product = _inventory.Products.Find(p => p.Name == productName);
        return product;
    }
}