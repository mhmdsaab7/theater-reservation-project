using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class PlayQuery
    {
        public string TheaterId { get; set; }
        public string PlayName { get; set; }
        public DateTime? EffectiveTime { get; set; }
    }
}
