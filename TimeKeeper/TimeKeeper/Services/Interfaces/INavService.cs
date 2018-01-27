using GalaSoft.MvvmLight.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TimeKeeper.Services.Interfaces
{
    public interface INavService : INavigationService
    {
        void Initialize(NavigationPage navigationPage);
        void NavigateToModal(string pageKey);
    }
}
