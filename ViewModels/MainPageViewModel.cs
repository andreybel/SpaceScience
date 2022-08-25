using SpaceScience.Interfaces;
using SpaceScience.Views;

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
            else if (target.ToLower().Equals("apod"))
                await _navigationService.NavigateAsync(nameof(ApodPage));
            else if (target.ToLower().Equals("mylocation"))
                await ShowMyLocation();
                //await _navigationService.NavigateAsync(nameof(MapPage));

            return;
        }

        #endregion

        #region overrides
        #endregion

        #region privates

        private async Task ShowMyLocation()
        {
            CancellationTokenSource _cancelTokenSource = new CancellationTokenSource();

            try
            {
                GeolocationRequest geolocationRequest = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

                Location location = await Geolocation.Default.GetLocationAsync(geolocationRequest, _cancelTokenSource.Token);

                await Map.Default.OpenAsync(location);
            }
            catch(FeatureNotSupportedException nse)
            {
                Console.WriteLine(nse.Message);
            }
            catch (TimeoutException te)
            {
                Console.WriteLine(te.Message);
            }
            catch (FeatureNotEnabledException nee)
            {
                Console.WriteLine(nee.Message);
            }
            catch(PermissionException pe)
            {
                Console.WriteLine(pe.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        #endregion
    }
}
