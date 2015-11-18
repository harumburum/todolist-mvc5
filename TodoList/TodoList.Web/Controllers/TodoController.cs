using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TodoList.Web.Data;
using TodoList.Web.Infrastructure;
using TodoList.Web.Models;

namespace TodoList.Web.Controllers
{
    [Authorize]
    public class TodoController : ApiController
    {
        private readonly ICurrentUser _currentUser;
        private readonly ITodoRepository _todoRepository;

        public TodoController(ICurrentUser currentUser, ITodoRepository todoRepository)
        {
            _currentUser = currentUser;
            _todoRepository = todoRepository;
        }

        [HttpGet]
        public IEnumerable<Todo> Get()
        {
            return _todoRepository.GetAll().Where(_ => _.UserId == _currentUser.UserId);
        }

        [HttpPost]
        public Todo Post([FromBody]Todo todo)
        {
            todo.UserId = _currentUser.UserId;

            _todoRepository.Add(todo);

            return todo;
        }

        [HttpPut]
        public HttpResponseMessage Put([FromBody]Todo todo)
        {
            var entity = _todoRepository.GetById(todo.Id);
            if (entity == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            if (entity.UserId != _currentUser.UserId)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            entity.IsDone = todo.IsDone;

            _todoRepository.Update();

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        [HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            var entity = _todoRepository.GetById(id);
            if (entity == null)
                return new HttpResponseMessage(HttpStatusCode.NotFound);
            if (entity.UserId != _currentUser.UserId)
                return new HttpResponseMessage(HttpStatusCode.Unauthorized);

            _todoRepository.Delete(entity);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}