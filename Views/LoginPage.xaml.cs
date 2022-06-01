using System.Windows;
using System.Windows.Controls;
using TableReservation.Helpers;
using TableReservation.Users;

namespace TableReservation.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static int NoOfWindows = 0;

        private Checks checks = new Checks();

        public LoginPage()
        {
            InitializeComponent();
        }
        private void logIn_Click(object sender, RoutedEventArgs e)
        {
            if (NoOfWindows == 0)
            {
                if (checks.InputCheck(Username.Text))
                {
                    SessionUser SessionUser = new SessionUser(new UserMng().LogedUser(Username.Text, Password.Password));
                    if (SessionUser.User != null)
                    {
                        if (SessionUser.User.IsTemp == true)
                        {
                            ChangePassword changePassword = new ChangePassword(SessionUser);
                            changePassword.Show();
                        }
                        else
                        {
                            this.Content = null;
                            if (SessionUser.User.IsAdmin != true)
                            {
                                this.NavigationService.Navigate(new UserPage(SessionUser));
                            }
                            else
                            {
                                this.NavigationService.Navigate(new AdminPage(SessionUser));
                            }
                        }
                    }
                }
            }
        }

        private void register_Click(object sender, RoutedEventArgs e)
        {
            if (NoOfWindows == 0)
            {
                UserRegWindow userRegWindow = new UserRegWindow(Username.Text);
                userRegWindow.Show();
            }
        }
    }
}
