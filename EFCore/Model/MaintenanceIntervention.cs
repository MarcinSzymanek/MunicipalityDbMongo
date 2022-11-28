using System.ComponentModel.DataAnnotations;

namespace EFCore.Model;

public class MaintenanceIntervention
{
    public long Id { get; set; }
    [Required]
    public string TechnicianName { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    public Facility Facility { get; set; }
}