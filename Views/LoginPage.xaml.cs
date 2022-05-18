﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TableReservation.Classes.Users;
using TableReservation.Helpers;

namespace TableReservation.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private Msgs Msgs = new Msgs();
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            SessionUser SessionUser = new SessionUser(new UserMng().LogedUser(Username.Text, Password.Password));

            if (SessionUser.User != null)
            {
                if (SessionUser.User.IsAdmin != true)
                {
                    this.Content = null;
                    this.NavigationService.Navigate(new UserPage(SessionUser));
                }
                else
                {
                    this.Content = null;
                    this.NavigationService.Navigate(new AdminPage(SessionUser));
                }
            }
            else
            {
                MessageBox.Show(Msgs.WrongUser, Msgs.Error, MessageBoxButton.OK);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            UserRegWindow UserRegWindow = new UserRegWindow();
            UserRegWindow.Show();
        }
    }
}
