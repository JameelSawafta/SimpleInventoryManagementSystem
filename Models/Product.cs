using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SimpleInventoryManagementSystem.Models;

public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id = Guid.NewGuid();
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