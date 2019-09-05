using System;
using System.ComponentModel;
using System.Windows.Input;
using Pastime.Models;
using Xamarin.Forms;

namespace Pastime.ViewModels
{
    public class CreateEventViewModel : INotifyPropertyChanged
    {
        private Event eventData;
        private int eventId;
        private string eventName;
        //TODO: sport probably won't stay a string
        private string eventSport;
        private string eventDesc;
        private string eventLocation;
        private DateTime startDate;

        public CreateEventViewModel()
        {
            SubmitCommand = new Command(PostEvent);
        }



        


        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand SubmitCommand { private set; get; }

        private void PostEvent(object obj)
        {
           
        }

    }
}
