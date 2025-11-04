namespace GrocyApi.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public int Quantity { get; set; } = 1;   // aantal van het item
        public bool IsChecked { get; set; } = false;

        // Foreign key naar lijst
        public int ShoppingListId { get; set; }
        public ShoppingList? ShoppingList { get; set; }  // navigatie property
    }
}