using AskSpace.Models;
using Microsoft.Maui.Dispatching;
using SpaceScience.Interfaces;
using SpaceScience.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceScience.ViewModels
{
    public class MapPageViewModel : ViewModelBase
    {
        public MapPageViewModel(INavigationService navigationService
            , IDataService dataService
            , IDispatcher dispatcher) : base(navigationService, dataService, dispatcher)
        {

        }

        #region properties

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #endregion

        #region commands



        #endregion

        #region overrides

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            Title = "Map";
            _dispatcher.StartTimer(TimeSpan.FromMilliseconds(50), () =>
            {
                LoadData();
                return false;
            });
            await base.InitializeAsync(parameters);
        }

        #endregion

        #region privates


        private void LoadData()
        {
           

        }
        #endregion
    }
}
