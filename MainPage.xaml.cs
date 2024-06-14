using System.Collections.ObjectModel;

namespace Imobiliare
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<House> RealEstateItems { get; set; }

        public MainPage()
        {
            InitializeComponent();
            RealEstateItems = new ObservableCollection<House>
            {
                new House { ImageUrl = "house1.jpg", Name = "Casa 1", Price = 200000, Adress="Soseaua Iancului", Description = "Beautiful house with 3 bedrooms and 2 bathrooms." },
                new House { ImageUrl = "house1.jpg", Name = "Casa 2", Price = 250000, Adress="Bldv. Iuliu Maniu 59", Description = "Modern house with 4 bedrooms and a swimming pool." },
            };
            BindingContext = this;
        }

        private async void OnAddHouseClicked(object sender, EventArgs e)
        {
            var addHousePage = new AddHousePage(AddNewHouse);
            await Navigation.PushAsync(addHousePage);
        }

        private void AddNewHouse(House newHouse)
        {
            RealEstateItems.Add(newHouse);
        }

        private async void OnItemTapped(object sender, EventArgs e)
        {
            var frame = sender as Frame;
            var realEstate = frame.BindingContext as House;
            if (realEstate != null)
            {
                await Navigation.PushAsync(new DetailsPage(realEstate));
            }
        }
    }

    public class House
    {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public string Adress { get; set; }
        public string Description { get; set; }
    }

}
