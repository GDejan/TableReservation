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
    internal class BuildMng
    {
        private DbBuildMng dbBuildMng = new DbBuildMng();
        private Checks checks = new Checks();
        private List<Building> buildings = new List<Building>();
        private Msgs Msgs = new Msgs();

        /// <summary>
        /// Add new building to the database
        /// </summary>
        /// <param name="name">Name of a building</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool NewBuilding(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                buildings = dbBuildMng.GetBuilding(name); //check if building is in database

                if (buildings.Count == 0) //if is not in database -> create new entry
                {
                    dbBuildMng.NewBuilding(new Building(name));
                    MessageBox.Show(Msgs.BuildCreated + "->" + name.ToString(), Msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(Msgs.BuildExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing building in database. If parameter is null then it takes old name
        /// </summary>
        /// <param name="id">id of a building in databas</param>
        /// <param name="name">new name of a building</param>
        /// <param name="oldBuilding"> old building object</param>
        public void ChangeBuilding(int id, string name, Building oldBuilding)
        {
            string newname = "";

            if (name != "") newname = name; else newname = oldBuilding.Name;

            if (checks.InputCheck(newname)) //check entry data
            {
                buildings = dbBuildMng.GetBuilding(newname);
                if ((buildings.Count == 0) || (newname == oldBuilding.Name)) //if is not in database -> change entry
                {

                    dbBuildMng.ChangeBuilding(id, newname);
                    MessageBox.Show(Msgs.BuildChanged + "->" + oldBuilding.Name.ToString() + " to " + newname.ToString(), Msgs.Ok, MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show(Msgs.BuildExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Remove building from a database
        /// </summary>
        /// <param name="id">id of a building in database</param>
        public void RemoveBuilding(int id)
        {
            dbBuildMng.RemoveBuilding(id);
            MessageBox.Show(Msgs.BuildRemoved + "->" + id.ToString(), Msgs.Ok, MessageBoxButton.OK);
        }

        /// <summary>
        /// returns a building by id
        /// </summary>
        /// <param name="id">id of a building in database</param>
        /// <returns>returned building object</returns>
        public Building getBuildById(string id)
        {
            try
            {
                int ID = Int32.Parse(id);
                buildings = dbBuildMng.GetBuilding(ID); //check if building is in database
                if (buildings.Count > 0) //if is in database -> check entry
                {
                    return buildings[0];
                }
                else
                {
                    MessageBox.Show(Msgs.BuildDontExist + "->" + id.ToString(), Msgs.Error, MessageBoxButton.OK);
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
        /// returns a building by name. Name has to be uniqe in database
        /// </summary>
        /// <param name="name">name of a building in database</param>
        /// <returns>returned building object</returns>
        public Building getBuildByName(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                buildings = dbBuildMng.GetBuilding(name); //check if building is in database
                if (buildings.Count > 0) //if is in database -> check entry
                {
                    return buildings[0];
                }
                else
                {
                    MessageBox.Show(Msgs.BuildDontExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
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
        /// <returns>list of buildings</returns>
        public List<Building> getAllBuilds()
        {
            buildings = dbBuildMng.GetAllBuildings(); //get all buildings from a database
            if (buildings.Count > 0) //if is in database -> returen entries
            {
                return buildings;
            }
            else
            {
                MessageBox.Show(Msgs.BuildsDontExist, Msgs.Error, MessageBoxButton.OK);
                return null;
            }
        }
    }
}
