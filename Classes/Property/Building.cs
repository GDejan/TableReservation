using System.Collections.Generic;

namespace TableReservation.Classes
{
    internal class Building
    {
        public int Id { get;}
        public string Name { get; set; }
        List<Storey> FLoors = new List<Storey>();
        public Building()
        {
        }
        public Building(string name)
        {
            this.Name = name;
        }
    }
}
