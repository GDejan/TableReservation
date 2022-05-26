using System;
using System.Collections.Generic;
using System.Windows;
using TableReservation.Classes.Users;
using TableReservation.Database;
using TableReservation.Helpers;

namespace TableReservation.Classes.Reservations
{
    internal class ResMng
    {
        private DbResMng dbResMng = new DbResMng();
        private Checks checks = new Checks();
        private List<Reservation> reservations = new List<Reservation>();
        private Reservation reservation = new Reservation();
        private Msgs msgs = new Msgs();


        public bool NewReservation(DateTime date, SessionUser sessionuser, Building building, Storey storey, Room room, Desk desk)
        {
            if (checks.InputCheck(building.Id.ToString()) && checks.InputCheck(storey.Id.ToString()) && checks.InputCheck(room.Id.ToString()) && checks.InputCheck(desk.Id.ToString())) //check entry data
            {
                reservations = dbResMng.GetReservations(building, storey, room, desk, date); //check if table reserved on a date 

                if (reservations.Count == 0) //if is not in database -> create new entry
                {
                    reservations = dbResMng.GetReservations(sessionuser.User, date); //check if user has already table reserved on a date 
                    if (reservations.Count == 0) //if is not in database -> create new entry
                    {
                        dbResMng.NewReservation(new Reservation(building.Id, storey.Id, room.Id, desk.Id, sessionuser.User.Id, date));
                        MessageBox.Show(msgs.ResCreated, msgs.Ok, MessageBoxButton.OK);
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(msgs.DoubleRes, msgs.Error, MessageBoxButton.OK);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(msgs.ResExist, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public void RemoveReservation(int id)
        {
            //DbResMng.RemoveReservation(id);
            MessageBox.Show(msgs.ResRemoved + "->" + id.ToString(), msgs.Ok, MessageBoxButton.OK);
        }
        public List<Reservation> getAllReservations(DateTime starttime, DateTime endtime)
        {
            reservations = dbResMng.GetAllReservations(starttime, endtime); //get all storeys from a database
            if (reservations!=null)
            {
                if (reservations.Count > 0) //if is in database -> returns entries
                {
                    return reservations;
                }
                else
                {
                    MessageBox.Show(msgs.ResNotExist, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public List<Reservation> getAllReservations(DateTime starttime, User user)
        {
            reservations = dbResMng.GetAllReservations(starttime, user); //get all storeys from a database
            if (reservations != null)
            {
                if (reservations.Count > 0) //if is in database -> returns entries
                {
                    return reservations;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
        public List<Reservation> getReservations(Building building, Storey storey, Room room, Desk desk, DateTime date)
        {
            reservations = dbResMng.GetReservations(building, storey, room, desk, date); //get all storeys from a database
            if (reservations != null)
            {
                if (reservations.Count > 0) //if is in database -> returns entries
                {
                    return reservations;
                }
                else
                {
                    MessageBox.Show(msgs.ResNotExist, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }
}
