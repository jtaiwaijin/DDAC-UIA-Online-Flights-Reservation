using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models
{
    public class Plane
    {
        public int ID { get; set; }
        public string TailNo { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int FirstCap { get; set; }
        public int BusCap { get; set; }
        public int EconCap { get; set; }
    }
}
