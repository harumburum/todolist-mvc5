using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TodoList.Web.Data;
using TodoList.Web.Models;

namespace TodoList.Tests.Data
{
    [TestClass]
    public class TodoRepositoryTests
    {
        [TestMethod]
        public void GetAllShouldReturnTodos()
        {
            var data = new List<Todo>
            {
                new Todo {Id = 1},
                new Todo {Id = 2},
                new Todo {Id = 3},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);

            var service = new TodoRepository(mockContext.Object);
            var todos = service.GetAll().ToList();

            Assert.AreEqual(3, todos.Count);
            Assert.AreEqual(1, todos[0].Id);
            Assert.AreEqual(2, todos[1].Id);
            Assert.AreEqual(3, todos[2].Id);
        }

        [TestMethod]
        public void GetByIdShouldReturnEntity()
        {
            var data = new List<Todo>
            {
                new Todo {Id = 1},
                new Todo {Id = 2},
                new Todo {Id = 3},
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Todo>>();
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Todo>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(c => c.Todos).Returns(mockSet.Object);

            var service = new TodoRepository(mockContext.Object);
            var entity = service.GetById(1);

            Assert.IsNotNull(entity);
        }

        [TestMethod]
        public void AddShuldCallAddAndSaveChanges()
        {
            var mockSet = new Mock<DbSet<Todo>>();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Todos).Returns(mockSet.Object);

            var service = new TodoRepository(mockContext.Object);
            service.Add(new Todo());

            mockSet.Verify(m => m.Add(It.IsAny<Todo>()), Times.Once());
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void UpdateShuldUpdateEntityAndSaveChanges()
        {
            var mockSet = new Mock<DbSet<Todo>>();

            var mockContext = new Mock<ApplicationDbContext>();
            mockContext.Setup(m => m.Todos).Returns(mockSet.Object);

            var service = new TodoRepository(mockContext.Object);
            service.Update();

            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [TestMethod]
        public void DeleteShuldCallDeleteAndSaveChanges()
        {
            var mockDbSet = new Mock<DbSet<Todo>>();

            var mockDbContext = new Mock<ApplicationDbContext>();
            mockDbContext.Setup(m => m.Todos).Returns(mockDbSet.Object);

            var repository = new TodoRepository(mockDbContext.Object);
            repository.Delete(new Todo());

            mockDbSet.Verify(m => m.Remove(It.IsAny<Todo>()), Times.Once());
            mockDbContext.Verify(m => m.SaveChanges(), Times.Once());
        }
    }
}