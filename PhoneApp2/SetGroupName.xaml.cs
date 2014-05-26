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

namespace PhoneApp2
{
    public partial class SetGroupName : PhoneApplicationPage
    {
        private int index = 0;
        public SetGroupName()
        {
            InitializeComponent();
            Loaded += SetGroupName_Loaded;
        }

        void SetGroupName_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!int.TryParse(NavigationContext.QueryString["index"].ToString(), out index)) {
                    DataContext = MainPage.APPLICATION_DATA.PHONE_CONTACTS.ContactsList[index];
                    return;
                }
            }catch{}

            DataContext = MainPage.APPLICATION_DATA.PHONE_CONTACTS.ContactsList.LastOrDefault();
        }

        //TO-DO: Add filters bindings at FilterContactsPage.xaml
        //TO-DO: Bind groups list at GroupSelect.xaml
        //TO-DO: Git integration 
        //TO-DO: Google Analystics 
        //TO-DO: Settings page for FilterContactsPage.xaml
        //TO-DO: Website portfolio on osmansekerlen.com
        //TO-DO: About
        //TO-DO: Search for templates
        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MainPage.APPLICATION_DATA.GroupList.Add(new Group()
            {
                ContactsList = FilterContactsPage.FILTERED_CONTACTS,
                GroupName = GroupNameTextBox.Text.ToString()
            });
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
        }
    }
}