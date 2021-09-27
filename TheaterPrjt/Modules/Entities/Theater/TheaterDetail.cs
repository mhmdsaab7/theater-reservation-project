using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class TheaterDetail
    { 
        public long TheaterId { get; set; }
        public string TheaterName { get; set; }
        public bool ShowEditAction { get; set; }
        public bool ShowDeleteAction { get; set; }
    }
}
