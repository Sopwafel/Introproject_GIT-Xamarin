using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static App1.App;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        MainPage mn;
		public LoginPage (MainPage main)
		{
            mn=main;
			InitializeComponent ();
		}

        /**
         * Checks if the credentials are correct.
         * TODO implement
         * */
        bool CredentialsCorrect(User user)
        {
            return true;
        }
        async void OnLoginButtonClicked(Object obj, EventArgs e)
        {
            var user = new User
            {
                Username = usernameEntry.Text,
                Password = passwordEntry.Text
            };
            if(CredentialsCorrect(user))
            {
                mn.loggedin = true;
               // Navigation.InsertPageBefore(new MainPage(), this);
                await Navigation.PopModalAsync();
            }
        }
        async void OnSignUpButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignUpPage());
        }
    }

}