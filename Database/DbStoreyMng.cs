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
    internal class DbStoreyMng
    {
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="storey">new object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Create(Storey storey)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewStorey @Name", storey);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.StoreyCreateErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }   
            }
        }

        /// <summary>
        /// Exchange interface for change item in a database
        /// </summary>
        /// <param name="storey">change object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Change(Storey storey)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeStorey @Id, @Name", storey);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.StoreyChangeErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for removing item from a database
        /// </summary>
        /// <param name="storey">remove object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Remove(Storey storey)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveStorey @Id", storey);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.StoreyRemoveErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Name
        /// </summary>
        /// <param name="storey">search object</param>
        /// <returns>list of objects</returns>
        public List<Storey> GetByName(Storey storey)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Storey>("dbo.procGetStoreyByName @Name", storey).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.StoreyGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Id
        /// </summary>
        /// <param name="id">id of search object</param>
        /// <returns>list of objects</returns>
        public List<Storey> GetById(int id)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Storey>("dbo.procGetStoreyById @Id", new { Id = id }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.StoreyGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
                
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database
        /// </summary>
        /// <returns>list of objects</returns>
        public List<Storey> GetAll()
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Storey>("dbo.procGetAllStoreys").ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.StoreyGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
