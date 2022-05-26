using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Classes;
using TableReservation.Helpers;

namespace TableReservation.Database
{
    internal class DbResMng
    {
        private Msgs msgs = new Msgs();
        public void NewReservation(Reservation reservation)
        {
            //set new reservation in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewReservation @BuildingId, @StoreyID, @RoomId, @DeskId, @UserId, @ReservedAt", reservation);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new reservation", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public void RemoveReservation(Reservation reservation)
        {
            //set new reservation in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveReservation @Id", reservation);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing reservation", msgs.Error, MessageBoxButton.OK);
                }   
            }
        }
        public List<Reservation> GetReservations(User user, DateTime reservedAt)
        {
            //check reservation by userid
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

        public List<Reservation> GetReservations(Building building, Storey storey, Room room, Desk desk, DateTime date)
        {
            //check reservation for a desk
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Reservation>("dbo.procGetResByDesk @BuildingId, @StoreyId, @RoomId, @DeskId, @ReservedAt",
                    new { BuildingId = building.Id, StoreyId = storey.Id, RoomId = room.Id, DeskId = desk.Id, ReservedAt = date.Date }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting reservation", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        public List<Reservation> GetAllReservations(DateTime reservedAtStart, DateTime reservedAtEnd)
        {
            //get all Reservation between range
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Reservation>("dbo.procGetReservationsRange @ReservedAtStart, @ReservedAtEnd", new { ReservedAtStart = reservedAtStart, ReservedAtEnd = reservedAtEnd }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting reservation", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        public List<Reservation> GetAllReservations(DateTime reservedAtStart, User user)
        {
            //check reservation by userid
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Reservation>("dbo.procGetResByUserId @UserId, @ReservedAtStart", new { UserId = user.Id, ReservedAtStart = reservedAtStart }).ToList();
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
