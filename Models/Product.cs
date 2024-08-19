namespace SimpleInventoryManagementSystem.Models;

public class Product
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public long Quantity { get; set; }

    public Product(string name, decimal price, long quantity)
    {
        Name = name;
        Price = price;
        Quantity = quantity;
    }
}