using Kurby.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Kurby.Test.Data
{
    class TestInMemoryContext
    {
        [Fact]
        public async void CanAddATodoItem()
        {
            await Run(async (AppDbContext context) =>
            {
                var todoService = new TodoRepository(context);

                var initialTodos = todoService.AllTodoItems().ToList();
                Assert.Equal(0, initialTodos.Count);

                todoService.AddTodoItem(new TodoItem { Name = "Item 1", IsComplete = false });
                var todos = todoService.AllTodoItems().ToList();
                Assert.Equal(1, todos.Count);
            });
        }


        public static async Task Run(Func testFunc)
        {
            var options = new DbContextOptionsBuilder()
                .UseInMemoryDatabase("IN_MEMORY_DATABASE")
                .Options;

            using (var context = new DbContext(options))
            {
                try
                {
                    await context.Database.EnsureCreatedAsync();

                    await testFunc(context);
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    CleanupTestDatabase(context);
                }
            }
        }
        public static void CleanupTestDatabase(DbContext context)
        {
            if (context.Database.IsInMemory())
            {
                context.Database.EnsureDeleted();
            }
            else
            {
                if (context.Database.IsSqlServer())
                {
                    //context.Database.ExecuteSqlCommand("DELETE FROM TodoItems");
                }
            }
        }
    }
}
