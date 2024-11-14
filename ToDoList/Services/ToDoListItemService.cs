using ToDoList.Models;
using ToDoList.Services.Interfaces;

namespace ToDoList.Services
{
    public class ToDoListItemService : IToDoListItemService
    {

        private static List<ToDoListItem> Tasks = new List<ToDoListItem>();
        private readonly IHttpContextAccessor _HttpContextAccessor;

        public ToDoListItemService(IHttpContextAccessor httpContextAccessor)
        {
            _HttpContextAccessor = httpContextAccessor;
        }
        public bool AddToDoListItem(string myTaskTitle, string myTaskDescription)
        {

            if (_HttpContextAccessor.HttpContext.Session.GetString("AuthenticateUser") == null)
            {
                return false;
            }

            var toDoItem = new ToDoListItem
            {
                Id = Guid.NewGuid().ToString(),
                Title = myTaskTitle,
                Description = myTaskDescription,
                CreatedAt = DateTime.Now,
                IsCompleted = false,
                IsDeleted = false,
                UserId = _HttpContextAccessor.HttpContext.Session.GetString("AuthenticateUser")
            };

            if (Tasks.Any(x => x.Id == toDoItem.Id))
            {
                return false;
            }

            Tasks.Add(toDoItem);
            return true;
        }

        public List<ToDoListItem> CompleteToDoListItem(string id)
        {
            if (Tasks.Any(x => x.Id == id))
            {
                Tasks.Where(x => x.Id == id).First().IsCompleted = true;
            }

            return Tasks;
        }

        public List<ToDoListItem> GetToDoListItems()
        {
            return Tasks;
        }

        public List<ToDoListItem> RemoveToDoListItem(string id)
        {
            if (Tasks.Any(x => x.Id == id))
            {
                Tasks.Where(x => x.Id == id).First().IsDeleted = true;
            }

            return Tasks;
        }
    }
}
