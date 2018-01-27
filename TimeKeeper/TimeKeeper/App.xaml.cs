using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TimeKeeper.Services;
using Xamarin.Forms;

namespace TimeKeeper
{
	public partial class App : Application
	{
        private static Locator _locator;
        public static Locator Locator
        {
            get
            {
                return _locator ?? (_locator = new Locator());
            }
        }

		public App ()
		{
			InitializeComponent();

            MainPage = new MainPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
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
