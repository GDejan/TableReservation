using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
        private Settings settings = new Settings();

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
            LogedUser.Text = "Loged user: \n" + sessionUser.User.FullName();
            Forecast.IsEnabled = false;
        }

        private void loadLists()
        {
            buildings = buildMng.GetAllBuilds();
            storeys = storeyMng.GetAll();
            rooms = roomMng.GetAll();
            desks = deskMng.GetAllDesks();
        }

        public void updateListbox()
        {
            Listbox.Items.Clear();
            viewModelResUser = resMng.GetAllFuture(DateTime.Today, SessionUser.User);
            if (viewModelResUser != null)
            {
                foreach (var item in viewModelResUser) Listbox.Items.Add(item);
                CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(Listbox.Items);
                view.SortDescriptions.Add(new SortDescription("ReservedAtDate", ListSortDirection.Ascending));
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
            if (NoOfWindows == 0)
            {
                if (SessionUser.IsActiv == true)
                {
                    var DialogResult = MessageBox.Show(msgs.ResRemove, msgs.Ok, MessageBoxButton.YesNo);
                    if (DialogResult == MessageBoxResult.Yes)
                    {
                        if (Listbox.SelectedItems.Count > 0)
                        {
                            bool multipleRemoved = false;
                            foreach (ResUser item in Listbox.SelectedItems)
                            {
                                if (resMng.Remove(item.Id)) multipleRemoved = multipleRemoved || true; else multipleRemoved = multipleRemoved || false;
                            }
                            if (multipleRemoved) MessageBox.Show(msgs.ResRemoved, msgs.Ok, MessageBoxButton.OK);
                        }
                    }
                    updateListbox();
                    updateCanvas();
                }
            }
        }

        private void showCalendar(object sender, RoutedEventArgs e)
        {
            if (NoOfWindows == 0)
            {
                Image imgDesk = sender as Image;
                getDesk(imgDesk.Tag.ToString().Split('-')[2]);

                DeskCalendar deskCalendar = new DeskCalendar(SessionUser, building, storey, room, desk, this);
                deskCalendar.Show();
            }
        }

        public void updateCanvas()
        {
            if ((building.Id > 0) && (storey.Id > 0) && (room.Id > 0))
            {
                GridCanvas.Children.Clear();
                readXMl();
            }
            if ((building.Id > 0) && (storey.Id > 0))
            {
                Forecast.IsEnabled = true;
            }
        }

        private void readXMl()
        {
            if (File.Exists(settings.XmlRoomSetup))
            {
                xDoc.Load(settings.XmlRoomSetup);
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
                                        string nameRoom = roomXml.Attributes.GetNamedItem("Name").Value;
                                        string iconRoom = roomXml.Attributes.GetNamedItem("Icon").Value;
                                        string columnRoom = roomXml.Attributes.GetNamedItem("Column").Value;
                                        string columnSpanRoom = roomXml.Attributes.GetNamedItem("ColumnSpan").Value;
                                        string rowRoom = roomXml.Attributes.GetNamedItem("Row").Value;
                                        string rowSpanRoom = roomXml.Attributes.GetNamedItem("RowSpan").Value;
                                        string rotationRoom = roomXml.Attributes.GetNamedItem("Rotation").Value;
                                        
                                        drawRoom(iconRoom, columnRoom, rowRoom, columnSpanRoom, rowSpanRoom, rotationRoom);

                                        foreach (XmlNode deskXml in roomXml)
                                        {
                                            string nameDesk = deskXml.Attributes.GetNamedItem("Name").Value;
                                            string columnDesk = deskXml.Attributes.GetNamedItem("Column").Value;
                                            string columnSpanDesk = deskXml.Attributes.GetNamedItem("ColumnSpan").Value;
                                            string rowDesk = deskXml.Attributes.GetNamedItem("Row").Value;
                                            string rowSpanDesk = deskXml.Attributes.GetNamedItem("RowSpan").Value;
                                            string rotationDesk = deskXml.Attributes.GetNamedItem("Rotation").Value;
                                            string fixedDesk = deskXml.Attributes.GetNamedItem("Fixed").Value;
                                           
                                            drawDesk(nameDesk, columnDesk, rowDesk, columnSpanDesk, rowSpanDesk, rotationDesk, fixedDesk);
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

        private void drawDesk(string name, string column, string row, string columnSpan, string rowSpan, string rotation, string fixeddesk)
        {
            if (checks.InputCheck(name) && checks.InputCheck(fixeddesk))
            {
                if (getDesk(name) != null)
                {
                    Image image = new Image();

                    image = drawShape(name, column, row, columnSpan, rowSpan, rotation);
                    if (image != null)
                    {
                        if (fixeddesk == "0")
                        {
                            image.MouseDown += showCalendar;
                            user = resMng.GetResUser(building, storey, room, desk, DateTime.Now.Date);
                            if (user != null)
                            {
                                image = getBitmap(settings.DeskReservedPath, image);
                                image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                                image.ToolTip = string.Format("Desk name: {0} \nStatus: Reserved by {1}", image.Tag, user.FullName());
                            }
                            else
                            {
                                image = getBitmap(settings.DeskFreePath, image);
                                image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                                image.ToolTip = string.Format("Desk name: {0} \nStatus: Free", image.Tag);
                            }
                        }
                        else 
                        {
                            image = getBitmap(settings.DeskFixedResPath, image);
                            image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                            image.ToolTip = string.Format("Desk name: {0} \nStatus: Fixed reservation", image.Tag);
                        }
                        
                        GridCanvas.Children.Add(image);
                        drawDeskName();
                    }
                }
            }
        }

        private void drawDeskName()
        {
            Label label = new Label();
            label.SetValue(Grid.RowProperty, rowInt +2 );
            label.SetValue(Grid.ColumnProperty, columnInt+2 );
            label.SetValue(Grid.RowSpanProperty, 2);
            label.SetValue(Grid.ColumnSpanProperty, 2);

            label.FontSize = 20;
            label.Content = desk.Name;
            label.Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255));

            GridCanvas.Children.Add(label);

            columnInt = 0;
            columnSpanInt = 0;
            rowInt = 0;
            rowSpanInt = 0;
            rotationInt = 0;
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
                    desk = item;
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

        private void Forecast_Click(object sender, RoutedEventArgs e)
        {
            if (NoOfWindows == 0)
            {
                ForeCast foreCast = new ForeCast(building, storey, this);
            foreCast.Show();
            }
        }
    }
}