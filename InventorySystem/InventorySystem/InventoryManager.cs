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
    
    /// <summary>
    /// Prints all items in an inventory to the console. If the inventory is empty, a message will be printed to the console.
    /// </summary>
    /// <param name="inventory"></param>
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
        // Check if any key in the dictionary matches the lowercase item
        var matchingKeys = inventory.Keys.Where(key => string.Equals(key.ToLower(), item.ToLower(), StringComparison.CurrentCultureIgnoreCase));
        
        //PrintItems(inventory);

        if (matchingKeys.Any())
        {
            // Use the first matching key (case-insensitive) to update the quantity
            var matchingKey = matchingKeys.First();
            inventory[matchingKey] += amount;
        }
        else
        {
            // Add a new item to the dictionary with the original item name
            inventory.Add(item, amount);
        }
    }
    
    public static bool EditItem(string item, int newQuantity, ref Dictionary<string, int> inventory)
    {
        // Check if any key in the dictionary matches the lowercase item
        var matchingKeys = inventory.Keys.Where(key => string.Equals(key.ToLower(), item.ToLower(), StringComparison.CurrentCultureIgnoreCase));
        
        //PrintItems(inventory);

        if (matchingKeys.Any())
        {
            // Use the first matching key (case-insensitive) to update the quantity
            var matchingKey = matchingKeys.First();

            if (newQuantity > 0)
            {
                // Update the quantity with the new valid amount
                inventory[matchingKey] = newQuantity;
                return true; // Successful edit
            }
            else
            {
                Console.WriteLine("Invalid quantity. Quantity must be greater than 0.");
                return false; // Invalid quantity
            }
        }
        else
        {
            PrintItemNotFound();
            return false; // Item not found in the inventory
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
        // Check if any key in the dictionary matches the lowercase item
        var matchingKeys = inventory.Keys.Where(key => string.Equals(key.ToLower(), item.ToLower(), StringComparison.CurrentCultureIgnoreCase));
        
        //PrintItems(inventory);

        if (matchingKeys.Any())
        {
            // Use the first matching key (case-insensitive) to update the quantity
            var matchingKey = matchingKeys.First();

            // Calculate the remaining quantity after removal
            int newQuantity = inventory[matchingKey] - amount;

            if (newQuantity >= 0)
            {
                // Update the quantity or remove the item based on the new quantity
                if (newQuantity == 0)
                {
                    inventory.Remove(matchingKey);
                }
                else
                {
                    inventory[matchingKey] = newQuantity;
                }

                return true; // Successful removal
            }
            else
            {
                Console.Write("Cannot remove more items than are available in the inventory, please try again: ");
                return false; // Removal not possible due to insufficient quantity
            }
        }
        else
        {
            PrintItemNotFound();
            return false; // Item not found in the inventory
        }
    }

    public static void PrintItemNotFound()
    {
        Console.WriteLine("Item not found in inventory.");
    }
    
}