namespace InventorySystem;

public class InventoryManager
{
    public static Dictionary<string, int> Presetinventory { get; private set; } = new Dictionary<string, int>
    {
        { "Stick", 16 },
        { "String", 24 },
        { "Rope", 4 },
        { "Cloth", 8 },
        { "Bandage", 4 },
        { "Med-kit", 2 },
        { "Metal Pipe", 1 },
        { "Wooden Bat", 1 },
    };
    
    public static void PrintItems(Dictionary<string, int> inventory)
    {
        if (inventory.Count < 1)
        {
            Console.WriteLine("No items found in inventory.");
        }
        else
        {
            Console.WriteLine("Current items in inventory:");
            foreach (var kvp in inventory)
            {
                var quantity = kvp.Value;
                Console.WriteLine("------------------------------------------------------------------------------------------");
                PrintItem(kvp.Key, quantity);
            }
        }
    }
    
    public static void PrintItem(string name, int quantity)
    {
        Console.WriteLine($"{name}: {quantity}");
    }
    
    /// <summary>
    /// Adds an item to an inventory. If the item already exists in the inventory, the amount will be added to the existing amount.
    /// </summary>
    /// <param name="item">The name of the item to add</param>
    /// <param name="amount">The amount to add</param>
    /// <param name="inventory">Reference to the inventory that will be searched</param>
    public static void AddItem(string item, int amount, ref Dictionary<string, int> inventory)
    {
        // using LINQ to filter results
        Dictionary<string, int> temp = inventory.Where(kvp => kvp.Key.ToLower().Contains(item.ToLower()))
            .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        
        if (temp.Count > 0)
        {
            inventory[item] += amount;
        }
        else
        {
            inventory.Add(item, amount);
        }
    }
    
    /// <summary>
    /// Attempts to remove an item from an inventory. If the removal fails, a message will be printed to the console.
    /// </summary>
    /// <param name="item">The name of the item to search for</param>
    /// <param name="amount">The amount to remove</param>
    /// <param name="inventory">Reference to the inventory that will be searched</param>
    /// <returns>Was the removal successful</returns>
    public static bool RemoveItem(string item, int amount, ref Dictionary<string, int> inventory)
    {
        bool result = false;
        
        if (inventory.ContainsKey(item))
        {
            // calculate the amount of items left after removal
            int temp = inventory[item] - amount;

            // if the calculated amount is greater than or equal to 0, then initiate the removal of the item
            if (temp >= 0)
            {
                switch (temp)
                {
                    case 0:
                        inventory.Remove(item);
                        result = true;
                        break;
                    
                    default:
                        inventory[item] -= amount;
                        result = true;
                        break;
                }
            }
            else
            {
                // you cannot remove items that you don't have
                Console.WriteLine("You are trying to remove more items than are available in the inventory.");
                result = false;
            }
        }
        else
        {
            PrintItemNotFound();
        }
        return result;
    }

    public static void PrintItemNotFound()
    {
        Console.WriteLine("Item not found in inventory.");
    }
    
}