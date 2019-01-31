using DataAccess.Entities;

namespace DataAccess.Repositories
{
    public class TodoRepository : GenericRepository<Todo>, ITodoRepository
    {
        public TodoRepository(TodoListContext dbContext)
        : base(dbContext)
        {

        }
    }
}
