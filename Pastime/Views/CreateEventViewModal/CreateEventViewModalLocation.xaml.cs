using Pastime.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime.Views.CreateEventViewModal
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEventViewModalLocation : ContentPage
    {
        public CreateEventViewModalLocation()
        {
            InitializeComponent();
        }

        private async void OnTextChanged(object sender, EventArgs e)
        {
            //Casts the binding context as CreateEventViewModel because it is set 
            //when this page is instantiated
            await ((CreateEventViewModel)BindingContext).GetPlacesPredictionsAsync();
        }

    }
}