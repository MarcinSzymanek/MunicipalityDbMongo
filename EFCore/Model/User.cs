using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Model
{
    public class User
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long CPRNumber { get; set; }

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int? CVR { get; set; }
        public int PhoneNumber { get; set; }
        public List<Reservation> Reservations { get; set; } = new();

    }

    public class BusinessUser : User
    {
        [Required]
        public int BusinessCVR { get; set; }
    }
}
