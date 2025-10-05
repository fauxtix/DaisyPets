namespace MauiPets.Controls
{
    using CommunityToolkit.Maui.Views;
    using Microsoft.Maui.Controls; // Certifique-se de importar

    public class PhotoPopup : Popup
    {
        public PhotoPopup(string imagePath)
        {
            Content = new VerticalStackLayout
            {
                Spacing = 16,
                Padding = 16,
                Children =
                {
                    new Image
                    {
                        Source = imagePath,
                        Aspect = Aspect.AspectFit,
                        WidthRequest = 320,
                        HeightRequest = 320
                    },
                    new Button
                    {
                        Text = "Fechar",
                        HorizontalOptions = LayoutOptions.Center,
                        Margin = new Thickness(0, 12, 0, 0),
                        CornerRadius = 8,
                        Command = new Command(() => Close())
                    }
                }
            };
        }
    }
}