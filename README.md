# ToDo List Web Application

This is a simple to-do list web application built using **ASP.NET Core MVC**. The application allows users to register, log in, and manage tasks in a to-do list. Tasks can be marked as completed, deleted, and are stored in memory. 

## Features
- **User Authentication**: Users can register, log in, and perform actions on their tasks.
- **Task Management**: Users can add, complete, and delete tasks.
- **Error Handling**: Detailed error messages are displayed to guide users during registration and login.

## Technologies Used
- **ASP.NET Core MVC**: For building the web application.
- **C#**: The primary programming language for back-end logic.
- **Bootstrap**: For responsive front-end design.
- **JavaScript**: For handling AJAX requests to update tasks without reloading the page.
- **HTML5 & CSS3**: For structuring and styling the user interface.

## Project Structure

### 1. **Services**
- **MockAuthService**: Handles user registration and authentication.
- **ToDoListItemService**: Manages to-do list tasks, allowing users to add, complete, and delete tasks.

### 2. **ViewModels**
- **ErrorViewModel**: Represents error messages for view rendering.
- **LoginViewModel**: Contains properties for user login (email and password).
- **RegisterViewModel**: Contains properties for user registration (username, email, password, and confirm password).

### 3. **Views**
- **Login View**: A login form where users can authenticate with their credentials.
- **Register View**: A registration form for new users to sign up.
- **ToDoList View**: Displays tasks and provides options to add, complete, and delete them.

## Setup Instructions

### Prerequisites
Before running the project, ensure that you have the following installed:
- **.NET SDK** (preferably version 8.0)
- **Visual Studio** or **Visual Studio Code**
- A modern browser to run the application.

### Steps to Run the Application

1. **Clone the Repository**
   ```bash
   git clone https://github.com/fragkiadakise/ToDoList.git
   cd todolist-app
