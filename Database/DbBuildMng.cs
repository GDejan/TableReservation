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
    internal class DbBuildMng
    {
        public void NewBuilding(Building Building)
        {
            //set new Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procSetNewBuilding @Name", Building);
            }
        }
        public void ChangeBuilding(int id, string name)
        {
            //change existing Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procChangeBuilding @Id, @Name", new { Id = id, Name = name});
            }
        }

        public void RemoveBuilding(int id)
        {
            //remove existing Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procRemoveBuilding @Id", new { Id = id });
            }
        }

        public List<Building> GetBuilding(string name)
        {
            //check Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Building>("dbo.procGetBuildingByName @Name", new { Name = name}).ToList();
            }
        }
        public List<Building> GetBuilding(int id)
        {
            //check Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Building>("dbo.procGetBuildingById @Id", new { Id = id }).ToList();
            }
        }
        public List<Building> GetAllBuildings()
        {
            //check Building in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Building>("dbo.procGetAllBuildings").ToList();
            }
        }

    }
}
