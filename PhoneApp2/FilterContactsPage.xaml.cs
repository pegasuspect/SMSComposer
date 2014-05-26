﻿using System;
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
    public partial class FilterContactsPage : PhoneApplicationPage
    {
        private bool isAllSelected = false;
        private bool isIncludingAllNumbers = false;
        public static List<MyContact> FILTERED_CONTACTS = new List<MyContact>();
        // Constructor
        public FilterContactsPage()
        {
            InitializeComponent();

            Loaded += FilterContactsPage_Loaded;

            BuildLocalizedApplicationBar();
        }

        void FilterContactsPage_Loaded(object sender, RoutedEventArgs e)
        {
            FILTERED_CONTACTS = MainPage.APPLICATION_DATA.PHONE_CONTACTS.ContactsList;
            FILTERED_CONTACTS = FILTERED_CONTACTS.Where(a => a.PhoneNumber.StartsWith(MainPage.APPLICATION_DATA.CountryCode)).ToList();
            filteredContactsList.ItemsSource = FILTERED_CONTACTS;
        }

        private void BuildLocalizedApplicationBar()
        {
            //Remove Buttons from Last Page
            ApplicationBar.Buttons.Clear();
            ApplicationBarIconButton button;

            button = new ApplicationBarIconButton()
            {
                IconUri = new Uri("/Assets/AppBar/next.png", UriKind.RelativeOrAbsolute),
                Text = AppResources.AppBarFilterContactsNextButton
            };

            button.Click += nextButtonClick;

            ApplicationBar.Buttons.Add(button);

            button = new ApplicationBarIconButton()
            {
                IconUri = new Uri("/Assets/AppBar/check.png", UriKind.RelativeOrAbsolute),
                Text = AppResources.SelectAll
            };

            button.Click += button_Click;

            ApplicationBar.Buttons.Add(button);

        }

        private void nextButtonClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/SetGroupName.xaml", UriKind.RelativeOrAbsolute));
        }

        void button_Click(object sender, EventArgs e)
        {
            isAllSelected = !isAllSelected;

            foreach (MyContact cont in FILTERED_CONTACTS)
            {
                cont.Checked = isAllSelected;
            }

            totalSelectedCountTextBox.Text = isAllSelected ? FILTERED_CONTACTS.Count.ToString() : "0";

            ApplicationBarIconButton button = sender as ApplicationBarIconButton;
            button.Text = isAllSelected ? AppResources.SelectAll : AppResources.SelectNone;
            string uri = !isAllSelected ? "/Assets/AppBar/check.png" : "/Assets/AppBar/checked.png";
            button.IconUri = new Uri(uri, UriKind.RelativeOrAbsolute);

            RefreshList();

        }

        private void CheckBox_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            isIncludingAllNumbers = (sender as CheckBox).IsChecked.Value == true;

            FilterContactsList();
            RefreshList();
        }

        private void RefreshList()
        {
            filteredContactsList.ItemsSource = null;
            filteredContactsList.ItemsSource = FILTERED_CONTACTS;

        }

        private void FilterContactsList()
        {
            FILTERED_CONTACTS = MainPage.APPLICATION_DATA.PHONE_CONTACTS.ContactsList.Where(a =>
                (ContainsTextBox.Text.Length > 0 ? a.DisplayName.ToLowerInvariant().Contains(ContainsTextBox.Text.ToLowerInvariant()) : true) &&
                (StartsWithTextBox.Text.Length > 0 ? a.DisplayName.ToLowerInvariant().StartsWith(StartsWithTextBox.Text.ToLowerInvariant()) : true) &&
                (PhoneNumberStartsWithTextBox.Text.Length > 0 ? a.PhoneNumber.StartsWith(MainPage.APPLICATION_DATA.CountryCode + PhoneNumberStartsWithTextBox.Text.ToLowerInvariant()) : true) &&
                (EndsWithTextBox.Text.Length > 0 ? a.DisplayName.ToLowerInvariant().EndsWith(EndsWithTextBox.Text.ToLowerInvariant()) : true) &&
                (isIncludingAllNumbers ? true : a.PhoneNumber.StartsWith(MainPage.APPLICATION_DATA.CountryCode))
            ).ToList();

            WarningTextBlock.Visibility = FILTERED_CONTACTS.Count > 0 ? Visibility.Collapsed : Visibility.Visible;
        }

        private void filteredContactsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LongListSelector lls = sender as LongListSelector;
            if (lls.SelectedItem == null)
                return;

            (lls.SelectedItem as MyContact).Checked = !(lls.SelectedItem as MyContact).Checked;
            
            totalSelectedCountTextBox.Text = FILTERED_CONTACTS.Where(a => a.Checked).Count().ToString();


            lls.SelectedItem = null;
        }

        private void Button_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            FilterContactsList();
            RefreshList();
        }

        private void stackPanel1_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            ContainsTextBox.Focus();
        }


        private void stackPanel2_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            StartsWithTextBox.Focus();
        }

        private void stackPanel3_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EndsWithTextBox.Focus();
        }

        private void stackPanel4_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            PhoneNumberStartsWithTextBox.Focus();
        }

    }
}