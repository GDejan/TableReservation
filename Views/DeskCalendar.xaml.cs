using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TableReservation.ViewModels;
using TableReservation.Users;
using TableReservation.Property;
using TableReservation.Resevations;
using TableReservation.Helpers;

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
            this.SessionUser = sessionUser;
            this.building = building;
            this.storey = storey;
            this.room = room;
            this.desk = desk;
            this.owner = owner;
            InitializeComponent();
        }
        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            Calendar.BlackoutDates.AddDatesInPast();
            
            viewModelDeskUser = resMng.getFutDeskRes(building, storey, room, desk, DateTime.Now);
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
                if (selectedDates.Count>0)
                {
                    bool multipleCreated = false;
                    foreach (var item in selectedDates)
                    {
                        if (resMng.Create(item.Date, SessionUser, building, storey, room, desk))
                        {
                            multipleCreated = multipleCreated || true;
                        }
                        else
                        {
                            multipleCreated = multipleCreated || false;
                        }
                    }
                    if (multipleCreated)
                    {
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
            owner.updateListbox();
        }
    }
}
