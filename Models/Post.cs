using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SignalRAssignment.Models;

public partial class Post
{
    [Display(Name = "Post ID")]
    public int PostId { get; set; }
    [Display(Name = "Created At")]
    public DateTime? CreatedDate { get; set; }
    [Display(Name = "Updated At")]
    public DateTime? UpdatedDate { get; set; }

    [Display(Name = "Title")]
    public string? Title { get; set; }
    [Display(Name = "Content")]
    public string? Content { get; set; }
    [Display(Name = "Status")]
    public int? PublishStatus { get; set; }
    [Display(Name = "Creator")]
    public int? AuthorId { get; set; }
    [Display(Name = "Category")]
    public int? CategoryId { get; set; }

    public virtual AppUser? Author { get; set; }

    public virtual PostCategory? Category { get; set; }

    public virtual string statusName { get { if (PublishStatus == 0) return "Private"; return "Public"; }}

}

public partial class PostStatus
{
    public int? PublishStatus { get; set; }
    public string? StatusName { get; set; }
    public static List<PostStatus> getSattus()
    {
        List<PostStatus> status = new List<PostStatus>()
        {
            new PostStatus{ PublishStatus = 0, StatusName = "Priavte" },
            new PostStatus{ PublishStatus = 1, StatusName = "Public" },
        };
        return status;
    }
}