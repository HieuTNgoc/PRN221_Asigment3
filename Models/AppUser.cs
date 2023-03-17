using System;
using System.Collections.Generic;

namespace SignalRAssignment.Models;

public partial class AppUser
{
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Addresss { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();
}
