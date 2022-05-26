using System.Collections.Generic;
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
        private Msgs msgs = new Msgs();

        /// <summary>
        /// Add new room to the database
        /// </summary>
        /// <param name="room">object room</param>
        /// <returns>Returns a true if succeded or false if not</returns>
        public bool NewRoom(Room room)
        {
            rooms = dbRoomMng.GetRoom(room); //check if room is in database

            if (rooms.Count == 0) //if is not in database -> create new entry
            {
                dbRoomMng.NewRoom(room);
                MessageBox.Show(msgs.RoomCreated + "->" + room.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
                return true;
            }
            else
            {
                MessageBox.Show(msgs.RoomExist + "->" + room.Name.ToString(), msgs.Error, MessageBoxButton.OK);
                return false;
            }
        }

        /// <summary>
        /// Change parameters of a existing room in database
        /// </summary>
        /// <param name="newRoom">new room as object</param>
        /// <param name="oldRoom">old room as object</param>
        public void ChangeRoom(Room newRoom, Room oldRoom)
        {
            rooms = dbRoomMng.GetRoom(newRoom);
            if (rooms.Count == 0) //if is not in database -> change entry
            {
                dbRoomMng.ChangeRoom(newRoom);
                MessageBox.Show(msgs.RoomChanged + "->" + oldRoom.Name.ToString() + " to " + newRoom.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(msgs.RoomExist + "->" + newRoom.Name.ToString(), msgs.Error, MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// Remove room from a database
        /// </summary>
        /// <param name="room">room as object</param>
        public void RemoveRoom(Room room)
        {
            rooms = dbRoomMng.GetRoom(room.Id);

            if (rooms.Count == 1)  //if is in database -> delete entry
            {
                dbRoomMng.RemoveRoom(room);
                MessageBox.Show(msgs.RoomRemoved + "->" + room.Name.ToString(), msgs.Ok, MessageBoxButton.OK);
            }
            else if (rooms.Count > 1)
            {
                MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
            }
            else
            {
                MessageBox.Show(msgs.RoomDontExist + "->" + room.Id.ToString(), msgs.Error, MessageBoxButton.OK);
            }
        }

        /// <summary>
        /// returns a room by id
        /// </summary>
        /// <param name="id">id of a room in databas</param>
        /// <returns>>returned room object</returns>
        public Room getRoomById(string id)
        {
            if (checks.InputCheckStringInt(id))
            {
                rooms = dbRoomMng.GetRoom(int.Parse(id)); //check if room is in database3
                if (rooms != null)
                {
                    if (rooms.Count == 1) //if is in database -> check entry
                    {
                        return rooms[0];
                    }
                    else if (rooms.Count >= 1)
                    {
                        MessageBox.Show(msgs.Wrong + " too many Ids", msgs.Error, MessageBoxButton.OK);
                        return null;
                    }
                    else
                    {
                        MessageBox.Show(msgs.RoomDontExist + "->" + id.ToString(), msgs.Error, MessageBoxButton.OK);
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
        /// list all rooms in a database
        /// </summary>
        /// <returns>list of rooms</returns>
        public List<Room> getAllRooms()
        {
            rooms = dbRoomMng.GetAllRooms(); //get all rooms from a database
            if (rooms != null)
            {
                if (rooms.Count > 0) //if is in database -> returns entries
                {
                    return rooms;
                }
                else
                {
                    MessageBox.Show(msgs.RoomsDontExist, msgs.Error, MessageBoxButton.OK);
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
