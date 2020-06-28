using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mocks.Models;

namespace Mocks.Controllers
{
    [Route("pass")]
    [ApiController]
    public class PassController : Controller
    {
        [HttpGet("{guid}")]
        public ActionResult Get(string guid)
        {
            string response;

            if ((response = Pass.GetPass(guid)) != null)
            {
                return Content(response);
            }
            return NotFound();
        }

    }
}
