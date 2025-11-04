using IspitTodo.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace IspitTodo.Controllers
{
    [Authorize]
    public class TaskController(ApplicationDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var tasks = from task in context.Tasks
                        join list in context.Todolists on task.TodolistId equals list.Id
                        where list.UserId == User.FindFirstValue(ClaimTypes.NameIdentifier)
                        select task;
            ViewBag.Lists = context.Todolists.ToList();
            return View(tasks.ToList());
        }
    }
}
