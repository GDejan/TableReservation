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
    internal class DbDeskMng
    {
        public void NewDesk(Desk Desk)
        {
            //set new Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procSetNewDesk @Name", Desk);
            }
        }
        public void ChangeDesk(int id, string name)
        {
            //change existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procChangeDesk @Id, @Name", new { Id = id, Name = name });
            }
        }

        public void RemoveDesk(int id)
        {
            //remove existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procRemoveDesk @Id", new { Id = id });
            }
        }

        public List<Desk> GetDesk(string name)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Desk>("dbo.procGetDeskByName @Name", new { Name = name }).ToList();
            }
        }
        public List<Desk> GetDesk(int id)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Desk>("dbo.procGetDeskById @Id", new { Id = id }).ToList();
            }
        }
        public List<Desk> GetAllDesks()
        {
            //check desks in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<Desk>("dbo.procGetAllDesks").ToList();
            }
        }
    }
}
