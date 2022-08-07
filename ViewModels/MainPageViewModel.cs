using Microsoft.Maui.Dispatching;
using SpaceScience.Interfaces;
using SpaceScience.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceScience.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public MainPageViewModel(INavigationService navigationService
            , IDataService dataService
            , IDispatcher dispatcher) : base(navigationService, dataService, dispatcher)
        {
        }

        #region properties
        #endregion

        #region commands

        private DelegateCommand<string> _goData;
        public DelegateCommand<string> GoData => (_goData ?? ( _goData = new DelegateCommand<string>(OnGoData)));

        private async void OnGoData(string target)
        {
            if (string.IsNullOrEmpty(target)) return;

            if (target.ToLower().Equals("mars"))
                await _navigationService.NavigateAsync(nameof(MarsPage));
            else if(target.ToLower().Equals("apod"))
                await _navigationService.NavigateAsync(nameof(ApodPage));

            return;
        }

        #endregion

        #region overrides
        #endregion

        #region privates
        #endregion
    }
}
