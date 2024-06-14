namespace Imobiliare
{
    public partial class AddHousePage : ContentPage
    {
        private string _imagePath;
        private Action<House> _addHouseCallback;
        private Entry priceEntry;

        public AddHousePage(Action<House> addHouseCallback)
        {
            InitializeComponent();
            _addHouseCallback = addHouseCallback;

            priceEntry = (Entry)FindByName("PriceEntry");
            priceEntry.TextChanged += OnPriceEntryTextChanged;
        }

        private async void OnImageTapped(object sender, EventArgs e)
        {
            var result = await FilePicker.PickAsync(new PickOptions
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Select an image"
            });

            if (result != null)
            {
                var stream = await result.OpenReadAsync();
                var houseImage = (Image)FindByName("HouseImage");
                houseImage.Source = ImageSource.FromStream(() => stream);

                // Save the file path for later use
                _imagePath = result.FullPath;
            }
        }

        private async void OnAddHouseClicked(object sender, EventArgs e)
        {
            // Retrieve entered data using FindByName
            var nameEntry = (Entry)FindByName("NameEntry");
            var priceEntry = (Entry)FindByName("PriceEntry");
            var addressEditor = (Editor)FindByName("AddressEditor");
            var descriptionEditor = (Editor)FindByName("DescriptionEditor");
            
            var name = nameEntry.Text;
            var price = int.Parse(priceEntry.Text);
            var adress = addressEditor.Text;
            var description = descriptionEditor.Text;

            // Validate and add the new house
            if (!string.IsNullOrWhiteSpace(name) && price != 0 && !string.IsNullOrWhiteSpace(description) && !string.IsNullOrWhiteSpace(_imagePath) && !string.IsNullOrWhiteSpace(adress))
            {
                var newHouse = new House
                {
                    Name = name,
                    Price = price,
                    Description = description,
                    ImageUrl = _imagePath
                };

                // Call the callback to add the new house
                _addHouseCallback?.Invoke(newHouse);

                // Navigate back to the main page
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Error", "Please fill in all fields and select an image.", "OK");
            }
        }

       private void OnPriceEntryTextChanged(object sender, TextChangedEventArgs e)
        {
            // Ensure only numeric input is allowed
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                // Check if the new text value contains non-numeric characters
                if (!IsNumeric(e.NewTextValue))
                {
                    // Remove non-numeric characters
                    priceEntry.Text = e.OldTextValue ?? string.Empty;
                }
            }
        }

        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }
    }
}