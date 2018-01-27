using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace TimeKeeper
{
	public partial class MainPage : TabbedPage
	{
		public MainPage()
		{
			InitializeComponent();
            BindingContext = App.Locator.MainPageViewModel;

            this.CurrentPageChanged += (object sender, EventArgs e) => {
                var i = this.Children.IndexOf(this.CurrentPage);
                System.Diagnostics.Debug.WriteLine("Page No:" + i);
            };
        }

        /// <summary>
        /// Sets the navigation context to the currently selected page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnCurrentPageChange(object sender, EventArgs e)
        {
            var page = CurrentPage as NavigationPage;
            App.Locator.NavService.Initialize(page);
        }
    }
}
