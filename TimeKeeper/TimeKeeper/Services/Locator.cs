using GalaSoft.MvvmLight.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using TimeKeeper.Services.Implementations;
using TimeKeeper.Services.Interfaces;
using TimeKeeper.ViewModels;
using TimeKeeper.Views;

namespace TimeKeeper.Services
{
    public class Locator
    {
        public Locator()
        {
            // Register view models
            SimpleIoc.Default.Register<MainPageViewModel>();
            SimpleIoc.Default.Register<OverviewViewModel>();
            SimpleIoc.Default.Register<CreateWorkdayViewModel>();

            // Register services
            NavService navService = ConfigureNavService();
            SimpleIoc.Default.Register<INavService>(() => navService);
        }

        // View models
        public MainPageViewModel MainPageViewModel => SimpleIoc.Default.GetInstance<MainPageViewModel>();
        public OverviewViewModel OverviewViewModel => SimpleIoc.Default.GetInstance<OverviewViewModel>();
        public CreateWorkdayViewModel CreateWorkdayViewModel => SimpleIoc.Default.GetInstance<CreateWorkdayViewModel>();

        // Services
        public INavService NavService => SimpleIoc.Default.GetInstance<INavService>();

        private NavService ConfigureNavService()
        {
            NavService navService = new NavService();
            navService.Configure(Constants.Navigation.MainPage, typeof(MainPage));
            navService.Configure(Constants.Navigation.Overview, typeof(Overview));
            navService.Configure(Constants.Navigation.CreateWorkdayView, typeof(CreateWorkdayView));

            return navService;
        }
    }
}
