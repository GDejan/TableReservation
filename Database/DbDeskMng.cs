using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Classes;
using TableReservation.Helpers;

namespace TableReservation.Database
{
    internal class DbDeskMng
    {
        private Msgs msgs = new Msgs();
        public void NewDesk(Desk desk)
        {
            //set new Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewDesk @Name", desk);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new desk", msgs.Error, MessageBoxButton.OK);
                }   
            }
        }
        public void ChangeDesk(Desk desk)
        {
            //change existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeDesk @Id, @Name", desk);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing desk", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public void RemoveDesk(Desk desk)
        {
            //remove existing Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveDesk @Id", desk);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing desk", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public List<Desk> GetDesk(Desk desk)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Desk>("dbo.procGetDeskByName @Name", desk).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting desk", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
        public List<Desk> GetDesk(int id)
        {
            //check Room in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Desk>("dbo.procGetDeskById @Id", new { Id = id }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting desk", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        public List<Desk> GetAllDesks()
        {
            //check desks in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Desk>("dbo.procGetAllDesks").ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting desk", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
