using System;

namespace TheWorld.Models
{
    public class Stop
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public double Longitud { get; set; }
        public double Latitud { get; set; }
        public DateTime Arrivo { get; set; }
        public int Orden { get; set; }
    }
}