using CustomConsole;
using GenericParse;

namespace InventorySystem
{
	internal class Program
	{
		public static Dictionary<string, int> Inventory = InventoryManager.Presetinventory;

		private static string[] menu1 = { "Add items", "Edit existing item", "Remove item", "View all items" };
		private static string[] menu2 = { "Exit program" };
		
		public static bool _loopMain = true;
        
		static void Main(string[] args)
		{
			while (_loopMain)
			{
				Console.Clear();
				PrintMenu();
				SelectMenuOption();	
			}
		}
		
		/// <summary>
		/// Displays all menu options in the console.
		/// </summary>
		static void PrintMenu()
		{
			Console.WriteLine("Simple inventory system");
			ConsoleHelper.PrintBlank();
			ConsoleHelper.PrintStrings( new[] {menu1, menu2} );
		}
		
		/// <summary>
		/// Waits for user input and calls SwitchOnMenuSelection(), passing the user's input as a parameter.
		/// </summary>
		private static void SelectMenuOption()
		{
			// looping until a valid option is selected
			while (true)
			{
				ConsoleHelper.PrintBlank();
				Console.Write("Select option: ");
				int tempSelect = GenericReadLine.TryReadLine<int>();

				if (!SwitchOnMenuSelection(tempSelect))
				{
					break;
				}
			}
		}
		
		/// <summary>
		/// Uses a switch statement to call the appropriate method based on the user's menu selection.
		/// </summary>
		/// <param name="selection">The user's menu selection</param>
		/// <returns>The desired loop state</returns>
		private static bool SwitchOnMenuSelection(int selection)
		{
			bool tempReturnValue = true;

			// clearing console and printing menu again to prevent clutter
			Console.Clear();
			PrintMenu();
			ConsoleHelper.PrintBlank();

			switch (selection)
			{
				case 1: // Add items
					AddItem();
					break;
				case 2: // Edit existing item
					EditItem();
					break;
				case 3: // Remove item
					RemoveItem();
					break;
				case 4: // View all items
                    InventoryManager.PrintItems(Inventory);
					break;
				case 5: // exit program
					tempReturnValue = false;
					_loopMain = false;
					break;
				default: // Invalid selection
					ConsoleHelper.PrintInvalidSelection();
					break;
			}
			return tempReturnValue;
		}

		/// <summary>
		/// handles adding items to the inventory
		/// </summary>
		public static void AddItem()
		{
			Console.Clear();
			
			InventoryManager.PrintItems(Inventory);
			
			ConsoleHelper.PrintBlank();
			
			Console.Write("Enter item name to add: ");
			string tempItemName = GenericReadLine.TryReadLine<string>();
			
			Console.Write("Enter quantity to add: ");
			int tempQuantity = GenericReadLine.TryReadLine<int>();
			
			InventoryManager.AddItem(tempItemName, tempQuantity, ref Inventory);
			
			Console.Clear();
			PrintMenu();
			ConsoleHelper.PrintBlank();
			
			Console.WriteLine($"Item(s) added: {tempQuantity} {tempItemName}");
		}

		/// <summary>
		/// handles editing items in the inventory
		/// </summary>
		public static void EditItem()
		{
			Console.Clear();
			
			InventoryManager.PrintItems(Inventory);
            
			// looping until a valid option is selected
			while (true)
			{
				ConsoleHelper.PrintBlank();
			
				Console.Write("Enter item name to edit: ");
				string tempItemName = GenericReadLine.TryReadLine<string>();
			
				Console.Write("Enter new item quantity: ");
				int tempQuantity = GenericReadLine.TryReadLine<int>();

				// if the item was successfully edited, break out of the loop
				if (InventoryManager.EditItem(tempItemName, tempQuantity, ref Inventory))
				{
					Console.Clear();
					PrintMenu();
					ConsoleHelper.PrintBlank();
			
					Console.WriteLine($"Item edited: {tempItemName} = {tempQuantity}");
					break;
				}
				else
				{
					ConsoleHelper.PrintInvalidSelection();
					//Console.Write("Please enter valid quantity to edit: ");
				}
			}
		}

		/// <summary>
		/// Handles removing items from the inventory
		/// </summary>
		public static void RemoveItem()
		{
			Console.Clear();
			
			InventoryManager.PrintItems(Inventory);
			
			// looping until a valid option is selected
			while (true)
			{
				ConsoleHelper.PrintBlank();
			
				Console.Write("Enter item name to remove: ");
				string tempItemName = GenericReadLine.TryReadLine<string>();
			
				ConsoleHelper.PrintBlank();
                
				Console.Write("Enter quantity to remove: ");
				int tempQuantity = GenericReadLine.TryReadLine<int>();

				// if the item was successfully removed, break out of the loop
				if (InventoryManager.RemoveItem(tempItemName, tempQuantity, ref Inventory))
				{
					Console.Clear();
					PrintMenu();
					ConsoleHelper.PrintBlank();
			
					Console.WriteLine($"Item(s) removed: {tempQuantity} {tempItemName}");
					break;
				}
				else
				{
					ConsoleHelper.PrintInvalidSelection();
					//Console.Write("Please enter valid quantity to remove: ");
				}
			}
		} 
	}
}