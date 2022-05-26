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
        private Checks checks = new Checks();
        public LoginPage()
        {
            InitializeComponent();
        }
        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            if (checks.InputCheck(Username.Text))
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
                        if (SessionUser.User.IsTemp == true)
                        {
                            ChangeTempPass ChangeTempPass = new ChangeTempPass(SessionUser);
                            ChangeTempPass.Show();
                        }
                        else 
                        {
                            this.Content = null;
                            this.NavigationService.Navigate(new AdminPage(SessionUser));
                        }
                    }
                }
            }
        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            UserRegWindow UserRegWindow = new UserRegWindow(Username.Text);
            UserRegWindow.Show();
        }
    }
}
