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
        private Reservation reservation = new Reservation();
        private Msgs msgs = new Msgs();
        private List<User> users = new List<User>();
        private List<Building> buildings = new List<Building>();
        private List<Storey> storeys = new List<Storey>();
        private List<Room> rooms = new List<Room>();
        private List<Desk> desks= new List<Desk>();
        private List<Reservation> reservations = new List<Reservation>();
        private Checks checks = new Checks();

        public AdminPage(SessionUser sessionUser)
        {
            InitializeComponent();
            this.SessionUser = sessionUser;
        }
        private void add_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if ((bool)UserChecked.IsChecked) addUser();
                else if ((bool)BuildingChecked.IsChecked) addBuilding();
                else if ((bool)StoreyChecked.IsChecked) addStorey();
                else if ((bool)RoomChecked.IsChecked) addRoom();
                else if ((bool)DeskChecked.IsChecked) addDesk();
                else MessageBox.Show(msgs.NoSelection, msgs.Error, MessageBoxButton.OK);
            }
        }
        private void remove_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if ((bool)UserChecked.IsChecked) removeUser();
                else if ((bool)BuildingChecked.IsChecked) removeBuilding();
                else if ((bool)StoreyChecked.IsChecked) removeStorey();
                else if ((bool)RoomChecked.IsChecked) removeRoom();
                else if ((bool)DeskChecked.IsChecked) removeDesk();
                else if ((bool)ResChecked.IsChecked) removeRes();
                else MessageBox.Show(msgs.NoSelection, msgs.Error, MessageBoxButton.OK);
            }
        }
        private void change_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if ((bool)UserChecked.IsChecked) changeUser();
                else if ((bool)BuildingChecked.IsChecked) changeBuilding();
                else if ((bool)StoreyChecked.IsChecked) changeStorey();
                else if ((bool)RoomChecked.IsChecked) changeRoom();
                else if ((bool)DeskChecked.IsChecked) changeDesk();
                else MessageBox.Show(msgs.NoSelection, msgs.Error, MessageBoxButton.OK);
            }
        }
        private void getByID_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if ((bool)UserChecked.IsChecked) getUserById();
                else if ((bool)BuildingChecked.IsChecked) getBuildingById();
                else if ((bool)StoreyChecked.IsChecked) getStoreyById();
                else if ((bool)RoomChecked.IsChecked) getRoomById();
                else if ((bool)DeskChecked.IsChecked) getDeskById();
                else if ((bool)ResChecked.IsChecked) getResById();
                else MessageBox.Show(msgs.NoSelection, msgs.Error, MessageBoxButton.OK);
            }
        }
        private void ListAll_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                if ((bool)UserChecked.IsChecked) listUsers();
                else if ((bool)BuildingChecked.IsChecked) listUBuildings();
                else if ((bool)StoreyChecked.IsChecked) listStoreys();
                else if ((bool)RoomChecked.IsChecked) listRooms();
                else if ((bool)DeskChecked.IsChecked) listDesks();
                else if ((bool)ResChecked.IsChecked) listRes();
                else MessageBox.Show(msgs.NoSelection, msgs.Error, MessageBoxButton.OK);
            }
        }
        private void addUser()
        {
            if ((checks.InputCheck(NewName.Text) == true) && (checks.InputCheck(NewSurname.Text)) == true && (checks.InputCheck(NewUsername.Text) == true))
            {
                if (checks.InputCheckPass(NewPassword.Password))
                {
                    var DialogResult = MessageBox.Show(msgs.CheckNewValues, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        PassHash passHash = new PassHash(NewPassword.Password);
                        userMng.Create(new User(NewName.Text, NewSurname.Text, NewUsername.Text, passHash.HashedPassword, (bool)NewIsAdmin.IsChecked, true));
                    }
                }                
            }
        }
        private void changeUser()
        {
            if (getUserById() == true)
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
        }
        private void removeUser()
        {
            if (getUserById() == true)
            {
                var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    userMng.Remove(user, SessionUser);
                }
            }
        }
        private void addBuilding()
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
        private void changeBuilding()
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
        private void removeBuilding()
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
        private void addStorey()
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
        private void changeStorey()
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
        private void removeStorey()
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
        private void addRoom()
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
        private void changeRoom()
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
        private void removeRoom()
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
        private void addDesk()
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
        private void changeDesk()
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
        private void removeDesk()
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

        private void removeRes()
        {
            if (getResById() == true)
            {
                var DialogResult = MessageBox.Show(msgs.CheckOldValues, msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                   resMng.Remove(reservation.Id);
                }
            }
        }
        private void listUsers()
        {
            Listbox.Items.Clear();
            users = userMng.GetAll();
            if (users != null)
            {
                foreach (var item in users) Listbox.Items.Add(item);
            }
        } 
        private void listUBuildings()
        {
            Listbox.Items.Clear();
            buildings = buildMng.GetAllBuilds();
            if (buildings != null)
            {
                foreach (var item in buildings) Listbox.Items.Add(item);
            }
        }
        private void listStoreys()
        {
            Listbox.Items.Clear();
            storeys = storeyMng.GetAll();
            if (storeys != null)
            {
                foreach (var item in storeys) Listbox.Items.Add(item);
            }
        }
        private void listRooms()
        {
            Listbox.Items.Clear();
            rooms = roomMng.GetAll();
            if (rooms != null)
            {
                foreach (var item in rooms) Listbox.Items.Add(item);
            }
        }
        private void listDesks()
        {
            Listbox.Items.Clear();
            desks = deskMng.GetAllDesks();
            if (desk != null)
            {
                foreach (var item in desks) Listbox.Items.Add(item);
            }
        }
        private void listRes() 
        {
            Listbox.Items.Clear();
            reservations = resMng.GetAll();
            if (reservations != null)
            {
                foreach (var item in reservations) Listbox.Items.Add(item);
            }
        }

        private void listResDate_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.User.IsAdmin == true)
            {
                DateTime startTime = DateTime.Now.Date;
                DateTime endTime = DateTime.Now.Date;
                if (Startdate.SelectedDate != null) startTime = (DateTime)Startdate.SelectedDate;
                if (Enddate.SelectedDate != null) endTime = (DateTime)Enddate.SelectedDate;

                Listbox.Items.Clear();
                reservations = resMng.GetResByDateRange(startTime, endTime);
                if (reservations != null)
                {
                    foreach (var item in reservations) Listbox.Items.Add(item);
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
            OldBuildId.Content = "";
            OldStoreyId.Content = "";
            OldRoomId.Content = "";
            OldDeskId.Content = "";
            OldUserId.Content = "";
            OldResAt.Content = "";
        }
        private bool getUserById()
        {
            if (checks.InputCheckStringIntId(OldId.Text))
            {
                clearOldContent();
                user = userMng.GetById(OldId.Text);
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
                building = buildMng.GetById(OldId.Text);
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
                storey = storeyMng.GetById(OldId.Text);
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
                room = roomMng.GetById(OldId.Text);
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
                desk = deskMng.GetById(OldId.Text);
                if (desk != null)
                {
                    OldName.Content = desk.Name;
                    return true;
                }
                return false;
            }
            return false;
        }     
        
        private bool getResById()
        {
            if (checks.InputCheckStringIntId(OldId.Text))
            {
                clearOldContent();
                reservation = resMng.GetById(OldId.Text);
                if (reservation != null)
                {
                    OldBuildId.Content = reservation.BuildingId;
                    OldStoreyId.Content = reservation.StoreyId;
                    OldRoomId.Content = reservation.RoomId;
                    OldDeskId.Content = reservation.DeskId;
                    OldUserId.Content = reservation.UserId;
                    OldResAt.Content = reservation.ReservedAt.ToString("d/M/yyyy");
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

        private void RemoveResList_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv == true)
            {
                var DialogResult = MessageBox.Show(msgs.ResRemove, msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    if (Listbox.SelectedItems.Count > 0)
                    {
                        bool multipleRemoved = false;
                        foreach (Reservation item in Listbox.SelectedItems)
                        {
                            if (resMng.Remove(item.Id)) multipleRemoved = multipleRemoved || true; else multipleRemoved = multipleRemoved || false;
                        }
                        if (multipleRemoved) MessageBox.Show(msgs.ResRemoved, msgs.Ok, MessageBoxButton.OK);
                    }
                }
            }
        }
    }
} 