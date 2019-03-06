using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsonTechMobileApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EditOrder : ContentPage
	{
        Order order;

        public EditOrder(Order order)
        {
            InitializeComponent();          
            this.order = order;
            FillBoxes();
        }

        private void btnSave_Clicked(object sender, EventArgs e)
        {
            order.Name = txtName.Text;
            order.Contact = txtContact.Text;
            order.Date = pickerDate.Date;
            if (numOfDevices.SelectedIndex == 0)
            {
                if (devicePicker1.SelectedIndex > -1 || colourPicker1.SelectedIndex > -1)
                {
                    order.Device = devicePicker1.SelectedItem.ToString();
                    order.Colour = colourPicker1.SelectedItem.ToString();
                }
                else
                {
                    return;
                }
            }
            if (numOfDevices.SelectedIndex == 1)
            {
                if (devicePicker1.SelectedIndex > -1 || colourPicker1.SelectedIndex > -1 || devicePicker2.SelectedIndex > -1 || colourPicker2.SelectedIndex > -1)
                {
                    order.Device = devicePicker1.SelectedItem.ToString();
                    order.Colour = colourPicker1.SelectedItem.ToString();
                    order.Device += "," + devicePicker2.SelectedItem.ToString();
                    order.Colour += "," + colourPicker2.SelectedItem.ToString();
                }
                else
                {
                    return;
                }
            }
            if (numOfDevices.SelectedIndex == 2)
            {
                if (devicePicker1.SelectedIndex > -1 || colourPicker1.SelectedIndex > -1 || devicePicker2.SelectedIndex > -1 || colourPicker2.SelectedIndex > -1 || devicePicker3.SelectedIndex > -1 || colourPicker3.SelectedIndex > -1)
                {
                    order.Device = devicePicker1.SelectedItem.ToString();
                    order.Colour = colourPicker1.SelectedItem.ToString();
                    order.Device += "," + devicePicker2.SelectedItem.ToString();
                    order.Colour += "," + colourPicker2.SelectedItem.ToString();
                    order.Device += "," + devicePicker3.SelectedItem.ToString();
                    order.Colour += "," + colourPicker3.SelectedItem.ToString();
                }
                else
                {
                    return;
                }
            }
            order.ReasonForFix = txtReason.Text;
            order.Price = txtPrice.Text;
            order.GotPart = lstPart.SelectedItem.ToString();
            order.Cost = txtCost.Text;
            FileManager fileManager = new FileManager();
            fileManager.SaveEdit(order);
            DisplayAlert("Saved", "Message was successfully updated", "Ok");
            this.Navigation.PopAsync();
        }

        private void btnCancel_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private Picker devicePicker1;
        private Picker devicePicker2;
        private Picker devicePicker3;
        private Picker colourPicker1;
        private Picker colourPicker2;
        private Picker colourPicker3;

        private void numOfDevices_SelectedIndexChanged(object sender, EventArgs e)
        {
            DisplayDevicesForFill();
        }

        private void FillBoxes()
        {
            txtName.Text = order.Name;
            txtContact.Text = order.Contact;
            pickerDate.Date = order.Date;
            string[] split = order.Device.Split(',');
            numOfDevices.SelectedIndex = split.Length - 1;
            DisplayDevicesForFill();
            txtReason.Text = order.ReasonForFix;
            txtPrice.Text = order.Price;
            if (order.GotPart == "Yes")
            {
                lstPart.SelectedIndex = 1;
            }
            else
            {
                lstPart.SelectedIndex = 0;
            }
            txtCost.Text = order.Cost;
        }

        private void DisplayDevicesForFill()
        {
            List<string> lstDevice = new List<string>();
            lstDevice.Add("IPhone 5");
            lstDevice.Add("IPhone 5c");
            lstDevice.Add("IPhone 5s");
            lstDevice.Add("IPhone 5 SE");
            lstDevice.Add("IPhone 6");
            lstDevice.Add("IPhone 6 Plus");
            lstDevice.Add("IPhone 6s");
            lstDevice.Add("IPhone 6s Plus");
            lstDevice.Add("IPhone 7");
            lstDevice.Add("IPhone 7 Plus");
            lstDevice.Add("IPhone 8");
            lstDevice.Add("IPhone 8 Plus");
            lstDevice.Add("IPhone X");
            lstDevice.Add("IPhone XS");
            lstDevice.Add("IPhone XS Max");
            lstDevice.Add("IPhone XR");
            lstDevice.Add("IPad");
            lstDevice.Add("IPad 2");
            lstDevice.Add("IPad 3");
            lstDevice.Add("IPad 4");
            lstDevice.Add("IPad 5");
            lstDevice.Add("IPad 6");
            lstDevice.Add("IPad Mini");
            lstDevice.Add("IPad Mini 2");
            lstDevice.Add("IPad Mini 3");
            lstDevice.Add("IPad Air");

            List<string> lstColour = new List<string>();
            lstColour.Add("Black");
            lstColour.Add("White");
            lstColour.Add("Gold");
            lstColour.Add("Rose Gold");
            lstColour.Add("Silver");
            lstColour.Add("Space Grey");

            devicePicker1 = new Picker();
            devicePicker1.ItemsSource = lstDevice;
            devicePicker1.WidthRequest = 200;
            devicePicker2 = new Picker();
            devicePicker2.ItemsSource = lstDevice;
            devicePicker2.WidthRequest = 200;
            devicePicker3 = new Picker();
            devicePicker3.ItemsSource = lstDevice;
            devicePicker3.WidthRequest = 200;

            devicePicker1.TextColor = Color.LimeGreen;
            devicePicker1.BackgroundColor = Color.Transparent;
            devicePicker2.TextColor = Color.LimeGreen;
            devicePicker2.BackgroundColor = Color.Transparent;
            devicePicker3.TextColor = Color.LimeGreen;
            devicePicker3.BackgroundColor = Color.Transparent;

            colourPicker1 = new Picker();
            colourPicker1.ItemsSource = lstColour;
            colourPicker1.WidthRequest = 200;
            colourPicker2 = new Picker();
            colourPicker2.ItemsSource = lstColour;
            colourPicker2.WidthRequest = 200;
            colourPicker3 = new Picker();
            colourPicker3.ItemsSource = lstColour;
            colourPicker3.WidthRequest = 200;

            colourPicker1.TextColor = Color.LimeGreen;
            colourPicker1.BackgroundColor = Color.Transparent;
            colourPicker2.TextColor = Color.LimeGreen;
            colourPicker2.BackgroundColor = Color.Transparent;
            colourPicker3.TextColor = Color.LimeGreen;
            colourPicker3.BackgroundColor = Color.Transparent;

            devices1.Children.Clear();
            devices2.Children.Clear();
            devices3.Children.Clear();

            string[] devicesplit = order.Device.Split(',');
            string[] coloursplit = order.Colour.Split(',');

            if (numOfDevices.SelectedIndex == 0)
            {
                devices1.Children.Add(devicePicker1);
                devices1.Children.Add(colourPicker1);

                devicePicker1.SelectedIndex = GetDeviceIndex(lstDevice, devicesplit[0]);
                colourPicker1.SelectedIndex = GetColourIndex(lstColour, coloursplit[0]);
            }
            if (numOfDevices.SelectedIndex == 1)
            {
                devices1.Children.Add(devicePicker1);
                devices1.Children.Add(colourPicker1);
                devices2.Children.Add(devicePicker2);
                devices2.Children.Add(colourPicker2);

                devicePicker1.SelectedIndex = GetDeviceIndex(lstDevice, devicesplit[0]);
                colourPicker1.SelectedIndex = GetColourIndex(lstColour, coloursplit[0]);

                devicePicker2.SelectedIndex = GetDeviceIndex(lstDevice, devicesplit[1]);
                colourPicker2.SelectedIndex = GetColourIndex(lstColour, coloursplit[1]);
            }
            if (numOfDevices.SelectedIndex == 2)
            {
                devices1.Children.Add(devicePicker1);
                devices1.Children.Add(colourPicker1);
                devices2.Children.Add(devicePicker2);
                devices2.Children.Add(colourPicker2);
                devices3.Children.Add(devicePicker3);
                devices3.Children.Add(colourPicker3);

                devicePicker1.SelectedIndex = GetDeviceIndex(lstDevice, devicesplit[0]);
                colourPicker1.SelectedIndex = GetColourIndex(lstColour, coloursplit[0]);

                devicePicker2.SelectedIndex = GetDeviceIndex(lstDevice, devicesplit[1]);
                colourPicker2.SelectedIndex = GetColourIndex(lstColour, coloursplit[1]);

                devicePicker3.SelectedIndex = GetDeviceIndex(lstDevice, devicesplit[2]);
                colourPicker3.SelectedIndex = GetColourIndex(lstColour, coloursplit[2]);
            }
        }

        public int GetDeviceIndex(List<string> devices, string device)
        {
            for (int i = 0; i < devices.Count; i++)
            {
                if (device == devices[i])
                {
                    return i;
                }
            }
            return 0;
        }

        public int GetColourIndex(List<string> colours, string colour)
        {
            for (int i = 0; i < colours.Count; i++)
            {
                if (colour == colours[i])
                {
                    return i;
                }
            }
            return 0;
        }
    }
}