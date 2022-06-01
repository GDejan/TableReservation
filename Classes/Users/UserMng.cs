using System.Collections.Generic;
using System.Windows;
using TableReservation.Database;
using TableReservation.Helpers;

namespace TableReservation.Users
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
                    users = dbUserMng.LoginCheck(new User(username, passHash.HashedPassword));

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

        /// <summary>
        /// Add new user to the database
        /// </summary>
        /// <param name="user">object user</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool Create(User user)
        {
            users = dbUserMng.GetByUsername(user); //check if user is in database

            if (users.Count == 0) //if is not in database -> create new entry
            {
                if (dbUserMng.Create(user))
                {
                    MessageBox.Show(msgs.UserCreated + "->" + user.Username.ToString(), msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                return false;
            }
            else
            {
                MessageBox.Show(msgs.UserExist + "->" + user.Username.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing user in database
        /// </summary>
        /// <param name="newUser">new name of a user</param>
        /// <param name="oldUser"> old user object</param>
        public bool Change(User newUser, User oldUser)
        {
            users = dbUserMng.GetByUsername(newUser);
            if ((users.Count == 0)||(newUser.Username==oldUser.Username))  //if new is not in database -> change entry
            {
                if (dbUserMng.Change(newUser)) 
                {
                    MessageBox.Show(msgs.UserChanged + "->" + oldUser.Name.ToString() + " to " + newUser.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                return false;
            }
            else
            {
                MessageBox.Show(msgs.UserExist + "->" + newUser.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }
        
        /// <summary>
        /// Change password of a existing user in database
        /// </summary>
        /// <param name="newUser">new name of a user</param>
        /// <param name="oldUser"> old user object</param>
        public bool ChangePass(User newUser, User oldUser)
        {
            users = dbUserMng.GetByUsername(newUser);
            if ((users.Count == 0)||(newUser.Username==oldUser.Username))  //if new is not in database -> change entry
            {
                if (dbUserMng.ChangePass(newUser)) 
                {
                    MessageBox.Show(msgs.UserChanged + "->" + oldUser.Name.ToString() + " to " + newUser.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                return false;
            }
            else
            {
                MessageBox.Show(msgs.UserExist + "->" + newUser.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Remove user from a database
        /// </summary>
        /// <param name="user">user as object</param>
        /// /// <returns>Returns a true if succeded or false if not</returns>
        public bool Remove(User user)
        {
            users = dbUserMng.GetById(user.Id);
            if (users.Count == 1)  //if is in database -> delete entry
            {
                if (dbUserMng.Remove(user)) 
                {
                    MessageBox.Show(msgs.UserRemoved + "->" + user.Username.ToString(), msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                return false;
            }
            else if (users.Count > 1)
            {
                MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
                return false;
            }
            else
            {
                MessageBox.Show(msgs.UserDontExist + "->" + user.Id.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// returns a user by id (string)
        /// </summary>
        /// <param name="id">id of a user in database</param>
        /// <returns>returned user object</returns>
        public User getById(string id)
        {
            if (checks.InputCheckStringIntId(id))
            {
                users = dbUserMng.GetById(int.Parse(id)); //check if user is in database
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

        /// <summary>
        /// list all users in a database
        /// </summary>
        /// <returns>list of users</returns>
        public List<User> getAll()
        {
            users = dbUserMng.GetAll(); //get all users from a database
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
