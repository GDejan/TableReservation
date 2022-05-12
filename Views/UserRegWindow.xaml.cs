using System;
using System.Windows;
using TableReservation.Classes;
using TableReservation.Classes.Users;
using TableReservation.Helpers;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for UserRegWindow.xaml
    /// </summary>
    public partial class UserRegWindow : Window
    {
        private UserMng UserMng = new UserMng();
        public UserRegWindow()
        {
            InitializeComponent();
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if (UserMng.NewUser(Name.Text, Surname.Text, Username.Text, Password.Password) == true)
            {
                this.Close();
            }  
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
