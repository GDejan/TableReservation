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
    internal class RoomMng
    {
        private DbRoomMng dbRoomMng = new DbRoomMng();
        private Checks checks = new Checks();
        private List<Room> rooms = new List<Room>();
        private Msgs Msgs = new Msgs();

        /// <summary>
        /// Add new room to the database
        /// </summary>
        /// <param name="name">Name of a room</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool NewRoom(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                rooms = dbRoomMng.GetRoom(name); //check if room is in database

                if (rooms.Count == 0) //if is not in database -> create new entry
                {
                    dbRoomMng.NewRoom(new Room(name));
                    MessageBox.Show(Msgs.RoomCreated + "->" + name.ToString(), Msgs.Ok, MessageBoxButton.OK);
                    return true;
                }
                else
                {
                    MessageBox.Show(Msgs.RoomExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing room in database. If parameter is null then it takes old name
        /// </summary>
        /// <param name="id">id of a room in databas</param>
        /// <param name="name">new name of a room</param>
        /// <param name="oldRoom">old room object</param>
        public void ChangeRoom(int id, string name, Room oldRoom)
        {
            string newname = "";

            if (name != "") newname = name; else newname = oldRoom.Name;

            if (checks.InputCheck(newname)) //check entry data
            {
                rooms = dbRoomMng.GetRoom(newname);
                if ((rooms.Count == 0) || (newname == oldRoom.Name)) //if is not in database -> change entry
                {

                    dbRoomMng.ChangeRoom(id, newname);
                    MessageBox.Show(Msgs.RoomChanged + "->" + oldRoom.Name.ToString() + " to " + newname.ToString(), Msgs.Ok, MessageBoxButton.OK);

                }
                else
                {
                    MessageBox.Show(Msgs.RoomExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                }
            }
        }

        /// <summary>
        /// Remove room from a database
        /// </summary>
        /// <param name="id">id of a room in databas</param>
        public void RemoveRoom(int id)
        {
            dbRoomMng.RemoveRoom(id);
            MessageBox.Show(Msgs.RoomRemoved + "->" + id.ToString(), Msgs.Ok, MessageBoxButton.OK);
        }

        /// <summary>
        /// returns a room by id
        /// </summary>
        /// <param name="id">id of a room in databas</param>
        /// <returns>>returned room object</returns>
        public Room getRoomById(string id)
        {
            try
            {
                int ID = Int32.Parse(id);
                rooms = dbRoomMng.GetRoom(ID); //check if room is in database
                if (rooms.Count > 0) //if is in database -> check entry
                {
                    return rooms[0];
                }
                else
                {
                    MessageBox.Show(Msgs.RoomDontExist + "->" + id.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            catch
            {
                MessageBox.Show(Msgs.WrongInput + "->" + id+ToString(), Msgs.Error, MessageBoxButton.OK);
                return null;
            }
        }

        /// <summary>
        /// returns a room by name. Name has to be uniqe in database
        /// </summary>
        /// <param name="name">name of a room in databas</param>
        /// <returns>>returned room object</returns>
        public Room getRoomByName(string name)
        {
            if (checks.InputCheck(name)) //check entry data
            {
                rooms = dbRoomMng.GetRoom(name); //check if room is in database
                if (rooms.Count > 0) //if is in database -> check entry
                {
                    return rooms[0];
                }
                else
                {
                    MessageBox.Show(Msgs.RoomDontExist + "->" + name.ToString(), Msgs.Error, MessageBoxButton.OK);
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// list all rooms in a database
        /// </summary>
        /// <returns>list of rooms</returns>
        public List<Room> getAllBuilds()
        {
            rooms = dbRoomMng.GetAllRooms(); //get all rooms from a database
            if (rooms.Count > 0) //if is in database -> returns entries
            {
                return rooms;
            }
            else
            {
                MessageBox.Show(Msgs.RoomsDontExist, Msgs.Error, MessageBoxButton.OK);
                return null;
            }
        }
    }
}
