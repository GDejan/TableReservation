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
    internal class DbBuildMng
    {
        private Msgs msgs = new Msgs();
        private Queries queries = new Queries();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="building">new object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Create(Building building)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procNewBuilding, building);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.BuildCreateErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for change item in a database
        /// </summary>
        /// <param name="building">change object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Change(Building building)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procChangeBuilding, building);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.BuildChangeErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for removing item from a database
        /// </summary>
        /// <param name="building">remove object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Remove(Building building)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    SQLconn.Execute(queries.procRemoveBuilding, building);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.BuildRemoveErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Name
        /// </summary>
        /// <param name="building">search object</param>
        /// <returns>list of objects</returns>
        public List<Building> GetByName(Building building)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Building>(queries.procGetBuildingByName, building).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.BuildGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Id
        /// </summary>
        /// <param name="id">id of search object</param>
        /// <returns>list of objects</returns>
        public List<Building> GetById(int id)
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Building>(queries.procGetBuildingById, new { Id = id }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.BuildGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database
        /// </summary>
        /// <returns>list of objects</returns>
        public List<Building> GetAll()
        {
            using (SQLiteConnection SQLconn = new SQLiteConnection(DbHelper.ConnectionString()))
            {
                try
                {
                    return SQLconn.Query<Building>(queries.procGetAllBuildings).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.BuildGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
