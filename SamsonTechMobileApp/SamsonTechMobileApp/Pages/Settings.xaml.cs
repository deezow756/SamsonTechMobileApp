using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsonTechMobileApp.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Settings : ContentPage
    {
        CloudStorageManager storageManager;

        public Settings(CloudStorageManager storageManager)
        {
            InitializeComponent();
            this.storageManager = storageManager;
            for (int i = 0; i < storageManager.clouds.Count; i++)
            {
                storageManager.clouds[i].settings = this;
            }            
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            CheckActive();
        }

        private void CheckActive()
        {
            if(storageManager.clouds.Count != 0)
            {
                for (int i = 0; i < storageManager.clouds.Count; i++)
                {
                    if(storageManager.clouds[i].GetType() == typeof(OneDrive))
                    {
                        txtOneDriveStatus.Text = "Active";
                        txtOneDriveStatus.TextColor = Color.LimeGreen;
                        btnOneDrive.Text = "Deactivate";
                        storageManager.clouds[i].Signin();
                    }
                    else if (storageManager.clouds[i].GetType() == typeof(FirebaseDatabaseHelper))
                    {
                        txtFireBaseStatus.Text = "Active";
                        txtFireBaseStatus.TextColor = Color.LimeGreen;
                        btnFireBase.Text = "Deactivate";
                        storageManager.clouds[i].Signin();
                    }
                }

                string temp = txtOneDriveStatus.Text;
                if(string.IsNullOrEmpty(temp))
                {
                    txtOneDriveStatus.Text = "Inactive";
                    txtOneDriveStatus.TextColor = Color.Red;
                    btnOneDrive.Text = "Activate";
                }

                temp = txtFireBaseStatus.Text;
                if(string.IsNullOrEmpty(temp))
                {
                    txtFireBaseStatus.Text = "Inactive";
                    txtFireBaseStatus.TextColor = Color.Red;
                    btnFireBase.Text = "Activate";
                }
            }
            else
            {
                txtOneDriveStatus.Text = "Inactive";
                txtOneDriveStatus.TextColor = Color.Red;
                btnOneDrive.Text = "Activate";

                txtFireBaseStatus.Text = "Inactive";
                txtFireBaseStatus.TextColor = Color.Red;
                btnFireBase.Text = "Activate";
            }
        }

        private void btnFireBase_Clicked(object sender, EventArgs e)
        {
            string temp = txtFireBaseStatus.Text;
            if(temp == "Active")
            {
                for (int i = 0; i < storageManager.clouds.Count; i++)
                {
                    if (storageManager.clouds[i].GetType() == typeof(FirebaseDatabaseHelper))
                    {
                        storageManager.clouds[i].Signout();
                    }
                }
                storageManager.UpdateUsage("firebase", false);
                txtFireBaseStatus.Text = "Inactive";
                txtFireBaseStatus.TextColor = Color.Red;
                btnFireBase.Text = "Activate";                
            }
            else
            {
                storageManager.UpdateUsage("firebase", true);
                txtFireBaseStatus.Text = "Active";
                txtFireBaseStatus.TextColor = Color.LimeGreen;
                btnFireBase.Text = "Deactivate";
                for (int i = 0; i < storageManager.clouds.Count; i++)
                {
                    if (storageManager.clouds[i].GetType() == typeof(FirebaseDatabaseHelper))
                    {
                        storageManager.clouds[i].Signin();
                    }
                }
            }
        }

        private void btnOneDrive_Clicked(object sender, EventArgs e)
        {
            string temp = txtOneDriveStatus.Text;
            if(temp == "Active")
            {
                for (int i = 0; i < storageManager.clouds.Count; i++)
                {
                    if (storageManager.clouds[i].GetType() == typeof(OneDrive))
                    {
                        storageManager.clouds[i].Signout();
                    }
                }
                storageManager.UpdateUsage("onedrive", false);
                txtOneDriveStatus.Text = "Inactive";
                txtOneDriveStatus.TextColor = Color.Red;
                btnOneDrive.Text = "Activate";
            }
            else
            {
                storageManager.UpdateUsage("onedrive", true);
                txtOneDriveStatus.Text = "Active";
                txtOneDriveStatus.TextColor = Color.LimeGreen;
                btnOneDrive.Text = "Deactivate";

                for (int i = 0; i < storageManager.clouds.Count; i++)
                {
                    if (storageManager.clouds[i].GetType() == typeof(OneDrive))
                    {
                        storageManager.clouds[i].Signin();
                    }
                }
            }
        }
    }
}