using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignalRAssignment.Models;

public partial class AppUser
{
    [Display(Name = "User ID")]
    public int UserId { get; set; }

    [Display(Name = "Fullname")]
    public string? FullName { get; set; }

    [Display(Name = "Address")]
    public string? Addresss { get; set; }

    [Display(Name = "Email")]
    [Required(ErrorMessage = "Email is required")]
    [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Email must be between 3 and 255")]
    public string? Email { get; set; }

    [Display(Name = "Password")]
    [Required(ErrorMessage = "Password is required")]
    [StringLength(maximumLength: 255, MinimumLength = 3, ErrorMessage = "Password must be between 3 and 255")]
    public string? Password { get; set; }

    public virtual ICollection<Post> Posts { get; } = new List<Post>();
}
