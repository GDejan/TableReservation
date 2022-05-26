using System.Collections.Generic;
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
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Add new building to the database
        /// </summary>
        /// <param name="building">object building</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool NewBuilding(Building building)
        {
            buildings = dbBuildMng.GetBuilding(building); //check if building is in database

            if (buildings.Count == 0) //if is not in database -> create new entry
            {
                dbBuildMng.NewBuilding(building);
                MessageBox.Show(msgs.BuildCreated + "->" + building.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else
            {
                MessageBox.Show(msgs.BuildExist + "->" + building.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing building in database
        /// </summary>
        /// <param name="newBuilding">new building as object</param>
        /// <param name="oldBuilding">old building as object</param>
        public void ChangeBuilding(Building newBuilding , Building oldBuilding)
        {
            buildings = dbBuildMng.GetBuilding(newBuilding);
            if (buildings.Count == 0)  //if new is not in database -> change entry
            {
                dbBuildMng.ChangeBuilding(newBuilding);
                MessageBox.Show(msgs.BuildChanged + "->" + oldBuilding.Name.ToString() + " to " + newBuilding.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(msgs.BuildExist + "->" + newBuilding.Name.ToString(), msgs.Error, MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Remove building from a database
        /// </summary>
        /// <param name="building">building as object</param>
        public void RemoveBuilding(Building building)
        {
            buildings = dbBuildMng.GetBuilding(building.Id);

            if (buildings.Count == 1)  //if is in database -> delete entry
            {
                dbBuildMng.RemoveBuilding(building);
                MessageBox.Show(msgs.BuildRemoved + "->" + building.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
            }
            else if (buildings.Count > 1)
            {
                MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(msgs.BuildDontExist + "->" + building.Id.ToString(), msgs.Error, MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// returns a building by id (string)
        /// </summary>
        /// <param name="id">id of a building in database</param>
        /// <returns>returned building object</returns>
        public Building getBuildById(string id)
        {
            if (checks.InputCheckStringInt(id))
            {
                buildings = dbBuildMng.GetBuilding(int.Parse(id)); //check if building is in database
                if (buildings!=null)
                {
                    if (buildings.Count == 1) //if is in database -> check entry
                    {
                        return buildings[0];
                    }
                    else if (buildings.Count >= 1)
                    {
                        MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(msgs.BuildDontExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
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
        /// list all buildinsg in a database
        /// </summary>
        /// <returns>list of buildings</returns>
        public List<Building> getAllBuilds()
        {
            buildings = dbBuildMng.GetAllBuildings(); //get all buildings from a database
            if (buildings!=null)
            {
                if (buildings.Count > 0) //if is in database -> returen entries
                {
                    return buildings;
                }
                else
                {
                    MessageBox.Show(msgs.BuildsDontExist, msgs.Error, MessageBoxButton.OK);
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
