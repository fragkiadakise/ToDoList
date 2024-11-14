using System.Text.RegularExpressions;
using ToDoList.Models;
using ToDoList.Services.Interfaces;
using ToDoList.ViewModels;

namespace ToDoList.Services
{
    public class MockAuthService : IMockAuthService
    {
        private static List<User> users = new List<User>();

        public string RegisterUser(RegisterViewModel userViewModel)
        {
            List<string> errorsList = new List<string>();
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            Regex regex = new Regex(pattern);

            if (users.Any(u => u.Username == userViewModel.Username))
            {
                errorsList.Add("<li>Username already exists</li>");
            }

            if (users.Any(u => u.Email == userViewModel.Email))
            {
                errorsList.Add("<li>Email already exists</li>");
            }

            if (!regex.IsMatch(userViewModel.Email))
            {
                errorsList.Add("<li>Please check email</li>");
            }

            if (userViewModel.Password != userViewModel.ConfirmPassword)
            {
                errorsList.Add("<li>Please check password</li>");
            }

            if (errorsList.Count > 0)
            {
                var errors = "<ul>";
                foreach (var error in errorsList)
                {
                    errors += error;
                }
                errors += "</ul>";
                return errors;
            }

            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Username = userViewModel.Username,
                Password = userViewModel.Password,
                Email = userViewModel.Email
            };

            users.Add(user);
            return string.Empty;
        }

        public User AuthenticateUser(string email, string password)
        {
            var user = users.FirstOrDefault(u => u.Email == email && u.Password == password);
            return user;
        }

    }
}
