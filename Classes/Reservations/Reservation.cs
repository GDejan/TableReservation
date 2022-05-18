using System;

namespace TableReservation.Classes
{
    internal class Reservation
    {
        public int Id { get; set; }
        public int BuildingId { get; set; }
        public int StoreyId { get; set; }
        public int RoomId { get; set; }
        public int DeskId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime TimeStamp { get; set; }
        public Reservation()
        {

        }
    }
}
