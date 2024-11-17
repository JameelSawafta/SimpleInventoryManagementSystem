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

    public void AddProduct(Product product)// implement this to take a list to be  reusable
    {
        _products.InsertOne(product);
    }

    public List<Product> ViewAllProducts()// let's rename it to get all products
    {
        return _products.Find(product => true).ToList();
    }

    public bool EditProduct(string name, Product updatedProduct)// also return the list of edited products
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);// i prefer add the filter to the id an update based on it
        _products.ReplaceOne(filter, updatedProduct);
        return true;
    }

    public bool DeleteProduct(string name)
    {
        var filter = Builders<Product>.Filter.Eq(p => p.Name, name);// id
        var result = _products.DeleteOne(filter);
        return result.DeletedCount > 0;
    }

    public Product SearchProductByName(string name)// return a list and take a list
    {
        return _products.Find<Product>(product => product.Name == name).FirstOrDefault();
    }
}