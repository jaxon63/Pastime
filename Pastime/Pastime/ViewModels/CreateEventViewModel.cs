using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GooglePlacesApi;
using GooglePlacesApi.Models;
using Newtonsoft.Json;
using Pastime.Models;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class CreateEventViewModel : INotifyPropertyChanged
    {
        private string name = string.Empty;
        private string nameErrMsg = string.Empty;
        private bool invalidName;





        private string desc = string.Empty;
        private string descErrMsg = string.Empty;
        private bool invalidDesc;

        private EventModel em;

        private Activity activity;

        private readonly GooglePlacesApiService service;

        public CreateEventViewModel()
        {
            SubmitCommand = new Command(PostEvent);

            em = new EventModel();

            //TODO: the API key crashes the app for some reason.
            //TODO: get environment variable to work Environment.GetEnvironmentVariable("AIzaSyDymVB9no6IHh7boHfCSzDoV-jU9eeeQTk")
            //var settings = GoogleApiSettings.Builder.WithApiKey("AIzaSyBnDLNBhZVaiHtpwzHtVeSgJNSSyiTz9FE").Build();
            //service = new GooglePlacesApiService(settings);
            
            



    }

    public string Name
        {
            get => name;
            set
            {
                if (name == value)
                    return;

                name = value;
                OnPropertyChanged();
            }
        }

        public bool InvalidName
        {
            get => invalidName;
            set
            {
                if (invalidName == value)
                    return;

                invalidName = value;
                OnPropertyChanged();
            }
        }

        public string Desc
        {
            get => desc;
            set
            {
                if (desc == value)
                    return;

                desc = value;
                OnPropertyChanged();
            }
        }

        public bool InvalidDesc
        {
            get => invalidDesc;
            set
            {
                if (invalidDesc == value)
                    return;

                invalidDesc = value;
                OnPropertyChanged();
            }
        }

        public string DescErrMsg
        {
            get => descErrMsg;
            set
            {
                if (descErrMsg == value)
                    return;

                descErrMsg = value;
                OnPropertyChanged();
            }
        }

        public string NameErrMsg
        {
            get => nameErrMsg;
            set
            {
                if (nameErrMsg == value)
                    return;

                nameErrMsg = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { private set; get; }
       // public ICommand DoSearchCommand => new Command(async () => await DoSearchAsync().ConfigureAwait(false), () => CanSearch);

        //TODO: Location search doesn't work
        private string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged();
            }
        }

        /*

               private int resultCount;
               public int ResultCount
               {
                   get => resultCount;
                   set => resultCount = value;
               }

               private List<Prediction> results;
               public List<Prediction> Results
               {
                   get => results;
                   set => results = value;
               }

               public bool CanSearch => !string.IsNullOrWhiteSpace(SearchText) && SearchText.Length > 2;
               public bool HasResults => resultCount > 0;

               private async Task DoSearchAsync()
               {
                   var results = await service.GetPredictionsAsync(SearchText).ConfigureAwait(false);

                   if (results != null && results.Status.Equals("OK"))
                   {
                       Results = results.Items;
                       OnPropertyChanged("HasResults");
                   }
               }  */



        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PostEvent(object obj)
        {
            //Create Event Model which is responsible for validating the input
            //The view is then updated to reflect any errors in the input
            
            //Assign result of validation to public properties, which triggers the display of the error messages in the UI
            //The value of the error message is generated in EventModel, depening on the validation (too long, too short, empty ect.)
            InvalidName = !em.validateName(Name, out string nameErrMsg);
            NameErrMsg = nameErrMsg;
            InvalidDesc = !em.validateDesc(Desc, out string descErrMsg);
            DescErrMsg = descErrMsg;

        }







        //Places autocomplete code
        //TODO: Move it somewhere else?
        public const string GooglePlacesApiAutoCompletePath = "https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}&input={1}&components=country:au";
        public const string GooglePlacesApiKey = "AIzaSyBnDLNBhZVaiHtpwzHtVeSgJNSSyiTz9FE";

        private static HttpClient _httpClientInstance;
        public static HttpClient HttpClientInstance => _httpClientInstance ?? (_httpClientInstance = new HttpClient());

        private ObservableCollection<AddressInfo> _addresses;
        public ObservableCollection<AddressInfo> Addresses
        {
            get => _addresses ?? (_addresses = new ObservableCollection<AddressInfo>());
            set
            {
                if (_addresses != value)
                {
                    _addresses = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _addressText;
        public string AddressText
        {
            get => _addressText;
            set
            {
                if (_addressText != value)
                {
                    _addressText = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task GetPlacesPredictionsAsync()
        {

            // TODO: Add throttle logic, Google begins denying requests if too many are made in a short amount of time

            CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(GooglePlacesApiAutoCompletePath, GooglePlacesApiKey, WebUtility.UrlEncode(_addressText))))
            { //Be sure to UrlEncode the search term they enter

                using (HttpResponseMessage message = await HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string json = await message.Content.ReadAsStringAsync().ConfigureAwait(false);

                        PlacesLocationPredictions predictionList = await Task.Run(() => JsonConvert.DeserializeObject<PlacesLocationPredictions>(json)).ConfigureAwait(false);

                        if (predictionList.Status == "OK")
                        {

                            Addresses.Clear();

                            if (predictionList.Predictions.Count > 0)
                            {
                                foreach (Prediction prediction in predictionList.Predictions)
                                {
                                    Addresses.Add(new AddressInfo
                                    {
                                        Address = prediction.Description
                                    });
                                }
                            }
                        }
                        else
                        {
                            throw new Exception(predictionList.Status);
                        }
                    }
                }
            }
        }


    }
}


public class AddressInfo
{
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string Longitue { get; set; }
    public string Latitude { get; set; }
  
}


public class PlacesMatchedSubstring
{
    [Newtonsoft.Json.JsonProperty("length")]
    public int Length { get; set; }

    [Newtonsoft.Json.JsonProperty("offset")]
    public int Offset { get; set; }
}

public class PlacesTerm
{
    [Newtonsoft.Json.JsonProperty("offset")]
    public int Offset { get; set; }

    [Newtonsoft.Json.JsonProperty("value")]
    public string Value { get; set; }

}

public class Prediction
{
    [Newtonsoft.Json.JsonProperty("id")]
    public string Id { get; set; }

    [Newtonsoft.Json.JsonProperty("description")]
    public string Description { get; set; }

    [Newtonsoft.Json.JsonProperty("matched_substrings")]
    public List<PlacesMatchedSubstring> MatchedSubstrings { get; set; }

    [Newtonsoft.Json.JsonProperty("place_id")]
    public string PlaceId { get; set; }

    [Newtonsoft.Json.JsonProperty("reference")]
    public string Reference { get; set; }

    [Newtonsoft.Json.JsonProperty("terms")]
    public List<PlacesTerm> Terms { get; set; }

    [Newtonsoft.Json.JsonProperty("types")]
    public List<string> Types { get; set; }
}

public class PlacesLocationPredictions
{

    [Newtonsoft.Json.JsonProperty("predictions")]
    public List<Prediction> Predictions { get; set; }

    [Newtonsoft.Json.JsonProperty("status")]
    public string Status { get; set; }
}


