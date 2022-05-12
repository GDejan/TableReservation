using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using TableReservation.Classes;
using TableReservation.Classes.Users;
using TableReservation.Helpers;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        private SessionUser SessionUser = new SessionUser();
        private int ID;
        
        private DbUserMng DbUserMng = new DbUserMng();
        private UserMng UserMng = new UserMng();
        User User = new User();
        public AdminPage(SessionUser sessionUser)
        {
            InitializeComponent();
            this.SessionUser = sessionUser;
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv = true) 
            {
                var DialogResult = MessageBox.Show(EnumMsgs.CheckNewValues, EnumMsgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    UserMng.NewUser(NewName.Text, NewSurname.Text, NewUsername.Text, NewPassword.Password);
                }
            }
            
        }
        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv = true)
            {
                if (getUserById() == true)
                {
                    var DialogResult = MessageBox.Show(EnumMsgs.CheckOldValues, EnumMsgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        UserMng.ChangeUser(ID, NewName.Text, NewSurname.Text, NewUsername.Text, (bool)NewIsAdmin.IsChecked, User);
                    }
                }
            }          
        }
        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv = true)
            {
                if (getUserById() == true)
                {
                    var DialogResult = MessageBox.Show(EnumMsgs.CheckOldValues, EnumMsgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        UserMng.RemoveUser(ID);
                    }
                }
            }
        }

        private void GetUserByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv = true)
            {
                getUserById();
            }
        }
        private bool getUserById()
        {            
            User = UserMng.getUserById(OldId.Text);
            if (User!=null)
            {
                OldUsername.Content = User.Username;
                OldName.Content = User.Name;
                OldSurname.Content = User.Surname;
                OldIsAdmin.Content = User.IsAdmin;
                ID = User.Id;
                return true;
            }
            return false;
        }     

        private void AddBuilding_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void ChangeBuilding_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void RemoveBuilding_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void AddStorey_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void ChangeStorey_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void RemoveStorey_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void ChangeRoom_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void RemoveRoom_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void AddDesk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void ChangeDesk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void RemoveDesk_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented, EnumMsgs.Ok, MessageBoxButton.OK);
        }

        private void ListUsers_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented + SessionUser.User.Username, EnumMsgs.Ok, MessageBoxButton.OK);
        } 
        private void ListReservations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(EnumMsgs.NotImplemented + SessionUser.User.Username, EnumMsgs.Ok, MessageBoxButton.OK);
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            SessionUser.IsActiv=false;
            
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();



        }

    }
}