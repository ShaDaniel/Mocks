using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Mocks.Controllers
{
    [Route("ping")]
    [ApiController]
    public class PingController : ControllerBase
    {
        [HttpGet]
        public ActionResult<string> Get()
        {
            return Ok(DateTime.Now.ToString("dd.MM.yyyy HH:mm:ss"));
        }
    }
}