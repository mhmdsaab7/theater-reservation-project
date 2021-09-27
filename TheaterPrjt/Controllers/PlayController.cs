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
    [Route("api/Play")]
    public class PlayController : ControllerBase
    {
        PlayManager manager = new PlayManager();
        [HttpPost("GetFilteredPlays")]
        public IEnumerable<PlayDetail> GetFilteredTheaters(PlayQuery query)
        {
            return manager.GetFilteredPlays(query);
        }

        public Play GetPlay(long playId)
        {
            return manager.GetPlay(playId);
        }

        [HttpPost("AddPlay")]
        public OperationOutput AddPlay(Play play)
        {
            return manager.AddPlay(play);
        }

        [HttpPost("EditPlay")]
        public OperationOutput EditPlay(Play play)
        {
            return manager.EditPlay(play);
        }

        [HttpDelete("DeletePlay")]
        public OperationOutput DeletePlay( long playId)
        {
            return manager.DeletePlay( playId);
        }
    }
}
