using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Xml;
using TableReservation.Helpers;
using TableReservation.Property;
using TableReservation.Resevations;
using TableReservation.ViewModels;

namespace TableReservation.Views
{
    /// <summary>
    /// Interaction logic for ForeCast.xaml
    /// </summary>
    public partial class ForeCast : Window
    {
        private readonly UserPage owner;
        private XmlDocument xDoc = new XmlDocument();
        private Settings settings = new Settings();
        private Checks checks = new Checks();
        private ResMng resMng = new ResMng();

        private Building building = new Building();
        private Storey storey = new Storey();

        private List<StoreyDesk> storeyDeskList = new List<StoreyDesk>();
        string[,] gridView = new string[16,100];

        public ForeCast(Building building, Storey storey, UserPage owner)
        {
            //UserPage.NoOfWindows++;
            this.owner = owner;
            this.building = building;
            this.storey = storey;
            loadList();
            InitializeComponent();
            readXMl();
            this.Title = "Overview of desk reservations for: " + building.Name + "-" + storey.Name;
            Visibility visibility = Visibility.Hidden;
            HeaderGrid.Visibility = visibility;
        }

        private void loadList()
        {
            storeyDeskList = resMng.GetResBuildStoryDate(building, storey, DateTime.Today.AddDays(14));
        }

        private void readXMl()
        {
            if (File.Exists(settings.XmlRoomSetup))
            {
                int j = 1;
                int i = 1;

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
                                    string nameRoom = roomXml.Attributes.GetNamedItem("Name").Value;

                                    if (checks.InputCheck(nameRoom))
                                    {
                                        foreach (XmlNode deskXml in roomXml)
                                        {
                                            string nameDesk = deskXml.Attributes.GetNamedItem("Name").Value;
                                            string fixedDesk = deskXml.Attributes.GetNamedItem("Fixed").Value;
                                            string deskFullName = building.Name + "-" + storey.Name + nameRoom + "-" + nameDesk;

                                            fillGridArray(ref j, ref i, nameDesk, fixedDesk, deskFullName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                drawGrid(i,j);
            }
        }

        private void fillGridArray(ref int j, ref int i, string nameDesk, string fixedDesk, string deskFullName)
        {
            gridView[0, j] = deskFullName;

            if (checks.InputCheck(nameDesk) && checks.InputCheck(fixedDesk))
            {
                if (fixedDesk == "0")
                {
                    i = 1;
                    foreach (DateTime day in EachDay(DateTime.Now.Date, DateTime.Now.Date.AddDays(13)))
                    {
                        gridView[i, 0] = string.Format(day.ToString("dd/MM/yyyy"));

                        foreach (var item in storeyDeskList)
                        {
                            if (item.DeskFullName == deskFullName)
                            {
                                if (day.Date == item.ReservedAt)
                                {
                                    gridView[i, j] = item.UserFullName;
                                }
                            }
                        }
                        i++;
                    }
                    j++;
                }
            }
        }

        private void drawGrid(int imax, int jmax)
        {
            for (int i = 0; i < imax; i++)
            {
                for (int j = 0; j < jmax; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        Button button = new Button();
                        Label label = new Label();

                        HorizontalAlignment horizontalAlignment = HorizontalAlignment.Center;
                        VerticalAlignment verticalAlignment = VerticalAlignment.Center;

                        if ((gridView[i, j] != "") && (gridView[i, j] != null))
                        {
                            if ((i == 0) || (j == 0))
                            {
                                drawHeader(i, j, label, horizontalAlignment, verticalAlignment);
                                drawOutHeader(i, j, horizontalAlignment, verticalAlignment);
                            }
                            else
                            {
                                drawOccupied(i, j, button, horizontalAlignment, verticalAlignment);
                            }
                        }
                        else
                        {
                            drawFree(i, j, button, horizontalAlignment, verticalAlignment);
                        }
                    }
                }
            }
        }

        private void drawFree(int i, int j, Button button, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            button.SetValue(Grid.RowProperty, j + 1);
            button.SetValue(Grid.ColumnProperty, i + 1);
            button.SetValue(Grid.RowSpanProperty, 1);
            button.SetValue(Grid.ColumnSpanProperty, 1);
            button.FontSize = 11;
            button.HorizontalAlignment = horizontalAlignment;
            button.VerticalAlignment = verticalAlignment;
            button.BorderThickness = new Thickness(1);
            button.Content = "";
            button.Background = new SolidColorBrush(Color.FromArgb(100, 0, 255, 0));
            ForecastGrid.Children.Add(button);
        }

        private void drawOccupied(int i, int j, Button button, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            button.SetValue(Grid.RowProperty, j + 1);
            button.SetValue(Grid.ColumnProperty, i + 1);
            button.SetValue(Grid.RowSpanProperty, 1);
            button.SetValue(Grid.ColumnSpanProperty, 1);
            button.FontSize = 11;
            button.HorizontalAlignment = horizontalAlignment;
            button.VerticalAlignment = verticalAlignment;
            button.Content = gridView[i, j];
            button.BorderThickness = new Thickness(1);
            button.Background = new SolidColorBrush(Color.FromArgb(100, 255, 0, 0));
            ForecastGrid.Children.Add(button);
        }

        private void drawHeader(int i, int j, Label label, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            label.SetValue(Grid.RowProperty, j + 1);
            label.SetValue(Grid.ColumnProperty, i + 1);
            label.SetValue(Grid.RowSpanProperty, 1);
            label.SetValue(Grid.ColumnSpanProperty, 1);
            label.FontSize = 11;
            label.HorizontalAlignment = horizontalAlignment;
            label.VerticalAlignment = verticalAlignment;
            label.BorderThickness = new Thickness(1);
            label.Content = gridView[i, j];
            ForecastGrid.Children.Add(label);

        }
        private void drawOutHeader(int i, int j, HorizontalAlignment horizontalAlignment, VerticalAlignment verticalAlignment)
        {
            Label label = new Label();
            label.SetValue(Grid.RowProperty, j + 1);
            label.SetValue(Grid.ColumnProperty, i + 1);
            label.SetValue(Grid.RowSpanProperty, 1);
            label.SetValue(Grid.ColumnSpanProperty, 1);
            label.FontSize = 11;
            label.HorizontalAlignment = horizontalAlignment;
            label.VerticalAlignment = verticalAlignment;
            label.BorderThickness = new Thickness(1);
            label.Content = gridView[i, j];
            HeaderGrid.Children.Add(label);
        }

        private IEnumerable<DateTime> EachDay(DateTime from, DateTime thru)
        {
            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
                yield return day;
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            ScrollViewer scrollViewer = (ScrollViewer)sender;
            if (scrollViewer.VerticalOffset != 0)
            {
                for (int i = 0; i < HeaderGrid.ColumnDefinitions.Count; i++)
                {
                    HeaderGrid.ColumnDefinitions[i].Width = new GridLength(ForecastGrid.ColumnDefinitions[i].ActualWidth);
                }
                Visibility visibility = Visibility.Visible;
                HeaderGrid.Visibility = visibility;
            }
            else 
            {
                Visibility visibility = Visibility.Hidden;
                HeaderGrid.Visibility = visibility;
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //UserPage.NoOfWindows--;
        }
    }
}

