using System;

namespace TableReservation.Classes
{
    internal class Reservation
    {
        public string Name { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime ReservedTill { get; set; }
        public User User { get; set; }
    }
}
