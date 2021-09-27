using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheaterPrjt.Entities
{
    public class SeatDetail
    {
        public long? SeatPlayId { get; set; }
        public long SeatId { get; set; }
        public string SeatNumber { get; set; }
        public string TheaterName { get; set; }
        public long TheaterId { get; set; }
        public long? PlayId { get; set; }
        public string PlayName { get; set; }
        public Guid SeatStatusId { get; set; }
        public string SeatStatusName { get; set; }
        public string Color { get; set; }
        public long? UserId { get; set; }
        public bool ShowEditAction { get; set; }
        public bool ShowDeleteAction { get; set; }
        public bool ShowReserveAction { get; set; }
        public bool ShowConfirmAction { get; set; }
        public bool ShowDeclineAction { get; set; }
    }
}
