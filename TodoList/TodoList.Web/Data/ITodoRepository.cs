using System.Collections.Generic;
using TodoList.Web.Models;

namespace TodoList.Web.Data
{
    public interface ITodoRepository
    {
        IEnumerable<Todo> GetAll();
        Todo GetById(int id);
        void Add(Todo entity);
        void Update();
        void Delete(Todo entity);
    }
}