using System;
using System.Collections.Generic;
using System.Text;

namespace SamsonTechMobileApp
{
    public class Item
    {
        public string Catergory { get; set; }
        public string Name { get; set; }
        public string Quantity { get; set; }
        public string Description { get; set; }
        public bool CatergoryType { get; set; }

        public bool MultiSelectionMode = false;
        public bool Selection = false;

        public string ClassId
        {
            get
            {
                return Name + Catergory;
            }
        }

        public string Image
        {
            get
            {
                if (MultiSelectionMode)
                {
                    if (Selection)
                    {
                        return "RadioSelected.png";
                    }
                    else
                    {
                        return "RadioUnselected.png";
                    }
                }
                else
                    return "Blank.png";
            }
        }
    }
}
