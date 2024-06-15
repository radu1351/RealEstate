using System.Net;

namespace Imobiliare;
public partial class DetailsPage : ContentPage
{

    private const string GoogleMapsApiKey = "AIzaSyAznlr4BpcW1KUJ-VE_nOlImn_IUf9vm34";

    public DetailsPage()
    {
        InitializeComponent();
    }

    public DetailsPage(House house)
    {
        InitializeComponent();
        BindingContext = house;
        ShowGoogleMap(house.Address);
    }

    private async void ShowGoogleMap(string address)
    {
        try
        {
            // Construiește URL-ul pentru Google Static Maps API
            string mapUrl = $"https://maps.googleapis.com/maps/api/staticmap?" +
                            $"center={Uri.EscapeDataString(address)}" +
                            $"&size=900x900" +
                            $"&markers=color:red%7Clabel:%7C{Uri.EscapeDataString(address)}" +
                            $"&key={GoogleMapsApiKey}";

            // Folosește HttpClient pentru a descărca imaginea de la URL
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(mapUrl);
                if (response.IsSuccessStatusCode)
                {

                    var stream = await response.Content.ReadAsStreamAsync();
                    GoogleMapImage.Source = ImageSource.FromStream(() => stream);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading Google Map. {ex}");
        }
    }
}