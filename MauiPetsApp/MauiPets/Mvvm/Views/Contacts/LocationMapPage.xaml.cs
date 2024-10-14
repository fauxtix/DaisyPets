using Mapsui;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.UI.Maui;

namespace MauiPets.Mvvm.Views.Contacts
{
    [QueryProperty(nameof(Latitude), "latitude")]
    [QueryProperty(nameof(Longitude), "longitude")]
    public partial class LocationMapPage : ContentPage
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public LocationMapPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            InitializeMap();
        }

        private void InitializeMap()
        {

            var mapControl = new MapControl();
            var map = mapControl.Map;
            map?.Layers.Add(OpenStreetMap.CreateTileLayer());
            var latLong = SphericalMercator.FromLonLat(Longitude, Latitude);
            var point = new MPoint(latLong.Item1, latLong.Item2);
            map.CRS = "EPSG:3857";

            map.Home = n => n.CenterOnAndZoomTo(point, map.Navigator.Resolutions[18]);  //0 zoomed out-19 zoomed in

            var pin = new Pin();
            pin.Position = new Position(Convert.ToDouble(Latitude), Convert.ToDouble(Longitude));
            pin.Label = "Location";
            pin.Color = Mapsui.UI.Maui.KnownColor.Black;
            pin.Scale = 0.7f;
            MapView.Map = map;
            MapView.Pins.Add(pin);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}", true);

        }
    }
}