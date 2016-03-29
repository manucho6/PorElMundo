using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class TripViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255, MinimumLength =5)]
        public String Nombre { get; set; }
        public DateTime Creacion { get; set; } = DateTime.UtcNow;
        
        public IEnumerable<StopViewModel> Stops { get; set; }
    }
}
