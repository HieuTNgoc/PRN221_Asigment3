using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SignalRAssignment.Models;

public partial class Post
{
    public int PostId { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? UpdatedDate { get; set; }

    public string? Title { get; set; }

    public string? Content { get; set; }

    public int? PublishStatus { get; set; }

    public int? AuthorId { get; set; }

    public int? CategoryId { get; set; }

    public virtual AppUser? Author { get; set; }

    public virtual PostCategory? Category { get; set; }
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