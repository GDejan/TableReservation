using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Classes;
using TableReservation.Helpers;

namespace TableReservation.Database
{
    internal class DbStoreyMng
    {
        private Msgs msgs = new Msgs();
        public void NewStorey(Storey Storey)
        {
            //set new Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewStorey @Name", Storey);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new storey", msgs.Error, MessageBoxButton.OK);
                }   
            }
        }
        public void ChangeStorey(Storey storey)
        {
            //change existing Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeStorey @Id, @Name", storey);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing storey", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public void RemoveStorey(Storey storey)
        {
            //remove existing Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveStorey @Id", storey);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing storey", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public List<Storey> GetStorey(Storey storey)
        {
            //check Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Storey>("dbo.procGetStoreyByName @Name", storey).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting storey", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
        public List<Storey> GetStorey(int id)
        {
            //check Storey in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Storey>("dbo.procGetStoreyById @Id", new { Id = id }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting storey", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
                
            }
        }
        public List<Storey> GetAllStoreys()
        {
            //check Storeys in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Storey>("dbo.procGetAllStoreys").ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting storey", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
