using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TableReservation.Helpers;
using TableReservation.Property;
using TableReservation.Resevations;
using TableReservation.Users;
using TableReservation.Views;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for AdminPage.xaml
    /// </summary>
    public partial class AdminPage : Page
    {
        public SessionUser SessionUser = new SessionUser();
        
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
        private ResMng resMng = new ResMng();
        private Msgs msgs = new Msgs();
        private List<User> users = new List<User>();
        private List<Building> buildings = new List<Building>();
        private List<Storey> storeys = new List<Storey>();
        private List<Room> rooms = new List<Room>();
        private List<Desk> desks= new List<Desk>();
        private List<Reservation> reservations = new List<Reservation>();
        private Checks checks = new Checks();
        private Settings settings = new Settings();

        public AdminPage(SessionUser sessionUser)
        {
            InitializeComponent();
            this.SessionUser = sessionUser;
        }
        private void getUserByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getUserById();
            }
        }
        private void getBuildingByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getBuildingById();
            }
        }
        private void getStoreyByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getStoreyById();
            }
        }
        private void getRoomByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getRoomById();
            }
        }
        private void getDeskByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                getDeskById();
            }
        } 

        private void addUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true) 
            {
                if ((checks.InputCheck(NewName.Text) == true) && (checks.InputCheck(NewSurname.Text)) == true && (checks.InputCheck(NewUsername.Text) == true))
                {
                    if ((!string.IsNullOrEmpty(NewPassword.Password)))
                    {
                        var DialogResult = MessageBox.Show(msgs.CheckNewValues, msgs.Ok, MessageBoxButton.YesNo);
                        if (DialogResult == MessageBoxResult.Yes)
                        {
                            PassHash passHash = new PassHash(NewPassword.Password);
                            userMng.Create(new User(NewName.Text, NewSurname.Text, NewUsername.Text, passHash.HashedPassword, (bool)NewIsAdmin.IsChecked, true));
                        }
                    }
                    else 
                    {
                        MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
                    }
                }
            }
        }
        private void changeUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getUserById() == true)
                {
                    if (!((user.Username == settings.MasterAdmin) && (user.IsAdmin == true)))
                    {
                        string newusername;
                        string newname;
                        string newsurname;

                        if (NewUsername.Text != "") newusername = NewUsername.Text; else newusername = (string)OldUsername.Content;
                        if (NewName.Text != "") newname = NewName.Text; else newname = (string)OldName.Content;
                        if (NewSurname.Text != "") newsurname = NewSurname.Text; else newsurname = (string)OldSurname.Content;

                        if ((checks.InputCheck(newusername) == true) && (checks.InputCheck(newname)) == true && (checks.InputCheck(newsurname) == true))
                        {
                            var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                            if (DialogResult == MessageBoxResult.Yes)
                            {
                                User chngUser = new User();
                                chngUser.Id = user.Id;
                                chngUser.Name = newname;
                                chngUser.Surname = newsurname;
                                chngUser.Username = newusername;
                                chngUser.IsAdmin = (bool)NewIsAdmin.IsChecked;
                                chngUser.IsTemp = user.IsTemp;

                                if ((bool)EnPass.IsChecked)
                                {
                                    PassHash passHash = new PassHash(NewPassword.Password);
                                    chngUser.Password = passHash.HashedPassword;
                                    userMng.ChangePass(chngUser, user);
                                    EnPass.IsChecked = false;
                                    NewPassword.Password = "";
                                }
                                else 
                                {
                                    userMng.Change(chngUser, user);
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show(msgs.UserIsMaster, msgs.Ok, MessageBoxButton.OK);
                    }
                }
            }          
        }
        private void removeUser_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getUserById()==true)
                {
                    if (!((user.Username == settings.MasterAdmin) && (user.IsAdmin == true)))
                    {
                        if ((user.Username !=SessionUser.User.Username))
                        {
                            var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                            if (DialogResult == MessageBoxResult.Yes)
                            {
                                userMng.Remove(user);
                            }
                        }
                        else
                        {
                            MessageBox.Show(msgs.UserItSelf + "->" + user.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                        }
                    }
                    else
                    {
                        MessageBox.Show(msgs.UserIsMaster, msgs.Ok, MessageBoxButton.OK);
                    }
                }
            }
        }
        private void addBuilding_Click(object sender, RoutedEventArgs e)
        {
            if(SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckNewValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        buildMng.Create(new Building(NewName.Text));
                    }
                }
            }
        }
        private void changeBuilding_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    if (getBuildingById() == true)
                    {
                        var DialogResult = MessageBox.Show(msgs.CheckValues, msgs.Ok, MessageBoxButton.YesNo);
                        if (DialogResult == MessageBoxResult.Yes)
                        {
                            buildMng.Change(new Building(building.Id, NewName.Text), building);
                        }
                    }
                }
            }
        }
        private void removeBuilding_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getBuildingById() == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        buildMng.Remove(building);
                    }
                }
            }
        }
        private void addStorey_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckNewValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        storeyMng.Create(new Storey(NewName.Text));
                    }
                }
            }
        }
        private void changeStorey_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    if (getStoreyById() == true)
                    {
                        var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                        if (DialogResult == MessageBoxResult.Yes)
                        {
                            storeyMng.Change(new Storey(storey.Id, NewName.Text), storey);
                        }
                    }
                }
            }
        }
        private void removeStorey_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getStoreyById() == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        storeyMng.Remove(storey);
                    }
                }
            }
        }
        private void addRoom_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckNewValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        roomMng.Create(new Room(NewName.Text));
                    }
                }
            }
        }
        private void changeRoom_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    if (getRoomById() == true)
                    {
                        var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                        if (DialogResult == MessageBoxResult.Yes)
                        {
                            roomMng.Change(new Room(room.Id, NewName.Text), room);
                        }
                    }
                }
            }
        }
        private void removeRoom_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getRoomById() == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        roomMng.Remove(room);
                    }
                }
            }
        }
        private void addDesk_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckNewValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        deskMng.Create(new Desk(NewName.Text));
                    }
                }
            }
        }
        private void changeDesk_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (checks.InputCheck(NewName.Text) == true)
                {
                    if (getDeskById() == true)
                    {
                        var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                        if (DialogResult == MessageBoxResult.Yes)
                        {
                            deskMng.Change(new Desk(desk.Id, NewName.Text), desk);
                        }
                    }
                }
            }
        }
        private void removeDesk_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if (getDeskById() == true)
                {
                    var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        deskMng.Remove(desk);
                    }
                }
            }
        }

        private void listUsers_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                Listbox.Items.Clear();
                users = userMng.getAll();

                foreach (var item in users) Listbox.Items.Add(item);

            }
        }
        private void listReservations_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                DateTime startTime = DateTime.Now.Date;
                DateTime endTime = DateTime.Now.Date;
                if (Startdate.SelectedDate != null)
                {
                    startTime = (DateTime)Startdate.SelectedDate;
                }
                if (Enddate.SelectedDate != null)
                {
                    endTime = (DateTime)Enddate.SelectedDate;
                }
                Listbox.Items.Clear();
                reservations = resMng.getAllReservations(startTime, endTime);
                if (reservations != null)
                {
                    foreach (var item in reservations) Listbox.Items.Add(item);
                }
            }
        }
        private void listUBuildings_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                Listbox.Items.Clear();
                buildings = buildMng.getAllBuilds();
                if (buildings != null)
                {
                    foreach (var item in buildings) Listbox.Items.Add(item);
                }
            }
        }
        private void listStoreys_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                Listbox.Items.Clear();
                storeys = storeyMng.getAll();
                if (storeys != null)
                {
                    foreach (var item in storeys) Listbox.Items.Add(item);
                }
                
            }
        }
        private void ListRooms_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                Listbox.Items.Clear();
                rooms = roomMng.getAll();
                if (rooms != null)
                {
                    foreach (var item in rooms) Listbox.Items.Add(item);
                }
            }
        }
        private void listDesks_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                Listbox.Items.Clear();
                desks = deskMng.getAllDesks();
                if (desk!=null)
                {
                    foreach (var item in desks) Listbox.Items.Add(item);
                }
            }
        }
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
            SessionUser.IsActiv=false;
            LoginPage loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }
        private void clearOldContent()
        {
            OldName.Content = "";
            OldUsername.Content = "";
            OldSurname.Content = "";
            OldIsAdmin.Content = "";
        }
        private bool getUserById()
        {
            if (checks.InputCheckStringIntId(OldId.Text))
            {
                clearOldContent();
                user = userMng.getById(OldId.Text);
                if (user != null)
                {
                    OldName.Content = user.Name;
                    OldUsername.Content = user.Username;
                    OldSurname.Content = user.Surname;
                    OldIsAdmin.Content = user.IsAdmin;
                    return true;
                }
                return false;
            }
            return false;
        }
        private bool getBuildingById()
        {
            if (checks.InputCheckStringIntId(OldId.Text))
            {
                clearOldContent();
                building = buildMng.getById(OldId.Text);
                if (building != null)
                {
                    OldName.Content = building.Name;
                    return true;
                }
                return false;
            }
            return false;
        }
        private bool getStoreyById()
        {
            if (checks.InputCheckStringIntId(OldId.Text))
            {
                clearOldContent();
                storey = storeyMng.getById(OldId.Text);
                if (storey != null)
                {
                    OldName.Content = storey.Name;
                    return true;
                }
                return false;
            }
            return false;
        }
        private bool getRoomById()
        {
            if (checks.InputCheckStringIntId(OldId.Text))
            {
                clearOldContent();
                room = roomMng.getById(OldId.Text);
                if (room != null)
                {
                    OldName.Content = room.Name;
                    return true;
                }
                return false;
            }
            return false;
        }
        private bool getDeskById()
        {
            if (checks.InputCheckStringIntId(OldId.Text))
            {
                clearOldContent();
                desk = deskMng.getById(OldId.Text);
                if (desk != null)
                {
                    OldName.Content = desk.Name;
                    return true;
                }
                return false;
            }
            return false;
        }

        private void enPassword_Checked(object sender, RoutedEventArgs e)
        {
            NewPassword.IsEnabled = true;
        }

        private void enPassword_Unchecked(object sender, RoutedEventArgs e)
        {
            NewPassword.IsEnabled = false;
        }
    }
} 