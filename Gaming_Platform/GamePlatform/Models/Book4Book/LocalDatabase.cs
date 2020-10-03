using GamePlatform.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace GamePlatform.Models
{
    public class LocalDatabase : ILocalDatabase
    {
        private readonly SQLiteConnection dbConnection;
        private readonly string dbPath = Path.Combine(Directory.GetCurrentDirectory(), "database.sqlite");

        public LocalDatabase()
        {
            if (!File.Exists(dbPath))
            {
                SQLiteConnection.CreateFile("database.sqlite");

                dbConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3;");
                dbConnection.Open();
                List<string> sqlList = new List<string>()
                {
                    "CREATE TABLE users (user_id INTEGER PRIMARY KEY, first_name VARCHAR(50), last_name VARCHAR(50), email VARCHAR(50), login VARCHAR(50), password VARCHAR(50))",
                    "CREATE TABLE authors (author_id INTEGER PRIMARY KEY, first_name VARCHAR(50), last_name VARCHAR(50), birth_date VARCHAR(50), gender VARCHAR(50))",
                    "CREATE TABLE books (book_id INTEGER PRIMARY KEY, title VARCHAR(50), category VARCHAR(50), description TEXT, author_id INTEGER)",
                    "CREATE TABLE announcements (announcement_id INTEGER PRIMARY KEY, date VARCHAR(50), city VARCHAR(50), category VARCHAR(50), title VARCHAR(50), author VARCHAR(50), description VARCHAR(200))"
                };

                foreach (var sql in sqlList)
                {
                    SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
                    command.ExecuteNonQuery();
                }
            }
            else
            {
                dbConnection = new SQLiteConnection("Data Source=database.sqlite;Version=3;");
                dbConnection.Open();
            }
        }

        public void CloseConnection()
        {
            dbConnection.Close();
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        public void AddUser(User user)
        {
            string sql = $"INSERT INTO users (first_name, last_name, email, login, password) values ('{user.FirstName}', '{user.LastName}', '{user.Email}', '{user.Login}', '{user.Password}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void AddAuthor(Author author)
        {
            string sql = $"INSERT INTO authors (first_name, last_name, birth_date, gender) values ('{author.FirstName}', '{author.LastName}', '{author.BirthDate}', '{author.Gender}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void AddBook(Book book)
        {
            string sql = $"INSERT INTO books (title, category, description, author_id) values ('{book.Title}', '{book.Category}', '{book.Description}', '{book.AuthorId}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void AddAnnouncement(Announcement announcement)
        {
            string sql = $"INSERT INTO announcements (date, city, category, title, author, description) values ('{announcement.Date}', '{announcement.City}', '{announcement.Category}', '{announcement.Title}', '{announcement.Author}', '{announcement.Description}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void RemoveUser(User user)
        {
            string sql = $"DELETE FROM users WHERE (first_name = '{user.FirstName}' AND last_name = '{user.LastName}' AND email = '{user.Email}' AND login = '{user.Login}' AND password = '{user.Password}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void RemoveAuthor(Author user)
        {
            string sql = $"DELETE FROM authors WHERE (first_name = '{user.FirstName}' AND last_name = '{user.LastName}' AND birth_date = '{user.BirthDate}' AND gender = '{user.Gender}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public void RemoveBook(Book book)
        {
            string sql = $"DELETE FROM books WHERE (title = '{book.Title}' AND category = '{book.Category}' AND description = '{book.Description}' AND author_id = '{book.AuthorId}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }
        
        public void RemoveAnnouncement(Announcement announcement)
        {
            string sql = $"DELETE FROM announcements WHERE (date = '{announcement.Date}' AND city = '{announcement.City}' AND category = '{announcement.Category}' AND title = '{announcement.Title}' AND author = '{announcement.Author}' AND description = '{announcement.Description}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            command.ExecuteNonQuery();
        }

        public List<User> GetAllUsers()
        {
            List<User> usersList = new List<User>();
            string sql = "select * from users";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var user = new User(reader["first_name"].ToString(), reader["last_name"].ToString(), reader["email"].ToString(), reader["login"].ToString(), reader["password"].ToString())
                {
                    UserId = Convert.ToInt32(reader["user_id"].ToString())
                };
                usersList.Add(user);
            }

            return usersList;
        }

        public List<Author> GetAllAuthors()
        {
            List<Author> authorsList = new List<Author>();
            string sql = "select * from authors";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var author = new Author(reader["first_name"].ToString(), reader["last_name"].ToString(), Convert.ToDateTime(reader["birth_date"].ToString()), reader["gender"].ToString())
                {
                    AuthorId = Convert.ToInt32(reader["author_id"].ToString())
                };
                authorsList.Add(author);
            }

            return authorsList;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> authorsList = new List<Book>();
            string sql = "select * from books";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var book = new Book(reader["title"].ToString(), reader["category"].ToString(), reader["description"].ToString(), Convert.ToInt32(reader["author_id"].ToString()))
                {
                    BookId = Convert.ToInt32(reader["book_id"].ToString())
                };
                authorsList.Add(book);
            }

            return authorsList;
        }

        public List<Announcement> GetAllAnnouncements()
        {
            List<Announcement> authorsList = new List<Announcement>();
            string sql = "select * from announcements";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                var announcement = new Announcement(
                    reader["date"].ToString(),
                    reader["city"].ToString(),
                    reader["category"].ToString(),
                    reader["title"].ToString(),
                    reader["author"].ToString(),
                    reader["description"].ToString())
                {
                    AnnouncementId = Convert.ToInt32(reader["announcement_id"].ToString())
                };
                authorsList.Add(announcement);
            }

            return authorsList;
        }

        public bool IsUserAuthenticated(string login, string password)
        {
            string sql = $"select * from users where (login = '{login}' AND password = '{password}')";
            SQLiteCommand command = new SQLiteCommand(sql, dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            return reader.HasRows;
        }
    }
}