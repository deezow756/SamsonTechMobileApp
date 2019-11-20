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
	public partial class AddStock : ContentPage
	{
        public Item curCat;
        public Item item;
        CatergoryEntry catergoryEntry;
        ItemEntry itemEntry;

		public AddStock (Item curCat)
		{
            InitializeComponent();

            item = new Item();

            if (curCat != null)
            {
                this.curCat = curCat;
                item.Catergory = curCat.Name;
            }
		}

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopAsync();
        }

        private void BtnSave_Clicked(object sender, EventArgs e)
        {
            if (pickerType.SelectedItem.ToString() == "Catergory")
            {
                if (catergoryEntry.txtName.Text != "")
                {
                    item.CatergoryType = true;
                    item.Name = catergoryEntry.txtName.Text;
                }
                else return;
            }
            else if (pickerType.SelectedItem.ToString() == "Item")
            {
                if (itemEntry.txtName.Text != "" || itemEntry.txtDescription.Text != "" || itemEntry.txtQuantity.Text != "")
                {
                    item.CatergoryType = false;
                    item.Name = itemEntry.txtName.Text;
                    item.Quantity = itemEntry.txtQuantity.Text;
                    item.Description = itemEntry.txtDescription.Text;
                }
                else return;
            }
            FileManager fileManager = new FileManager();
            fileManager.AddItem(this, item);
            this.Navigation.PopAsync();
        }

        private void PickerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            var pick = (Picker)sender;
            if(pick.SelectedItem.ToString() == "Catergory")
            {
                catergoryEntry = new CatergoryEntry(this, enterSection);
            }
            else if (pick.SelectedItem.ToString() == "Item")
            {
                itemEntry = new ItemEntry(this, enterSection);
            }
        }
    }
}