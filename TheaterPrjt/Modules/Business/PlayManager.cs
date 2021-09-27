using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheaterPrjt.Data;
using TheaterPrjt.Entities;

namespace TheaterPrjt.Business
{
    public class PlayManager
    {
        PlayDataManager _dataManager = new PlayDataManager();
        TheaterManager _theaterManager = new TheaterManager();

        public string GetPlayName(long playId)
        {
            var play = GetPlay(playId);
            if (play == null)
                return null;
            return play.Name;
        }
        public Play GetPlay(long playId)
        {
            return _dataManager.GetPlay(playId);
        }

        public IEnumerable<PlayDetail> GetFilteredPlays(PlayQuery query)
        {
            var theaterId = long.Parse(query.TheaterId);
            var plays = _dataManager.GetPlays(theaterId, query.PlayName, query.EffectiveTime);
            if (plays == null || plays.Count == 0)
                return null;
            return plays.Select(x => PlayDetailMapper(x));
        }

        public OperationOutput AddPlay(Play playToAdd)
        {
            var operationOutput = new OperationOutput();
            if (_dataManager.AddPlay(playToAdd, out _))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Internal Server Error: Cannot Add Play";
            }
            return operationOutput;
        }
        public OperationOutput EditPlay(Play playToUpdate)
        {
            var operationOutput = new OperationOutput();
            if (_dataManager.UpdatePlay(playToUpdate))
            {
                operationOutput.IsSucceeded = true;
            }
            else
            {
                operationOutput.ErrorMessage = "Internal Server Error: Cannot Update Play";
            }
            return operationOutput;
        }

        public OperationOutput DeletePlay(long playId)
        {
            var operationOutput = new OperationOutput();
            var seatManager = new SeatManager();
            var reservedSeats = seatManager.GetSeatsByStatusId(null, playId, null, SeatStatus.Confirmed);
            if (reservedSeats == null || reservedSeats.Count == 0)
            {
                if (_dataManager.DeletePlay(playId))
                {
                    operationOutput.IsSucceeded = true;
                }
                else
                {
                    operationOutput.ErrorMessage = $"Internal Server Error: Cannot Delete play {playId}";
                }
            }
            else
            {
                operationOutput.ErrorMessage = "Cannot Delete play having seats reserved";
            }

            return operationOutput;
        }

        private PlayDetail PlayDetailMapper(Play play)
        {
            return new PlayDetail
            {
                PlayId = play.PlayId,
                PlayName = play.Name,
                TheaterId = play.TheaterId,
                TheaterName = _theaterManager.GetTheaterName(play.TheaterId),
                StartTime = play.StartTime,
                EndTime = play.EndTime,
            };
        }
    }
}
