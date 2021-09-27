using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Data
{
    public class SeatStatusDataManager : BaseSQLDataManager
    {
        public SeatStatus GetSeatStatus(Guid seatStatusId)
        {
            return GetSPItem("sp_SeatStatus_GetById", SeatStatusMapper, seatStatusId);
        }

        private SeatStatus SeatStatusMapper(IDataReader dataReader)
        {
            return new SeatStatus
            {
                SeatStatusId = (Guid)dataReader["ID"],
                Color = (string)dataReader["Color"],
                Name = (string)dataReader["Name"],
            };
        }
    }
}
