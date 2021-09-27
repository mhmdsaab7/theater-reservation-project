using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class SeatQuery
    {
        public string TheaterId { get; set; }
        public string PlayId { get; set; }
        public string SeatNumber { get; set; }
        public long LoggedInUserId { get; set; }
    }
}
