using AskSpace.Models;
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
    public class MarsPageViewModel : ViewModelBase
    {
        public MarsPageViewModel(INavigationService navigationService, IDataService dataService ) : base(navigationService, dataService)
        {

        }

        #region properties

        private bool _isMarsByCamera;
        public bool IsMarsByCamera
        {
            get => _isMarsByCamera;
            set => SetProperty(ref _isMarsByCamera, value);
        }

        private bool _isMarsByDate;
        public bool IsMarsByDate
        {
            get => _isMarsByDate;
            set => SetProperty(ref _isMarsByDate, value);
        }

        private ObservableCollection<CameraTypeVm> _cameraTypes;
        public ObservableCollection<CameraTypeVm> CameraTypes
        {
            get => _cameraTypes;
            set => SetProperty(ref _cameraTypes, value);
        }

        private ObservableCollection<MarsPhoto> _marsPhotos = new ObservableCollection<MarsPhoto>();
        public ObservableCollection<MarsPhoto> MarsPhotos
        {
            get => _marsPhotos;
            set => SetProperty(ref _marsPhotos, value);
        }

        private MarsDto _mars;
        public MarsDto Mars
        {
            get => _mars;
            set => SetProperty(ref _mars, value);
        }

        private DateTime _selectedDate = DateTime.Now;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                if (SetProperty(ref _selectedDate, value))
                {
                    _selectedDate = value;
                    Task.Run(() => GetMarsPhotosByDate(_selectedDate.ToString("yyyy-MM-dd")));

                }
            }
        }

        public DateTime MaxDate => DateTime.Today;

        private string _title;
        public string Title
        {
            get => _title;
            set => SetProperty(ref _title, value);
        }

        #endregion

        #region commands

        private DelegateCommand<CameraTypeVm> _goSelectCamera;
        public DelegateCommand<CameraTypeVm> GoSelectCamera => _goSelectCamera ?? (_goSelectCamera = new DelegateCommand<CameraTypeVm>(ProcessSelectCamera));


        private DelegateCommand<string> _goSelectMarsSource;
        public DelegateCommand<string> GoSelectMarsSource => _goSelectMarsSource ?? (_goSelectMarsSource = new DelegateCommand<string>(ProcessSelectMarsSource));

        private async void ProcessSelectCamera(CameraTypeVm camera)
        {
            if (camera == null) return;

            try
            {
                IsBusy = true;
                foreach (var item in CameraTypes)
                {
                    if (!item.Equals(camera))
                    {
                        item.IsSelected = false;
                        continue;
                    };

                    item.IsSelected = true;

                    RaisePropertyChanged(nameof(item));
                }
                var response = await _dataService.GetMarsDataByCameraType(camera.Type.ToLower());
                if (response == null) return;

                Mars = response;
                MarsPhotos = new ObservableCollection<MarsPhoto>(Mars.photos);
            }
            catch (Exception)
            {

            }
            finally
            {
                IsBusy = false;
            }


        }

        private async void ProcessSelectMarsSource(string source)
        {
            if (string.IsNullOrEmpty(source)) return;

            try
            {
                MarsPhotos?.Clear();
                if (source.Equals("ByCamera"))
                {
                    IsMarsByDate = false;
                    IsMarsByCamera = !IsMarsByDate;
                    GoSelectCamera.Execute(CameraTypes?.FirstOrDefault());
                }
                if (source.Equals("ByDate"))
                {
                    IsMarsByCamera = false;
                    IsMarsByDate = !IsMarsByCamera;

                    await GetMarsPhotosByDate(SelectedDate.ToString("yyyy-MM-dd"));

                }

                RaisePropertyChanged(nameof(MarsPhotos));
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region overrides

        public override async Task InitializeAsync(INavigationParameters parameters)
        {
            Title = "Mars";
            LoadData();
            await base.InitializeAsync(parameters);
        }

        #endregion

        #region privates


        private void LoadData()
        {
            IsMarsByCamera = true;
            MarsPhotos?.Clear();
            CameraTypes = new ObservableCollection<CameraTypeVm>(GenerateTypesList());
            GoSelectCamera.Execute(CameraTypes?.FirstOrDefault());

        }

        private async Task GetMarsPhotosByDate(string date)
        {
            try
            {
                IsBusy = true;
                var photos = new List<MarsPhoto>();
                var mars = await _dataService.GetMarsDataByDate(date);
                if (mars != null && mars.photos?.Count > 20)
                    photos = mars.photos.Take(20).ToList();
                else
                    photos = mars.photos.ToList();

                MarsPhotos = new ObservableCollection<MarsPhoto>(photos);
                RaisePropertyChanged(nameof(Mars));
                RaisePropertyChanged(nameof(MarsPhotos));
            }
            catch (Exception ex)
            {

            }
            finally
            {
                IsBusy = false;
            }
        }

        private List<CameraTypeVm> GenerateTypesList()
        {
            return new List<CameraTypeVm>()
            {
                new CameraTypeVm
                {
                    Type = "FHAZ"
                },
                new CameraTypeVm
                {
                    Type = "RHAZ"
                },
                new CameraTypeVm
                {
                    Type = "MAST"
                },
                new CameraTypeVm
                {
                    Type = "CHEMCAM"
                },
                new CameraTypeVm
                {
                    Type = "MAHLI"
                },
                new CameraTypeVm
                {
                    Type = "MARDI"
                },
                new CameraTypeVm
                {
                    Type = "NAVCAM"
                }
            };
        }

        #endregion
    }
}
