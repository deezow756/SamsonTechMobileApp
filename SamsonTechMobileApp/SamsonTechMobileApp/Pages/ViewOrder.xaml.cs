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
	public partial class ViewOrder : ContentPage
	{
        private Order order;

        public ViewOrder(Order order)
        {
            InitializeComponent();
            this.order = order;
            FillBoxes();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            FileManager fileManager = new FileManager();
            order = fileManager.RefreshOrder(order);
            FillBoxes();
        }

        private void btnDelete_Clicked(object sender, EventArgs e)
        {
            ShowDeleteDialog();
        }

        private async void ShowDeleteDialog()
        {
            var result = await DisplayAlert("Warning!!", "Are you sure you want to delete this order?", "Yes", "No");

            if (result)
            {
                FileManager fileManager = new FileManager();
                fileManager.DeleteOrder(order);
                await DisplayAlert("Deleted", "Successfully Deleted Order", "Ok");
                await this.Navigation.PopAsync();
            }
        }

        private void btnComplete_Clicked(object sender, EventArgs e)
        {
            order.Completed = "Yes";
            txtCompleted.Text = order.Completed;
            FileManager fileManager = new FileManager();
            fileManager.SaveEdit(order);
            this.Navigation.PopAsync();
        }

        private void btnEdit_Clicked(object sender, EventArgs e)
        {
            EditOrder editOrder = new EditOrder(order);
            this.Navigation.PushAsync(editOrder);
        }

        private void FillBoxes()
        {
            txtName.Text = order.Name;
            txtContact.Text = order.Contact;
            txtDate.Text = order.Date.Day.ToString() + "/" + order.Date.Month.ToString() + "/" + order.Date.Year.ToString();
            if (order.Colour != null)
            {
                string[] split = order.Device.Split(',');
                string[] csplit = order.Colour.Split(',');
                deviceLabels.Children.Clear();
                if (split.Length == 1)
                {
                    Label label1 = new Label();
                    label1.TextColor = Color.LimeGreen;
                    label1.Text = split[0] + " " + csplit[0];

                    deviceLabels.Children.Add(label1);
                }
                if (split.Length == 2)
                {
                    Label label1 = new Label();
                    label1.Text = split[0] + " " + csplit[0];
                    Label label2 = new Label();
                    label2.Text = split[1] + " " + csplit[1];

                    label1.TextColor = Color.LimeGreen;
                    label2.TextColor = Color.LimeGreen;

                    deviceLabels.Children.Add(label1);
                    deviceLabels.Children.Add(label2);
                }
                if (split.Length == 3)
                {
                    Label label1 = new Label();
                    label1.Text = split[0] + " " + csplit[0];
                    Label label2 = new Label();
                    label2.Text = split[1] + " " + csplit[1];
                    Label label3 = new Label();
                    label3.Text = split[2] + " " + csplit[2];

                    label1.TextColor = Color.LimeGreen;
                    label2.TextColor = Color.LimeGreen;
                    label3.TextColor = Color.LimeGreen;

                    deviceLabels.Children.Add(label1);
                    deviceLabels.Children.Add(label2);
                    deviceLabels.Children.Add(label3);
                }
            }
            txtReason.Text = order.ReasonForFix;
            txtPrice.Text = order.Price;
            txtCost.Text = order.Cost;
            txtCompleted.Text = order.Completed;
        }
    }
}