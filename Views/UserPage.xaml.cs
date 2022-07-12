using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Xml;
using TableReservation.Helpers;
using TableReservation.Property;
using TableReservation.Resevations;
using TableReservation.Users;
using TableReservation.ViewModels;
using TableReservation.Views;
using System.Linq;

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
        
        private int level;
        public static int NoOfWindows = 0;

        public UserPage(SessionUser sessionUser)
        {
            this.SessionUser = sessionUser;
            loadLists();
            InitializeComponent();
            updateListbox();
            LogedUser.Text = "Loged user: \n" + sessionUser.User.FullName();
            Forecast.IsEnabled = false;
            readXMlBuilding();
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
                xmlRoomDeskRead();
            }
        }

        private void readXMlBuilding()
        {
            level = 1;
            Forecast.IsEnabled = false;
            if (File.Exists(settings.XmlRoomSetup))
            {
                xDoc.Load(settings.XmlRoomSetup);
                GridCanvas.Children.Clear();

                string nameProperty = xDoc.DocumentElement.Attributes.GetNamedItem("Name").Value;
                string iconProperty = xDoc.DocumentElement.Attributes.GetNamedItem("Icon").Value;
                string columnProperty = xDoc.DocumentElement.Attributes.GetNamedItem("Column").Value;
                string columnSpanProperty = xDoc.DocumentElement.Attributes.GetNamedItem("ColumnSpan").Value;
                string rowProperty = xDoc.DocumentElement.Attributes.GetNamedItem("Row").Value;
                string rowSpanProperty = xDoc.DocumentElement.Attributes.GetNamedItem("RowSpan").Value;
                string rotationProperty = xDoc.DocumentElement.Attributes.GetNamedItem("Rotation").Value;

                drawProperty(nameProperty, iconProperty, columnProperty, rowProperty, columnSpanProperty, rowSpanProperty, rotationProperty);

                foreach (XmlNode buildingXml in xDoc.DocumentElement.ChildNodes)
                {
                    string nameBuilding = buildingXml.Attributes.GetNamedItem("Name").Value;
                    string iconBuilding = buildingXml.Attributes.GetNamedItem("Icon").Value;
                    string columnBuilding = buildingXml.Attributes.GetNamedItem("Column").Value;
                    string columnSpanBuilding = buildingXml.Attributes.GetNamedItem("ColumnSpan").Value;
                    string rowBuilding = buildingXml.Attributes.GetNamedItem("Row").Value;
                    string rowSpanBuilding = buildingXml.Attributes.GetNamedItem("RowSpan").Value;
                    string rotationBuilding = buildingXml.Attributes.GetNamedItem("Rotation").Value;

                    drawBuilding(nameBuilding, iconBuilding, columnBuilding, rowBuilding, columnSpanBuilding, rowSpanBuilding, rotationBuilding);
                }
            }
        }

        private void readXMlFloor(object sender, RoutedEventArgs e)
        {            
            Image imgFloor = sender as Image;
            getBuilding(imgFloor);

            XmlFloorRead();
        }

        private void XmlFloorRead()
        {
            if (File.Exists(settings.XmlRoomSetup))
            {
                level = 2;
                Forecast.IsEnabled = false;
                xDoc.Load(settings.XmlRoomSetup);
                foreach (XmlNode buildingXml in xDoc.DocumentElement.ChildNodes)
                {
                    if (buildingXml.Attributes.GetNamedItem("Name").Value == building.Name)
                    {
                        GridCanvas.Children.Clear();
                        foreach (XmlNode storeyXml in buildingXml)
                        {
                            string nameFloor = storeyXml.Attributes.GetNamedItem("Name").Value;
                            string iconFloor = storeyXml.Attributes.GetNamedItem("Icon").Value;
                            string columnFloor = storeyXml.Attributes.GetNamedItem("Column").Value;
                            string columnSpanFloor = storeyXml.Attributes.GetNamedItem("ColumnSpan").Value;
                            string rowFloor = storeyXml.Attributes.GetNamedItem("Row").Value;
                            string rowSpanFloor = storeyXml.Attributes.GetNamedItem("RowSpan").Value;
                            string rotationFloor = storeyXml.Attributes.GetNamedItem("Rotation").Value;

                            drawFloor(nameFloor, iconFloor, columnFloor, rowFloor, columnSpanFloor, rowSpanFloor, rotationFloor);
                        }
                        break;
                    }
                }
            }
        }

        private void readXMlStorey(object sender, RoutedEventArgs e)
        {
            Image imgStorey = sender as Image;
            getStorey(imgStorey);
            XMlStoreyRead();
        }

        private void XMlStoreyRead()
        {
            if (File.Exists(settings.XmlRoomSetup))
            {
                level = 3;
                Forecast.IsEnabled = true;
                xDoc.Load(settings.XmlRoomSetup);
                foreach (XmlNode buildingXml in xDoc.DocumentElement.ChildNodes)
                {
                    if (buildingXml.Attributes.GetNamedItem("Name").Value == building.Name)
                    {
                        foreach (XmlNode storeyXml in buildingXml)
                        {
                            if (storeyXml.Attributes.GetNamedItem("Name").Value == storey.Name)
                            {
                                GridCanvas.Children.Clear();
                                foreach (XmlNode roomXml in storeyXml)
                                {
                                    string nameRoom = roomXml.Attributes.GetNamedItem("Name").Value;
                                    string iconRoom = roomXml.Attributes.GetNamedItem("Icon").Value;
                                    string columnRoom = roomXml.Attributes.GetNamedItem("ColumnS").Value;
                                    string columnSpanRoom = roomXml.Attributes.GetNamedItem("ColumnSpanS").Value;
                                    string rowRoom = roomXml.Attributes.GetNamedItem("RowS").Value;
                                    string rowSpanRoom = roomXml.Attributes.GetNamedItem("RowSpanS").Value;
                                    string rotationRoom = roomXml.Attributes.GetNamedItem("RotationS").Value;

                                    drawStorey(nameRoom, iconRoom, columnRoom, rowRoom, columnSpanRoom, rowSpanRoom, rotationRoom);
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void readXMlRoomDesk(object sender, RoutedEventArgs e)
        {
            Image imgRoom = sender as Image;
            getRoom(imgRoom);
            xmlRoomDeskRead();
        }

        private void xmlRoomDeskRead() 
        {
            if (File.Exists(settings.XmlRoomSetup))
            {
                level = 4;
                Forecast.IsEnabled = true;
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
                                        GridCanvas.Children.Clear();

                                        string nameRoom = roomXml.Attributes.GetNamedItem("Name").Value;
                                        string iconRoom = roomXml.Attributes.GetNamedItem("Icon").Value;
                                        string columnRoom = roomXml.Attributes.GetNamedItem("Column").Value;
                                        string columnSpanRoom = roomXml.Attributes.GetNamedItem("ColumnSpan").Value;
                                        string rowRoom = roomXml.Attributes.GetNamedItem("Row").Value;
                                        string rowSpanRoom = roomXml.Attributes.GetNamedItem("RowSpan").Value;
                                        string rotationRoom = roomXml.Attributes.GetNamedItem("Rotation").Value;

                                        drawRoom(nameRoom, iconRoom, columnRoom, rowRoom, columnSpanRoom, rowSpanRoom, rotationRoom);

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
                                        break;
                                    }
                                }
                                break;
                            }
                        }
                        break;
                    }
                }
            }
        }

        private void drawProperty(string name, string iconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            lblStatus.Content = "";
            RoomCanvas.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            RoomCanvas.ScrollToHorizontalOffset(0);
            Image image = new Image();
            image = drawShape(name, iconPath, column, row, columnSpan, rowSpan, rotation);
            if (image != null)
            {
                GridCanvas.Children.Add(image);
            }
        } 
        
        private void drawBuilding(string name, string iconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            lblStatus.Content = "";
            RoomCanvas.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            RoomCanvas.ScrollToHorizontalOffset(0);
            Image image = new Image();
            image = drawShape(name, iconPath, column, row, columnSpan, rowSpan, rotation);
            if (image != null)
            {
                image.ToolTip = "Building name: " + name;
                foreach (var item in buildings) 
                {
                    if (item.Name== name)
                    {
                        image.MouseLeftButtonDown+= readXMlFloor;
                        image.MouseEnter += zoomInImage;
                        image.MouseLeave += zoomOutImage;
                        break;
                    }
                }             
                GridCanvas.Children.Add(image);
                //drawName(name, image.ToolTip.ToString());
            }
        }

        private void drawFloor(string name, string iconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            lblStatus.Content = "Building: " + building.Name;
            RoomCanvas.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            RoomCanvas.ScrollToHorizontalOffset(0);
            Image image = new Image();
            image = drawShape(name, iconPath, column, row, columnSpan, rowSpan, rotation);
            if (image != null)
            {
                image.ToolTip = "Floor: " + name;
                foreach (var item in storeys)
                {
                    if (item.Name == name)
                    {
                        image.MouseLeftButtonDown += readXMlStorey;
                        image.MouseEnter += zoomInImage;
                        image.MouseLeave += zoomOutImage;
                        break;
                    }
                }
                
                GridCanvas.Children.Add(image);
                drawName(name, image.ToolTip.ToString());
            }
        }

        private void drawStorey(string name, string iconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            lblStatus.Content = "Building: " + building.Name + " Floor: " + storey.Name;
            RoomCanvas.HorizontalScrollBarVisibility = ScrollBarVisibility.Auto;
            Image image = new Image();
            image = drawShape(name, iconPath, column, row, columnSpan, rowSpan, rotation);
            if (image != null)
            {
                image.ToolTip = "Room: " + name;
                foreach (var item in rooms)
                {
                    if (item.Name == name)
                    {
                        image.MouseLeftButtonDown += readXMlRoomDesk;
                        image.MouseEnter += zoomInImage;
                        image.MouseLeave += zoomOutImage;
                        break;
                    }
                }
                
                GridCanvas.Children.Add(image);
                //drawName(name, image.ToolTip.ToString());
            }
        }

        private void drawRoom(string name, string iconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
        {
            lblStatus.Content = "Building: " + building.Name + " Floor: " + storey.Name + " Room: " + room.Name;
            RoomCanvas.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
            RoomCanvas.ScrollToHorizontalOffset(0);
            Image image = new Image();
            image = drawShape(name, iconPath, column, row, columnSpan, rowSpan, rotation);
            if (image != null)
            {
                image.Tag = building.Name + "-" + storey.Name + room.Name;
                image.ToolTip = "Room: " + name;

                GridCanvas.Children.Add(image);
                //drawName(name, image.ToolTip.ToString());
            }
        }

        private void drawDesk(string name, string column, string row, string columnSpan, string rowSpan, string rotation, string fixeddesk)
        {
            string tooltip;
            Image image = new Image();
            image = drawShape(name, "", column, row, columnSpan, rowSpan, rotation);
            getDesk(name);
            if (image != null)
            {
                foreach (var item in desks)
                {
                    if (item.Name == name)
                    {
                        tooltip = "";
                        if (fixeddesk == "0")
                        {
                            image.MouseLeftButtonDown += showCalendar;
                            user = resMng.GetResUser(building, storey, room, desk, DateTime.Now.Date);
                            if (user != null)
                            {
                                image = getBitmap(settings.DeskReservedPath, image);
                                image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                                tooltip = string.Format("Desk name: {0} \nStatus: Reserved by {1}", image.Tag, user.FullName());
                                image.ToolTip = tooltip;
                                image.MouseEnter += zoomInImage;
                                image.MouseLeave += zoomOutImage;
                            }
                            else
                            {
                                image = getBitmap(settings.DeskFreePath, image);
                                image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                                tooltip = string.Format("Desk name: {0} \nStatus: Free", image.Tag);
                                image.ToolTip = tooltip;
                                image.MouseEnter += zoomInImage;
                                image.MouseLeave += zoomOutImage;
                            }
                        }
                        else
                        {
                            image = getBitmap(settings.DeskFixedResPath, image);
                            image.Tag = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
                            tooltip = string.Format("Desk name: {0} \nStatus: Fixed reservation", image.Tag);
                            image.ToolTip = tooltip;
                        }
                        GridCanvas.Children.Add(image);

                        drawName(name,tooltip);

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

        private Image drawShape(string name, string iconPath, string column, string row, string columnSpan, string rowSpan, string rotation)
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
                image = getBitmap(iconPath, image);

                image.Stretch = Stretch.Fill;
                image.Tag = name;

                return image;
            }
            return null;
        }

    
        private void drawName(string name, string tooltip)
        {
            Label label = new Label();
            label.SetValue(Grid.RowProperty, rowInt + rowSpanInt / 2);
            label.SetValue(Grid.ColumnProperty, columnInt + columnSpanInt / 2);
            label.SetValue(Grid.RowSpanProperty, 2);
            label.SetValue(Grid.ColumnSpanProperty, 2);
            label.Margin = new Thickness(0, -10, 0, 0);
            label.ToolTip = tooltip;
            label.FontSize = 20;
            label.Content = name;
            label.Foreground = new SolidColorBrush(Color.FromRgb(0, 0, 0));

            GridCanvas.Children.Add(label);

            columnInt = 0;
            columnSpanInt = 0;
            rowInt = 0;
            rowSpanInt = 0;
            rotationInt = 0;
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

        private void getBuilding(Image image)
        {
            foreach (var item in from item in buildings
                                 where item.Name == image.Tag.ToString()
                                 select item)
            {
                building = item;
                break;
            }
        }
        private void getStorey(Image image)
        {
            foreach (var item in from item in storeys
                                 where item.Name == image.Tag.ToString()
                                 select item)
            {
                storey = item;
                break;
            }
        }
        private void getRoom(Image image)
        {
            foreach (var item in from item in rooms
                                 where item.Name == image.Tag.ToString()
                                 select item)
            {
                room = item;
                break;
            }
        }

        private void getDesk(string name)
        {
            foreach (var item in from item in desks
                                 where item.Name == name
                                 select item)
            {
                desk = item;
                break;
            }
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
            //if (NoOfWindows == 0)
            //{
            ForeCast foreCast = new ForeCast(building, storey);
            foreCast.Show();
            //}
        }
        private void zoomInImage(object sender, RoutedEventArgs e)
        {
            Image image = sender as Image;            
            image.SetValue(Grid.RowSpanProperty, Grid.GetRowSpan(image)+1);
            image.SetValue(Grid.ColumnSpanProperty, Grid.GetColumnSpan(image)+1);
            image.Margin = new Thickness(-10, -10, 0, 0);
            Panel.SetZIndex(image, 5);
        }    
        private void zoomOutImage(object sender, RoutedEventArgs e)
        {
            Image image = sender as Image;            
            image.SetValue(Grid.RowSpanProperty, Grid.GetRowSpan(image)-1);
            image.SetValue(Grid.ColumnSpanProperty, Grid.GetColumnSpan(image)-1);
            image.Margin = new Thickness(0, 0, 0, 0);
            Panel.SetZIndex(image, 0);
        }

        void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Refresh)
                e.Cancel = true;
            if (e.NavigationMode == NavigationMode.Back)
                e.Cancel = true;
            if (e.NavigationMode == NavigationMode.Forward)
                e.Cancel = true;
        }

        private void goBack_Click(object sender, MouseButtonEventArgs e)
        {
            if (NoOfWindows == 0)
            {
                switch (level)
                {
                    case 2:
                        readXMlBuilding();
                        break;
                    case 3:
                        XmlFloorRead();
                        break;
                    case 4:
                        XMlStoreyRead();
                        break;
                    default:
                        break;
                }
            }
        }
    }
}