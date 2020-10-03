using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamePlatform.Models
{
    public interface ILocalDatabase
    {
        void CloseConnection();
        void AddUser(User user);
        void AddAuthor(Author author);
        void AddBook(Book book);
        void AddAnnouncement(Announcement announcement);
        void RemoveUser(User user);
        void RemoveAuthor(Author user);
        void RemoveBook(Book book);
        void RemoveAnnouncement(Announcement announcement);
        List<User> GetAllUsers();
        List<Author> GetAllAuthors();
        List<Book> GetAllBooks();
        List<Announcement> GetAllAnnouncements();
        bool IsUserAuthenticated(string login, string password);
    }
}
