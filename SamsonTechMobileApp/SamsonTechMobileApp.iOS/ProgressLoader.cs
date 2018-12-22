using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BigTed;
using Foundation;
using SamsonTechMobileApp.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ProgressLoader))]
namespace SamsonTechMobileApp.iOS
{
    class ProgressLoader : IProgressInterface
    {
        public void Hide()
        {
            BTProgressHUD.Dismiss();
        }

        public void Show(string title = "Loading")
        {
            BTProgressHUD.Show(title, maskType: ProgressHUD.MaskType.Black);
        }
    }
}