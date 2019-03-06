using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ToDoList.Interfaces;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoController : Controller
    {
        private readonly ITodoRepository _todoRepository;

        public TodoController(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        // GET: Todo
        public ActionResult Index()
        {
            var entities = _todoRepository.GetAll().Result.ToList();
            var models = entities.Select(t => new TodoModel
            {
                Id = Convert.ToInt32(t.RowKey),
                Name = t.PartitionKey,
                Category = t.Category,
                Completed = t.Completed
            });
            return View(models);
        }

        // GET: Todo/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Todo/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Todo/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Todo/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Todo/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Todo/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Todo/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}