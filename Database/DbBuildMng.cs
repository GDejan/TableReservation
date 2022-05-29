using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Classes;
using TableReservation.Helpers;

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
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new building", msgs.Error, MessageBoxButton.OK);
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
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing building", msgs.Error, MessageBoxButton.OK);
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
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing building", msgs.Error, MessageBoxButton.OK);
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
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting building", msgs.Error, MessageBoxButton.OK);
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
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting building", msgs.Error, MessageBoxButton.OK);
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
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting building", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
