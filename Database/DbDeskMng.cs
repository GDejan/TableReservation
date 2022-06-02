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
    internal class DbDeskMng 
    {
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="desk">new object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Create(Desk desk)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewDesk @Name", desk);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.DeskCreateErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }   
            }
        }

        /// <summary>
        /// Exchange interface for change item in a database
        /// </summary>
        /// <param name="desk">change object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Change(Desk desk)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeDesk @Id, @Name", desk);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.DeskChangeErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for removing item from a database
        /// </summary>
        /// <param name="desk">remove object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Remove(Desk desk)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveDesk @Id", desk);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.DeskRemoveErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Name
        /// </summary>
        /// <param name="desk">search object</param>
        /// <returns>list of objects</returns>
        public List<Desk> GetByName(Desk desk)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Desk>("dbo.procGetDeskByName @Name", desk).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.DeskGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Id
        /// </summary>
        /// <param name="id">id of search object</param>
        /// <returns>list of objects</returns>
        public List<Desk> GetById(int id)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Desk>("dbo.procGetDeskById @Id", new { Id = id }).ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.DeskGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database
        /// </summary>
        /// <returns>list of objects</returns>
        public List<Desk> GetAll()
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<Desk>("dbo.procGetAllDesks").ToList();
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.DeskGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        } 
    }
}
