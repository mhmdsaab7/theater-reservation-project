using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Data
{
    public class PlayDataManager : BaseSQLDataManager
    {
        public bool AddPlay(Play playToAdd, out long playId)
        {
            playId = 0;
            var result = ExecuteNonQuery("sp_Play_Insert", out var objectId, playToAdd.Name, playToAdd.TheaterId, playToAdd.StartTime, playToAdd.EndTime);
            if (result)
                playId = (long)objectId;
            return result;
        }
        public bool DeletePlay(long playId)
        {
            return ExecuteNonQuery("sp_Play_Delete", playId);
        }

        public bool UpdatePlay(Play play)
        {
            return ExecuteNonQuery("sp_Play_Update", play.PlayId, play.Name, play.TheaterId, play.StartTime, play.EndTime);
        }

        public Play GetPlay(long playId)
        {
            return GetSPItem("sp_Play_GetById", PlayMapper, playId);
        }
        public List<Play> GetPlays(long theaterId, string playName, DateTime? effectiveTime)
        {
            return GetSPItems("sp_Play_GetPlays", PlayMapper, theaterId, playName, effectiveTime);
        }
        private Play PlayMapper(IDataReader dataReader)
        {
            return new Play
            {
                PlayId = (long)dataReader["ID"],
                Name = (string)dataReader["Name"],
                TheaterId = (long)dataReader["TheaterID"],
                StartTime = (DateTime)dataReader["StartTime"],
                EndTime = (DateTime)dataReader["EndTime"],
            };
        }
    }
}
