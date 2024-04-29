using User_Management_API.Models;

namespace User_Management_API
{
    public class UsersDataStore
    {
        public List<UserDto> Users { get; set; }

        public UsersDataStore()
        {
            Users = new List<UserDto>()
            {
                new UserDto()
                {
                    Id = 1,
                    FirstName = "Emily",
                    LastName = "Johnson",
                    Mail = "emily.johnson@example.com"
                },
                new UserDto()
                {
                    Id = 2,
                    FirstName = "Alexander",
                    LastName = "Smith",
                    Mail = "alexander.smith@example.com"
                },
                new UserDto()
                {
                    Id = 3,
                    FirstName = "Olivia",
                    LastName = "Williams",
                    Mail = "olivia.williams@example.com"
                },
                new UserDto()
                {
                    Id = 4,
                    FirstName = "Ethan",
                    LastName = "Brown",
                    Mail = "ethan.brown@example.com"
                },
                new UserDto()
                {
                    Id = 5,
                    FirstName = "Sophia",
                    LastName = "Davis",
                    Mail = "sophia.davis@example.com"
                }
            };
        }
    }
}