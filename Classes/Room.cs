using System.Collections.Generic;

namespace TableReservation.Classes
{
    internal class Room
    {
        public string Name { get; set; }
        List<Desk> Tables = new List<Desk>();
    }
}
