using System.Windows;
using TableReservation.Helpers;
using TableReservation.Users;

namespace TableReservation.Views
{
    /// <summary>
    /// Interaction logic for ChangeTempPass.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        public SessionUser SessionUser = new SessionUser();
        private Checks checks = new Checks();
        private Msgs msgs = new Msgs();
        private UserMng userMng = new UserMng();

        public ChangePassword(SessionUser SessionUser)
        {
            LoginPage.NoOfWindows++;
            UserPage.NoOfWindows++;
            InitializeComponent();
            this.SessionUser = SessionUser;
        }

        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (checks.InputCheckPass(Password.Password) && checks.InputCheckPass(ConfirmPass.Password))
            {
                if (Password.Password == ConfirmPass.Password)
                {
                    PassHash passHash = new PassHash(Password.Password);

                    User user = new User();
                    user.Id = SessionUser.User.Id;
                    user.Name = SessionUser.User.Name;
                    user.Username = SessionUser.User.Username;
                    user.Surname = SessionUser.User.Surname;
                    user.Password = passHash.HashedPassword;
                    user.IsAdmin = SessionUser.User.IsAdmin;
                    user.IsTemp = false;

                    if (userMng.ChangePass(user, SessionUser.User) == true)
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

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginPage.NoOfWindows--;
            UserPage.NoOfWindows--;
        }
    }
}
