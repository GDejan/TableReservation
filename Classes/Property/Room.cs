using System.Collections.Generic;

namespace TableReservation.Classes
{
    internal class Room
    {
        public int Id { get; }
        public string Name { get; set; }
        List<Desk> Tables = new List<Desk>();
        public Room()
        {
        }
        public Room(string name)
        {
            this.Name = name;
        }
    }
}
