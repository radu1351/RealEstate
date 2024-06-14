using System.Collections.ObjectModel;

namespace Imobiliare
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<RealEstate> RealEstateItems { get; set; }

        public MainPage()
        {
            InitializeComponent();
            RealEstateItems = new ObservableCollection<RealEstate>
            {
                new RealEstate { ImageUrl = "house1.jpg", Name = "Casa", Price = "$200,000" },
                new RealEstate { ImageUrl = "house1.jpg", Name = "Casa", Price = "$250,000" },
                // Add more real estate items here
            };
            BindingContext = this;
        }

        private void OnAddRealEstateClicked(object sender, EventArgs e)
        {
            // Add logic to add a new real estate item
            RealEstateItems.Add(new RealEstate { ImageUrl = "house2.jpeg", Name = "Casa", Price = "$300,000" });
        }

        private async void OnItemTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var realEstate = frame.BindingContext as RealEstate;
            if (realEstate != null)
            {
                await Navigation.PushAsync(new DetailsPage(realEstate));
            }
        }
    }

   
    public class RealEstate
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
    }

}
