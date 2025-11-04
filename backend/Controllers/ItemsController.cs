using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using GrocyApi.Data;
using GrocyApi.Models;

namespace GrocyApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ItemsController(AppDbContext db) => _db = db;

        // GET: api/Items/{listId}
        [HttpGet("{listId}")]
        public IActionResult GetItems(int listId)
        {
            var email = User.Identity?.Name;
            var user = _db.Users.Include(u => u.ShoppingLists)
                                .ThenInclude(l => l.Items)
                                .SingleOrDefault(u => u.Email == email);
            if (user == null) return Unauthorized();

            var list = user.ShoppingLists.SingleOrDefault(l => l.Id == listId);
            if (list == null) return NotFound("List not found");

            return Ok(list.Items);
        }

        // POST: api/Items/{listId}
        [HttpPost("{listId}")]
        public IActionResult AddItem(int listId, [FromBody] Item item)
        {
            var email = User.Identity?.Name;
            var user = _db.Users.Include(u => u.ShoppingLists)
                                .SingleOrDefault(u => u.Email == email);
            if (user == null) return Unauthorized();

            var list = user.ShoppingLists.SingleOrDefault(l => l.Id == listId);
            if (list == null) return NotFound("List not found");

            item.ShoppingListId = listId;
            _db.Items.Add(item);
            _db.SaveChanges();

            return Ok(item);
        }

        // PUT: api/Items/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateItem(int id, [FromBody] Item updatedItem)
        {
            var email = User.Identity?.Name;
            var user = _db.Users.Include(u => u.ShoppingLists)
                                .ThenInclude(l => l.Items)
                                .SingleOrDefault(u => u.Email == email);
            if (user == null) return Unauthorized();

            var item = user.ShoppingLists.SelectMany(l => l.Items)
                                         .SingleOrDefault(i => i.Id == id);
            if (item == null) return NotFound("Item not found");

            item.Name = updatedItem.Name;
            item.Quantity = updatedItem.Quantity;
            item.IsChecked = updatedItem.IsChecked;

            _db.SaveChanges();

            return Ok(item);
        }

        // DELETE: api/Items/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteItem(int id)
        {
            var email = User.Identity?.Name;
            var user = _db.Users.Include(u => u.ShoppingLists)
                                .ThenInclude(l => l.Items)
                                .SingleOrDefault(u => u.Email == email);
            if (user == null) return Unauthorized();

            var item = user.ShoppingLists.SelectMany(l => l.Items)
                                         .SingleOrDefault(i => i.Id == id);
            if (item == null) return NotFound("Item not found");

            _db.Items.Remove(item);
            _db.SaveChanges();

            return Ok("Item deleted");
        }
    }
}
