using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace App1
{
    public partial class MainPage : ContentPage
    {
        public bool loggedin = false;
        public List<Project> ProjectList = new List<Project>();
        public MainPage()
        {
            InitializeComponent();
            if(!loggedin)
            {
                Navigation.PushModalAsync(new LoginPage(this));
            }
        }

        //Question: Where can i put this code so I dont have to rewrite it everywhere
        async void OnLoginToolbarClick(object obj, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new LoginPage(this)));
        }
        async void OnLogoutToolbarClick(object obj, EventArgs e)
        {
            //TODO actually log out
            loggedin = false;
            await DisplayAlert("Logout", "You have been logged out", "OK");
            //This gave me a headache. You have to use PushModalAsync on modal pages or it crashes without error...
            await Navigation.PushModalAsync(new LoginPage(this));       
        }

        async void OnNewProjectClick(object obj, EventArgs e)
        {
            await Navigation.PushAsync(new CreateProjectPage(this));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            ProjectLayout.Children.Clear();
            Label l;
            foreach(Project p in ProjectList)
            {
                    l = p.MakeLabel();
                    var tapGestureRecognizer = new TapGestureRecognizer();
                    tapGestureRecognizer.Tapped += async (s, e) =>
                    {
                        await Navigation.PushAsync(new ProjectPage(p));
                    };
                    l.GestureRecognizers.Add(tapGestureRecognizer);
                    ProjectLayout.Children.Add(l);
            }
            //Label b;
            //b = new Label();
            //b.Text = name;
            ////This is so you can remove names by tapping
            //var tapGestureRecognizer = new TapGestureRecognizer();
            //tapGestureRecognizer.Tapped += (s, e) => {
            //    Label ding = s as Label;
            //    names.Remove(ding.Text);
            //    LayoutStack.Children.Remove(ding);
            //};
            //b.GestureRecognizers.Add(tapGestureRecognizer);
            //LayoutStack.Children.Add(b);
        }
    }
}
