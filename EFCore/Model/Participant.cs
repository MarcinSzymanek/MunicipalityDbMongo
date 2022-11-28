using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Model;

public class Participant
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public long CPRNumber { get; set; }
    public List<Reservation> Reservations { get; set; } = new();
}