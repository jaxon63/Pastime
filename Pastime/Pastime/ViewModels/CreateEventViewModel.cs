using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using GooglePlacesApi;
using GooglePlacesApi.Models;
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

        private Activity activity;

        private readonly GooglePlacesApiService service;

        public CreateEventViewModel()
        {
            SubmitCommand = new Command(PostEvent);

            var settings = GoogleApiSettings.Builder.WithApiKey(Environment.GetEnvironmentVariable("AIzaSyDymVB9no6IHh7boHfCSzDoV-jU9eeeQTk")).Build();
            service = new GooglePlacesApiService(settings);
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

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { private set; get; }
        public ICommand DoSearchCommand => new Command(async () => await DoSearchAsync().ConfigureAwait(false), () => CanSearch);

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

            if(results != null && results.Status.Equals("OK"))
            {
                Results = results.Items;
                OnPropertyChanged("HasResults");
            }
        }

        

        void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void PostEvent(object obj)
        {
            //Create Event Model which is responsible for validating the input
            //The view is then updated to reflect any errors in the input
            //TODO: the error messages aren't meaningful enough yet
            EventModel em = new EventModel(name, activity, desc);
            InvalidName = !em.validateName();
            InvalidDesc = !em.validateDesc();

            if(!InvalidName && !invalidDesc)
            {
                

            }

        }

    }
}
