using System;
using System.Collections.Generic;
using System.Text;

namespace SamsonTechMobileApp
{
    public interface IProgressInterface
    {
        void Show(string title = "Loading");

        void Hide();
    }
}
