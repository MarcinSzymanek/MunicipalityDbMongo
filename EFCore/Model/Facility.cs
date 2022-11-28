using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Model
{
    public class Facility
    {
        public int Id { get; set; }
        public string FacName { get; set; } = null!;
        public double GPS_lat { get; set; }
        public double GPS_lon { get; set; }
        public string? FacType { get; set; } = null!;
        public string? FacRules { get; set; } = null!;
        public string? FacItems { get; set; } = null!;
        public List<Reservation> Reservations { get; set; } = new();
        public List<MaintenanceIntervention> MaintenanceHistory { get; set; } = new();
    }
}
