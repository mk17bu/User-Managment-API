namespace User_Management_API.Entities;

public class User
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Mail { get; set; }

    public Role Role { get; set; }
    
    public void UpdateUser(string firstName, string lastName, string mail, Role role)
    {
        FirstName = firstName;
        LastName = lastName;
        Mail = mail;
        Role = role;
    }
}