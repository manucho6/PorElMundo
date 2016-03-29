using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.ViewModels
{
    public class StopViewModel
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255, MinimumLength =5)]
        public string Nombre { get; set; }

        public double Longitud { get; set; }
        public double Latitud { get; set; }

        [Required]
        public DateTime Arrivo { get; set; }
    }
}
