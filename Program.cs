﻿using SimpleInventoryManagementSystem.Models;
using SimpleInventoryManagementSystem.Repositories;
using SimpleInventoryManagementSystem.utalis;

namespace SimpleInventoryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        var inventory = new Inventory();
        var inMemoryRepository = new InMemoryInventoryRepository(inventory);
        
        var postegreSQLRepository = new PostgreSQLInventoryRepository();
        
       
        var mongoRepository = new MongoInventoryRepository();
        

        
        var menu = new MenuHandler(mongoRepository);
        menu.DisplayMenu();
    }
}