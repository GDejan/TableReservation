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
    internal class DbStoreyMng
    {
        public void NewStorey(Storey Storey)
        {
            //set new Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procSetNewStorey @Name", Storey);
            }
        }
        public void ChangeStorey(int id, string name)
        {
            //change existing Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procChangeStorey @Id, @Name", new { Id = id, Name = name });
            }
        }

        public void RemoveStorey(int id)
        {
            //remove existing Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procRemoveStorey @Id", new { Id = id });
            }
        }

        public List<Storey> GetStorey(string name)
        {
            //check Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Storey>("dbo.procGetStoreyByName @Name", new { Name = name }).ToList();
            }
        }
        public List<Storey> GetStorey(int id)
        {
            //check Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Storey>("dbo.procGetStoreyById @Id", new { Id = id }).ToList();
            }
        }
        public List<Storey> GetAllStoreys()
        {
            //check Storeys in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Storey>("dbo.procGetAllStoreys").ToList();
            }
        }
    }
}
