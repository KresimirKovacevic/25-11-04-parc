using IspitTodo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IspitTodo.Controllers
{
    public class ProfileController(ApplicationDbContext context) : Controller
    {
        [Authorize]
        public IActionResult Index()
        {
            ViewBag.CurrUser = context.Users.Find(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            return View();
        }
    }
}
