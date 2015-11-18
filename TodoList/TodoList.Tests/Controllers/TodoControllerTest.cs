using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoList.Web.Controllers;
using TodoList.Web.Data;
using TodoList.Web.Infrastructure;
using TodoList.Web.Models;

namespace TodoList.Tests.Controllers
{
    [TestClass]
    public class TodoControllerTest
    {
        [TestMethod]
        public void GetShouldReturnEntities()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            mockCurrentUser.Setup(_ => _.UserId).Returns("USERID");

            var mockTodoRepository = new Mock<ITodoRepository>();
            mockTodoRepository.Setup(_ => _.GetAll())
                .Returns(new List<Todo> { new Todo { UserId = "USERID" } });

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var result = controller.Get();

            Assert.AreEqual(1, result.Count());
        }

        [TestMethod]
        public void GetShouldReturnEmptyResult()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            mockCurrentUser.Setup(_ => _.UserId).Returns("USERID");

            var mockTodoRepository = new Mock<ITodoRepository>();
            mockTodoRepository.Setup(_ => _.GetAll())
                .Returns(new List<Todo> { new Todo() });

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var result = controller.Get();

            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public void PostShouldReturnEntity()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            mockCurrentUser.Setup(_ => _.UserId).Returns("USERID");

            var mockTodoRepository = new Mock<ITodoRepository>();
            mockTodoRepository.Setup(_ => _.Add(It.IsAny<Todo>())).
                Callback((Todo entity) => entity.Id = 1);

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var todo = controller.Post(new Todo());

            Assert.AreEqual(1, todo.Id);
            Assert.AreEqual("USERID", todo.UserId);
        }

        [TestMethod]
        public void PutShouldReturnNotUnauthorized()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            var mockTodoRepository = new Mock<ITodoRepository>();
            mockTodoRepository
               .Setup(_ => _.GetById(It.IsAny<int>()))
               .Returns(new Todo() { UserId = "Some" });

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var response = controller.Put(new Todo());

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public void PutShouldReturnNotFound()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            var mockTodoRepository = new Mock<ITodoRepository>();

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var response = controller.Put(new Todo());

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void PutShouldReturnOk()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            var mockTodoRepository = new Mock<ITodoRepository>();
            mockTodoRepository
                .Setup(_ => _.GetById(It.IsAny<int>()))
                .Returns(new Todo());

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var response = controller.Put(new Todo());

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }

        [TestMethod]
        public void DeleteShouldReturnNotUnauthorized()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            var mockTodoRepository = new Mock<ITodoRepository>();
            mockTodoRepository
               .Setup(_ => _.GetById(It.IsAny<int>()))
               .Returns(new Todo() { UserId = "Some" });

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var response = controller.Delete(1);

            Assert.AreEqual(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [TestMethod]
        public void DeleteShouldReturnNotFound()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            var mockTodoRepository = new Mock<ITodoRepository>();

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var response = controller.Delete(1);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TestMethod]
        public void DeleteShouldReturnOk()
        {
            var mockCurrentUser = new Mock<ICurrentUser>();
            var mockTodoRepository = new Mock<ITodoRepository>();
            mockTodoRepository
                .Setup(_ => _.GetById(It.IsAny<int>()))
                .Returns(new Todo());

            var controller = new TodoController(mockCurrentUser.Object, mockTodoRepository.Object);

            var response = controller.Delete(1);

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
