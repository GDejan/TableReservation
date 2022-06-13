using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TableReservation.ViewModels
{
    internal class StoreyDesk
    {
        public string DeskFullName { get; set; }
        public string UserFullName { get; set; }        
        public DateTime ReservedAt
        {
            get
            {
                return ReservedAtDate.ToDateTime(TimeOnly.Parse("00:00 AM"));
            }
            set
            {
                ReservedAtDate = DateOnly.FromDateTime(value);
            }
        }
        public DateOnly ReservedAtDate { get; set; }
        
    }
}
