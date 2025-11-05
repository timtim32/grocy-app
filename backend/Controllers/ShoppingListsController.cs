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
    public class ShoppingListsController : ControllerBase
    {
        private readonly AppDbContext _db;
        public ShoppingListsController(AppDbContext db) => _db = db;

        // DTO voor frontend
        public class ShoppingListDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public List<ItemDto> Items { get; set; } = new();
        }

        public class ItemDto
        {
            public int Id { get; set; }
            public string Name { get; set; } = "";
            public int Quantity { get; set; }
            public bool IsChecked { get; set; }
        }

        // GET: api/ShoppingLists
        [HttpGet]
        public IActionResult GetLists()
        {
            var email = User.Identity?.Name;
            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null) return Unauthorized();

            var lists = _db.ShoppingLists
                .Where(l => l.UserId == user.Id)
                .Include(l => l.Items)
                .Select(l => new ShoppingListDto
                {
                    Id = l.Id,
                    Name = l.Name,
                    Items = l.Items.Select(i => new ItemDto
                    {
                        Id = i.Id,
                        Name = i.Name,
                        Quantity = i.Quantity,
                        IsChecked = i.IsChecked
                    }).ToList()
                })
                .ToList();

            return Ok(lists);
        }

        // POST: api/ShoppingLists
        [HttpPost]
        public IActionResult AddList([FromBody] ShoppingList list)
        {
            var email = User.Identity?.Name;
            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null) return Unauthorized();

            list.UserId = user.Id;
            _db.ShoppingLists.Add(list);
            _db.SaveChanges();
            
            var dto = new ShoppingListDto
            {
                Id = list.Id,
                Name = list.Name,
                Items = new List<ItemDto>()
            };

            return Ok(dto);
        }
        
        // DELETE: api/ShoppingLists/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteList(int id)
        {
            var email = User.Identity?.Name;
            var user = _db.Users.SingleOrDefault(u => u.Email == email);
            if (user == null) return Unauthorized();

            var list = _db.ShoppingLists
                .Include(l => l.Items)
                .SingleOrDefault(l => l.Id == id && l.UserId == user.Id);

            if (list == null) return NotFound();

            _db.ShoppingLists.Remove(list);
            _db.SaveChanges();

            return NoContent();
        }

    }
}
