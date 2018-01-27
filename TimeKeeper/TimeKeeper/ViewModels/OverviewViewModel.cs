using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Services.Interfaces;

namespace TimeKeeper.ViewModels
{
    public class OverviewViewModel : ViewModelBase
    {
        private INavService _navService;

        private RelayCommand _navigateToWorkdayCreation;
        public RelayCommand NavigateToWorkdayCreation
        {
            get
            {
                if (_navigateToWorkdayCreation == null)
                {
                    _navigateToWorkdayCreation = new RelayCommand(DoNavigation);
                }
                return _navigateToWorkdayCreation;
            }
        }

        public OverviewViewModel(INavService navService)
        {
            _navService = navService;
        }

        private void DoNavigation()
        {
            _navService.NavigateTo(Constants.Navigation.CreateWorkdayView);
        }
    }
}
