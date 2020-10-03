namespace GamePlatform.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; }
        public string Category { get; }
        public string Description { get; }
        public int AuthorId { get; }

        public Book(string title, string category, string description, int authorId)
        {
            Title = title;
            Category = category;
            Description = description;
            AuthorId = authorId;
        }
    }
}