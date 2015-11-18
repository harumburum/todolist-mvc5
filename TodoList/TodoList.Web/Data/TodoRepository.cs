using System.Collections.Generic;
using System.Linq;
using TodoList.Web.Models;

namespace TodoList.Web.Data
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public TodoRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Todo> GetAll()
        {
            return _dbContext.Todos;
        }

        public Todo GetById(int id)
        {
            return _dbContext.Todos.FirstOrDefault(_ => _.Id == id);
        }

        public void Add(Todo entity)
        {
            _dbContext.Todos.Add(entity);
            _dbContext.SaveChanges();
        }

        public void Update()
        {
            _dbContext.SaveChanges();
        }

        public void Delete(Todo entity)
        {
            _dbContext.Todos.Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}