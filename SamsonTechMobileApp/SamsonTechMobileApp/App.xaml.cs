using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace SamsonTechMobileApp
{
    public partial class App : Application
    {
        public static PublicClientApplication PCA = null;

        /// <summary>
        /// The ClientID is the Application ID found in the portal (https://apps.dev.microsoft.com). 
        /// You can use the below id however if you create an app of your own you should replace the value here.
        /// </summary>
        public static string ClientID = "bf8de7e6-5b16-48f9-ba5e-c12c2e89839c";

        public static string[] Scopes = { "User.Read", "Files.ReadWrite", "Files.ReadWrite.Selected" };
        public static string Username = string.Empty;

        public static UIParent UiParent { get; set; }
        public static GraphServiceClient graphClient = null;

        public App()
        {
            InitializeComponent();
            PCA = new PublicClientApplication(ClientID)
            {
                RedirectUri = $"msal{App.ClientID}://auth",
            };

            DependencyService.Register<IProgressInterface>();
            MainPage = new NavigationPage(new MainPage() { Title = "Login"}) { BarBackgroundColor = Color.LimeGreen, BarTextColor = Color.Black};
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
