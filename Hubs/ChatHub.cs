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
            var post = await _context.Posts.Include(p => p.Author).Include(p => p.Category).OrderByDescending(x => x.PostId).FirstOrDefaultAsync();
            await Clients.All.SendAsync("ReceivePost", post.PostId.ToString(), post.Author.FullName.ToString(), post.Author.Email.ToString(), post.CreatedDate.ToString(), post.UpdatedDate.ToString(), post.Title.ToString(),post.Content.ToString(), post.statusName.ToString(), Post.Category.CategoryName.ToString());
        }

    }
}
