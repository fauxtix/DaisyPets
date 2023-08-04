namespace DaisyPets.Web.Blazor.Services.SEO
{
    public class MetadataProvider
    {
        public Dictionary<string, MetadataValue> RouteDetailMapping { get; set; } = new()
        {
            {
                "/",
                new()
                {
                    Title = "Manage the day to day life of your pets ",
                    Description = "This application aims to help pet owners in their day-to-day management."
                }
            },
            {
                "/about",
                new()
                {
                    Title = "About us",
                    Description = "Email us: fauxtix.luix@gmail.com - The FL Systems team."
                }
            }
        };
    }
}
