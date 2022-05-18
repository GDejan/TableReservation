using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TableReservation.Classes;
using TableReservation.Classes.Reservations;
using TableReservation.Classes.Users;
using TableReservation.Helpers;

namespace TableReservation.Views
{
    /// <summary>
    /// Interaction logic for TableResWindow.xaml
    /// </summary>
    public partial class TableResWindow : Window
    {
        private ResMng ResMng = new ResMng();
        public SessionUser SessionUser = new SessionUser();
        

        public TableResWindow(SessionUser Sessionuser)
        {
            InitializeComponent();
            this.SessionUser= Sessionuser;
        }
        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            Calendar Calendar = sender as Calendar;
            Calendar.BlackoutDates.AddDatesInPast();
        }

        private void  ReserveTable_Click(object sender, RoutedEventArgs e)
        {
            SelectedDatesCollection selectedDates = Calendar.SelectedDates;
            string DeskTag = (sender as Button).Tag.ToString();
            string[] DeskTags = DeskTag.Split('-');

            

            

            foreach (var item in selectedDates)
            {
                DateOnly Date = new DateOnly(item.Date.Year, item.Date.Month, item.Date.Day);
                ResMng.NewReservation(Date, SessionUser, DeskTags);
            }

            this.Close();
        }      

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
