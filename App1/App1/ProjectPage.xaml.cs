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
	public partial class ProjectPage : ContentPage
	{
        Project pr;
		public ProjectPage (Project p)
		{
            pr = p;
			InitializeComponent ();
            Title = p.name;
            Label l;
            foreach(string name in p.participants)
            {
                l = new Label();
                l.Text = name;
                NameList.Children.Add(l);
            }
		}
        async void OnStartTimerButtonClicked(object obj, EventArgs e)
        {
            //Start p.Timer
            
        }

    }
}