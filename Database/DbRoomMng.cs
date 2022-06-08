using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Windows;
using TableReservation.Helpers;
using TableReservation.Property;

namespace TableReservation.Database
{
    internal class DbRoomMng
    {
        private Msgs msgs = new Msgs();
        private Queries queries = new Queries();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="room">new object</param>
        /// <returns>true if ok, false is it fail</returns>></returns>
        public bool Create(Room room)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procNewRoom, room);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.RoomCreateErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procChangeRoom, room);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.RoomChangeErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procRemoveRoom, room);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.RoomRemoveErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Room>(queries.procGetRoomByName, room).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.RoomGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Room>(queries.procGetRoomById, new { Id = id }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.RoomGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Room>(queries.procGetAllRooms).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.RoomGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
