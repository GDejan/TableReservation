using System;
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
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            SessionUser SessionUser = new SessionUser(new LogInUser().LogedUser(Username.Text, Password.Password));

            if (SessionUser.User != null)
            {
                if (SessionUser.User.IsAdmin != true)
                {
                    UserPage UserPage = new UserPage(SessionUser); //predati session usera na sljedeci window
                    this.Content = UserPage;
                }
                else
                {
                    AdminPage AdminPage = new AdminPage(SessionUser); //predati session usera na sljedeci window
                    this.Content = AdminPage;
                }
            }
            else
            {
                MessageBox.Show(EnumMsgs.WrongUser, EnumMsgs.Error, MessageBoxButton.OK);
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            UserRegWindow UserRegWindow = new UserRegWindow();
            UserRegWindow.Show();

        }

    }
}
