using Dapper;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace TableReservation.Classes
{
    internal class DbUserMng
    {
        public void SQLnewUser(User User) 
         {
            //set new user in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procSetNewUser @Name, @Surname, @Username, @Password, @IsAdmin", User);
            }
        }
        public void SQLchangeUser(int id, string name, string surname, string username, bool isAdmin)
        {
            //change existing user in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procChangeUser @Id, @Name, @Surname, @Username, @IsAdmin", new { Id=id, Name=name, Surname=surname, UserName = username, IsAdmin=isAdmin });
            }
        }
        
        public void SQLremoveUser(int id)
        {
            //remove existing user in database
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                SQLconn.Execute("dbo.procRemoveUser @Id", new {Id=id});
            }
        }

        public List<User> SQLgetUser(string username, string password)
        {
            //check login
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<User>("dbo.procGetUsersByUsernameAndPass @UserName, @PassWord", new { UserName = username, PassWord = password }).ToList();
            }
        }

        public List<User> SQLgetUser(string username)
        {
            //check if user exist
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<User>("dbo.procGetUsersByUsername @UserName", new { UserName = username}).ToList();
            }
        }
        public List<User> SQLgetUser(int id)
        {
            //check if user exist
            using (SqlConnection SQLconn = new SqlConnection(DbHelper.ConnectionString("connectionString")))
            {
                return SQLconn.Query<User>("dbo.procGetUsersById @Id", new { Id= id}).ToList();
            }
        }


    }
}
