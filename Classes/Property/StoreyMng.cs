using System.Collections.Generic;
using System.Windows;
using TableReservation.Database;
using TableReservation.Helpers;

namespace TableReservation.Property
{
    internal class StoreyMng
    {
        private DbStoreyMng dbStoreyMng = new DbStoreyMng();
        private Checks checks = new Checks();
        private List<Storey> storeys = new List<Storey>();
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Add new storey to the database
        /// </summary>
        /// <param name="storey">object storey</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool Create(Storey storey)
        {
            storeys = dbStoreyMng.GetByName(storey); //check if storey is in database
            if (storeys != null)
            {
                if (storeys.Count == 0) //if is not in database -> create new entry
                {
                    dbStoreyMng.Create(storey);
                    MessageBox.Show(msgs.StoreyCreated + "->" + storey.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(msgs.StoreyExist + "->" + storey.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else 
            {
                return false;   
            }
            
        }

        /// <summary>
        /// Change parameters of a existing storey in database
        /// </summary>
        /// <param name="newStorey">new name of a storey</param>
        /// <param name="oldStorey"> old storey object</param>
        /// /// <returns>Returns a true if succeded or false if not</returns>
        public bool Change(Storey newStorey, Storey oldStorey)
        {
            storeys = dbStoreyMng.GetByName(newStorey);
            if (storeys!=null)
            {
                if (storeys.Count == 0) //if is not in database -> change entry
                {
                    dbStoreyMng.Change(newStorey);
                    MessageBox.Show(msgs.StoreyChanged + "->" + oldStorey.Name.ToString() + " to " + newStorey.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(msgs.StoreyExist + "->" + newStorey.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Remove storey from a database
        /// </summary>
        /// <param name="storey">storey as object</param>
        /// /// <returns>Returns a true if succeded or false if not</returns>
        public bool Remove(Storey storey)
        {
            storeys = dbStoreyMng.GetById(storey.Id);
            if (storeys != null)
            {
                if (storeys.Count == 1)  //if is in database -> delete entry
                {
                    dbStoreyMng.Remove(storey);
                    MessageBox.Show(msgs.StoreyRemoved + "->" + storey.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else if (storeys.Count > 1)
                {
                    MessageBox.Show(msgs.Wrong + "->" + msgs.ManyIds, msgs.Error, MessageBoxButton.OK);
                    return false;
                }
                else
                {
                    MessageBox.Show(msgs.StoreyDontExist + "->" + storey.Id.ToString(), msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// returns a storey by id
        /// </summary>
        /// <param name="id">id of a storey in databas</param>
        /// <returns>returned storey object</returns>
        public Storey GetById(string id)
        {
            if (checks.InputCheckStringIntId(id))
            {
                storeys = dbStoreyMng.GetById(int.Parse(id)); //check if storey is in database
                if (storeys != null)
                {
                    if (storeys.Count == 1) //if is in database -> check entry
                    {
                        return storeys[0];
                    }
                    else if (storeys.Count >= 1)
                    {
                        MessageBox.Show(msgs.Wrong + "->" + msgs.ManyIds, msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(msgs.StoreyDontExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
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
        /// list all storeys in a database
        /// </summary>
        /// <returns>list of storeys</returns>
        public List<Storey> GetAll()
        {
            storeys = dbStoreyMng.GetAll(); //get all storeys from a database
            if (storeys != null)
            {
                if (storeys.Count > 0) //if is in database -> returns entries
                {
                    return storeys;
                }
                else
                {
                    MessageBox.Show(msgs.StoreyDontExist, msgs.Error, MessageBoxButton.OK);
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
