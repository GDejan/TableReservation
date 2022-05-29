using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Database;
using TableReservation.Helpers;

namespace TableReservation.Classes
{
    internal class DbUserMng 
    {
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Exchange interface for creating new item in a database
        /// </summary>
        /// <param name="user">new object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Create(User user) 
         {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewUser @Name, @Surname, @Username, @Password, @IsAdmin, @IsTemp", user);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new user", msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for change item in a database
        /// </summary>
        /// <param name="user">change object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Change(User user)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeUser @Id, @Name, @Surname, @Username, @IsAdmin, @IsTemp", user);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing user", msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for removing item from a database
        /// </summary>
        /// <param name="user">remove object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool Remove(User user)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveUser @Id", user);
                    return true;
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing user", msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }

        /// <summary>
        /// Exchange interface for login check
        /// </summary>
        /// <param name="user">search object</param>
        /// <returns>list of objects</returns>
        public List<User> LoginCheck(User user)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<User>("dbo.procGetUserLoginCheck @Username, @Password", user).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting user", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Id
        /// </summary>
        /// <param name="id">id of search object</param>
        /// <returns>list of objects</returns>
        public List<User> GetById(int id)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<User>("dbo.procGetUsersById @Id", new { Id = id }).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting user", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database by Username
        /// </summary>
        /// <param name="user">search object</param>
        /// <returns>list of objects</returns>
        public List<User> GetByUsername(User user)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<User>("dbo.procGetUsersByUsername @Username", user).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting user", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }

        /// <summary>
        /// Exchange interface for getting all data of an object from database
        /// </summary>
        /// <returns>list of objects</returns>
        public List<User> GetAll()
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<User>("dbo.procGetAllUsers").ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting user", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
