namespace GamePlatform.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string FirstName { get; }
        public string LastName { get; }
        public string Login { get; }
        public string Password { get; }
        public string Email { get; }

        public User(string firstName, string lastName, string email, string login, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            Login = login;
            Password = password;
            Email = email;
        }
    }
}