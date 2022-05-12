using System.Collections.Generic;

namespace TableReservation.Classes
{
    internal class Building
    {
        public string Name { get; set; }
        List<Storey> FLoors = new List<Storey>();
    }
}
