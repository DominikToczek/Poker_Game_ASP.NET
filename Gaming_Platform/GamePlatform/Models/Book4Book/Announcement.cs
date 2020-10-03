namespace GamePlatform.Models
{
    public class Announcement
    {
        public int AnnouncementId { get; set; }
        public string Date { get; }
        public string City { get; }
        public string Category { get; }
        public string Title { get; }
        public string Author { get; }
        public string Description { get; }

        public Announcement(string date, string city, string category, string title, string author, string description)
        {
            Date = date;
            City = city;
            Category = category;
            Title = title;
            Author = author;
            Description = description;
        }
    }
}