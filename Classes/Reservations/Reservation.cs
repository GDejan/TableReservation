using System;

namespace TableReservation.Resevations
{
    internal class Reservation
    {
        public int Id { get; private set; }
        public int BuildingId { get; set; }
        public int StoreyId { get; set; }
        public int RoomId { get; set; }
        public int DeskId { get; set; }
        public int UserId { get; set; }
        public DateTime ReservedAt { get; set; }
        public DateTime TimeStamp { get;}
        public Reservation()
        {

        }
        public Reservation(int buildingId, int storeyId, int roomId, int deskId, int userId, DateTime reservedAt)
        {
            this.BuildingId = buildingId;
            this.StoreyId = storeyId;
            this.RoomId = roomId;
            this.DeskId = deskId;
            this.UserId = userId;
            this.ReservedAt = reservedAt;
        }
        public Reservation(int id, int buildingId, int storeyId, int roomId, int deskId, int userId, DateTime reservedAt)
            : this(buildingId, storeyId, roomId, deskId, userId, reservedAt)
        {
            this.Id = Id;
        }
    }
}
