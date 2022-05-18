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
        private DbUserMng dbUserMng = new DbUserMng();
        private Checks checks = new Checks();
        private List<User> users = new List<User>();
        private Msgs Msgs = new Msgs();
        public UserMng()
        {
        }
        public User LogedUser(string username, string password)
        {
            PassHash PassHash = new PassHash(password);
            string passwordHas = PassHash.HashPass;

            if (checks.InputCheck(username))
            {
                users = dbUserMng.GetUser(username, passwordHas);

                if (users.Count > 0)
                {
                    return users[0];
                }
            }
            return null;
        }

        public bool NewUser(string name, string surname, string username, string password)
        {
            PassHash PassHash = new PassHash(password);
            string passwordHas = PassHash.HashPass;

            if (checks.InputCheck(name) && checks.InputCheck(surname) && checks.InputCheck(username)) //check entry data
            {
                users = dbUserMng.GetUser(username); //check if user is in database

                if (users.Count == 0) //if is not in database -> create new entry
                {
                    //!!!!!!//check if name and surname are equal - or use PC username for validation

                    dbUserMng.NewUser(new User(name, surname, username, passwordHas));
                    MessageBox.Show(Msgs.UserCreated + "->" + username.ToString(), Msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(Msgs.UserExist + "->" + username.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
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

            if (checks.InputCheck(newname) && checks.InputCheck(newsurname) && checks.InputCheck(newusername)) //check entry data
            {
                users = dbUserMng.GetUser(newusername);
                if ((users.Count == 0) || (newusername == oldUser.Username)) //if is not in database -> change entry
                {
                    //!!!!!!//check if name and surname are equal - or use PC username for validation

                    dbUserMng.ChangeUser(id, newname, newsurname, newusername, newisadmin);
                    MessageBox.Show(Msgs.UserChanged + "->" + oldUser.Name.ToString() + " to " + newname.ToString(), Msgs.Ok, MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show(Msgs.UserExist + "->" + username.ToString(), Msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        public void RemoveUser(int id)
        {
            dbUserMng.RemoveUser(id);
            MessageBox.Show(Msgs.UserRemoved, Msgs.Ok, MessageBoxButton.OK);
        }

        public User getUserById(string id)
        {
            try
            {
                int ID = Int32.Parse(id);
                users = dbUserMng.GetUser(ID); //check if user is in database
                if (users.Count > 0) //if is in database -> check entry
                {
                    return users[0];
                }
                else
                {
                    MessageBox.Show(Msgs.UserDontExist + "->" + id.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            catch
            {
                MessageBox.Show(Msgs.WrongInput + "->" + id.ToString(), Msgs.Error, MessageBoxButton.OK);
                return null;
            }
        }


    }
}
