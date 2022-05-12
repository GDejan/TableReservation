using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace TableReservation.Classes.Users
{
    public class SessionUser
    {
        public User User { get; set; }
        public bool IsActiv { get; set; }
        public SessionUser()
        {

        }
        public SessionUser(User user)
        {
            this.User = user;
            this.IsActiv = true;
        }
    }
}
