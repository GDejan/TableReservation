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
        public void NewBuilding(Building building)
        {
            //set new Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewBuilding @Name", building);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new building", msgs.Error, MessageBoxButton.OK);
                }
            }
        }
        public void ChangeBuilding(Building building)
        {
            //change existing Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeBuilding @Id, @Name", building);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing building", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public void RemoveBuilding(Building building)
        {
            //remove existing Building from database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveBuilding @Id", building);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing building", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public List<Building> GetBuilding(Building building)
        {
            //check Building in database
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

        public List<Building> GetBuilding(int id)
        {
            //check Building in database
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

        public List<Building> GetAllBuildings()
        {
            //check Building in database
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
