using Npgsql;
using SimpleInventoryManagementSystem.Models;

namespace SimpleInventoryManagementSystem.Repositories;

public class PostgreSQLInventoryRepository : IInventoryRepository
{
    private readonly string _connectionString = "Host=localhost;Database=inventorydb;Username=postgres;Password=0597071618";
    public void AddProduct(Product product)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("INSERT INTO Products (id, name, price, quantity) VALUES (@id, @name, @price, @quantity)", conn);
        cmd.Parameters.AddWithValue("id", product.Id);
        cmd.Parameters.AddWithValue("name", product.Name);
        cmd.Parameters.AddWithValue("price", product.Price);
        cmd.Parameters.AddWithValue("quantity", product.Quantity);

        cmd.ExecuteNonQuery();
    }

    public List<Product>? ViewAllProducts()
    {
        var products = new List<Product>();

        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT id, name, price, quantity FROM Products", conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            products.Add(new Product(
                reader.GetString(1), 
                reader.GetDecimal(2), 
                reader.GetInt64(3)  
            ));
        }

        return products;
    }

    public bool EditProduct(string productName, Product updatedProduct)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("UPDATE Products SET name = @newname, price = @newprice, quantity = @newquantity WHERE name = @productname", conn);
        cmd.Parameters.AddWithValue("newname", updatedProduct.Name);
        cmd.Parameters.AddWithValue("newprice", updatedProduct.Price);
        cmd.Parameters.AddWithValue("newquantity", updatedProduct.Quantity);
        cmd.Parameters.AddWithValue("productname", productName);

        return cmd.ExecuteNonQuery() > 0;
    }

    public bool DeleteProduct(string productname)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("DELETE FROM Products WHERE name = @productname", conn);
        cmd.Parameters.AddWithValue("productname", productname);

        return cmd.ExecuteNonQuery() > 0;
    }

    public Product SearchProductByName(string productName)
    {
        using var conn = new NpgsqlConnection(_connectionString);
        conn.Open();

        using var cmd = new NpgsqlCommand("SELECT id, name, price, quantity FROM Products WHERE name = @productname", conn);
        cmd.Parameters.AddWithValue("productname", productName);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            return new Product(
                reader.GetString(1), 
                reader.GetDecimal(2), 
                reader.GetInt64(3)   
            );
        }
        return null;
    }
}