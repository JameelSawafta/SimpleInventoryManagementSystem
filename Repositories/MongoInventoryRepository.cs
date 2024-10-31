using MongoDB.Bson;
using MongoDB.Driver;
using SimpleInventoryManagementSystem.Models;

namespace SimpleInventoryManagementSystem.Repositories;

public class MongoInventoryRepository : IInventoryRepository
{
    private readonly IMongoCollection<Product> _products;
    public MongoInventoryRepository()
    {
        var connectionString = "mongodb+srv://jameelsawafta:TwWELuJ4DTtpTdHa@cluster0.jcwn2.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0";
        var databaseName = "inventorydb";
        var client = new MongoClient(connectionString);
        var database = client.GetDatabase(databaseName);
        _products = database.GetCollection<Product>("products");
    }

    public void AddProduct(Product product)
    {
        _products.InsertOne(product);
    }

    public List<Product> ViewAllProducts()
    {
        return _products.Find(product => true).ToList();
    }

    public bool EditProduct(string name, Product updatedProduct)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        _products.ReplaceOne(filter, updatedProduct);
        return true;
    }

    public bool DeleteProduct(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);
        var result = _products.DeleteOne(filter);
        return result.DeletedCount > 0;
    }

    public Product SearchProductByName(string name)
    {
        return _products.Find<Product>(product => product.Name == name).FirstOrDefault();
    }
}