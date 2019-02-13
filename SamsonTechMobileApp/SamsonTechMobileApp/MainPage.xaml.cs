using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Fingerprint;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsonTechMobileApp
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
		public MainPage ()
		{
			InitializeComponent ();
            FingerPrint();
		}

        public async void FingerPrint()
        {
            var cancellationTocken = new System.Threading.CancellationToken();
            var auth = await CrossFingerprint.Current.AuthenticateAsync("Touch Finger Print Sensor", cancellationTocken);
            if (auth.Authenticated)
            {
                Progress.Show();
                Menu menu = new Menu();
                await Navigation.PushAsync(menu);
                Progress.Hide();
            }
        }

        private void btn0_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "0";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn1_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "1";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn2_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "2";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn3_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "3";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn4_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "4";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn5_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "5";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn6_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "6";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn7_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "7";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn8_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "8";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btn9_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            passCode += "9";
            txtPasscode.Text = passCode;
            CheckPassCodeCount();
        }
        private void btnBk_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;

            if (passCode != "")
            {
                char[] passcodeSplit = passCode.ToArray();
                passCode = "";

                if (passcodeSplit.Length > 1)
                {
                    for (int i = 0; i < passcodeSplit.Length - 1; i++)
                    {
                        passCode += passcodeSplit[i];
                    }
                }

                txtPasscode.Text = passCode;
                CheckPassCodeCount();
            }
        }
        private void btnSubmit_Clicked(object sender, EventArgs e)
        {
            string passCode = txtPasscode.Text;
            string correctPassCode = "000000";

            if (passCode == correctPassCode)
            {
                Menu menu = new Menu();
                this.Navigation.PushAsync(menu);
            }
            else
            {
                DisplayAlert("Incorrect","Incorrect Password","Ok");
                txtPasscode.Text = "";
            }
        }

        private void CheckPassCodeCount()
        {
            string passCode = txtPasscode.Text;
            if (passCode.Length >= 6)
            {
                string correctPassCode = "000000";

                if (passCode == correctPassCode)
                {
                    Menu menu = new Menu();
                    this.Navigation.PushAsync(menu);
                }
                btn0.IsEnabled = false;
                btn1.IsEnabled = false;
                btn2.IsEnabled = false;
                btn3.IsEnabled = false;
                btn4.IsEnabled = false;
                btn5.IsEnabled = false;
                btn6.IsEnabled = false;
                btn7.IsEnabled = false;
                btn8.IsEnabled = false;
                btn9.IsEnabled = false;
            }
            else
            {
                btn0.IsEnabled = true;
                btn1.IsEnabled = true;
                btn2.IsEnabled = true;
                btn3.IsEnabled = true;
                btn4.IsEnabled = true;
                btn5.IsEnabled = true;
                btn6.IsEnabled = true;
                btn7.IsEnabled = true;
                btn8.IsEnabled = true;
                btn9.IsEnabled = true;
            }
        }

        private void BtnFingerPrint_Clicked(object sender, EventArgs e)
        {
            FingerPrint();
        }
    }
}