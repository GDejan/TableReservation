using System;
using System.Collections.Generic;
using System.Windows;
using TableReservation.Classes.Users;
using TableReservation.Database;
using TableReservation.Helpers;
using TableReservation.ViewModel;

namespace TableReservation.Classes.Reservations
{
    internal class ResMng
    {
        private DbResMng dbResMng = new DbResMng();
        private Checks checks = new Checks();
        private List<Reservation> reservations = new List<Reservation>();
        private List<UserReservation> userReservation = new List<UserReservation>();
        private List<User> users = new List<User>();        
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Add new reservation to the database
        /// </summary>
        /// <param name="date">reservation date</param>
        /// <param name="sessionuser">user as object</param>
        /// <param name="building">building as object</param>
        /// <param name="storey">storey as object</param>
        /// <param name="room">room as object</param>
        /// <param name="desk">desk as object</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool Create(DateTime date, SessionUser sessionuser, Building building, Storey storey, Room room, Desk desk)
        {
            if (checks.InputCheck(building.Id.ToString()) && checks.InputCheck(storey.Id.ToString()) && checks.InputCheck(room.Id.ToString()) && checks.InputCheck(desk.Id.ToString())) //check entry data
            {
                reservations = dbResMng.GetForDate(building, storey, room, desk, date); //check if table reserved on a date 

                if (reservations.Count == 0) //if is not in database -> create new entry
                {
                    reservations = dbResMng.GetByUserDate(sessionuser.User, date); //check if user has already table reserved on a date 
                    if (reservations.Count == 0) //if is not in database -> create new entry
                    {
                        dbResMng.Create(new Reservation(building.Id, storey.Id, room.Id, desk.Id, sessionuser.User.Id, date));
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

        /// <summary>
        /// Remove reservation from a database
        /// </summary>
        /// <param name="id">id of a reservation in database</param>
        /// /// <returns>Returns a true if succeded or false if not</returns>
        public bool Remove(int id)
        {
            reservations = dbResMng.GetById(id);

            if (reservations.Count == 1)  //if is in database -> delete entry
            {
                dbResMng.Remove(id);
                MessageBox.Show(msgs.ResRemoved + "->" + id.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else if (reservations.Count > 1)
            {
                MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
                return false;
            }
            else
            {
                MessageBox.Show(msgs.ResNoExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// list all reservations in a database for a given range
        /// </summary>
        /// <param name="starttime">start time</param>
        /// <param name="endtime">end time</param>
        /// <returns>list of reservations</returns>
        public List<Reservation> getAllReservations(DateTime starttime, DateTime endtime)
        {
            reservations = dbResMng.GetByDateRange(starttime, endtime); //get all reservations from a database
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

        /// <summary>
        /// list all reservations in a database for user in a future for displaying on a user page
        /// </summary>
        /// <param name="starttime">start time</param>
        /// <param name="user">user as object</param>
        /// <returns>list of user reservations</returns>
        public List<UserReservation> getAllFuture(DateTime starttime, User user)
        {
            userReservation = dbResMng.GetFutureForUserId(starttime, user); //get all reservations from a database
            if (userReservation != null)
            {
                if (userReservation.Count > 0) //if is in database -> returns entries
                {
                    return userReservation;
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

        /// <summary>
        /// get user of a reserved property on a given date
        /// </summary>
        /// <param name="building">building as object</param>
        /// <param name="storey">storey as object</param>
        /// <param name="room">room as object</param>
        /// <param name="desk">desk as object</param>
        /// <param name="date">reservation date</param>
        /// <returns>returns user informations</returns>
        public User getResUser(Building building, Storey storey, Room room, Desk desk, DateTime date)
        {
            users = dbResMng.GetReservationsInnerUser(building, storey, room, desk, date);
            if (users != null)
            {
                if (users.Count == 1) //if is in database -> check entry
                {
                    return users[0];
                }
                else if (users.Count >= 1)
                {
                    MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
                    return null;
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
    }
}
