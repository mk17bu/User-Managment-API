namespace User_Management_API.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }

    public ICollection<Role> Roles { get; set; } = new List<Role>();
    
    public void UpdateUser(string firstName, string lastName, string mail, ICollection<Role> roles)
    {
        FirstName = firstName;
        LastName = lastName;
        Mail = mail;
        Roles = roles;
    }
}