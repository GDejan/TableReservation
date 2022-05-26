using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using TableReservation.Helpers;

namespace TableReservation.Classes
{
    internal class DbUserMng
    {
        private Msgs msgs = new Msgs();
        public void NewUser(User User) 
         {
            //set new user in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procNewUser @Name, @Surname, @Username, @Password, @IsAdmin", User);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error creating new user", msgs.Error, MessageBoxButton.OK);
                }
            }
        }
        public void ChangeUser(User user)
        {
            //change existing user in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procChangeUser @Id, @Name, @Surname, @Username, @IsAdmin", user);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error changing user", msgs.Error, MessageBoxButton.OK);
                }
            }
        }
        
        public void RemoveUser(User user)
        {
            //remove existing user in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    SQLconn.Execute("dbo.procRemoveUser @Id", user);
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error removing user", msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public List<User> GetAllUsers()
        {
            //check all users
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
        public List<User> GetUser(int id)
        {
            //check if user exist by id
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
        public List<User> GetUser(User user)
        {
            //check login credentials in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                try
                {
                    return SQLconn.Query<User>("dbo.procGetUsersByUsernameAndPass @Username, @Password", user).ToList();
                }
                catch
                {
                    MessageBox.Show(msgs.Wrong + " error getting user", msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
        }
        public List<User> GetUserByUsername(User user)
        {
            //check if user exist by username
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
    }
}
