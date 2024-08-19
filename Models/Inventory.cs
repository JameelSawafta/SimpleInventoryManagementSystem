namespace SimpleInventoryManagementSystem.Models;

public class Inventory
{
    public Guid Id = Guid.NewGuid();
    public List<Product> Products = new List<Product>();
}