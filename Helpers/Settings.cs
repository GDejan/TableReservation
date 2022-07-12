namespace TableReservation.Helpers
{
    public class Settings
    {
        //xml file - desk setup in a room
        public string XmlRoomSetup = @"./Design/RoomDesing.xml";
        //jpg icon for a reserved desk
        public string DeskFixedResPath = @"/Icons/DeskFixedRes.png";
        //jpg icon for a reserved desk
        public string DeskReservedPath = @"/Icons/DeskReserved.png";
        //jpg icon for a free desk
        public string DeskFreePath = @"/Icons/DeskFree.png";
        //name of a masteradmin in a database
        public string MasterAdmin = "masteradmin";
    }
}
