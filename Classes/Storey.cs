using System.Collections.Generic;

namespace TableReservation.Classes
{
    internal class Storey
    {
        public string Name { get; set; }
        List<Room> Rooms = new List<Room>();
    }
}
