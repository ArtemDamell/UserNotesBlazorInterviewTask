# Here is an assignment for a Junior ASP .NET Developer:

## Create a simple web application that allows users to register and login. Once logged in, users should be able to create, read, update, and delete simple notes. Each note should have a title, a body, and a date/time stamp indicating when it was created.

The application should have the following features:

- A registration page that allows users to create a new account by entering a username and password.
- A login page that allows users to log in with their username and password.
- A dashboard page that displays a list of all notes created by the logged-in user.
- An add note page that allows the user to create a new note by entering a title and body.
- An edit note page that allows the user to edit an existing note.
- A delete note feature that allows the user to delete a note.
- Use of a database to store the user account information and notes.
- Use of ASP .NET Identity for user authentication and authorization.

Bonus points for implementing additional features such as:

- Pagination of notes on the dashboard page.
- Search functionality for notes.
- User profile page with ability to change password and edit user details.

# Decomposed task:
### 1.	Define the data model
  -	Create a class to represent a user, including properties for username and password
  -	Create a class to represent a note, including properties for title, body, and creation date/time, as well as a foreign key to the user who created it
### 2.	Set up the database
  - Choose a database engine (e.g. SQL Server, MySQL, SQLite)
  - Create a new database for the application
  - Create tables for the user and note classes, with appropriate columns and constraints
  - Set up the appropriate relationships between the tables
### 3.	Set up ASP .NET Identity
  - Configure ASP .NET Identity for user authentication and authorization
  - Customize the user registration and login pages to fit the design of the application
  - Implement the necessary controller actions and views for user registration and login
### 4.	Implement the note functionality
  - Create a controller for the note actions (e.g. NotesController)
  - Implement the necessary controller actions and views for adding, editing, and deleting notes
  - Implement the dashboard page to display a list of all notes created by the logged-in user, including links to edit and delete each note
### 5.	Implement additional features (bonus points)
  - Implement pagination of notes on the dashboard page, with a configurable number of notes per page
  - Implement search functionality for notes, allowing users to search by title and/or body
  - Implement a user profile page with the ability to change the user's password and edit user details
### 6.	Test and deploy
  - Test the application thoroughly to ensure all functionality works as expected and security vulnerabilities have been addressed
  - Deploy the application to a hosting service, such as Microsoft Azure or Amazon Web Services, or to a self-hosted server
