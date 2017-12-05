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
	public partial class CreateProjectPage : ContentPage
	{
        MainPage m;
        public List<string> activities = new List<string>();
        public CreateProjectPage (MainPage mp)
		{
            m = mp;
			InitializeComponent ();
		}
        async void OnAddParticipantsClicked(object obj, EventArgs e)
        {
            await Navigation.PushAsync(new AddParticipantPage(this));
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            foreach(string name in activities)
            {
                Label b;
                b = new Label();
                b.Text = name;
                //This is so you can remove names by tapping
                var tapGestureRecognizer = new TapGestureRecognizer();
                tapGestureRecognizer.Tapped += (s, e) => {
                    Label LabelToRemove = s as Label;
                    activities.Remove(LabelToRemove.Text);
                    LayoutStack.Children.Remove(LabelToRemove);
                };
                b.GestureRecognizers.Add(tapGestureRecognizer);
                LayoutStack.Children.Add(b);
            }
        }
        async void OnFinalizeButtonClicked(object obj, EventArgs e)
        {
            m.ProjectList.Add(new Project(ProjectName.Text, activities));
            await Navigation.PopAsync();
            //TODO: actually communicate with server and make a project
        }
    }
}