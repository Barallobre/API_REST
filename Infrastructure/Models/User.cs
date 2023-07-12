
namespace API_REST.Infrastructure.Models;

public class User
{
    public int Id { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string PasswordSalt { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? UserTypeId { get; set; }

    public DateTime? EntryDate { get; set; }

    public bool? Active { get; set; }

    public string? UserName { get; set; }

    public virtual UserType? UserType { get; set; }
}
