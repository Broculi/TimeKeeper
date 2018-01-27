using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using TimeKeeper.Services.Interfaces;
using Xamarin.Forms;

namespace TimeKeeper.Services.Implementations
{
    // refer to https://wolfprogrammer.com/2016/07/22/navigation-using-mvvm-light/
    // for more info
    public class NavService : INavService
    {
        private readonly Dictionary<string, Type> _pagesByKey = new Dictionary<string, Type>();
        private NavigationPage _navigation;

        public void Initialize(NavigationPage navigationPage)
        {
            _navigation = navigationPage;
        }

        public string CurrentPageKey
        {
            get
            {
                lock (_pagesByKey)
                {
                    if (_navigation.CurrentPage == null)
                    {
                        return null;
                    }

                    var pageType = _navigation.CurrentPage.GetType();

                    return _pagesByKey.ContainsValue(pageType)
                        ? _pagesByKey.First(p => p.Value == pageType).Key
                        : null;
                }
            }
        }

        private Page _currentPage => _navigation?.CurrentPage;

        public void GoBack()
        {
            if (CanGoBack())
            {
                _navigation.PopAsync();
            }
        }

        public bool CanGoBack()
        {
            return _navigation?.Navigation?.NavigationStack?.Count > 1;
        }

        public void NavigateToModal(string pageKey)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];

                    var constructor = GetConstructor(type);
                    if (constructor == null)
                    {
                        var exceptionMessage = $"No suitable constructor found for page {pageKey}";
                        throw new InvalidOperationException(exceptionMessage);
                    }

                    var page = constructor.Invoke(null) as Page;

                    _currentPage.Navigation.PushModalAsync(page);
                }
                else
                {
                    var exceptionMessage = $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?";
                    throw new ArgumentException(exceptionMessage, nameof(pageKey));
                }
            }
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        // Required for interface
        public void NavigateTo(string pageKey, object parameter)
        {
            NavigateTo(pageKey, parameter, false);
        }

        private void NavigateTo(string pageKey, object parameter, bool newPageRoot)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    var type = _pagesByKey[pageKey];

                    var constructor = GetConstructor(type);
                    if (constructor == null)
                    {
                        var exceptionMessage = $"No suitable constructor found for page {pageKey}";
                        throw new InvalidOperationException(exceptionMessage);
                    }
                    
                    var page = constructor.Invoke(null) as Page;

                    if (newPageRoot)
                    {
                        // TODO add root page swapping
                    } else
                    {
                        _navigation.PushAsync(page, false);
                    }
                }
                else
                {
                    var exceptionMessage = $"No such page: {pageKey}. Did you forget to call NavigationService.Configure?";
                    throw new ArgumentException(exceptionMessage, nameof(pageKey));
                }
            }
        }

        private ConstructorInfo GetConstructor(Type type)
        {
            var constructor = type.GetTypeInfo()
                                .DeclaredConstructors
                                .FirstOrDefault(c => !c.GetParameters().Any());

            return constructor;
        }

        private ConstructorInfo GetConstructor(Type type, object[] parameters)
        {
            var parameterCount = parameters.Length;
            ConstructorInfo constructor;
            if (parameterCount > 0)
            {
                constructor = type.GetTypeInfo().DeclaredConstructors.SingleOrDefault(
                c =>
                {
                    var p = c.GetParameters();
                    return p.Count() == parameterCount && p[parameterCount - 1].ParameterType == parameters[parameterCount - 1].GetType();
                });
            }
            else
            {
                constructor = type.GetTypeInfo()
                                .DeclaredConstructors
                                .FirstOrDefault(c => !c.GetParameters().Any());
            }
            return constructor;
        }

        public void Configure(string pageKey, Type pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(pageKey))
                {
                    _pagesByKey[pageKey] = pageType;
                }
                else
                {
                    _pagesByKey.Add(pageKey, pageType);
                }
            }
        }

        public void SetNewRoot(string pageKey)
        {
            NavigateTo(pageKey, null, true);
        }
    }
}
