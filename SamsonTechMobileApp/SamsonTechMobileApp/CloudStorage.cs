using SamsonTechMobileApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SamsonTechMobileApp
{
    public class CloudStorage
    {
        public Settings settings;

        public virtual Task Signin() {return null; }
        public virtual Task Signout() {return null; }

        public virtual Task TryGetOrders() {return null; }
        public virtual Task TryGetStock() {return null; }
        public virtual Task SaveOrders() {return null; }
        public virtual Task SaveStock() {return null; }
        public virtual Task Sync() {return null; }

    }
}
