﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace App1
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			
            //TODO logged in check
            MainPage = new NavigationPage(new MainPage());

        }

		protected override void OnStart ()
		{

		}

        protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
