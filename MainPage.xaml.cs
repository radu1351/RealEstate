using System.Collections.ObjectModel;
using System.Linq;

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

        private void OnSortOrderChanged(object sender, EventArgs e)
        {
            var picker = sender as Picker;
            var selectedIndex = picker.SelectedIndex;
            if (selectedIndex == -1) return;

            switch (selectedIndex)
            {
                case 0:
                    // Sort ascending by price
                    RealEstateItems = new ObservableCollection<House>(RealEstateItems.OrderBy(h => h.Price));
                    break;
                case 1:
                    // Sort descending by price
                    RealEstateItems = new ObservableCollection<House>(RealEstateItems.OrderByDescending(h => h.Price));
                    break;
            }

            // Refresh the binding
            OnPropertyChanged(nameof(RealEstateItems));
        }
    }
}
