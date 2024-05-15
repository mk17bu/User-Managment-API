namespace User_Management_API.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }
    
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ICollection<Reaction> Reactions { get; set; } = new List<Reaction>();
    
    public void UpdateUser(string firstName, string lastName, string mail)
    {
        FirstName = firstName;
        LastName = lastName;
        Mail = mail;
    }
}