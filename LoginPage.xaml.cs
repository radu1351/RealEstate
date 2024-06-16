namespace Imobiliare
{
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
            InitializeComponent();
        }

      
        private async void OnLoginClicked(object sender, EventArgs e)
        {
            string email = EmailEntry.Text;
            string password = PasswordEntry.Text;

            // Check if user exists in the database
            var user = await App.Database.GetUserByEmailAndPasswordAsync(email, password);

            if (user != null)
            {
                await Navigation.PushAsync(new MainPage());
            }
            else
            {
                await DisplayAlert("Error", "Invalid email or password", "OK");
            }
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            // Navigate to the RegisterPage
            await Navigation.PushAsync(new RegisterPage());
        }
    }
}
