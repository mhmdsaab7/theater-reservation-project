using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class PlayDetail
    {
        public long PlayId { get; set; }
        public string PlayName { get; set; }
        public string TheaterName { get; set; }
        public long TheaterId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool ShowEditAction { get; set; }
        public bool ShowDeleteAction { get; set; }
    }
}
