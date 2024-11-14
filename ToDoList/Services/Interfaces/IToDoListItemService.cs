using ToDoList.Models;

namespace ToDoList.Services.Interfaces
{
    public interface IToDoListItemService
    {

        public bool AddToDoListItem(string myTaskTitle, string myTaskDescription);
        public List<ToDoListItem> RemoveToDoListItem(string id);
        public List<ToDoListItem> GetToDoListItems();

        public List<ToDoListItem> CompleteToDoListItem(string id);
    }
}