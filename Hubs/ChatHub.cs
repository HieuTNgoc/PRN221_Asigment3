using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using SignalRAssignment.Models;

namespace SignalRAssignment.Hubs
{
    public class ChatHub : Hub
    {
        private readonly PostAppContext _context;

        public ChatHub(PostAppContext context)
        {
            _context = context;
        }
        public Post Post { get; set; } = new Post();
        public async Task SendMessage(string user, string message)
            => await Clients.All.SendAsync("ReceiveMessage", user, message);
        public async Task SendPost(string PostId, string AuthorId, string Title, string Content, string PublishStatus, string CategoryId)
        {
            Post.CreatedDate = DateTime.Now;
            Post.UpdatedDate = DateTime.Now;
            Post.AuthorId = Int32.Parse(AuthorId);
            Post.Title = Title;
            Post.Content = Content;
            Post.PublishStatus = Int32.Parse(PublishStatus);
            Post.CategoryId = Int32.Parse(CategoryId);

            _context.Posts.Add(Post);
            await _context.SaveChangesAsync();
            await Clients.All.SendAsync("ReceivePost", Post.PostId.ToString(), Post.AuthorId.ToString(), Post.CreatedDate.ToString(), Post.UpdatedDate.ToString(), Post.Title.ToString(),Post.Content.ToString(), Post.PublishStatus.ToString(), Post.CategoryId.ToString());
        }

    }
}
