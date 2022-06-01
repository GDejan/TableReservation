using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using TableReservation.Helpers;
using TableReservation.Property;
using TableReservation.Resevations;
using TableReservation.Users;
using TableReservation.ViewModels;
using TableReservation.Views;

namespace TableReservation
{
    /// <summary>
    /// Interaction logic for UserPage.xaml
    /// </summary>
    public partial class UserPage : Page
    {
        private string xmlRoomSetup = @"./Design/RoomDesing.xml";
        private string deskReservedPath = @"/Icons/DeskReserved.jpg";
        private string deskFreePath = @"/Icons/DeskFree.jpg";

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
        private User user = new User();
        private Checks checks = new Checks();

        private List<Building> buildings = new List<Building>();
        private List<Storey> storeys = new List<Storey>();
        private List<Room> rooms = new List<Room>();
        private List<Desk> desks = new List<Desk>();
        private List<ResUser> viewModelResUser = new List<ResUser>();
        private XmlDocument xDoc = new XmlDocument();

        private int columnInt;
        private int columnSpanInt;
        private int rowInt;
        private int rowSpanInt;
        private int rotationInt;

        public static int NoOfWindows = 0;

        public UserPage(SessionUser sessionUser)
        {
            this.SessionUser = sessionUser;
            loadLists();
            InitializeComponent();
            updateListbox();
        }

        private void loadLists()
        {
            buildings = buildMng.getAllBuilds();
            storeys = storeyMng.getAll();
            rooms = roomMng.getAll();
            desks = deskMng.getAllDesks();
        }

        private void updateListbox()
        {
            Listbox.Items.Clear();
            viewModelResUser = resMng.getAllFuture(DateTime.Today, SessionUser.User);
            if (viewModelResUser != null)
            {
                foreach (var item in viewModelResUser) Listbox.Items.Add(item);
            }
        }

        private void buildingBox_loaded(object sender, RoutedEventArgs e)
        {
            BuildingBox.Items.Clear();
            if (buildings != null)
            {
                foreach (var item in buildings) BuildingBox.Items.Add(item);
            }
        }

        private void storeyBox_loaded(object sender, RoutedEventArgs e)
        {
            StoreyBox.Items.Clear();
            if (storeys != null)
            {
                foreach (var item in storeys) StoreyBox.Items.Add(item);
            }
        }

        private void roomBox_loaded(object sender, RoutedEventArgs e)
        {
            RoomBox.Items.Clear();
            if (rooms != null)
            {
                foreach (var item in rooms) RoomBox.Items.Add(item);
            }
        }

        private void buildingBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            building = (Building)BuildingBox.SelectedItem;
            updateCanvas();
        }

