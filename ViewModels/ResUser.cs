using System;   

namespace TableReservation.ViewModels
{
    /// <summary>
    /// View model for list box on user page
    /// </summary>
    internal class ResUser
    {
        public int Id { get; set; }
        public string BuildingName { get; set; }
        public string StoreyName { get; set; }
        public string RoomName { get; set; }
        public string DeskName { get; set; }
        public string FullName { get; set; }
        public DateTime ReservedAt
        {
            set 
            {
                ReservedAtDate = DateOnly.FromDateTime(value);
            }
        }
        public DateOnly ReservedAtDate { get; set; }
    }
}
