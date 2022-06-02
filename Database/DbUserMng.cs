using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Helpers;
using TableReservation.Users;

namespace TableReservation.Database
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
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserCreateErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserChangeErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
        }
        
        /// <summary>
        /// Exchange interface for change user password 
        /// </summary>
        /// <param name="user">change object</param>
        /// <returns>true if ok, false is it fail</returns>
        public bool ChangePass(User user)
        {
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeUserPass @Id, @IsTemp, @Password", user);
                    return true;
                }
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserChangeErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserRemoveErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
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
                catch (Exception e)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.UserGetErr + "->" + e.Message, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
    }
}
