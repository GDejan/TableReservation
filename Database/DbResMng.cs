using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using TableReservation.Helpers;
using TableReservation.Property;
using TableReservation.Resevations;
using TableReservation.Users;
using TableReservation.ViewModels;

namespace TableReservation.Database
{
    internal class DbResMng
    {
        private Msgs msgs = new Msgs();
        private Queries queries = new Queries();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="reservation">new object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Create(Reservation reservation)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procNewReservation, reservation);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResCreateErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for change item in a database
        /// </summary>
        /// <param name="reservation">change object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Change(Reservation reservation)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procChangeReservation, reservation);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResChangeErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for removing item from a database
        /// </summary>
        /// <param name="id">remove object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Remove(int id)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procRemoveReservation, new { Id=id});
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResRemoveErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Id
        /// </summary>
        /// <param name="id">id of search object</param>
        /// <returns>list of objects</returns>
        public List<Reservation> GetById(int id)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Reservation>(queries.procGetResById, new { Id = id }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by UserId on a given date
        /// </summary>
        /// <param name="user">search object</param>
        /// <param name="reservedAt">search date</param>
        /// <returns>list of objects</returns>
        public List<Reservation> GetByUserDate(User user, DateTime reservedAt)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Reservation>(queries.procGetResByUserIdDate, new { UserId = user.Id, ReservedAt = reservedAt }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database on a given date
        /// </summary>
        /// <param name="building">search object</param>
        /// <param name="storey">search object</param>
        /// <param name="room">search object</param>
        /// <param name="desk">search object</param>
        /// <param name="date">search object</param>
        /// <returns>list of objects</returns>
        public List<Reservation> GetForDate(Building building, Storey storey, Room room, Desk desk, DateTime date)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Reservation>(queries.procGetResForDate,
                    new { BuildingId = building.Id, StoreyId = storey.Id, RoomId = room.Id, DeskId = desk.Id, ReservedAt = date.Date }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database on for a given date range
        /// </summary>
        /// <param name="reservedAtStart">start date</param>
        /// <param name="reservedAtEnd">end date</param>
        /// <returns>list of objects</returns>
        public List<Reservation> GetByDateRange(DateTime reservedAtStart, DateTime reservedAtEnd)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Reservation>(queries.procGetResForRange, new { ReservedAtStart = reservedAtStart, ReservedAtEnd = reservedAtEnd }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database for a user from a selected date
        /// </summary>
        /// <param name="reservedAtStart">start date</param>
        /// <param name="user">search object</param>
        /// <returns>list of objects</returns>
        public List<ResUser> GetFutureForUserId(DateTime reservedAtStart, User user)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<ResUser>(queries.procGetFutureResByUserId, new { UserId = user.Id, ReservedAtStart = reservedAtStart }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting user of an object from database for a selected date
        /// </summary>
        /// <param name="building">search object</param>
        /// <param name="storey">search object</param>
        /// <param name="room">search object</param>
        /// <param name="desk">search object</param>
        /// <param name="date">search date</param>
        /// <returns>list of objects</returns>
        public List<User> GetReservationsInnerUser(Building building, Storey storey, Room room, Desk desk, DateTime date)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<User>(queries.procGetResInnerUser,
                    new { BuildingId = building.Id, StoreyId = storey.Id, RoomId = room.Id, DeskId = desk.Id, ReservedAt = date.Date }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database from a given date and property ids
        /// </summary>
        /// <param name="building">search object</param>
        /// <param name="storey">search object</param>
        /// <param name="room">search object</param>
        /// <param name="desk">search object</param>
        /// <param name="starttime">start time</param>
        /// <returns>list of objects</returns>
        public List<DeskUser> GetDeskDateFut(Building building, Storey storey, Room room, Desk desk, DateTime starttime)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<DeskUser>(queries.procGetResForDateDesk,
                    new { BuildingId = building.Id, StoreyId = storey.Id, RoomId = room.Id, DeskId = desk.Id, ReservedFrom = starttime.Date }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
        /// <summary>
        ///  Exchange interface for getting all data from database
        /// </summary>
        /// <returns>list of objects</returns>
        public List<Reservation> GetAll()
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Reservation>(queries.procGetAllRes).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ResGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
