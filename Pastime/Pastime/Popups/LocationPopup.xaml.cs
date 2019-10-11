using Pastime.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pastime.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LocationPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LocationPopup()
        {
            InitializeComponent();
        }


        private async void OnTextChanged(object sender, EventArgs e)
        {
            await ((CreateEventViewModel)this.BindingContext).GetPlacesPredictionsAsync();
        }

        private void CreateEventNavAsync(object sender, SelectedItemChangedEventArgs e)
        {
            PopupNavigation.Instance.PopAllAsync();
        }
    }
}