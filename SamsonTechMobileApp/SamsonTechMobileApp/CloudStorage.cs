using SamsonTechMobileApp.Pages;
using System;
using System.Collections.Generic;
using System.Text;

namespace SamsonTechMobileApp
{
    public class CloudStorage
    {
        public Settings settings;

        public virtual void Signin() {; }
        public virtual void Signout() {; }

        public virtual void TryGetOrders() {; }
        public virtual void TryGetStock() {; }
        public virtual void SaveOrders() {; }
        public virtual void SaveStock() {; }
        public virtual void Sync() {; }

    }
}
