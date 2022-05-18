using System.Collections.Generic;

namespace TableReservation.Classes
{
    internal class Storey
    {
        public int Id { get; }
        public string Name { get; set; }
        List<Room> Rooms = new List<Room>();
        public Storey()
        {
        }
        public Storey(string name)
        {
            this.Name = name;
        }
    }
}
