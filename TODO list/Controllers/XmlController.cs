using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;
using TODO_list.Models;
using TODO_list.Repository;
using TODO_list.ViewModels;

namespace TODO_list.Controllers
{
    public class XmlController : Controller
    {
		private XmlRepository repo;

		public XmlController()
		{
			string filePath = "D:\\demoPrakt\\TODO list\\TODO list\\XmlTasks.xml";
			repo = new XmlRepository(filePath);
		}

		public IActionResult Index(TODOCreationViewModel model)
        {
            string filePath = "D:\\demoPrakt\\TODO list\\TODO list\\XmlTasks.xml";
            XDocument xDoc = XDocument.Load(filePath);
		
			IEnumerable<TODOlist> todoList = xDoc.Descendants("Task")
	   .Select(t => new TODOlist {
		   TaskId = t.Attribute("TaskId") != null ? int.Parse(t.Attribute("TaskId").Value) : int.MinValue,
			TaskName = t.Element("TaskName")?.Value,
			Deadline = t.Element("Deadline") != null ? DateTime.Parse(t.Element("Deadline").Value) : DateTime.MinValue,
			CategoryName = t.Element("CategoryName")?.Value,
		   Status = bool.Parse(t.Element("Status")?.Value ?? "false")
	   }).ToList();
			model.ToDoList = todoList;


			IEnumerable<Categories> categories = xDoc.Descendants("Categories")
   .Select(c => new Categories
   {
	   CategoryName = c.Element("CategoryName")?.Value,
	   Id = c.Element("Id") != null ? int.Parse(c.Element("Id").Value) : int.MinValue
   }).ToList();
			model.Categories = categories;

			return View(model);
            }

		[HttpPost]
		public IActionResult DeleteCategory(int id)
		{
			repo.Delete(id);
			return RedirectToAction("Index");
		}
		[HttpPost]
		public IActionResult DeleteTaskItem(int id)
		{
			repo.DeleteTask(id);
			return RedirectToAction("Index");
		}

		[HttpPost]
        public ActionResult CreateList(TODOCreationViewModel list)
        {
            repo.SaveListToXml(list.CreateNewItem);

            return RedirectToAction("Index");
        }

		[HttpPost]
		public ActionResult CreateCategory(TODOCreationViewModel list)
		{
			repo.CreateCategory(list.CreateCategory);

			return RedirectToAction("Index");
		}

		[HttpPost]
		public IActionResult Update(int id, TODOlist status)
		{
			string filePath = "D:\\demoPrakt\\TODO list\\TODO list\\XmlTasks.xml";
			XDocument xDoc = XDocument.Load(filePath); 

			XElement taskElement = xDoc.Descendants("Task").FirstOrDefault(e => e.Attribute("TaskId")?.Value == id.ToString());
			if (taskElement != null)
			{
				bool current = taskElement.Element("Status") != null && Convert.ToBoolean(taskElement.Element("Status").Value);
				current = !current;
				taskElement.Element("Status").SetValue(current.ToString());
			}
			xDoc.Save(filePath); 

			return RedirectToAction("Index");
		}
	}
}
