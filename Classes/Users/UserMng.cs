using System;
using System.Collections.Generic;
using System.Windows;
using TableReservation.Helpers;

namespace TableReservation.Classes.Users
{
    internal class UserMng
    {
        private DbUserMng dbUserMng = new DbUserMng();
        private Checks checks = new Checks();
        private List<User> users = new List<User>();
        private Msgs msgs = new Msgs();

        public User LogedUser(string username, string password)
        {
            if (checks.InputCheck(username) == true)
            {
                if (!string.IsNullOrEmpty(password))
                {
                    PassHash passHash = new PassHash(password);
                    users = dbUserMng.GetUser(new User(username, passHash.HashedPassword));

                    if (users.Count == 1) //if is in database -> check entry
                    {
                        return users[0];
                    }
                    else if (users.Count > 1)
                    {
                        MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(msgs.WrongUser, msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                }
                else 
                {
                    MessageBox.Show(msgs.EmptyInput, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else 
            {
                return null;
            }
        }        

        public bool NewUser(User user)
        {
            users = dbUserMng.GetUserByUsername(user); //check if user is in database

            if (users.Count == 0) //if is not in database -> create new entry
            {
                dbUserMng.NewUser(user);
                MessageBox.Show(msgs.UserCreated + "->" + user.Username.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else
            {
                MessageBox.Show(msgs.UserExist + "->" + user.Username.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        public bool ChangeUser(User newUser, User oldUser)
        {
            users = dbUserMng.GetUserByUsername(newUser);
            if ((users.Count == 0)||(newUser.Username==oldUser.Username))  //if new is not in database -> change entry

                ///ili da je tzo isto trenutni user pa se smije promjeniti
            {
                dbUserMng.ChangeUser(newUser);
                MessageBox.Show(msgs.UserChanged + "->" + oldUser.Name.ToString() + " to " + newUser.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else
            {
                MessageBox.Show(msgs.UserExist + "->" + newUser.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        public void RemoveUser(User user)
        {
            users = dbUserMng.GetUser(user.Id);

            if (users.Count == 1)  //if is in database -> delete entry
            {
                dbUserMng.RemoveUser(user);
                MessageBox.Show(msgs.UserRemoved + "->" + user.Username.ToString(), msgs.Ok, MessageBoxButton.OK);
            }
            else if (users.Count > 1)
            {
                MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(msgs.UserDontExist + "->" + user.Id.ToString(), msgs.Error, MessageBoxButton.OK);
            }
        }

        public User getUserById(string id)
        {
            if (checks.InputCheckStringInt(id))
            {
                users = dbUserMng.GetUser(int.Parse(id)); //check if user is in database
                if (users!=null)
                {
                    if (users.Count == 1) //if is in database -> check entry
                    {
                        return users[0];
                    }
                    else if (users.Count >= 1)
                    {
                        MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(msgs.UserDontExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public List<User> getAllUsers()
        {
            users = dbUserMng.GetAllUsers(); //get all storeys from a database
            if (users!=null)
            {
                if (users.Count > 0) //if is in database -> returns entries
                {
                    return users;
                }
                else
                {
                    MessageBox.Show(msgs.UsersDontExist, msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else
            {
                return null;
            }

        }
    }
}
