    using System.ComponentModel.DataAnnotations.Schema;

    namespace TODO_list.Models
    {
        public class TODOlist
        {
            public int TaskId { get; set; }
            public string TaskName { get; set; }
            public bool Status { get; set; }
            public DateTime Deadline { get; set; }
            public int CategoryId { get; set; }
            public List<Categories> CategoryList { get; set; }
            public string CategoryName { get; set; }

        }
    }
