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

            return Ok(list);
        }
    }
}