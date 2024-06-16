using System.Collections.ObjectModel;

namespace Imobiliare
{
    public partial class MainPage : ContentPage
    {
        public ObservableCollection<House> RealEstateItems { get; set; }

        public MainPage()
        {
            InitializeComponent();
            RealEstateItems = new ObservableCollection<House>();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Load houses from database
            var houses = await App.Database.GetHousesAsync();
            RealEstateItems.Clear();
            foreach (var house in houses)
            {
                RealEstateItems.Add(house);
            }
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
}
