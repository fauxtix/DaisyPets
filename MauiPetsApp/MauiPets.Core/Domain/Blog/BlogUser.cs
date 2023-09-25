using Microsoft.AspNetCore.Identity;

namespace MauiPetsApp.Core.Domain.Blog
{
    // Add profile data for application users by adding properties to the BlogUser class
    public class BlogUser : IdentityUser
    {
        public ICollection<Comment>? Comments { get; set; }
    }
}
