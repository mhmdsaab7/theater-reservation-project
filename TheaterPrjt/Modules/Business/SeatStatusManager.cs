using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Data;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Business
{
    public class SeatStatusManager
    {
        SeatStatusDataManager _dataManager = new SeatStatusDataManager();

        public string GetSeatStatusName(Guid seatStatusId)
        {
            var seatStatus = GetSeatStatus(seatStatusId);
            if (seatStatus == null)
                return null;
            return seatStatus.Name;
        }

        public SeatStatus GetSeatStatus(Guid seatStatusId)
        {
            return _dataManager.GetSeatStatus(seatStatusId);
        }
    }
}
