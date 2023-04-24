using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TODO_list.Models;
using TODO_list.Repository;
using TODO_list.ViewModels;

namespace TODO_list.Controllers
{
    public class List : Controller
    {
        IListRepository repo;
        public List(IListRepository r)
        {
            repo = r;
        }
        public IActionResult Index(TODOCreationViewModel viewModel)
        {
            viewModel.Categories = repo.GetCategories();
            viewModel.ToDoList = repo.GetList();
            return View(viewModel);
        }
       
        [HttpPost]
        public IActionResult Create(TODOCreationViewModel viewModel)
        {
            
                repo.Create(viewModel.CreateNewItem);
                return RedirectToAction("Index");
         
        }
        [HttpPost]
        public IActionResult CreateCategory(TODOCreationViewModel viewModel)
        {

            repo.CreateCategory(viewModel.CreateCategory);
            return RedirectToAction("Index");

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            repo.Delete(id);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public IActionResult Update(int id, TODOlist list)
        {
           var currentList = repo.GetStatus(id);
            currentList.Status = !currentList.Status;

            repo.Update(id, currentList);

            return RedirectToAction("Index");

        }
        [HttpGet]
        public IActionResult Change(string action)
        {
            if (action == "Sql")
            {
                return RedirectToAction("Index", "List");
            }
            else if (action == "Xml")
            {
                return RedirectToAction("Index", "Xml");
            }
            return RedirectToAction("Index");
        }

    }
}
