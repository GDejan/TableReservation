using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TableReservation.Helpers;

namespace TableReservation.Classes.Users
{
    internal class UserMng
    {
        private DbUserMng DbUserMng = new DbUserMng();
        private Checks Checks = new Checks();
        private List<User> users = new List<User>();
        public UserMng()
        {
        }

        public bool NewUser(string name, string surname, string username, string password)
        {
            PassHash PassHash = new PassHash(password);
            string passwordHas = PassHash.HashPass;

            if (Checks.InputCheck(name) && Checks.InputCheck(surname) && Checks.InputCheck(username)) //check entry data
            {
                users = DbUserMng.SQLgetUser(username); //check if user is in database

                if (users.Count == 0) //if is not in database -> create new entry
                {
                    //!!!!!!//check if name and surname are equal - or use PC username for validation

                    DbUserMng.SQLnewUser(new User(name, surname, username, passwordHas));
                    MessageBox.Show(EnumMsgs.UserCreated, EnumMsgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(EnumMsgs.UserExist, EnumMsgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                MessageBox.Show(EnumMsgs.WrongInput, EnumMsgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        public void ChangeUser(int id, string name, string surname, string username, bool isadmin, User oldUser)
        {
            string newusername = "";
            string newname = "";
            string newsurname = "";
            bool newisadmin;

            if (username != "") newusername = username; else newusername = oldUser.Username;
            if (name != "") newname = name; else newname = oldUser.Name;
            if (surname != "") newsurname = surname; else newsurname = oldUser.Surname;
            if (isadmin != oldUser.IsAdmin) newisadmin = isadmin; else newisadmin = oldUser.IsAdmin;

            if (Checks.InputCheck(newname) && Checks.InputCheck(newsurname) && Checks.InputCheck(newusername)) //check entry data
            {
                users = DbUserMng.SQLgetUser(newusername);
                if ((users.Count == 0) || (newusername == oldUser.Username)) //if is not in database -> change entry
                {
                    //!!!!!!//check if name and surname are equal - or use PC username for validation

                    DbUserMng.SQLchangeUser(id, newname, newsurname, newusername, newisadmin);
                    MessageBox.Show(EnumMsgs.UserChanged, EnumMsgs.Ok, MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show(EnumMsgs.UserExist, EnumMsgs.Error, MessageBoxButton.OK);
                }
            }
            else
            {
                MessageBox.Show(EnumMsgs.WrongInput, EnumMsgs.Error, MessageBoxButton.OK);
            }

        }

        public void RemoveUser(int id)
        {
            DbUserMng.SQLremoveUser(id);
            MessageBox.Show(EnumMsgs.UserRemoved, EnumMsgs.Ok, MessageBoxButton.OK);
        }

        public User getUserById(string id)
        {
            try
            {
                int ID = Int32.Parse(id);
                users = DbUserMng.SQLgetUser(ID); //check if user is in database
                if (users.Count > 0) //if is in database -> check entry
                {
                    return users[0];
                }
                else
                {
                    MessageBox.Show(EnumMsgs.UserDontExist, EnumMsgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            catch
            {
                MessageBox.Show(EnumMsgs.WrongInput, EnumMsgs.Error, MessageBoxButton.OK);
                return null;
            }
        }


    }
}
