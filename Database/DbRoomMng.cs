﻿using Dapper;
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

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="room">new object</param>
        /// <returns>true if ok, false is it fail</returns>></returns>
        public bool Create(Room room)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewRoom @Name", room);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new room", msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for change item in a database
        /// </summary>
        /// <param name="room">change object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Change(Room room)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeRoom @Id, @Name", room);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing room", msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for removing item from a database
        /// </summary>
        /// <param name="room">remove object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Remove(Room room)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveRoom @Id", room);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing room", msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }


        /// <summary>
        /// Exchange interface for getting all data of an object from database by Name
        /// </summary>
        /// <param name="room">search object</param>
        /// <returns>list of objects</returns>
        public List<Room> GetByName(Room room)
        {
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

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Id
        /// </summary>
        /// <param name="id">id of search object</param>
        /// <returns>list of objects</returns>
        public List<Room> GetById(int id)
        {
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

        /// <summary>
        /// Exchange interface for getting all data of an object from database
        /// </summary>
        /// <returns>list of objects</returns>
        public List<Room> GetAll()
        {
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
