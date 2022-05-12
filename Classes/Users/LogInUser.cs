using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TableReservation.Helpers;

namespace TableReservation.Classes.Users
{
    internal class LogInUser
    {
        public User LogedUser(string username, string password)
        {
            DbUserMng DbUserMng = new DbUserMng();
            Checks Checks = new Checks();
            List<User> users = new List<User>();
            
            PassHash PassHash = new PassHash(password);
            string passwordHas = PassHash.HashPass;

            if (Checks.InputCheck(username))
            {
                users = DbUserMng.SQLgetUser(username, passwordHas);

                if (users.Count > 0)
                {
                    return users[0];
                }
            }
            else
            {
                MessageBox.Show(EnumMsgs.WrongInput, EnumMsgs.Error, MessageBoxButton.OK);
            }

            return null;
        }

    }
}
