using Syncfusion.Maui.Cards;
namespace MauiPets.Mvvm.Views.Settings;

public partial class MainSettingsPage : ContentPage
{
	public MainSettingsPage()
	{
		InitializeComponent();
        Grid mainStack = new Grid()
        {
            Children =
        {
            new SfCardView(){
            Content =  new Grid()
            {
                Padding = 20,
                Children =
                {
                    new Label(){Text="Wells Fargo", HorizontalOptions=LayoutOptions.Start, TextColor=Colors.White, FontSize=20, FontAttributes=FontAttributes.Bold},
                    new Grid(){
                        Children=
                        {
                            new Image(){Source="cardchip.png", WidthRequest=60, HeightRequest=30, HorizontalOptions=LayoutOptions.Center, VerticalOptions=LayoutOptions.Center},
                            new Label(){Text="Business Elite", FontAttributes=FontAttributes.Bold, TextColor=Colors.White, FontSize=17, HorizontalOptions=LayoutOptions.Center,VerticalOptions=LayoutOptions.Center ,Padding=30 }
                        } },
                    new Label(){HorizontalOptions=LayoutOptions.Start ,VerticalOptions=LayoutOptions.End, Text="Rick Sanchez", FontSize=17, FontAttributes=FontAttributes.Bold, TextColor=Colors.White},
                    new Label(){HorizontalOptions=LayoutOptions.Start,  VerticalOptions=LayoutOptions.End, Text="9 0 5 7    4 0 8 1    2 1 7 5    0 0 5 6", TextColor=Colors.White, Padding=10},
                }
            },
            BackgroundColor = Colors.Brown
            }
        }
        };
    }
}