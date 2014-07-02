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
using Microsoft.Phone.UserData;
using System.IO.IsolatedStorage;
using Newtonsoft.Json;

namespace PhoneApp2
{
    public partial class MainPage : PhoneApplicationPage
    {
        public static SMSComposer APPLICATION_DATA;
        public static string GroupInputText = "";

        private bool isLoaded = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            if (!isLoaded)
                LoadApplication();

            BuildLocalizedApplicationBar();
        }

        private async void LoadApplication()
        {
            APPLICATION_DATA = await IsolatedStorageOperations.Load<SMSComposer>(SMSComposer.FileKey);
            if (APPLICATION_DATA.PHONE_CONTACTS.ContactsList.Count == 0)
                APPLICATION_DATA.GatherPhoneContactsAndSave();
            GroupInputField.Text = GroupInputText;
            isLoaded = true;
        }

        private void BuildLocalizedApplicationBar()
        {

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/feature.email.png", UriKind.RelativeOrAbsolute));
            appBarButton.Text = AppResources.MainPageOK;
            ApplicationBar.Buttons.Add(appBarButton);
        }
    }
}