using System;
using System.Collections.Generic;
using System.Windows;
using TableReservation.Database;
using TableReservation.Helpers;
using TableReservation.Property;
using TableReservation.Users;
using TableReservation.ViewModels;

namespace TableReservation.Resevations
{
    internal class ResMng
    {
        private DbResMng dbResMng = new DbResMng();
        private Checks checks = new Checks();
        private List<Reservation> reservations = new List<Reservation>();
        private List<ResUser> viewModelResUser = new List<ResUser>();
        private List<DeskUser> viewModelDeskUser = new List<DeskUser>();
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
                    reservations = dbResMng.GetByUserDate(sessionuser.User, date); //check if user has already another table reserved on a date 
                    if (reservations.Count == 0) //if is not in database -> create new entry
                    {
                        dbResMng.Create(new Reservation(building.Id, storey.Id, room.Id, desk.Id, sessionuser.User.Id, date));
                        return true;
                    }
                    else
                    {
                        MessageBox.Show(msgs.DoubleRes + "->" + date.Date, msgs.Error, MessageBoxButton.OK);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show(msgs.ResExist + "->" + date.Date, msgs.Error, MessageBoxButton.OK);
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

                return true;
            }
            else if (reservations.Count > 1)
            {
                MessageBox.Show(msgs.Wrong + "->" + msgs.ManyIds, msgs.Error, MessageBoxButton.OK);
                return false;
            }
            else
            {
                MessageBox.Show(msgs.ResNoExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// list reservations in a database for a given date range
        /// </summary>
        /// <param name="starttime">start time</param>
        /// <param name="endtime">end time</param>
        /// <returns>list of reservations</returns>
        public List<Reservation> GetResByDateRange(DateTime starttime, DateTime endtime)
        {
            reservations = dbResMng.GetByDateRange(starttime, endtime); //get all reservations from a database
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

        /// <summary>
        /// list all reservations in a database for user in a future for displaying on a user page
        /// </summary>
        /// <param name="starttime">start time</param>
        /// <param name="user">user as object</param>
        /// <returns>list of user reservations</returns>
        public List<ResUser> GetAllFuture(DateTime starttime, User user)
        {
            viewModelResUser = dbResMng.GetFutureForUserId(starttime, user); //get all reservations from a database
            if (viewModelResUser != null)
            {
                if (viewModelResUser.Count > 0) //if is in database -> returns entries
                {
                    return viewModelResUser;
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
        public User GetResUser(Building building, Storey storey, Room room, Desk desk, DateTime date)
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
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ManyIds, msgs.Error, MessageBoxButton.OK);
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

        /// <summary>
        /// list all reservations in a database from a given range by a desk
        /// </summary>
        /// <param name="building">building as object</param>
        /// <param name="storey">storey as object</param>
        /// <param name="room">room as object</param>
        /// <param name="desk">desk as object</param>
        /// <param name="starttime">start time</param>
        /// <returns>list of reservations</returns>
        public List<DeskUser> GetFutDeskRes(Building building, Storey storey, Room room, Desk desk, DateTime starttime)
        {
            viewModelDeskUser = dbResMng.GetDeskDateFut(building, storey, room, desk, starttime);
            if (viewModelDeskUser != null)
            {
                if (viewModelDeskUser.Count > 0) //if is in database -> returns entries
                {
                    return viewModelDeskUser;
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
        ///  list all reservations in a database
        /// </summary>
        /// <returns>list of reservations</returns>
        public List<Reservation> GetAll()
        {
            reservations = dbResMng.GetAll(); //get all reservations from a database
            if (reservations != null)
            {
                if (reservations.Count > 0) //if is in database -> returns entries
                {
                    return reservations;
                }
                else
                {
                    MessageBox.Show(msgs.ResDontExist, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// returns a reservation by id
        /// </summary>
        /// <param name="id">id of a reservation in databas</param>
        /// <returns>>returned reservation object</returns>
        public Reservation GetById(string id)
        {
            if (checks.InputCheckStringIntId(id))
            {
                reservations = dbResMng.GetById(int.Parse(id)); //check if reservation is in database3
                if (reservations != null)
                {
                    if (reservations.Count == 1) //if is in database -> check entry
                    {
                        return reservations[0];
                    }
                    else if (reservations.Count >= 1)
                    {
                        MessageBox.Show(msgs.Wrong + "->" + msgs.ManyIds, msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(msgs.ResDontExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
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
