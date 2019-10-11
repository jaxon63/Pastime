using Pastime.Models;
using Pastime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventView : ContentPage
    {
        public EventView(string eventId)
        {
            InitializeComponent();
            this.BindingContext = new EventViewModel(Navigation, eventId);
        }
    }
}