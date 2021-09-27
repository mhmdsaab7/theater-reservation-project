using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Data
{
    public class SeatDataManager : BaseSQLDataManager
    {
        public List<SeatPlay> GetSeats(long theaterId, long playId, string seatNumber)
        {
            return GetSPItems("sp_Seat_GetAll", SeatPlayMapper, theaterId, playId, seatNumber);
        }
        public Seat GetSeat(long seatId)
        {
            return GetSPItem("sp_Seat_GetById", SeatMapper, seatId);
        }
        public bool AddSeat(Seat seatToAdd, out long seatId)
        {
            seatId = 0;
            var result = ExecuteNonQuery("sp_Seat_Insert", out var objectId, seatToAdd.Number, seatToAdd.TheaterId);
            if (result)
                seatId = (long)objectId;
            return result;
        }

        public bool UpdateSeat(Seat seatToUpdate)
        {
            return ExecuteNonQuery("sp_Seat_Update", seatToUpdate.SeatId, seatToUpdate.Number, seatToUpdate.TheaterId);
        }

        public bool ReserveSeat(SeatPlay seatPlay)
        {
            return ExecuteNonQuery("sp_SeatPlay_Reserve", seatPlay.SeatId, seatPlay.PlayId, seatPlay.SeatStatusId, seatPlay.UserId);
        }


        public bool RespondSeatReservation(long seatPlayId, Guid newStatusId)
        {
            return ExecuteNonQuery("sp_SeatPlay_Respond", seatPlayId, newStatusId);
        }

        public bool DeleteSeat(long seatId)
        {
            return ExecuteNonQuery("sp_Seat_Delete", seatId);
        }

        public List<Seat> GetSeatsByStatusId(long? theaterId, long? playId, long? seatId, Guid statusId)
        {
            return GetSPItems("sp_Seat_GetSeatsByStatusId", SeatMapper, theaterId, playId, seatId, statusId);
        }

        private Seat SeatMapper(IDataReader dataReader)
        {
            return new Seat
            {
                SeatId = (long)dataReader["ID"],
                Number = (string)dataReader["Number"],
                TheaterId = (long)dataReader["TheaterID"],
            };
        }
        private SeatPlay SeatPlayMapper(IDataReader dataReader)
        {
            return new SeatPlay
            {
                SeatId = (long)dataReader["ID"],
                SeatPlayId = dataReader["SeatPlayID"] != System.DBNull.Value ? (long?)dataReader["SeatPlayID"] :null,
                SeatNumber = (string)dataReader["SeatNumber"],
                SeatStatusId = dataReader["StatusID"] != System.DBNull.Value ? (Guid?)dataReader["StatusID"] :null,
                TheaterId = (long)dataReader["TheaterID"],
                UserId = dataReader["UserID"] != System.DBNull.Value ? (long?)dataReader["UserID"] : null,
                PlayId = dataReader["PlayID"] != System.DBNull.Value ? (long?)dataReader["PlayID"] :null,
            };
        }
    }
}
