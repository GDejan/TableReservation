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
    internal class DbRoomMng
    {
        public void NewRoom(Room Room)
        {
            //set new Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procSetNewRoom @Name", Room);
            }
        }
        public void ChangeRoom(int id, string name)
        {
            //change existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procChangeRoom @Id, @Name", new { Id = id, Name = name });
            }
        }

        public void RemoveRoom(int id)
        {
            //remove existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procRemoveRoom @Id", new { Id = id });
            }
        }

        public List<Room> GetRoom(string name)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Room>("dbo.procGetRoomByName @Name", new { Name = name }).ToList();
            }
        }
        public List<Room> GetRoom(int id)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Room>("dbo.procGetRoomById @Id", new { Id = id }).ToList();
            }
        }
        public List<Room> GetAllRooms()
        {
            //check Rooms in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Room>("dbo.procGetAllRooms").ToList();
            }
        }
    }
}
