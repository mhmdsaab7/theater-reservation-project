using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class Seat
    {
        public long SeatId { get; set; }
        public string Number { get; set; }
        public long TheaterId { get; set; }
    }
}
