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

        private void SetGroupName_Loaded(object sender, RoutedEventArgs e)
        {

            DataContext = FilterContactsPage.FILTERED_GROUP;

            if (NavigationContext.QueryString.Count > 0 && int.TryParse(NavigationContext.QueryString["index"].ToString(), out index))
            {
                DataContext = MainPage.APPLICATION_DATA.PHONE_CONTACTS.ContactsList[index];
            }

            formatSelectedNamesForSmallTitle();
            
        }

        private void formatSelectedNamesForSmallTitle()
        {

            List<MyContact> list = (DataContext as Group).ContactsList;

            if (list.Count == 0)
            {
                MessageBox.Show("En az bir telefon numarası seçmelisiniz!");
                NavigationService.GoBack();
            }
            else if (list.Count == 1)
            {
                SmallTitle.Text = list[0].DisplayName;
            }
            else if (list.Count >= 2)
            {
                SmallTitle.Text = list[0].DisplayName + "; " + list[1].DisplayName + "; ...";
            }
        }

        //TO-DO: Add filters bindings at FilterContactsPage.xaml
        //TO-DO: Bind groups list at GroupSelect.xaml
        //TO-DO: Git integration 
        //TO-DO: Google Analystics 
        //TO-DO: Settings page for FilterContactsPage.xaml
        //TO-DO: Website portfolio on osmansekerlen.com
        //TO-DO: About
        //TO-DO: Search for templates
        private async void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FilterContactsPage.FILTERED_GROUP.GroupName = GroupNameTextBox.Text.ToString();

            MainPage.APPLICATION_DATA.GroupList.Add(FilterContactsPage.FILTERED_GROUP);
            await MainPage.APPLICATION_DATA.Save(SMSComposer.FileKey);

            NavigationService.Navigate(new Uri(SMSComposer.PagesRoot + "/GroupSelect.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
            NavigationService.RemoveBackEntry();
        }
    }
}