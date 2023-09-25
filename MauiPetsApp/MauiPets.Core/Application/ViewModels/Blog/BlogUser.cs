using Microsoft.AspNetCore.Identity;

namespace MauiPetsApp.Core.Application.ViewModels
{
    // Add profile data for application users by adding properties to the BlogUser class
    public class BlogUser : IdentityUser
    {
        public ICollection<CommentDto>? Comments { get; set; }
    }
}
