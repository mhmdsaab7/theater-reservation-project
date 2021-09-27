using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Data;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Business
{
    public class TheaterManager
    {
        TheaterDataManager _dataManager = new TheaterDataManager();

        public IEnumerable<TheaterDetail> GetFilteredTheaters(TheaterQuery query)
        {
            return _dataManager.GetTheaters(query.Name).Select(x => TheaterDetailMapper(x));
        }

        private TheaterDetail TheaterDetailMapper(Theater theater)
        {
            return new TheaterDetail
            {
                TheaterId = theater.TheaterId,
                TheaterName = theater.Name,
            };
        }

        public OperationOutput AddTheater(Theater theater)
        {
            var operationOutput = new OperationOutput();
            if (_dataManager.AddTheater(theater, out _))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Internal Server Error: Cannot Add Theater";
            }
            return operationOutput;
        }

        public OperationOutput EditTheater(Theater theater)
        {
            var operationOutput = new OperationOutput();
            if (_dataManager.UpdateTheater(theater))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Internal Server Error: Cannot Update Theater";
            }
            return operationOutput;
        }

        public List<Theater> GetTheaters()
        {
            return _dataManager.GetTheaters(null);
        }
        public string GetTheaterName(long theaterId)
        {
            var theater = _dataManager.GetTheater(theaterId);
            if (theater == null)
                return null;
            return theater.Name;
        }

        public Theater GetTheater(long theaterId)
        {
            return _dataManager.GetTheater(theaterId);
        }

        public OperationOutput DeleteTheater(long theaterId)
        {
            var operationOutput = new OperationOutput();
            var seatManager = new SeatManager();
            var reservedSeats = seatManager.GetSeatsByStatusId(theaterId, null, null, SeatStatus.Confirmed);
            if (reservedSeats == null || reservedSeats.Count == 0)
            {
                if (_dataManager.DeleteTheater(theaterId))
                {
                    operationOutput.IsSucceeded = true;
                }
                else
                {
                    operationOutput.ErrorMessage = $"Internal Server Error: Cannot Delete theater {theaterId}";
                }
            }
            else
            {
                operationOutput.ErrorMessage = "Cannot Delete theater having seats reserved";
            }

            return operationOutput;
        }
    }
}
