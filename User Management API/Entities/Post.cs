namespace User_Management_API.Entities;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
    
    public int UserId { get; set; }
    public User User { get; set; }

    public ICollection<Reaction> Reactions { get; set; }
    public ICollection<Comment> Comments { get; set; }
    
    public void UpdatePost(string title, string content)
    {
        Title = title;
        Content = content;
        Date = DateTime.Now;
    }
}