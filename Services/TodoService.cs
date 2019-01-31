using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class TodoService : ITodoService
    {
        const double PAGE_SIZE = 5;
        private readonly ITodoRepository todoRepository;

        public TodoService(ITodoRepository repository)
        {
            todoRepository = repository;
        }

        public async Task<int> GetPageNumber()
        {
            double rowCount = await todoRepository.GetAll().CountAsync();
            int pageNumber = (int)Math.Ceiling(rowCount / PAGE_SIZE);
            return pageNumber;
        }

        public async Task<IEnumerable<Todo>> GetData(int page)
        {
            return await todoRepository.GetAll()
                .OrderByDescending(c => c.TodoId).Skip((int)((page - 1)*PAGE_SIZE)).Take((int)PAGE_SIZE).ToListAsync();
        }
        public async Task<IEnumerable<Todo>> GetAllData()
        {
            return await todoRepository.GetAll()
                .OrderByDescending(c => c.TodoId).ToListAsync();
        }

        public async Task<Todo> GetById(int id)
        {
            return await todoRepository.GetById(id);
        }

        public async Task Create(Todo entity)
        {
            await todoRepository.Create(entity);
        }

        public async Task Delete(int id)
        {
            await todoRepository.Delete(id);
        }

        public async Task Update(Todo entity)
        {
            await todoRepository.Update(entity);
        }
    }
}
