using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using TableReservation.Helpers;
using TableReservation.Property;
using TableReservation.Resevations;
using TableReservation.Users;
using TableReservation.ViewModels;

namespace TableReservation.Views
{
    /// <summary>
    /// Interaction logic for DeskCalendar.xaml
    /// </summary>
    public partial class DeskCalendar : Window
    {
        public SessionUser SessionUser = new SessionUser();
        private Building building = new Building();
        private Storey storey = new Storey();
        private Room room = new Room();
        private Desk desk = new Desk();
        private ResMng resMng = new ResMng();
        private Msgs msgs = new Msgs();
        private readonly UserPage owner;

        private List<DeskUser> viewModelDeskUser = new List<DeskUser>();

        public DeskCalendar(SessionUser sessionUser, Building building, Storey storey, Room room, Desk desk, UserPage owner)
        {
            UserPage.NoOfWindows++;
            this.Top = Mouse.GetPosition(null).Y;
            this.Left = Mouse.GetPosition(null).X;
            this.SessionUser = sessionUser;
            this.building = building;
            this.storey = storey;
            this.room = room;
            this.desk = desk;
            this.owner = owner;
            InitializeComponent();
            this.Title = building.Name + "-" + storey.Name + room.Name + "-" + desk.Name;
        }
        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            Calendar.DisplayDateStart = DateTime.Now.AddMonths(-1);
            Calendar.DisplayDateEnd = DateTime.Now.AddMonths(1);

            Calendar.BlackoutDates.AddDatesInPast();

            viewModelDeskUser = resMng.GetFutDeskRes(building, storey, room, desk, DateTime.Now);
            if (viewModelDeskUser != null)
            {
                foreach (var item in viewModelDeskUser)
                {
                    Calendar.BlackoutDates.Add(new CalendarDateRange(item.ReservedAt));
                }
            }
        }

        private void ReserveTable_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv == true)
            {
                SelectedDatesCollection selectedDates = Calendar.SelectedDates;
                if (selectedDates.Count > 0)
                {
                    bool multipleCreated = false;
                    foreach (var item in selectedDates)
                    {
                        if (resMng.Create(item.Date, SessionUser, building, storey, room, desk)) multipleCreated = multipleCreated || true; else multipleCreated = multipleCreated || false;
                    }
                    if (multipleCreated)
                    {
                        owner.updateListbox();
                        owner.updateCanvas();
                        MessageBox.Show(msgs.ResCreated, msgs.Ok, MessageBoxButton.OK);
                    }
                    this.Close();
                }
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            UserPage.NoOfWindows--;
        }

        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            Mouse.Capture(null);
        }
    }
}
