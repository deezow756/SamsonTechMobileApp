using System;
using System.Collections.Generic;
using System.Text;

namespace SamsonTechMobileApp
{
    public class CloudStorageManager
    {
        public List<CloudStorage> clouds;

        Menu menu;

        public CloudStorageManager(Menu menu)
        {
            this.menu = menu;
            clouds = new List<CloudStorage>();
            sync();
        }

        public void sync()
        {
            FileManager fileManager = new FileManager();
            clouds.Clear();
            
            List<string> usages;

            usages = new List<string>();
            usages.Add("onedrive,1");
            usages.Add("firebase,0");
            fileManager.SaveStorageUsage(usages.ToArray());

            
            if (fileManager.StorageUsageExists())
            {
                usages = new List<string>(fileManager.GetStorageUsage());
            }
            else
            {
                usages = new List<string>();
                usages.Add("onedrive,1");
                usages.Add("firebase,0");
                fileManager.SaveStorageUsage(usages.ToArray());
            }

            for (int i = 0; i < usages.Count; i++)
            {
                string[] split = usages[i].Split(',');

                if(split[0] == "onedrive")
                {
                    if(split[1] == "1")
                    {
                        clouds.Add(new OneDrive(menu));
                    }
                }
                else if(split[0] == "firebase")
                {
                    if (split[1] == "1")
                    {
                        clouds.Add(new FirebaseDatabaseHelper(menu));
                    }
                }
            }
        }

        public void UpdateUsage(string name, bool status)
        {
            FileManager file = new FileManager();
            List<string> usages = new List<string>(file.GetStorageUsage());

            string usage = "";
            for (int i = 0; i < usages.Count; i++)
            {
                if(usages[i].Contains(name))
                {
                    usage = usages[i];
                    usages.Remove(usage);
                    break;
                }
            }

            string[] split = usage.Split(',');

            bool temp = false;

            if(split[1] == "1")
            { temp = true; }

            if(temp != status)
            {
                if(status)
                {
                    split[1] = "1";
                }
                else
                {
                    split[1] = "0";
                }
            }

            usages.Add(split[0] + "," + split[1]);
            file.SaveStorageUsage(usages.ToArray());
            sync();        }
    }
}
