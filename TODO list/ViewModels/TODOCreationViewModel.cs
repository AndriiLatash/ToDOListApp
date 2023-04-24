using TODO_list.Models;

namespace TODO_list.ViewModels
{
    public class TODOCreationViewModel
    {
        public TODOlist CreateNewItem { get; set; }
        public IEnumerable<TODOlist> ToDoList { get; set; }
        public EditViewModel EditViewModel { get; set; }   
        public IEnumerable<Categories> Categories { get; set; }
        public Categories CreateCategory { get; set; }
        public SelectForm SelectForm { get; set; }
	}
}
