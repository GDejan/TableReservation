using System.Collections.Generic;
using System.Windows;
using TableReservation.Database;
using TableReservation.Helpers;

namespace TableReservation.Property
{
    internal class DeskMng
    {
        private DbDeskMng dbDeskdMng = new DbDeskMng();
        private Checks checks = new Checks();
        private List<Desk> desks = new List<Desk>();
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Add new desk to the database
        /// </summary>
        /// <param name="desk">object desk</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool Create(Desk desk)
        {
            desks = dbDeskdMng.GetByName(desk); //check if desk is in database

            if (desks.Count == 0) //if is not in database -> create new entry
            {
                dbDeskdMng.Create(desk);
                MessageBox.Show(msgs.DeskCreated + "->" + desk.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else
            {
                MessageBox.Show(msgs.DeskExist + "->" + desk.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing desk in database
        /// </summary>
        /// <param name="newDesk">new desk as object</param>
        /// <param name="oldDesk">old desk as object</param>
        /// /// <returns>Returns a true if succeded or false if not</returns>
        public bool Change(Desk newDesk, Desk oldDesk)
        {
            desks = dbDeskdMng.GetByName(newDesk);
            if (desks.Count == 0) //if new is not in database -> change entry
            {
                dbDeskdMng.Change(newDesk);
                MessageBox.Show(msgs.DeskChanged + "->" + oldDesk.Name.ToString() + " to " + newDesk.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else
            {
                MessageBox.Show(msgs.DeskExist + "->" + newDesk.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Remove desk from a database
        /// </summary>
        /// <param name="desk">desk as object</param>
        /// /// <returns>Returns a true if succeded or false if not</returns>
        public bool Remove(Desk desk)
        {
            desks = dbDeskdMng.GetById(desk.Id);

            if (desks.Count == 1)  //if is in database -> delete entry
            {
                dbDeskdMng.Remove(desk);
                MessageBox.Show(msgs.DeskRemoved + "->" + desk.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else if (desks.Count > 1)
            {
                MessageBox.Show(msgs.Wrong + "->" + msgs.ManyIds, msgs.Error, MessageBoxButton.OK);
                return false;
            }
            else
            {
                MessageBox.Show(msgs.DeskDontExist + "->" + desk.Id.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// returns a desk by id (string)
        /// </summary>
        /// <param name="id">id of a desk in databas</param>
        /// <returns>returned desk object</returns>
        public Desk GetById(string id)
        {
            if (checks.InputCheckStringIntId(id))
            {
                desks = dbDeskdMng.GetById(int.Parse(id)); //check if desk is in database
                if (desks != null)
                {
                    if (desks.Count == 1) //if is in database -> check entry
                    {
                        return desks[0];
                    }
                    else if (desks.Count >= 1)
                    {
                        MessageBox.Show(msgs.Wrong + "->" + msgs.ManyIds, msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(msgs.DeskDontExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
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
        /// list all desks in a database
        /// </summary>
        /// <returns>list of desks</returns>
        public List<Desk> GetAllDesks()
        {
            desks = dbDeskdMng.GetAll(); //get all desks from a database
            if (desks!=null)
            {
                if (desks.Count > 0) //if is in database -> returns entries
                {
                    return desks;
                }
                else
                {
                    MessageBox.Show(msgs.DesksDontExist, msgs.Error, MessageBoxButton.OK);
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