        private void storeyBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            storey = (Storey)StoreyBox.SelectedItem;
            updateCanvas();
        }

        private void roomBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            room = (Room)RoomBox.SelectedItem;
            updateCanvas();
        }

        private void removeRes_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv == true)
            {
                var DialogResult = MessageBox.Show(msgs.ResRemove, msgs.Ok, MessageBoxButton.YesNo);
                if (DialogResult == MessageBoxResult.Yes)
                {
                    foreach (ResUser item in Listbox.SelectedItems) resMng.Remove(item.Id);
                    updateListbox();
                }
            }
        }

        private void showCalendar(object sender, RoutedEventArgs e)
        {
            if (NoOfWindows==0)
            {
                GridCanvas.IsEnabled = false;

                Image imgDesk = sender as Image;
                string test = imgDesk.Tag.ToString().Split('-')[2];
                getDesk(test);

                DeskCalendar deskCalendar = new DeskCalendar(SessionUser, building, storey, room, desk);
                deskCalendar.Show();
                updateListbox();

                GridCanvas.IsEnabled = true;
            }
        }

        private void updateCanvas()
        {
            if ((building.Id > 0) && (storey.Id > 0) && (room.Id > 0))
            {
                GridCanvas.Children.Clear();
                readXMl();
            }
        }

        private void readXMl()
        {
            if (File.Exists(xmlRoomSetup))
            {
                xDoc.Load(xmlRoomSetup);
                foreach (XmlNode buildingXml in xDoc.DocumentElement.ChildNodes)
                {
                    if (buildingXml.Attributes.GetNamedItem("Name").Value == building.Name)
                    {
                        foreach (XmlNode storeyXml in buildingXml)
                        {
                            if (storeyXml.Attributes.GetNamedItem("Name").Value == storey.Name)
                            {
                                foreach (XmlNode roomXml in storeyXml)
                                {
                                    if (roomXml.Attributes.GetNamedItem("Name").Value == room.Name)
                                    {
                                        string nameRoom = "";
                                        string iconRoom = "";
                                        string columnRoom = "";
                                        string columnSpanRoom = "";
                                        string rowRoom = "";
                                        string rowSpanRoom = "";
                                        string rotationRoom = "";
                                        foreach (XmlAttribute item in roomXml.Attributes)
                                        {
                                            if ((item.Name) == "Name") nameRoom = item.Value;
                                            if ((item.Name) == "Icon") iconRoom = item.Value;
                                            if ((item.Name) == "Column") columnRoom = item.Value;
                                            if ((item.Name) == "ColumnSpan") columnSpanRoom = item.Value;
                                            if ((item.Name) == "Row") rowRoom = item.Value;
                                            if ((item.Name) == "RowSpan") rowSpanRoom = item.Value;
                                            if ((item.Name) == "Rotation") rotationRoom = item.Value;
                                        }
                                        drawRoom(iconRoom, columnRoom, rowRoom, columnSpanRoom, rowSpanRoom, rotationRoom);

                                        foreach (XmlNode deskXml in roomXml)
                                        {
                                            string nameDesk = "";
                                            string columnDesk = "";
                                            string columnSpanDesk = "";
                                            string rowDesk = "";
                                            string rowSpanDesk = "";
                                            string rotationDesk = "";
                                            foreach (XmlAttribute item in deskXml.Attributes)
                                            {
                                                if ((item.Name) == "Name") nameDesk = item.Value;
                                                if ((item.Name) == "Column") columnDesk = item.Value;
                                                if ((item.Name) == "ColumnSpan") columnSpanDesk = item.Value;
                                                if ((item.Name) == "Row") rowDesk = item.Value;
                                                if ((item.Name) == "RowSpan") rowSpanDesk = item.Value;
                                                if ((item.Name) == "Rotation") rotationDesk = item.Value;
                                            }
                                            drawDesk(nameDesk, columnDesk, rowDesk, columnSpanDesk, rowSpanDesk, rotationDesk);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void drawBuilding(string buildingIconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            Image image = new Image();
            image = drawShape(building.Name, column, row, columnSpan, rowSpan, rotation);
            if (image != null)
            {
                image = getBitmap(buildingIconPath, image);
                image.Stretch = Stretch.Fill;
                image.Tag = building.Name;
                GridCanvas.Children.Add(image);
            }
        }        
        private void drawStorey(string storeyIconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            Image image = new Image();
            image = drawShape(storey.Name, column, row, columnSpan, rowSpan, rotation);
            if (image != null)
            {
                image = getBitmap(storeyIconPath, image);
                image.Stretch = Stretch.Fill;
                image.Tag = building.Name + "-" + storey.Name;
                GridCanvas.Children.Add(image);
            }
        }

        private void drawRoom(string roomIconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            Image image = new Image();
            image = drawShape(room.Name, column, row, columnSpan, rowSpan, rotation);

            if (image != null)
            {
                image = getBitmap(roomIconPath, image);
                image.Stretch = Stretch.Fill;
                image.Tag = building.Name + "-" + storey.Name + room.Name;
                GridCanvas.Children.Add(image);
            }
        }

        private void drawDesk(string name, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            if (checks.InputCheck(name)) 
            {
                if (getDesk(name) != null)
                {
                    Image image = new Image();
                    
                    image = drawShape(name, column, row, columnSpan, rowSpan, rotation);
                    image.MouseDown += showCalendar;
                    if (image != null)
                    {
                        user = resMng.getResUser(building, storey, room, desk, DateTime.Now.Date);
                        if (user != null)
                        {
                            image = getBitmap(deskReservedPath, image);
                            image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                            image.ToolTip = string.Format("Desk name: {0} \nStatus: Reserved by {1}", image.Tag, user.FullName());
                        }
                        else
                        {
                            image = getBitmap(deskFreePath, image);
                            image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                            image.ToolTip = string.Format("Desk name: {0} \nStatus: Free", image.Tag);
                        }
                        GridCanvas.Children.Add(image);
                    }
                }
            }
        }

        private Image getBitmap(string url, Image image)
        {
            BitmapImage bitmapImage = new BitmapImage();
            image.SetValue(Image.SourceProperty, bitmapImage);
            image.SetValue(Image.VisibilityProperty, Visibility.Visible);

            bitmapImage.BeginInit();
            bitmapImage.UriSource = new Uri(url, UriKind.Relative);
            bitmapImage.EndInit();

            return image;
        }

        private Image drawShape(string name, string column, string row, string columnSpan, string rowSpan, string rotation) 
        {
            if (checkInput(name, column, row, columnSpan, rowSpan, rotation))
            {
                Image image = new Image();
                image.SetValue(Grid.RowProperty, rowInt);
                image.SetValue(Grid.ColumnProperty, columnInt);
                image.SetValue(Grid.RowSpanProperty, rowSpanInt);
                image.SetValue(Grid.ColumnSpanProperty, columnSpanInt);

                RotateTransform rotateTransform = new RotateTransform(rotationInt);
                image.RenderTransform = rotateTransform;
                image.RenderTransformOrigin = new Point(0.5, 0.5);
                
                columnInt = 0;
                columnSpanInt = 0;
                rowInt = 0;
                rowSpanInt = 0;
                rotationInt = 0;

                return image;
            }
            return null;
        }

        private bool checkInput(string name, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            if (checks.InputCheck(name) && checks.InputCheckStringIntId(column) && checks.InputCheckStringIntId(row) && checks.InputCheckStringIntId(columnSpan) && checks.InputCheckStringIntId(rowSpan) && checks.InputCheckStringInt(rotation))
            {
                columnInt = int.Parse(column);
                columnSpanInt = int.Parse(columnSpan);
                rowInt = int.Parse(row);
                rowSpanInt = int.Parse(rowSpan);
                rotationInt = int.Parse(rotation);
                return true;
            }
            return false;
        }

        private Desk getDesk(string name)
        {
            foreach (var item in desks)
            {
                if (item.Name == name)
                {
                    desk=item;
                    return desk;
                }
            }
            return null;
        } 
        private void changePass_Click(object sender, RoutedEventArgs e)
        {
            if (NoOfWindows == 0)
            {
                ChangePassword changePassword = new ChangePassword(SessionUser);
                changePassword.Show();
            }
        }
        
        private void logout_Click(object sender, RoutedEventArgs e)
        {
            if (NoOfWindows == 0)
            {
                this.Content = null;
                SessionUser.IsActiv = false;
                LoginPage loginPage = new LoginPage();
                this.NavigationService.Navigate(loginPage);
            }
        }
    }
}