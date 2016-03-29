using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheWorld.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Creacion { get; set; }
        public string Usuario { get; set; }

        public ICollection<Stop>Stops { get; set; }
    }
}
