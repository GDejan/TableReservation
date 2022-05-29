using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Classes;
using TableReservation.Helpers;
using TableReservation.ViewModel;

namespace TableReservation.Database
{
    internal class DbResMng
    {
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="reservation">new object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Create(Reservation reservation)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewReservation @BuildingId, @StoreyID, @RoomId, @DeskId, @UserId, @ReservedAt", reservation);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new reservation", msgs.Error, MessageBoxButton.OK);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeReservation @Id, @BuildingId, @StoreyID, @RoomId, @DeskId, @UserId, @ReservedAt", reservation);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing desk", msgs.Error, MessageBoxButton.OK);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveReservation @Id", new { Id=id});
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing reservation", msgs.Error, MessageBoxButton.OK);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Reservation>("dbo.procGetResById @Id", new { Id = id }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting building", msgs.Error, MessageBoxButton.OK);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Reservation>("dbo.procGetResByUserIdDate @UserId, @ReservedAt", new { UserId = user.Id, ReservedAt = reservedAt }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting reservation", msgs.Error, MessageBoxButton.OK);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Reservation>("dbo.procGetResForDate @BuildingId, @StoreyId, @RoomId, @DeskId, @ReservedAt",
                    new { BuildingId = building.Id, StoreyId = storey.Id, RoomId = room.Id, DeskId = desk.Id, ReservedAt = date.Date }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting reservation", msgs.Error, MessageBoxButton.OK);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Reservation>("dbo.procGetResForRange @ReservedAtStart, @ReservedAtEnd", new { ReservedAtStart = reservedAtStart, ReservedAtEnd = reservedAtEnd }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting reservation", msgs.Error, MessageBoxButton.OK);
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
        public List<UserReservation> GetFutureForUserId(DateTime reservedAtStart, User user)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<UserReservation>("dbo.procGetFutureResByUserId @UserId, @ReservedAtStart", new { UserId = user.Id, ReservedAtStart = reservedAtStart }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting reservation", msgs.Error, MessageBoxButton.OK);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<User>("dbo.procGetResInnerUser @BuildingId, @StoreyId, @RoomId, @DeskId, @ReservedAt",
                    new { BuildingId = building.Id, StoreyId = storey.Id, RoomId = room.Id, DeskId = desk.Id, ReservedAt = date.Date }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting reservation", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
