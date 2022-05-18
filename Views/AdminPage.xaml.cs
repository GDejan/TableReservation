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
using TableReservation.Views;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public SessionUser SessionUser = new SessionUser();
        private int ID;       
        private UserMng userMng = new UserMng();
        private User user = new User();
        private BuildMng buildMng = new BuildMng();
        private Building building = new Building();
        private StoreyMng storeyMng = new StoreyMng();
        private Storey storey = new Storey();
        private RoomMng roomMng = new RoomMng();
        private Room room = new Room();
        private DeskMng deskMng = new DeskMng();
        private Desk desk = new Desk();
        private Msgs Msgs = new Msgs();
        private List<Building> buildings = new List<Building>();

        public AdminPage(SessionUser sessionUser)
        {
            InitializeComponent();
            this.SessionUser = sessionUser;
        }
        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true) 
            {
                var DialogResult = MessageBox.Show(Msgs.CheckNewValues, Msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    userMng.NewUser(NewName.Text, NewSurname.Text, NewUsername.Text, NewPassword.Password);
                }
            }
            
        }
        private void ChangeUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getUserById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        userMng.ChangeUser(ID, NewName.Text, NewSurname.Text, NewUsername.Text, (bool)NewIsAdmin.IsChecked, user);
                    }
                }
            }          
        }
        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getUserById()==true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        userMng.RemoveUser(ID);
                    }
                }
            }
        }
        private void GetUserByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getUserById();
            }
        } 
        private void GetBuildingByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getBuildingById();
            }
        }   
        private void GetStoreyByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getStoreyById();
            }
        }   
        private void GetRoomByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getRoomById();
            }
        }   
        private void GetDeskByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getDeskById();
            }
        }
        private bool getUserById()
        {
            ID = 0;
            OldName.Content = "";
            OldUsername.Content = "";
            OldSurname.Content = "";
            OldIsAdmin.Content = "";
            user = userMng.getUserById(OldId.Text);
            if (user!=null)
            {
                ID = user.Id;
                OldName.Content = user.Name;
                OldUsername.Content = user.Username;
                OldSurname.Content = user.Surname;
                OldIsAdmin.Content = user.IsAdmin;
                return true;
            }
            return false;
        }  
        private bool getBuildingById()
        {
            ID = 0;
            OldName.Content = "";
            building = buildMng.getBuildById(OldId.Text);
            if (building != null)
            {
                ID = building.Id;
                OldName.Content = building.Name;
                OldUsername.Content = "";
                OldSurname.Content = "";
                OldIsAdmin.Content = "";
                return true;
            }
            return false;
        }
        private bool getStoreyById()
        {
            ID = 0;
            OldName.Content = "";
            storey = storeyMng.getStoreyById(OldId.Text);
            if (storey != null)
            {
                ID = storey.Id;
                OldName.Content = storey.Name;
                OldUsername.Content = "";
                OldSurname.Content = "";
                OldIsAdmin.Content = "";
                return true;
            }
            return false;
        }
        private bool getRoomById()
        {
            ID = 0;
            OldName.Content = "";
            room = roomMng.getRoomById(OldId.Text);
            if (room != null)
            {
                ID = room.Id;
                OldName.Content = room.Name;
                OldUsername.Content = "";
                OldSurname.Content = "";
                OldIsAdmin.Content = "";
                return true;
            }
            return false;
        }
        private bool getDeskById()
        {
            ID = 0;
            OldName.Content = "";
            desk = deskMng.getDeskById(OldId.Text);
            if (desk != null)
            {
                ID = desk.Id;
                OldName.Content = desk.Name;
                OldUsername.Content = "";
                OldSurname.Content = "";
                OldIsAdmin.Content = "";
                return true;
            }
            return false;
        }
        private void AddBuilding_Click(object sender, RoutedEventArgs e)
        {
            if(SessionUser.User.IsAdmin == true)
            {
                var DialogResult = MessageBox.Show(Msgs.CheckNewValues, Msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    buildMng.NewBuilding(NewName.Text);
                }
            }
        }
        private void ChangeBuilding_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getBuildingById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        buildMng.ChangeBuilding(ID, NewName.Text, building);
                    }
                }
            }
        }
        private void RemoveBuilding_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getBuildingById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        buildMng.RemoveBuilding(ID);
                    }
                }
            }
        }
        private void AddStorey_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                var DialogResult = MessageBox.Show(Msgs.CheckNewValues, Msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    storeyMng.NewStorey(NewName.Text);
                }
            }
        }
        private void ChangeStorey_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getStoreyById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        storeyMng.ChangeStorey(ID, NewName.Text, storey);
                    }
                }
            }
        }
        private void RemoveStorey_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getStoreyById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        storeyMng.RemoveStorey(ID);
                    }
                }
            }
        }
        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                var DialogResult = MessageBox.Show(Msgs.CheckNewValues, Msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    roomMng.NewRoom(NewName.Text);
                }
            }
        }
        private void ChangeRoom_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getRoomById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        roomMng.ChangeRoom(ID, NewName.Text, room);
                    }
                }
            }
        }
        private void RemoveRoom_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getRoomById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        roomMng.RemoveRoom(ID);
                    }
                }
            }
        }
        private void AddDesk_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                var DialogResult = MessageBox.Show(Msgs.CheckNewValues, Msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    deskMng.NewDesk(NewName.Text);
                }
            }
        }
        private void ChangeDesk_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getDeskById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        deskMng.ChangeDesk(ID, NewName.Text, desk);
                    }
                }
            }
        }
        private void RemoveDesk_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getDeskById() == true)
                {
                    var DialogResult = MessageBox.Show(Msgs.CheckOldValues, Msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        deskMng.RemoveDesk(ID);
                    }
                }
            }
        }

        private void ListUsers_Click(object sender, RoutedEventArgs e)
        {
            
            MessageBox.Show(Msgs.NotImplemented + SessionUser.User.Username, Msgs.Ok, MessageBoxButton.OK);
        } 
        private void ListReservations_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Msgs.NotImplemented + SessionUser.User.Username, Msgs.Ok, MessageBoxButton.OK);
        }
        private void ListUBuildings_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                string itemstring = "";
                Listbox.Items.Clear();
                buildings = buildMng.getAllBuilds();
                foreach (var item in buildings)
                {
                    itemstring = item.Id + ":\t" + item.Name;
                    Listbox.Items.Add(itemstring);
                }
            }
            MessageBox.Show(Msgs.NotImplemented + SessionUser.User.Username, Msgs.Ok, MessageBoxButton.OK);
        }
        private void ListStoreys_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Msgs.NotImplemented + SessionUser.User.Username, Msgs.Ok, MessageBoxButton.OK);
        }
        private void ListRooms_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Msgs.NotImplemented + SessionUser.User.Username, Msgs.Ok, MessageBoxButton.OK);
        }
        private void ListDesks_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Msgs.NotImplemented + SessionUser.User.Username, Msgs.Ok, MessageBoxButton.OK);
        }
        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
            SessionUser.IsActiv=false;
            LoginPage LoginPage = new LoginPage();
            this.NavigationService.Navigate(LoginPage);
        }
    }
}