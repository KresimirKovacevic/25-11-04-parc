using IspitTodo.Data;
using IspitTodo.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.Blazor;
using System.Collections.Generic;
using System.Security.Claims;
using Task = IspitTodo.Models.Task;

namespace IspitTodo.Controllers
{
    [Authorize]
    public class TodolistController(ApplicationDbContext context) : Controller
    {
        public IActionResult Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var lists = context.Todolists.Where(l => l.UserId == userId).ToList();
            return View(lists);
        }

        [HttpPost]
        public IActionResult FinishTask(int taskId)
        {
            var task = context.Tasks.Find(taskId);
            if (task != null) 
            {
                task.TimeFinished = DateTime.Now;
                task.Status = true;
                context.SaveChanges();
            }

            return RedirectToAction("Edit", new { id = task.TodolistId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var list = context.Todolists.Find(id);
            if (list != null)
            {
                if(list.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return NotFound();
                }
                ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
                ViewBag.Tasks = context.Tasks.Where(t => t.TodolistId == id).ToList();
                return View(list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, Todolist list)
        {
            if (id != list.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Tasks");
            ModelState.Remove("User");

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(list);
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!context.Todolists.Any(l => l.Id == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }

        public IActionResult Details(int id)
        {
            var list = context.Todolists.Find(id);
            if (list != null)
            {
                if (list.UserId != User.FindFirstValue(ClaimTypes.NameIdentifier))
                {
                    return NotFound();
                }
                ViewBag.UserName = User.FindFirstValue(ClaimTypes.Name);
                ViewBag.Tasks = context.Tasks.Where(t => t.TodolistId == id).ToList();
                return View(list);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateTask(int todolistId, string taskTitle, string taskDesc)
        {
            var task = new Task()
            {
                Title = taskTitle,
                Description = taskDesc,
                Status = false,
                TodolistId = todolistId
            };
            context.Tasks.Add(task);
            context.SaveChanges();
            return RedirectToAction("Edit", new { id = todolistId });
        }

        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Todolist list)
        {
            ModelState.Remove("Tasks");
            ModelState.Remove("User");
            ModelState.Remove("UserId");

            if (ModelState.IsValid)
            {
                list.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                context.Add(list);
                context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(list);
        }
    }
}
