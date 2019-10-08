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
    public partial class CreateEventViewModalName : ContentPage
    {
        private CreateEventViewModel vm;
        public CreateEventViewModalName()
        {
            InitializeComponent();
            vm = new CreateEventViewModel();
            vm.Navigation = Navigation;
            this.BindingContext = vm;
        }

       
    }
}