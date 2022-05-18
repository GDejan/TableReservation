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
using TableReservation.Classes.Reservations;
using TableReservation.Classes.Users;
using TableReservation.Views;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        public SessionUser SessionUser = new SessionUser();
        public UserPage(SessionUser sessionUser)
        {
            InitializeComponent();
            this.SessionUser = sessionUser;
        }

        private void ReserveTable_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == false)
            {
                TableResWindow TableResWindow = new TableResWindow(this.SessionUser); 
                TableResWindow.Show();

            }
        }




        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
            SessionUser.IsActiv = false;
            LoginPage LoginPage = new LoginPage();
            this.NavigationService.Navigate(LoginPage);
        }
       
    }
}
