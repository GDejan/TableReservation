using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using TableReservation.Classes;
using TableReservation.Classes.Reservations;
using TableReservation.Classes.Users;
using TableReservation.Helpers;
using TableReservation.Views;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private string xmlRoomSetup = "RoomDesing.xml";

        public SessionUser SessionUser = new SessionUser();

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

        private List<Building> buildings = new List<Building>();
        private List<Storey> storeys = new List<Storey>();
        private List<Room> rooms = new List<Room>();
        private List<Desk> desks = new List<Desk>();
        private List<Reservation> reservations = new List<Reservation>();

        public UserPage(SessionUser sessionUser)
        {
            this.SessionUser = sessionUser;
            loadLists();
            InitializeComponent();
        }

        private void loadLists() 
        {
            buildings = buildMng.getAllBuilds();
            storeys = storeyMng.getAllStoreys();
            rooms = roomMng.getAllRooms();
            desks = deskMng.getAllDesks();
            reservations = resMng.getAllReservations(DateTime.Today, SessionUser.User);
        }

        private void Listbox_Loaded(object sender, RoutedEventArgs e)
        {
            Listbox.Items.Clear();
            if (reservations != null)
            {
                foreach (var item in reservations) Listbox.Items.Add(item);
            }
        }

        private void BuildingBox_loaded(object sender, RoutedEventArgs e)
        {
            BuildingBox.Items.Clear();
            if (buildings != null)
            {
                foreach (var item in buildings) BuildingBox.Items.Add(item);
            }
        }
        
        private void StoreyBox_loaded(object sender, RoutedEventArgs e)
        {
            StoreyBox.Items.Clear();
            if (storeys != null)
            {
                foreach (var item in storeys) StoreyBox.Items.Add(item);
            }
        }

        private void RoomBox_loaded(object sender, RoutedEventArgs e)
        {
            RoomBox.Items.Clear();
            if (rooms != null)
            {
                foreach (var item in rooms) RoomBox.Items.Add(item);
            }
        }

        private void BuildingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            building = (Building)BuildingBox.SelectedItem;
            updateCanvas();
        }

        private void StoreyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            storey = (Storey)StoreyBox.SelectedItem;
            updateCanvas();
        }

        private void RoomBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            room = (Room)RoomBox.SelectedItem;
            updateCanvas();
        }

        private void RemoveRes_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv == true)
            {
                var DialogResult = MessageBox.Show(msgs.ResRemove, msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    foreach (Reservation item in Listbox.SelectedItems) resMng.RemoveReservation(item.Id);
                }
            }
        }

        private void showCalendar(object sender, RoutedEventArgs e) 
        {
            Button button = sender as Button;

            DeskCalendar deskCalendar = new DeskCalendar(SessionUser,building,storey,room,new Desk(int.Parse((string)button.Tag),desk.Name));
            deskCalendar.Show();
        }

        private void updateCanvas()
        {
            if ((building.Id > 0) && (storey.Id > 0) && (room.Id > 0))
            {
                GridCanvas.Children.Clear();
                string name;
                int column;
                int columnSpan;
                int row;
                int rowSpan;

                XmlDocument xDoc = new XmlDocument();
                xDoc.Load(xmlRoomSetup);
                foreach (var deskXml in from XmlNode buildingXml in xDoc.DocumentElement.ChildNodes
                                     where buildingXml.Attributes.Item(0).Value == building.Name
                                     from XmlNode storeyXml in buildingXml
                                     where storeyXml.Attributes.Item(0).Value == storey.Name
                                     from XmlNode roomXml in storeyXml
                                     where roomXml.Attributes.Item(0).Value == room.Name
                                     from XmlNode deskXml in roomXml
                                     select deskXml)
                {
                    name = deskXml.Attributes.Item(0).Value;
                    column = int.Parse(deskXml.ChildNodes.Item(0).InnerText);
                    columnSpan = int.Parse(deskXml.ChildNodes.Item(1).InnerText);
                    row = int.Parse(deskXml.ChildNodes.Item(2).InnerText);
                    rowSpan = int.Parse(deskXml.ChildNodes.Item(3).InnerText);
                    desk.Name=name;

                    Button btnDesk = new Button();
                    btnDesk.SetValue(Grid.RowProperty, row);
                    btnDesk.SetValue(Grid.ColumnProperty, column);
                    btnDesk.SetValue(Grid.RowSpanProperty, rowSpan); 
                    btnDesk.SetValue(Grid.ColumnSpanProperty, columnSpan);
                    btnDesk.Name = string.Format(building.Name + storey.Name + room.Name + desk.Name);
                    btnDesk.Content = desk.Name;
                    btnDesk.Tag = desk.Name;
                    btnDesk.Click += showCalendar;

                    //pitaj u bazu da li je slobodan 
                    //ako je zauzet uzmi podatke tko ga je zauzeo za taj dan
                    //-> bojaj i blokiraj ovisno o tome
                    //Desk.Fill = new SolidColorBrush(Color.FromArgb(100, 0, 255, 255)); 
                    //Desk.Stroke = new SolidColorBrush(Color.FromArgb(100, 255, 255, 0)); 

                    GridCanvas.Children.Add(btnDesk);
                }
            }         
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Content = null;
            SessionUser.IsActiv = false;
            LoginPage loginPage = new LoginPage();
            this.NavigationService.Navigate(loginPage);
        }
    }
}