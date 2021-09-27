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
    [Route("api/Theater")]
    public class TheaterController : ControllerBase
    {
        TheaterManager manager = new TheaterManager();
        [HttpPost("GetFilteredTheaters")]
        public IEnumerable<TheaterDetail> GetFilteredTheaters(TheaterQuery query)
        {
            return manager.GetFilteredTheaters(query);
        }

        [HttpGet("GetTheater")]
        public Theater GetTheater(long theaterId)
        {
            return manager.GetTheater(theaterId);
        }


        [HttpPost("AddTheater")]
        public OperationOutput AddTheater(Theater theater)
        {
            return manager.AddTheater(theater);
        }

        [HttpPost("EditTheater")]
        public OperationOutput EditTheater(Theater theater)
        {
            return manager.EditTheater(theater);
        }

        [HttpDelete("DeleteTheater")]
        public OperationOutput DeleteTheater(long theaterId)
        {
            return manager.DeleteTheater(theaterId);
        }
    }
}
