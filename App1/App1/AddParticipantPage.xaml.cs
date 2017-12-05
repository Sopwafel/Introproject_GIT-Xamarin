using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddParticipantPage : ContentPage
	{
        CreateProjectPage c;
        List<string> names = new List<string>();
		public AddParticipantPage (CreateProjectPage cpp)
		{
            c = cpp;
			InitializeComponent ();
		}
        async void OnAddParticipantClicked(object obj, EventArgs ea)
        {
            string name = participantName.Text;
            if (AskServerIfNameExists(name))
                if (!names.Contains(name))
                {
                    names.Add(name);
                    Label b;
                    b = new Label();
                    b.Text = name;
                    //This is so you can remove names by tapping
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += (s, e) => {
                        Label ding = s as Label;
                        names.Remove(ding.Text);
                        StackLayouyt.Children.Remove(ding);
                    };
                    b.GestureRecognizers.Add(tapGestureRecognizer);
                    StackLayouyt.Children.Add(b);
                }
                else;   //ugly
            else
                await DisplayAlert("Error", "The entered name was not found", "OK");
        }

        bool AskServerIfNameExists(string name)
        {
            //TODO Make this ask the server if name has an account
            return true;
        }
        async void OnFinalizeButtonClicked(object obj, EventArgs e)
        {
            foreach(string name in names)
                if(!c.names.Contains(name))
                    c.names.Add(name);
            await Navigation.PopAsync();
        }
    }
}