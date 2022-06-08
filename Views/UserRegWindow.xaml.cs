using System.Windows;
using TableReservation.Helpers;
using TableReservation.Users;
using TableReservation.Views;

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
            LoginPage.NoOfWindows++;
            InitializeComponent();
            Username.Text = username;
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            if ((checks.InputCheck(Name.Text) == true) && (checks.InputCheck(Surname.Text)) == true && (checks.InputCheck(Username.Text) == true))
            {
                if (checks.InputCheckPass(Password.Password) && checks.InputCheckPass(ConfirmPass.Password))
                {
                    if (Password.Password == ConfirmPass.Password)
                    {
                        PassHash passHash = new PassHash(Password.Password);
                        if (userMng.Create(new User(Name.Text, Surname.Text, Username.Text, passHash.HashedPassword, false, false)) == true)
                        {
                            this.Close();
                        }
                    }
                    else
                    {
                        MessageBox.Show(msgs.NoMatch, msgs.Error, MessageBoxButton.OK);
                    }
                }
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginPage.NoOfWindows--;
        }
    }
}
