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
                Menu menu = new Menu() { Title = "Menu"};
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

        private async void CheckPassCodeCount()
        {
            string passCode = txtPasscode.Text;
            if (passCode.Length == 4)
            {
                string correctPassCode = "0000";

                if (passCode == correctPassCode)
                {
                    ChangeButtonsIsEnabled(false);
                    await Task.Delay(50);                    
                    txtPasscode.Text = "";
                    Menu menu = new Menu() { Title = "Menu" };
                    await Navigation.PushAsync(menu);
                    ChangeButtonsIsEnabled(true);
                }
                else
                {
                    ChangeButtonsIsEnabled(false);
                    Shake();
                    await Task.Delay(500);
                    ChangeButtonsIsEnabled(true);
                    txtPasscode.Text = "";
                }

            }            
        }

        private void ChangeButtonsIsEnabled(bool change)
        {
            btn0.IsEnabled = change;
            btn1.IsEnabled = change;
            btn2.IsEnabled = change;
            btn3.IsEnabled = change;
            btn4.IsEnabled = change;
            btn5.IsEnabled = change;
            btn6.IsEnabled = change;
            btn7.IsEnabled = change;
            btn8.IsEnabled = change;
            btn9.IsEnabled = change;
        }

        async void Shake()
        {
            uint timeout = 50;
            await line.TranslateTo(-15, 0, timeout);
            await line.TranslateTo(15, 0, timeout);
            await line.TranslateTo(-10, 0, timeout);
            await line.TranslateTo(10, 0, timeout);
            await line.TranslateTo(-5, 0, timeout);
            await line.TranslateTo(5, 0, timeout);
            line.TranslationX = 0;
        }


        private void BtnFingerPrint_Clicked(object sender, EventArgs e)
        {
            FingerPrint();
        }
    }
}