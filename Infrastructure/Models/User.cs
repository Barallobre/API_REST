using System;
using System.Collections.Generic;

namespace API_REST.Models;

public partial class User
{
    public int Id { get; set; }

    public string Password { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public int? UserTypeId { get; set; }

    public DateTime? EntryDate { get; set; }

    public bool? Active { get; set; }

    public string? UserName { get; set; }

    public virtual UserType? UserType { get; set; }
}
