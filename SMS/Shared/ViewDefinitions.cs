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
        public static View SessionsView = new View() { Name = "SessionsView", Title = "Setup Current Session" };
        public static View GradesView = new View() { Name = "GradesView", Title = "Setup Grades" };
        public static View SectionsView = new View() { Name = "SectionsView", Title = "Setup Sections" };
        public static View UsersView = new View() { Name = "UsersView", Title = "Manage Users" };
        public static View StudentsView = new View() { Name = "StudentsView", Title = "Manage Students" };
    }
}
