using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsonTechMobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        AuthenticationResult authResult = null;
        IEnumerable<IAccount> accounts;

        OneDrive oneDrive;

        public Settings(OneDrive oneDrive)
        {
            InitializeComponent();
            this.oneDrive = oneDrive;
            this.oneDrive.settings = this;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (oneDrive.SignedIn)
            {
                txtStatus.Text = "Signed In";
                btnSignIn.IsEnabled = false;
            }
            else
            {
                txtStatus.Text = "Signed Out";
                btnSignOut.IsEnabled = false;
            }
        }

        private void btnSignIn_Clicked(object sender, EventArgs e)
        {
            oneDrive.SignIn();
            CheckSignedIn();
        }

        private void btnSignOut_Clicked(object sender, EventArgs e)
        {
            oneDrive.SignOut();
            CheckSignedIn();
        }

        private async void CheckSignedIn()
        {
            await Task.Delay(2000);
            if (oneDrive.SignedIn)
            {
                txtStatus.Text = "Signed In";
                btnSignOut.IsEnabled = true;
                btnSignIn.IsEnabled = false;
            }
            else
            {
                txtStatus.Text = "Signed Out";
                btnSignIn.IsEnabled = true;
                btnSignOut.IsEnabled = false;
            }
        }
    }
}