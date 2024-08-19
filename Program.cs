using SimpleInventoryManagementSystem.Models;
using SimpleInventoryManagementSystem.Repostories;
using SimpleInventoryManagementSystem.utalis;

namespace SimpleInventoryManagementSystem;

class Program
{
    static void Main(string[] args)
    {
        Inventory inventory = new Inventory();
        IInventoryRepository repository = new InMemoryInventoryRepository(inventory);
        var menu = new MenuHandler(repository);
        menu.DisplayMenu();
    }
}