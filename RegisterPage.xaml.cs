namespace Imobiliare
{
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;
            string fullName = FullNameEntry.Text;
            string address = AddressEntry.Text;

            if (string.IsNullOrWhiteSpace(email) || !email.Contains('@') || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(address))
            {
                await DisplayAlert("Error", "Please fill in all fields correctly", "OK");
                return;
            }

            var newUser = new User
            {
                Email = email,
                Password = password,
                FullName = fullName,
                Address = address
            };
            try
            {
                await App.Database.SaveUserAsync(newUser);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString()); 
            }
    
            await Navigation.PopAsync();
        }
    }
}
