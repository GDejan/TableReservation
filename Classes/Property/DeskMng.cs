using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TableReservation.Database;
using TableReservation.Helpers;

namespace TableReservation.Classes
{
    internal class DeskMng
    {
        private DbDeskMng dbDeskdMng = new DbDeskMng();
        private Checks checks = new Checks();
        private List<Desk> desks = new List<Desk>();
        private Msgs Msgs = new Msgs();

        /// <summary>
        /// Add new desk to the database
        /// </summary>
        /// <param name="name">Name of a desk</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool NewDesk(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                desks = dbDeskdMng.GetDesk(name); //check if Desk is in database

                if (desks.Count == 0) //if is not in database -> create new entry
                {
                    dbDeskdMng.NewDesk(new Desk(name));
                    MessageBox.Show(Msgs.DeskCreated + "->" + name.ToString(), Msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(Msgs.DeskExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing desk in database. If parameter is null then it takes old name
        /// </summary>
        /// <param name="id">id of a desk in databas</param>
        /// <param name="name">new name of a desk </param>
        /// <param name="oldDesk">old desk object</param>
        public void ChangeDesk(int id, string name, Desk oldDesk)
        {
            string newname = "";

            if (name != "") newname = name; else newname = oldDesk.Name;

            if (checks.InputCheck(newname)) //check entry data
            {
                desks = dbDeskdMng.GetDesk(newname);
                if ((desks.Count == 0) || (newname == oldDesk.Name)) //if is not in database -> change entry
                {

                    dbDeskdMng.ChangeDesk(id, newname);
                    MessageBox.Show(Msgs.DeskChanged + "->" + oldDesk.Name.ToString() + " to " + newname.ToString(), Msgs.Ok, MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show(Msgs.DeskExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Remove desk from a database
        /// </summary>
        /// <param name="id">id of a desk in databas</param>
        public void RemoveDesk(int id)
        {
            dbDeskdMng.RemoveDesk(id);
            MessageBox.Show(Msgs.DeskRemoved + "->" + id.ToString(), Msgs.Ok, MessageBoxButton.OK);
        }

        /// <summary>
        /// returns a desk by id
        /// </summary>
        /// <param name="id">id of a desk in databas</param>
        /// <returns>returned desk object</returns>
        public Desk getDeskById(string id)
        {
            try
            {
                int ID = Int32.Parse(id);
                desks = dbDeskdMng.GetDesk(ID); //check if Desk is in database
                if (desks.Count > 0) //if is in database -> check entry
                {
                    return desks[0];
                }
                else
                {
                    MessageBox.Show(Msgs.DeskDontExist + "->" + id.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            catch
            {
                MessageBox.Show(Msgs.WrongInput + "->" + id.ToString(), Msgs.Error, MessageBoxButton.OK);
                return null;
            }
        }

        /// <summary>
        /// returns a desk by name. Name has to be uniqe in database
        /// </summary>
        /// <param name="name">name of a desk in databas</param>
        /// <returns>returned desk object</returns>
        public Desk getDeskByName(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                desks = dbDeskdMng.GetDesk(name); //check if Desk is in database
                if (desks.Count > 0) //if is in database -> check entry
                {
                    return desks[0];
                }
                else
                {
                    MessageBox.Show(Msgs.DeskDontExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// list all buildinsg in a database
        /// </summary>
        /// <returns>list of desks</returns>
        public List<Desk> getAllBuilds()
        {
            desks = dbDeskdMng.GetAllDesks(); //get all desks from a database
            if (desks.Count > 0) //if is in database -> returns entries
            {
                return desks;
            }
            else
            {
                MessageBox.Show(Msgs.DesksDontExist, Msgs.Error, MessageBoxButton.OK);
                return null;
            }
        }
    }
}
