using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class SeatPlay
    {
        public long? SeatPlayId { get; set; }
        public long SeatId { get; set; }
        public string SeatNumber { get; set; }
        public Guid? SeatStatusId { get; set; }
        public long? UserId { get; set; }
        public long? PlayId { get; set; }
        public long TheaterId { get; set; }
    }
}
