using DataAccess.Entities;
using System.Collections.Generic;

namespace TodoList.ViewModels
{
    public class TodoViewModel : ITodoViewModel
    {
        public int Pages { get; set; }
        public IEnumerable<Todo> TodoList { get; set; }

        public TodoViewModel ()
        {

        }
    }
}
