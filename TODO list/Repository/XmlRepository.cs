using System.Xml.Linq;
using TODO_list.Models;

namespace TODO_list.Repository
{
	public class XmlRepository
	{
		private string filePath;

		public XmlRepository(string filePath)
		{
			this.filePath = filePath;
		}

		public void SaveListToXml(TODOlist list)
		{

			string path = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = "D:\\demoPrakt\\TODO list\\TODO list\\XmlTasks.xml";
			XDocument xDoc;
			if (System.IO.File.Exists(filePath))
			{
				xDoc = XDocument.Load(filePath);
			}
			else
			{
				xDoc = new XDocument();
				xDoc.Add(new XElement("List"));
			}

			int newId = GetNextId(xDoc);

			XDocument newEl = new XDocument(
				new XElement("Task",
					new XAttribute("TaskId", newId),
					new XElement("TaskName", list.TaskName),
					new XElement("Deadline", list.Deadline),
					new XElement("CategoryName", list.CategoryName),
					new XElement("Status", list.Status)
				)
			);

			xDoc.Root.Add(newEl.Elements());
			xDoc.Save(filePath);
		}

		public void DeleteTask(int taskId)
		{
			string filePath = "D:\\demoPrakt\\TODO list\\TODO list\\XmlTasks.xml";
			XDocument xDoc = XDocument.Load(filePath); 

			XElement taskElement = xDoc.Descendants("Task")
										.FirstOrDefault(e => (int)e.Attribute("TaskId") == taskId);

			if (taskElement != null)
			{
				taskElement.Remove(); 

				xDoc.Save(filePath); 
			}
		}
		private int GetNextId(XDocument xDoc)
		{
			int maxId = 0;

			foreach (var el in xDoc.Descendants("Task"))
			{
				int id = Convert.ToInt32(el.Attribute("TaskId")?.Value);
				if (id > maxId)
				{
					maxId = id;
				}
			}

			return maxId + 1;
		}

		private int GetIdCategory(XDocument xDoc)
		{
			int maxId = 0;

			foreach (var el in xDoc.Descendants("Categories").Elements("Id"))
			{
				int id = Convert.ToInt32(el.Value);
				if (id > maxId)
				{
					maxId = id;
				}
			}

			return maxId + 1;
		}

		public void CreateCategory(Categories categories)
		{

			string path = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = "D:\\demoPrakt\\TODO list\\TODO list\\XmlTasks.xml";
			XDocument xDoc;
			if (System.IO.File.Exists(filePath))
			{
				xDoc = XDocument.Load(filePath);
			}
			else
			{
				xDoc = new XDocument();
				xDoc.Add(new XElement("List"));
			}

			int newId = GetIdCategory(xDoc);


			XElement newEl = new XElement(
				"Categories",
					new XElement("Id", newId),
					new XElement("CategoryName", categories.CategoryName)

			);

			xDoc.Root.Add(newEl);
			xDoc.Save(filePath);
		}

		public void Delete(int categoryId)
		{
			string path = AppDomain.CurrentDomain.BaseDirectory;
			string filePath = "D:\\demoPrakt\\TODO list\\TODO list\\XmlTasks.xml";
			if (System.IO.File.Exists(filePath))
			{
				XDocument xDoc = XDocument.Load(filePath);
				XElement categoryToRemove = xDoc.Root.Elements("Categories")
													.FirstOrDefault(x => (int)x.Element("Id") == categoryId);
				if (categoryToRemove != null)
				{
					categoryToRemove.Remove();
					xDoc.Save(filePath);
				}
			}
		}
	}
}
