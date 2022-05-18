using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TableReservation.Classes;

namespace TableReservation.Database
{
    internal class DbResMng
    {
        public void NewReservation(Reservation Reservation)
        {
            //set new reservation in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procSetNewReservation @BuildingId, @StoreyID, @RoomId, @DeskId, @Username, @UserName, @ReservedAt @TimeStamp", Reservation);
            }
        }
        public List<Reservation> GetReservations(string username, DateOnly reservedAt)
        {
            //check reservation by username
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Reservation>("dbo.procGetResByUsername @UserName @ReservedAt", new { UserName = username, ReservedAt= reservedAt }).ToList();
            }
        }

        public List<Reservation> GetReservations(int buildingId, int storeyId, int roomId, int deskId, string username, DateOnly reservedAt)
        {
            //check if reservation is made
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Reservation>("dbo.procGetResByAllIds" +
                    "@BuildingId, @StoreyId, @RoomId, @DeskId, @UserName, @ReservedAt", 
                    new { BuildingId = buildingId, StoreyId= storeyId, RoomId= roomId, DeskId= deskId, UserName = username, ReservedAt= reservedAt }).ToList();
            }
        }

        public List<Reservation> GetReservations(string[] deskTags, DateOnly date)
        {
            //check reservation for a desk
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Reservation>("dbo.procGetResByDesk" +
                    "@BuildingId, @StoreyId, @RoomId, @DeskId",
                    new { BuildingId = deskTags[0], StoreyId = deskTags[1], RoomId = deskTags[2], DeskId = deskTags[3] }).ToList();
            }
        }
    }
}
