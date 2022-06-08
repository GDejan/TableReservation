using System;

namespace TableReservation.ViewModels
{
    internal class DeskUser
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public DateTime ReservedAt
        {
            get
            {
                return ReservedAtDate.ToDateTime(TimeOnly.Parse("00:00 PM"));
            }
            set
            {
                ReservedAtDate = DateOnly.FromDateTime(value);
            }
        }
        public DateOnly ReservedAtDate { get; set; }
        public string FullName()
        {
            return string.Format(this.Name + " " + this.Surname);
        }
    }
}
