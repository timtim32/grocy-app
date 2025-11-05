using System.Collections.Generic;

namespace GrocyApi.Models
{
    public class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        
        public int UserId { get; set; }  
        public User? User { get; set; }

        public List<Item> Items { get; set; } = new();
    }
}