namespace User_Management_API.Entities;

public class Reaction
{
    public int Id { get; set; }
    public ReactionType Type { get; set; }
    
    public User? User { get; set; }
    public int UserId { get; set; }
    
    public Post? Post { get; set; }
    public int PostId { get; set; }
}