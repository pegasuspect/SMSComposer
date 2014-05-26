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
        //Phone specific information
        public static SMSComposer APPLICATION_DATA;

        //Aplication lifetime spesific information
        private bool isLoaded = false;

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            if (!isLoaded)
                LoadApplication();

            BuildLocalizedApplicationBar();
        }

        void LoadApplication()
        {
            APPLICATION_DATA = new SMSComposer();
            APPLICATION_DATA.GetPhoneContactsAndSave();
            isLoaded = true;
        }

        private void BuildLocalizedApplicationBar()
        {
            //Remove Buttons from Last Page
            ApplicationBar.Buttons.Clear();

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/feature.email.png", UriKind.RelativeOrAbsolute));
            appBarButton.Text = AppResources.MainPageOK;
            ApplicationBar.Buttons.Add(appBarButton);
        }
    }
}