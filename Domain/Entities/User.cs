namespace MiniDevTo.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; } 
        public string Password { get; set; } 
        public string Role { get; set; } = "Author"; // "Admin" ou "Author"
    }
}
