using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CrossFAB
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            FB.OnClickCommand = new Command(async () =>
            {
                await DisplayAlert("Share", "Shared on Facebook!", "OK!");
            });
            TW.OnClickCommand = new Command(async () =>
            {
                await DisplayAlert("Share", "Shared on Twitter!", "OK!");
            });
            TB.OnClickCommand = new Command(async () =>
            {
                await DisplayAlert("Share", "Shared on Tumblr!", "OK!");
            });
        }
    }
}
