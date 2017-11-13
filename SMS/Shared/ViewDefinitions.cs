using SMS_Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.Shared
{
    public class ViewDefinitions
    {
        public static View FeeCollectionListView = new View() { Name = "FeeCollectionListView", Title = "Fee Collection" };
        public static View SyncView = new View() { Name = "SyncView", Title = "Sync with Online" };
        public static View SettingsView = new View() { Name = "SettingsView", Title = "Settings" };
    }
}
