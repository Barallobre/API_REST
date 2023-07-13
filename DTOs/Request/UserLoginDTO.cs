namespace API_REST.DTOs.Request;

public class UserLoginDTO
{
    public string UserPassword { get; set; } = null!;

    public string? UserName { get; set; }

}
