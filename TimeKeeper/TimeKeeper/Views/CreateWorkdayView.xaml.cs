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
	public partial class CreateWorkdayView : ContentPage
	{
		public CreateWorkdayView ()
		{
			InitializeComponent ();
            BindingContext = App.Locator.CreateWorkdayViewModel;
		}
	}
}