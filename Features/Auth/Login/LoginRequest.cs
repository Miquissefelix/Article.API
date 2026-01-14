namespace MiniDevTo.Features.Auth.Login
{
    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}

public class LoginResponse
{
    public string Message { get; set; }= "";    
    public string Token { get; set; } = "";
    public string Role { get; set; } = "";
}