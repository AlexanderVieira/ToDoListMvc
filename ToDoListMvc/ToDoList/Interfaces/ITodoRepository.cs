using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models;

namespace ToDoList.Interfaces
{
    public interface ITodoRepository
    {
        Task CreateOrUpdate(TodoModel entity);
        Task<IEnumerable<TodoModel>> GetAll();
        Task<TodoModel> GetTodoModel(string pKey, string rKey);
        Task Delete(TodoModel entity);
    }
}
