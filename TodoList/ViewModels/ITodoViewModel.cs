using DataAccess.Entities;
using System.Collections.Generic;

namespace TodoList.ViewModels
{
    public interface ITodoViewModel
    {
        int Pages { get; set; }
        IEnumerable<Todo> TodoList { get; set; }
    }
}
