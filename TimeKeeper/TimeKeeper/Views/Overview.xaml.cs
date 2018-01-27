﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TimeKeeper.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Overview : ContentPage
	{
		public Overview ()
		{
			InitializeComponent ();
            
            BindingContext = App.Locator.OverviewViewModel;
        }

    }
}