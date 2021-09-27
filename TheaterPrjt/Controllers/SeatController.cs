using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Business;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Controllers
{
    [ApiController]
    [Route("api/Seat")]
    public class SeatController : ControllerBase
    {
        SeatManager manager = new SeatManager();
        [HttpPost("GetFilteredSeats")]
        public IEnumerable<SeatDetail> GetFilteredSeats(SeatQuery query)
        {
            return manager.GetFilteredSeats(query);
        }

        [HttpGet("GetSeat")]
        public Seat GetSeat(long seatId)
        {
            return manager.GetSeat(seatId);
        }

        [HttpPost("AddSeat")]
        public OperationOutput AddSeat(Seat seat)
        {
            return manager.AddSeat(seat);
        }

        [HttpPost("EditSeat")]
        public OperationOutput EditSeat(Seat seat)
        {
            return manager.EditSeat(seat);
        }

        [HttpDelete("DeleteSeat")]
        public OperationOutput DeleteSeat(long seatId)
        {
            return manager.DeleteSeat(seatId);
        }

        [HttpGet("ReserveSeat")]
        public OperationOutput ReserveSeat(long seatId, long playId)
        {
            return manager.RequestSeatReservation(seatId, playId);
        }
        [HttpGet("ConfirmSeat")]
        public OperationOutput ConfirmSeat(long seatPlayId)
        {
            return manager.ConfirmSeatReservation(seatPlayId);
        }
        [HttpGet("DeclineSeat")]
        public OperationOutput DeclineSeat(long seatPlayId)
        {
            return manager.DeclineSeatReservation(seatPlayId);
        }

    }
}
