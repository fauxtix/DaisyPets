namespace MauiPets.Controls
{
    using CommunityToolkit.Maui.Views;

    public class PhotoPopup : Popup
    {
        public PhotoPopup(string imagePath)
        {
            Content = new VerticalStackLayout
            {
                Children =
            {
                new Image
                {
                    Source = imagePath,
                    Aspect = Aspect.AspectFit,
                    WidthRequest = 320,
                    HeightRequest = 320
                }
            }
            };
        }
    }
}
