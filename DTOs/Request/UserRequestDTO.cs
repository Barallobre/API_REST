namespace API_REST.DTOs.Request;

public class UserRequestDTO
{
    public string UserPassword { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

}
