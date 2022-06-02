using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableReservation.Helpers
{
    public class Settings
    {
        //xml file - desk setup in a room
        public string XmlRoomSetup = @"./Design/RoomDesing.xml";
        //jpg icon for a reserved desk
        public string DeskReservedPath = @"/Icons/DeskReserved.jpg";
        //jpg icon for a free desk
        public string DeskFreePath = @"/Icons/DeskFree.jpg";
        //name of a masteradmin in a database
        public string MasterAdmin = "masteradmin";
    }
}
