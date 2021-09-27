using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Data;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Business
{
    public class SeatManager
    {
        SeatDataManager _seatDataManager = new SeatDataManager();
        SeatStatusManager seatStatusManager = new SeatStatusManager();
        TheaterManager theaterManager = new TheaterManager();
        PlayManager playManager = new PlayManager();
        UserManager userManager = new UserManager();
        public IEnumerable<SeatDetail> GetFilteredSeats(SeatQuery query)
        {
            var theaterId = long.Parse(query.TheaterId);
            var playId = long.Parse(query.PlayId);
            var user = userManager.GetUser(query.LoggedInUserId);
            var seats = _seatDataManager.GetSeats(theaterId, playId, query.SeatNumber);
            if (seats == null || seats.Count == 0)
                return null;
            return seats.Select(x => SeatDetailMapper(x, query.LoggedInUserId, user.IsAdmin));
        }

        public OperationOutput AddSeat(Seat seatToAdd)
        {
            var operationOutput = new OperationOutput();
            if (_seatDataManager.AddSeat(seatToAdd, out _))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Internal Server Error: Cannot Add Seat";
            }
            return operationOutput;
        }

        public OperationOutput EditSeat(Seat seat)
        {
            var operationOutput = new OperationOutput();
            if (_seatDataManager.UpdateSeat(seat))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Internal Server Error: Cannot Update Seat";
            }
            return operationOutput;
        }

        public OperationOutput DeleteSeat(long seatId)
        {
            var operationOutput = new OperationOutput();
            var reservedSeats = GetSeatsByStatusId(null, null, seatId, SeatStatus.Confirmed);
            if (reservedSeats == null || reservedSeats.Count == 0)
            {
                if (_seatDataManager.DeleteSeat(seatId))
                {
                    operationOutput.IsSucceeded = true;
                }
                else
                {
                    operationOutput.ErrorMessage = $"Internal Server Error: Cannot Delete seat {seatId}";
                }
            }
            else
            {
                operationOutput.ErrorMessage = "Cannot Delete reserved seat ";
            }

            return operationOutput;
        }

        public Seat GetSeat(long seatId)
        {
            return _seatDataManager.GetSeat(seatId);
        }

        public OperationOutput RequestSeatReservation(long seatId, long playId)
        {
            var operationOutput = new OperationOutput();
            var seatPlay = new SeatPlay
            {
                SeatId = seatId,
                PlayId = playId,
                SeatStatusId = SeatStatus.Requested,
                UserId = UserManager.GetLoggedInUserId()
            };
            if (_seatDataManager.ReserveSeat(seatPlay))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Cannot Reserve Seat";
            }
            return operationOutput;

        }
        public OperationOutput ConfirmSeatReservation(long seatPlayId)
        {
            var operationOutput = new OperationOutput();
            if (_seatDataManager.RespondSeatReservation(seatPlayId, SeatStatus.Confirmed))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Cannot Confirm Seat Reservation";
            }
            return operationOutput;
        }

        public OperationOutput DeclineSeatReservation(long seatPlayId)
        {
            var operationOutput = new OperationOutput();
            if (_seatDataManager.RespondSeatReservation(seatPlayId, SeatStatus.Declined))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Cannot Decline Seat Reservation";
            }
            return operationOutput;
        }

        public List<Seat> GetSeatsByStatusId(long? theaterId, long? playId, long? seatId, Guid statusId)
        {
            return _seatDataManager.GetSeatsByStatusId(theaterId, playId, seatId, statusId);
        }

        private SeatDetail SeatDetailMapper(SeatPlay seatPlay, long loggedInUserId, bool isAdmin)
        {
            Guid statusId;
            if (!seatPlay.SeatStatusId.HasValue)
            {
                statusId = SeatStatus.Empty;
            }
            else
            {
                if (seatPlay.UserId.Value == loggedInUserId || isAdmin)
                {
                    statusId = seatPlay.SeatStatusId.Value;
                }
                else
                {
                    statusId = seatPlay.SeatStatusId.Value == SeatStatus.Requested || seatPlay.SeatStatusId.Value == SeatStatus.Confirmed ? SeatStatus.AlreadyTaken : SeatStatus.Empty;
                }
            }
            var status = seatStatusManager.GetSeatStatus(statusId);
            if (status == null)
            {
                throw new NullReferenceException($"Seat Status: {statusId}");
            }
            return new SeatDetail
            {
                PlayId = seatPlay.PlayId,
                SeatPlayId = seatPlay.SeatPlayId,
                SeatId = seatPlay.SeatId,
                SeatStatusId = statusId,
                TheaterId = seatPlay.TheaterId,
                UserId = seatPlay.UserId,
                SeatNumber = seatPlay.SeatNumber,
                Color = status.Color,
                SeatStatusName = status.Name,
                TheaterName = theaterManager.GetTheaterName(seatPlay.TheaterId),
                PlayName = seatPlay.PlayId.HasValue ? playManager.GetPlayName(seatPlay.PlayId.Value) : null,
                ShowReserveAction = statusId == SeatStatus.Empty,
                ShowConfirmAction = statusId == SeatStatus.Requested,
                ShowDeclineAction = statusId == SeatStatus.Requested
            };
        }
    }
}
