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
        private UserMng userMng = new UserMng();
        private Checks checks = new Checks();
        private Msgs msgs = new Msgs();

        public UserRegWindow(string username)
        {
            InitializeComponent();
            Username.Text = username;
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            if ((checks.InputCheck(Name.Text)==true) && (checks.InputCheck(Surname.Text)) == true && (checks.InputCheck(Username.Text) == true))
            {
                if ((!string.IsNullOrEmpty(Password.Password))&& (!string.IsNullOrEmpty(ConfirmPass.Password)))
                {
                    if (Password.Password == ConfirmPass.Password)
                    {
                        PassHash passHash = new PassHash(Password.Password);
                        if (userMng.NewUser(new User(Name.Text, Surname.Text, Username.Text, passHash.HashedPassword)) == true)
                        {
                            this.Close();
                        }
                    }
                    else 
                    {
                        MessageBox.Show(msgs.NoMatch, msgs.Error, MessageBoxButton.OK);
                    }
                }
                else 
                {
                    MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
                }
            }            
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
