using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using PhoneApp2.Resources;
using Newtonsoft.Json;
using System.IO.IsolatedStorage;

namespace PhoneApp2
{
    public partial class GroupSelect : PhoneApplicationPage
    {
        bool isInitialized = false;
        public GroupSelect()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();

            if (isInitialized)
            {
                InitializeContactGroups();
                isInitialized = true;
            }
        }

        private void InitializeContactGroups()
        {
            
        }

        private void BuildLocalizedApplicationBar()
        {
            //Remove Buttons from Last Page
            ApplicationBar.Buttons.Clear();

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/check.png", UriKind.RelativeOrAbsolute));
            appBarButton.Text = AppResources.SelectGroups;
            ApplicationBar.Buttons.Add(appBarButton);
        }
    }
}