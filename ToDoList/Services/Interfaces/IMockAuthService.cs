using ToDoList.Models;
using ToDoList.ViewModels;

namespace ToDoList.Services.Interfaces
{
    public interface IMockAuthService
    {

        public string RegisterUser(RegisterViewModel userViewModel);
        public User AuthenticateUser(string email, string password);
    }
}
