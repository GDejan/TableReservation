using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TableReservation.Classes;
using TableReservation.Classes.Reservations;
using TableReservation.Classes.Users;

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
        
        private List<Reservation> reservations = new List<Reservation>();

        public DeskCalendar(SessionUser sessionUser, Building building, Storey storey, Room room, Desk desk)
        {
            this.SessionUser = sessionUser;
            this.building = building;
            this.storey = storey;
            this.room = room;
            this.desk = desk;
            InitializeComponent();
        }
        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            Calendar.BlackoutDates.AddDatesInPast();

            reservations = resMng.getReservations(building, storey, room, desk, DateTime.Now);
            foreach (var item in reservations)
            {
                Calendar.BlackoutDates.Add(new CalendarDateRange(item.ReservedAt));
            }
        }
        private void ReserveTable_Click(object sender, RoutedEventArgs e)
        {
            if (SessionUser.IsActiv == true)
            {
                SelectedDatesCollection selectedDates = Calendar.SelectedDates;
                foreach (var item in selectedDates)
                {
                    DateTime Date = new DateTime(item.Date.Year, item.Date.Month, item.Date.Day);
                    resMng.NewReservation(Date, SessionUser, building, storey, room, desk);
                }
            }
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

    }
}
