using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Classes;
using TableReservation.Helpers;

namespace TableReservation.Database
{
    internal class DbRoomMng
    {
        private Msgs msgs = new Msgs();
        public void NewRoom(Room Room)
        {
            //set new Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewRoom @Name", Room);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new room", msgs.Error, MessageBoxButton.OK);
                }
            }
        }
        public void ChangeRoom(Room room)
        {
            //change existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeRoom @Id, @Name", room);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing room", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public void RemoveRoom(Room room)
        {
            //remove existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveRoom @Id", room);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing room", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public List<Room> GetRoom(Room room)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Room>("dbo.procGetRoomByName @Name", room).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting room", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
        public List<Room> GetRoom(int id)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Room>("dbo.procGetRoomById @Id", new { Id = id }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting room", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
        public List<Room> GetAllRooms()
        {
            //check Rooms in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Room>("dbo.procGetAllRooms").ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting room", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
