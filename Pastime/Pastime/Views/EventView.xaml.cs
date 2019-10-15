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
        private EventViewModel viewModel;
        public EventView(Event e)
        {
            InitializeComponent();
            viewModel = new EventViewModel(Navigation, e);
            this.BindingContext = viewModel;
        }
        public EventViewModel ViewModel
        {
            get => viewModel;
        }
    }
}