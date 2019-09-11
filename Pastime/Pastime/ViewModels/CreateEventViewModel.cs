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

        private string addressText;

        private string displayAddress = string.Empty;


        private bool displayList;

        private List<Activity> activities;

        private string locErrMsg = string.Empty;
        private bool invalidLoc;


        private AddressInfo selectedAddress;

        private readonly EventModel em;

        public CreateEventViewModel()
        {
            SubmitCommand = new Command(PostEvent);

            em = new EventModel();
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

        public List<Activity> Activities
        {
            get => activities;
            set
            {
                if (activities == value)
                    return;

                activities = value;
                OnPropertyChanged();
            }
        }
     
        public string LocErrMsg
        {
            get => locErrMsg;
            set
            {
                if (locErrMsg == value)
                    return;

                locErrMsg = value;
                OnPropertyChanged();
            }
        }

        public AddressInfo SelectedAddress
        {
            get => selectedAddress;
            set
            {
                if (selectedAddress == value)
                    return;

                selectedAddress = value;

                DisplayAddress = selectedAddress.Address;
                OnPropertyChanged();
            }
        }

        public bool InvalidLoc
        {
            get => invalidLoc;
            set
            {
                if (invalidLoc == value)
                    return;

                invalidLoc = value;
                OnPropertyChanged();
            }
        }

        public string AddressText
        {
            get => addressText;
            set
            {
                if (addressText != value)
                {
                    addressText = value;
                    OnPropertyChanged();
                }
            }
        }

        //Label for displaying the currently selected address 
        //When set it hides the list from the view and resets the search text
        public string DisplayAddress
        {
            get => displayAddress;
            set
            {
                if (displayAddress == value)
                    return;

                displayAddress = value;

                AddressText = string.Empty;
                DisplayList = false;
                
                OnPropertyChanged();
            }
        }

        public bool DisplayList
        {
            get => displayList;
            set
            {
                if (displayList == value)
                    return;

                displayList = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { private set; get; }

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

            InvalidLoc = !em.validateLocationString(AddressText, out string locErrMsg);
            LocErrMsg = locErrMsg;

        }


        //Code for location autocomplete
        public const string GooglePlacesApiAutoCompletePath = "https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}&input={1}&components=country:au";

        //TODO: store the key on the server
        public const string GooglePlacesApiKey = "AIzaSyC88PtFFRYXHEaTfvTjiXy-KrvZhAvOnb4";

        private static HttpClient httpClientInstance;
        public static HttpClient HttpClientInstance => httpClientInstance ?? (httpClientInstance = new HttpClient());

        private ObservableCollection<AddressInfo> addresses;
        public ObservableCollection<AddressInfo> Addresses
        {
            get => addresses ?? (addresses = new ObservableCollection<AddressInfo>());
            set
            {
                if (addresses != value)
                {
                    addresses = value;
                    OnPropertyChanged();
                }
            }
        }

        public async Task GetPlacesPredictionsAsync()
        {

            // TODO: Add throttle logic, Google begins denying requests if too many are made in a short amount of time

            CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;

            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(GooglePlacesApiAutoCompletePath, GooglePlacesApiKey, WebUtility.UrlEncode(addressText))))
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
                                DisplayList = true;
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








