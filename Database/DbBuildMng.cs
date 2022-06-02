using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Helpers;
using TableReservation.Property;

namespace TableReservation.Database
{
    internal class DbBuildMng 
    {
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="building">new object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Create(Building building)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewBuilding @Name", building);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeBuilding @Id, @Name", building);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveBuilding @Id", building);
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Building>("dbo.procGetBuildingByName @Name", building).ToList();
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Building>("dbo.procGetBuildingById @Id", new { Id = id }).ToList();
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
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Building>("dbo.procGetAllBuildings").ToList();
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
