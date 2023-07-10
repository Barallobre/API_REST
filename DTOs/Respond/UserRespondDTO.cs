namespace API_REST.DTOs.Respond;

public class UserRespondDTO
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? UserTypeId { get; set; }

    public bool? Active { get; set; }

    public string? UserName { get; set; }

}
