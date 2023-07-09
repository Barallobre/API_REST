using System;
using System.Collections.Generic;

namespace API_REST.Infrastructure.Models;

public partial class UserType
{
    public int Id { get; set; }

    public string? Type { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
