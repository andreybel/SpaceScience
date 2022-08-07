using Microsoft.Maui.Dispatching;
using SpaceScience.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceScience.ViewModels
{
    public class ViewModelBase : BindableBase, IInitializeAsync, IInitialize, INavigatedAware, IPageLifecycleAware
    {
        protected readonly INavigationService _navigationService;
        protected readonly IDataService _dataService;
        protected readonly IDispatcher _dispatcher;

        public ViewModelBase(INavigationService navigationService, IDataService dataService, IDispatcher dispatcher)
        {
            _navigationService = navigationService;
            _dataService = dataService;
            _dispatcher = dispatcher;
        }

        #region properties

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        #endregion

        #region commands

        private DelegateCommand _goBack;
        public DelegateCommand GoBack => _goBack ?? (_goBack = new DelegateCommand(ProcessGoBack));

        #endregion

        #region virtual methods

        protected virtual async void ProcessGoBack()
        {
            await _navigationService.GoBackAsync();
        }

        public virtual void Initialize(INavigationParameters parameters)
        {
        }

        public virtual Task InitializeAsync(INavigationParameters parameters)
        {
            return Task.CompletedTask;
        }

        public virtual void OnAppearing()
        {
        }

        public virtual void OnDisappearing()
        {
        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {
        }

        #endregion


    }
}
