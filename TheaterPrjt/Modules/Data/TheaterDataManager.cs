using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Data
{
    public class TheaterDataManager : BaseSQLDataManager
    {
        public bool AddTheater(Theater theaterToAdd, out long theaterId)
        {
            theaterId = 0;
            var result = ExecuteNonQuery("sp_Theater_Insert", out var objectId, theaterToAdd.Name);
            if (result)
                theaterId = (long)objectId;
            return result;
        }
        public bool DeleteTheater(long theaterId)
        {
            return ExecuteNonQuery("sp_Theater_Delete", theaterId);
        }

        public bool UpdateTheater(Theater theater)
        {
            return ExecuteNonQuery("sp_Theater_Update", theater.TheaterId, theater.Name);
        }

        public Theater GetTheater(long theaterId)
        {
            return GetSPItem("sp_Theater_GetById", TheaterMapper, theaterId);
        }
        public List<Theater> GetTheaters(string name)
        {
            return GetSPItems("sp_Theater_GetAll", TheaterMapper, name);
        }

        private Theater TheaterMapper(IDataReader dataReader)
        {
            return new Theater
            {
                TheaterId = (long)dataReader["ID"],
                Name = (string)dataReader["Name"]
            };
        }
    }
}
