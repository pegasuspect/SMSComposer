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
        public GroupSelect()
        {
            InitializeComponent();
            BuildLocalizedApplicationBar();
            RefreshData();
        }

        private void RefreshData()
        {
            DataContext = null;
            DataContext = MainPage.APPLICATION_DATA;
            bool isInfoVisible = MainPage.APPLICATION_DATA.GroupList.Count == 0;
            NoGroupInfo.Visibility = isInfoVisible ? Visibility.Visible : Visibility.Collapsed;

        }


        private void BuildLocalizedApplicationBar()
        {

            // Create a new menu item with the localized string from AppResources.
            ApplicationBarIconButton appBarButton;

            appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/upload.png", UriKind.RelativeOrAbsolute));
            appBarButton.Text = AppResources.SelectGroups;
            appBarButton.Click += appBarButton_Click2;
            ApplicationBar.Buttons.Add(appBarButton);

            appBarButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Select.png", UriKind.RelativeOrAbsolute));
            appBarButton.Text = AppResources.DeleteGroups;
            appBarButton.Click += appBarButton_Click3;
            ApplicationBar.Buttons.Add(appBarButton);

            appBarButton = new ApplicationBarIconButton(new Uri("/Toolkit.Content/ApplicationBar.Delete.png", UriKind.RelativeOrAbsolute));
            appBarButton.Text = AppResources.DeleteGroups;
            appBarButton.Click += appBarButton_Click;
            ApplicationBar.Buttons.Add(appBarButton);
        }

        private void appBarButton_Click3(object sender, EventArgs e)
        {
            listOfGroups.IsSelectionEnabled = !listOfGroups.IsSelectionEnabled;
        }

        private void appBarButton_Click2(object sender, EventArgs e)
        {
            foreach (Group grp in listOfGroups.SelectedItems)
                MainPage.GroupInputText += grp.GroupName + "; ";

            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri(SMSComposer.PagesRoot + "/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private async void appBarButton_Click(object sender, EventArgs e)
        {
            if (listOfGroups.SelectedItems.Count > 0)
            {

                MessageBoxResult res = MessageBox.Show(string.Format("You are going to delete {0} group(s).", listOfGroups.SelectedItems.Count), "Are you sure?", MessageBoxButton.OKCancel);
                if (res == MessageBoxResult.OK)
                {
                    foreach (Group grp in listOfGroups.SelectedItems)
                        MainPage.APPLICATION_DATA.GroupList.Remove(grp);
                    await MainPage.APPLICATION_DATA.Save(SMSComposer.FileKey);
                    RefreshData();
                }
                return;
            }

            MessageBox.Show("You haven't chosen any groups");
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            MainPage.GroupInputText += (sender as Group).GroupName + "; ";

            NavigationService.RemoveBackEntry();
            NavigationService.Navigate(new Uri(SMSComposer.PagesRoot + "/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}