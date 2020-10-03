using System;

namespace GamePlatform.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public DateTime BirthDate { get; }
        public string Gender { get; }

        public Author(string firstName, string lastName, DateTime birthDate, string gender)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Gender = gender;
        }
    }
}