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
            if(oneDrive.SignedIn)
            {
                btnSignIn.IsEnabled = false;
            }
            if (!oneDrive.SignedIn)
            {
                btnSignOut.IsEnabled = false;
            }
        }

        private void btnSignIn_Clicked(object sender, EventArgs e)
        {
            oneDrive.SignIn();
            
        }

        private void btnSignOut_Clicked(object sender, EventArgs e)
        {
            oneDrive.SignOut();
            
        }
    }
}