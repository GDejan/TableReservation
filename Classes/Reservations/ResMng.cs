using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TableReservation.Database;
using TableReservation.Helpers;
using TableReservation.Classes.Users;

namespace TableReservation.Classes.Reservations
{
    internal class ResMng
    {
        private DbResMng DbResMng = new DbResMng();
        private Checks Checks = new Checks();
        private List<Reservation> Reservations = new List<Reservation>();
        private Reservation Reservation = new Reservation();
        private Msgs Msgs = new Msgs();
        public ResMng()
        {
        }
        public bool NewReservation(DateOnly date, SessionUser sessionuser, string[] deskTags) 
        {  
            if (Checks.InputCheck(deskTags[0]) && Checks.InputCheck(deskTags[1]) && Checks.InputCheck(deskTags[2]) && Checks.InputCheck(deskTags[3])) //check entry data
            {
                Reservations = DbResMng.GetReservations(deskTags, date); //check if table reserved on a date 
                if (Reservations.Count == 0) //if is not in database -> create new entry
                {
                    Reservations = DbResMng.GetReservations(sessionuser.User.Username.ToString(), date); //check if user has 2 table reserved on a date 
                    if (Reservations.Count == 0) //if is not in database -> create new entry
                    {
                        DbResMng.NewReservation(Reservation);
                        MessageBox.Show(Msgs.ResCreated, Msgs.Ok, MessageBoxButton.OK);
                        return true;
                    }
                    else 
                    {
                        return false;
                        MessageBox.Show(Msgs.DoubleRes, Msgs.Error, MessageBoxButton.OK);
                    }                    
                }
                else
                {
                    MessageBox.Show(Msgs.ResExist, Msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
}
