using AskSpace.Models;
using Microsoft.Maui.Dispatching;
using SpaceScience.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceScience.ViewModels
{
    public class ApodPageViewModel : ViewModelBase
    {
        public ApodPageViewModel(INavigationService navigationService
            , IDataService dataService
            , IDispatcher dispatcher) : base(navigationService, dataService, dispatcher)
        {
        }

        #region properties

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty( ref _title, value );
        }

        private ApodDto _apod;
        public ApodDto Apod
        {
            get => _apod;
            set => SetProperty(ref _apod, value);
        }

        #endregion

        #region commands

        #endregion

        #region overrides

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            Title = "Astronomy Picture Of the Day";

            _dispatcher.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                Task.Run(() => LoadData());
                return false;
            });

            await base.InitializeAsync(parameters);
        }

        #endregion

        #region privates

        private async Task LoadData()
        {
            try
            {
                IsBusy = true;
                var apod = await _dataService.GetNasaApod();
                if (apod != null)
                {
                    Apod = apod;
                }
            }
            finally
            {
                IsBusy = false;
            }
        }

        #endregion
    }
}
