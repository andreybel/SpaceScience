using Android.Telephony.Data;
using AskSpace.Models;
using Newtonsoft.Json;
using SpaceScience.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceScience.Services
{
    public class DataService : IDataService
    {
        public async Task<MarsDto> GetMarsDataByCameraType(string cameraType)
        {

            try
            {
                var result = new MarsDto();

                var uri = new Uri($"{AppSettings.Constants.BaseApi}/mars-photos/api/v1/rovers/curiosity/photos?sol=1000&camera={cameraType}&api_key=x5XFUSsKp0IdLjW16D9HPVKq2cpwpzZapI4uhwwZ");

                var client = new HttpClient();

                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<MarsDto>(responseString);
                }

                return result;

            }
            catch (JsonException jsEx)
            {
                Application.Current.Dispatcher.Dispatch(() => Application.Current.MainPage.DisplayAlert("Error", jsEx.Message, "Ok"));
                return new MarsDto();
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Dispatch(() => Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"));
                return new MarsDto();
            }
        }

        public async Task<MarsDto> GetMarsDataByDate(string date)
        {
            try
            {
                var result = new MarsDto();

                var uri = new Uri($"{AppSettings.Constants.BaseApi}/mars-photos/api/v1/rovers/curiosity/photos?earth_date={date}&api_key=x5XFUSsKp0IdLjW16D9HPVKq2cpwpzZapI4uhwwZ");

                var client = new HttpClient();

                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<MarsDto>(responseString);
                }

                
                return result;

            }
            catch (JsonException jsEx)
            {
                Application.Current.Dispatcher.Dispatch(() => Application.Current.MainPage.DisplayAlert("Error", jsEx.Message, "Ok"));
                return new MarsDto();
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Dispatch(() => Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"));
                return new MarsDto();
            }
        }

        public async Task<ApodDto> GetNasaApod()
        {
            try
            {
                var result = new ApodDto();

                var uri = new Uri($"{AppSettings.Constants.BaseApi}/planetary/apod?api_key=x5XFUSsKp0IdLjW16D9HPVKq2cpwpzZapI4uhwwZ");


                var client = new HttpClient();

                var response = await client.GetAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    result = JsonConvert.DeserializeObject<ApodDto>(responseString);
                }

              
                return result;

            }
            catch (JsonException jsEx)
            {
                Application.Current.Dispatcher.Dispatch(() => Application.Current.MainPage.DisplayAlert("Error", jsEx.Message, "Ok"));
                return new ApodDto();
            }
            catch (Exception ex)
            {
                Application.Current.Dispatcher.Dispatch(() => Application.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok"));
                return new ApodDto();
            }

        }

        public async Task<List<EpicDto>> GetNasaEpic()
        {
            //var result = await _networkService.ProcessApi(x => x.GetEpic());

            //return result;

            return null;
        }
    }
}

