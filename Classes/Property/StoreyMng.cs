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
    internal class StoreyMng
    {
        private DbStoreyMng dbStoreyMng = new DbStoreyMng();
        private Checks checks = new Checks();
        private List<Storey> storeys = new List<Storey>();
        private Msgs Msgs = new Msgs();

        /// <summary>
        /// Add new storey to the database
        /// </summary>
        /// <param name="name">Name of a storey</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool NewStorey(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                storeys = dbStoreyMng.GetStorey(name); //check if storey is in database

                if (storeys.Count == 0) //if is not in database -> create new entry
                {
                    dbStoreyMng.NewStorey(new Storey(name));
                    MessageBox.Show(Msgs.StoreyCreated + "->" + name.ToString(), Msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(Msgs.StoreyExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing storey in database. If parameter is null then it takes old name
        /// </summary>
        /// <param name="id">id of a storey in databas</param>
        /// <param name="name">new name of a storey</param>
        /// <param name="oldStorey"> old storey object</param>
        public void ChangeStorey(int id, string name, Storey oldStorey)
        {
            string newname = "";

            if (name != "") newname = name; else newname = oldStorey.Name;

            if (checks.InputCheck(newname)) //check entry data
            {
                storeys = dbStoreyMng.GetStorey(newname);
                if ((storeys.Count == 0) || (newname == oldStorey.Name)) //if is not in database -> change entry
                {

                    dbStoreyMng.ChangeStorey(id, newname);
                    MessageBox.Show(Msgs.StoreyChanged + "->" + oldStorey.Name.ToString() + " to " + newname.ToString(), Msgs.Ok, MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show(Msgs.StoreyExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Remove building from a database
        /// </summary>
        /// <param name="id">id of a storey in databas</param>
        public void RemoveStorey(int id)
        {
            dbStoreyMng.RemoveStorey(id);
            MessageBox.Show(Msgs.StoreyRemoved + "->" + id.ToString(), Msgs.Ok, MessageBoxButton.OK);
        }

        /// <summary>
        /// returns a building by id
        /// </summary>
        /// <param name="id">id of a storey in databas</param>
        /// <returns>returned storey object</returns>
        public Storey getStoreyById(string id)
        {
            try
            {
                int ID = Int32.Parse(id);
                storeys = dbStoreyMng.GetStorey(ID); //check if storey is in database
                if (storeys.Count > 0) //if is in database -> check entry
                {
                    return storeys[0];
                }
                else
                {
                    MessageBox.Show(Msgs.StoreyDontExist + "->" + id.ToString(), Msgs.Error, MessageBoxButton.OK);
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
        /// returns a storey by name. Name has to be uniqe in database
        /// </summary>
        /// <param name="name">name of a storey in databas</param>
        /// <returns>returned storey object</returns>
        public Storey getStoreyByName(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                storeys = dbStoreyMng.GetStorey(name); //check if storey is in database
                if (storeys.Count > 0) //if is in database -> check entry
                {
                    return storeys[0];
                }
                else
                {
                    MessageBox.Show(Msgs.StoreyDontExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// list all storeys in a database
        /// </summary>
        /// <returns>list of storeys</returns>
        public List<Storey> getAllStoreys()
        {
            storeys = dbStoreyMng.GetAllStoreys(); //get all storeys from a database
            if (storeys.Count > 0) //if is in database -> returns entries
            {
                return storeys;
            }
            else
            {
                MessageBox.Show(Msgs.StoreysDontExist, Msgs.Error, MessageBoxButton.OK);
                return null;
            }
        }
    }
}
