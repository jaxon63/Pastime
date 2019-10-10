using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using GooglePlacesApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pastime.Models;
using Pastime.Popups;
using Pastime.Views;
using Pastime.Views.CreateEventViewModal;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class CreateEventViewModel : INotifyPropertyChanged
    {

        //Fields that require validation also have an error message and a boolean
        private string name = string.Empty;
        private string nameErrMsg = string.Empty;
        private bool invalidName;

        private string desc = string.Empty;
        private string descErrMsg = string.Empty;
        private bool invalidDesc;

        //Address is the text used as a search string for the location search
        private string addressText;

        //Display address is bound to the seleced address
        private string displayAddress = string.Empty;
        private bool displayList;

        //The selected address stores the address string as well as the lat and long.
        //This data can be used to create a Xamarin Essentials Location object
        private AddressInfo selectedAddress;

        //TODO: activities need to be retrieved from the database
        private List<Activity> activities;

        private string locErrMsg = string.Empty;
        private bool invalidLoc;

        private Activity selectedActivity;
        private bool invalidSport;
        private string sportErrMsg;

        //The default date/time is now
        private DateTime eventDate = DateTime.Now.Date;
        private string eventDateErrMsg;
        private bool invalidEventDate;

        //Number of guests is 1 by default and can't be lower than 1 or higher than 10
        private int numberOfGuests = 1;

        //TODO: maybe some validation to make sure only alphanumeric is used
        //User added equipment gets stored in the ObservableCollection
        private ObservableCollection<string> equipmentList = new ObservableCollection<string>();
        private string equipment = string.Empty;
        //equipmentListToString displays the list as a single string separated by commas
        private string equipmentListToString = string.Empty;

        private Event finalEvent;


        //The default start time is set to one hour from the current time
        private TimeSpan startTime = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 0, 0));

        //End time is automatically set to 2 hours from the current time
        //TODO: Make it default to 1 hour from the start time instead of the current time
        private TimeSpan endTime = DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 0, 0));

        private static HttpClient httpClientInstance;
        public static HttpClient HttpClientInstance => httpClientInstance ?? (httpClientInstance = new HttpClient());

        private ObservableCollection<AddressInfo> addresses;

        public const string GooglePlacesApiAutoCompletePath = "https://maps.googleapis.com/maps/api/place/autocomplete/json?key={0}&input={1}&components=country:au";
        public const string GooglePlacesDetailPath = "https://maps.googleapis.com/maps/api/place/details/json?place_id={0}&fields=geometry&key={1}";

        //TODO: store the key on the server
        public const string GooglePlacesApiKey = "";

        //Event model used to handle all the business logic regarding events
        private readonly EventModel model;



        public CreateEventViewModel()
        {
            //SubmitCommand = new Command(PostEvent);
            AddEquipCommand = new Command(AddEquipmentToList);
            ClearEquipCommand = new Command(ClearEquipmentList);

            //Modal commands
            NameCommand = new Command(SubmitName);
            SportCommand = new Command(SubmitSport);
            DescCommand = new Command(SubmitDesc);
            DateCommand = new Command(SubmitDate);
            GuestsCommand = new Command(SubmitGuests);
            EquipmentCommand = new Command(SubmitEquipment);
            LocationCommand = new Command(SubmitLocation);
            SubmitEventCommand = new Command(async () => await SubmitEvent());
            GoBackCommand = new Command(NavigateGoBackAsync);
            CancelCommand = new Command(BackToHomeScreen);

            //Instantiate the EventModel
            model = new EventModel();

            activities = model.GetActivities();

            //Instantiate the minimium date the user can select to the current time
            DateTime MinimumDate = DateTime.Now;
        }

        //TODO: the app crashes when setting the time near midnight
        //Need to handle the logic here
        private void InitializeTimes()
        {

        }

        public INavigation Navigation
        {
            get;
            set;
        }


        //Getters and setters
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


        public Xamarin.Essentials.Location Location
        {
            get
            {
                Xamarin.Essentials.Location loc = new Xamarin.Essentials.Location(double.Parse(selectedAddress.Latitude), double.Parse(selectedAddress.Longitue));
                return loc;
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

        public Activity SelectedActivity
        {
            get => selectedActivity;
            set
            {
                if (selectedActivity == value)
                    return;
                selectedActivity = value;
                OnPropertyChanged();
            }
        }

        public bool InvalidSport
        {
            get => invalidSport;
            set
            {
                if (invalidSport == value)
                    return;

                invalidSport = value;
                OnPropertyChanged();
            }
        }

        public string SportErrMsg
        {
            get => sportErrMsg;
            set
            {
                if (sportErrMsg == value)
                    return;

                sportErrMsg = value;
                OnPropertyChanged();
            }
        }

        public DateTime EventDate
        {
            get => eventDate + startTime;

            set
            {
                if (eventDate == value)
                    return;

                eventDate = value;
                OnPropertyChanged();

            }
        }

        public DateTime EventEndDate
        {
            get => eventDate + endTime;

        }

        public TimeSpan StartTime
        {
            get => startTime;
            set
            {

                if (startTime == value)
                    return;

                startTime = value;
                OnPropertyChanged();
            }
        }

        public TimeSpan EndTime
        {
            get => endTime;
            set
            {
                if (endTime == value)
                    return;

                endTime = value;
                OnPropertyChanged();
            }
        }

        public string EventDateErrMsg
        {
            get => eventDateErrMsg;
            set
            {
                if (eventDateErrMsg == value)
                    return;

                eventDateErrMsg = value;
                OnPropertyChanged();
            }
        }

        public bool InvalidEventDate
        {
            get => invalidEventDate;
            set
            {
                if (invalidEventDate == value)
                    return;

                invalidEventDate = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<string> EquipmentList
        {
            get => equipmentList;
            set
            {
                if (equipmentList == value)
                    return;

                equipmentList = value;
                OnPropertyChanged();
            }
        }

        public string Equipment
        {
            get => equipment;
            set
            {
                if (equipment == value)
                    return;

                equipment = value;
                OnPropertyChanged();
            }
        }

        public string EquipmentListToString
        {
            get => equipmentListToString;

            set
            {
                if (equipmentListToString == value)
                    return;

                equipmentListToString = value;
                OnPropertyChanged();
            }
        }


        public int NumberOfGuests
        {
            get => numberOfGuests;
            set
            {
                if (numberOfGuests == value)
                    return;

                numberOfGuests = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(DisplayNumberOfGuests));
            }
        }

        public string DisplayNumberOfGuests
        {
            get
            {
                if (NumberOfGuests > 1)
                {
                    return $"I would like  invite {NumberOfGuests} people";

                }
                else
                {
                    return $"I would like  invite {NumberOfGuests} person";

                }
            }
        }

        public string DisplayEventDateString
        {

            get
            {
                string monthString = string.Empty;
                //I've used this switch statement instead of a formatter because the output would always be "MMMM" instead of the month string
                switch (EventDate.Month)
                {
                    case (1):
                        monthString = "January";
                        break;
                    case (2):
                        monthString = "February";
                        break;
                    case (3):
                        monthString = "March";
                        break;
                    case (4):
                        monthString = "April";
                        break;
                    case (5):
                        monthString = "May";
                        break;
                    case (6):
                        monthString = "June";
                        break;
                    case (7):
                        monthString = "July";
                        break;
                    case (8):
                        monthString = "August";
                        break;
                    case (9):
                        monthString = "September";
                        break;
                    case (10):
                        monthString = "October";
                        break;
                    case (11):
                        monthString = "November";
                        break;
                    case (12):
                        monthString = "December";
                        break;
                }
                if (EventDate.Day == 1 || EventDate.Day == 21 || EventDate.Day == 31)
                {
                    return $"{EventDate.DayOfWeek} the {EventDate.Day}st of {monthString}";
                }
                else if (EventDate.Day == 2 || EventDate.Day == 22)
                {
                    return $"{EventDate.DayOfWeek} the {EventDate.Day}nd of {monthString}";

                }
                else
                {
                    return $"{EventDate.DayOfWeek} the {EventDate.Day}th of {monthString}";
                }

            }
        }

        public string DisplayEventStartTimeString
        {
            get
            {
                string result = string.Empty;
                result = EventDate.ToString("h:mm tt", CultureInfo.InvariantCulture);

                return result;
            }
        }
        public string DisplayEventEndTimeString
        {
            get
            {
                string result = string.Empty;
                result = EventEndDate.ToString("h:mm tt", CultureInfo.InvariantCulture);

                return result;
            }
        }

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


        public Event FinalEvent
        {
            get => finalEvent;
            set
            {
                if (finalEvent == value)
                    return;
                finalEvent = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { private set; get; }
        public ICommand AddEquipCommand { private set; get; }
        public ICommand ClearEquipCommand { private set; get; }
        public ICommand LocationCommand { private set; get; }


        //Modal commands
        public ICommand NameCommand { private set; get; }
        public ICommand SportCommand { private set; get; }
        public ICommand DescCommand { private set; get; }
        public ICommand DateCommand { private set; get; }
        public ICommand GuestsCommand { private set; get; }
        public ICommand EquipmentCommand { private set; get; }
        public ICommand SubmitEventCommand { private set; get; }
        public ICommand GoBackCommand { private set; get; }
        public ICommand CancelCommand { private set; get; }


        //Modal command functions

        private async void NavigateGoBackAsync()
        {
            await Navigation.PopModalAsync();
        }

        private void BackToHomeScreen()
        {
            Application.Current.MainPage = new MasterView();
        }
        private void SubmitName()
        {
            InvalidName = !model.ValidateName(Name, out string nameErrMsg);
            NameErrMsg = nameErrMsg;

            if (!InvalidName)
            {
                var nextPage = new CreateEventViewModalSport();
                nextPage.BindingContext = this;
                Navigation.PushModalAsync(nextPage, true);
            }
        }

        private void SubmitSport()
        {
            if (selectedActivity == null)
            {
                InvalidSport = true;
                SportErrMsg = "Please select a sport";
            }
            else
            {
                InvalidSport = false;
            }
            if (!InvalidSport)
            {
                var nextPage = new CreateEventViewModalDesc();
                nextPage.BindingContext = this;
                Navigation.PushModalAsync(nextPage, true);
            }
        }

        private void SubmitDesc()
        {
            InvalidDesc = !model.ValidateDesc(Desc, out string descErrMsg);
            DescErrMsg = descErrMsg;

            if (!InvalidDesc)
            {
                var nextPage = new CreateEventViewModalDate();
                nextPage.BindingContext = this;
                Navigation.PushModalAsync(nextPage, true);
            }
        }

        private void SubmitDate()
        {

            InvalidEventDate = !model.ValidateEventDate(EventDate, EndTime, out string eventDateErrMsg);
            EventDateErrMsg = eventDateErrMsg;

            if (!InvalidEventDate)
            {
                var nextPage = new CreateEventViewModalGuests();
                nextPage.BindingContext = this;
                Navigation.PushModalAsync(nextPage, true);
                Console.WriteLine("hello: " + EventDate + " " + EndTime);
            }

        }

        private void SubmitGuests()
        {
            var nextPage = new CreateEventViewModalEquipment();
            nextPage.BindingContext = this;
            Navigation.PushModalAsync(nextPage, true);
        }

        private void SubmitEquipment()
        {
            var nextPage = new CreateEventViewModalLocation();
            nextPage.BindingContext = this;
            Navigation.PushModalAsync(nextPage, true);
        }

        private async void SubmitLocation()
        {
            InvalidLoc = !model.ValidateLocationString(DisplayAddress, out string locErrMsg);
            LocErrMsg = locErrMsg;
            Xamarin.Essentials.Location loc = null;


            if (!string.IsNullOrWhiteSpace(displayAddress))
            {
                if (!string.IsNullOrWhiteSpace(selectedAddress.Latitude) && !string.IsNullOrWhiteSpace(selectedAddress.Longitue))
                {
                    loc = new Xamarin.Essentials.Location(Double.Parse(selectedAddress.Latitude), Double.Parse(selectedAddress.Longitue));
                    var nextPage = new CreateEventViewModalConfirm();
                    nextPage.BindingContext = this;
                    await Navigation.PushModalAsync(nextPage);
                }
                else
                    LocErrMsg = "Location can not be empty";
            }
        }

        private async Task SubmitEvent()
        {
            if (!invalidName && !invalidDesc && !invalidEventDate && !invalidLoc && !invalidSport)
            {
                Event newEvent = model.CreateEvent(name, selectedActivity, equipmentList, Location, numberOfGuests, desc, EventDate, EventEndDate);
                await  newEvent.getLocationLocality();
                this.finalEvent = newEvent;
                //Handle this differently when the database is involved
                //Probably messaging center to tell the main page to update to display the new event
                Xamarin.Forms.Application.Current.MainPage = new MasterView();
            }
            else
            {
                //TODO: Handle this in the UI
                Console.WriteLine("Event could not be created");
            }
        }

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        //TODO: Maybe delete
        /*
        private void PostEvent(object obj)
        {
            //Calls Event Model which is responsible for validating the input
            //The view is then updated to reflect any errors in the input

            //Assign result of validation to public properties, which triggers the display of the error messages in the UI
            //The value of the error message is generated in EventModel, depening on the validation (too long, too short, empty ect.)
            InvalidName = !model.ValidateName(Name, out string nameErrMsg);
            NameErrMsg = nameErrMsg;

            InvalidDesc = !model.ValidateDesc(Desc, out string descErrMsg);
            DescErrMsg = descErrMsg;

            InvalidLoc = !model.ValidateLocationString(DisplayAddress, out string locErrMsg);
            LocErrMsg = locErrMsg;
            Xamarin.Essentials.Location loc = null;


            if (!string.IsNullOrWhiteSpace(displayAddress))
            {
                if (!string.IsNullOrWhiteSpace(selectedAddress.Latitude) && !string.IsNullOrWhiteSpace(selectedAddress.Longitue))
                {
                    loc = new Xamarin.Essentials.Location(Double.Parse(selectedAddress.Latitude), Double.Parse(selectedAddress.Longitue));

                }
                else
                    LocErrMsg = "Location can not be empty";

            }

            if (selectedActivity == null)
            {
                InvalidSport = true;
                SportErrMsg = "Please select an activity";
            }
            else
            {
                InvalidSport = false;
            }

            DateTime combinedDate = EventDate + StartTime;
            InvalidEventDate = !model.ValidateEventDate(combinedDate, EndTime, out string eventDateErrMsg);
            EventDateErrMsg = eventDateErrMsg;


            //Create event if all data validates
            //TODO: 
            if (!invalidName && !invalidDesc && !invalidLoc && loc != null && !invalidSport && !invalidEventDate)
            {
                Event newEvent = model.CreateEvent(name, selectedActivity, equipmentList, loc, numberOfGuests, desc, combinedDate, EndTime);
            }
            else
                Console.WriteLine("Failed");

        } */

        //Add equipment to the list of equipment and clear the input
        //Also joins the list into a string to display to the user
        private void AddEquipmentToList()
        {
            if (!string.IsNullOrWhiteSpace(Equipment))
            {
                EquipmentList.Add(Equipment);
                EquipmentListToString = String.Join(", ", EquipmentList);
                Equipment = string.Empty;
            }

        }

        //Clears the lears
        //TODO: Delete individual items
        private void ClearEquipmentList()
        {
            if (EquipmentList.Count > 0)
            {
                EquipmentList.Clear();
                EquipmentListToString = string.Empty;
            }
        }

        public async Task<string> GetLocationLocality(double latitude, double longitude)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                Placemark placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    return placemark.Locality;
                }
                else
                {
                    return "Unknown Location";
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                throw fnsEx;
            }
            catch (Exception ex)
            {
                throw ex;
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
                                    AddressInfo address = new AddressInfo
                                    {
                                        Address = prediction.Description,
                                        Id = prediction.PlaceId
                                    };
                                    //Gets the lat and long for each address
                                    await GetPlaceDetails(address);
                                    address.City = await GetLocationLocality(double.Parse(address.Latitude), double.Parse(address.Longitue));

                                    Addresses.Add(address);


                                    /*
                                    Addresses.Add(new AddressInfo
                                    {
                                        Address = prediction.Description,
                                        Id = prediction.PlaceId,
                                        
                                    });
                                    */

                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("No results found");
                            //throw new Exception(predictionList.Status);
                        }
                    }
                }
            }
        }

        public async Task GetPlaceDetails(AddressInfo address)
        {
            //Get the addres details
            //Store the lat and long in the Addresses List
            CancellationToken cancellationToken = new CancellationTokenSource(TimeSpan.FromMinutes(2)).Token;


            using (HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, string.Format(GooglePlacesDetailPath, address.Id, GooglePlacesApiKey)))
            {
                using (HttpResponseMessage message = await HttpClientInstance.SendAsync(request, HttpCompletionOption.ResponseContentRead, cancellationToken).ConfigureAwait(false))
                {
                    if (message.IsSuccessStatusCode)
                    {
                        string data = await message.Content.ReadAsStringAsync().ConfigureAwait(false);
                        var json = JObject.Parse(data);
                        address.Latitude = json["result"]["geometry"]["location"]["lat"].ToString();
                        address.Longitue = json["result"]["geometry"]["location"]["lng"].ToString();
                    }

                }
            }

        }
    }
}








