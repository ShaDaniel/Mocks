using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mocks.Models;
using Newtonsoft.Json;

namespace Mocks.Controllers
{

    [Route("pass")]
    [ApiController]
    public class PassController : Controller
    {
        [HttpGet("{guid}")]
        public ActionResult Get(string guid)
        {
            Pass response;

            if ((response = Pass.GetPass(guid)) != null)
            {
                return Ok(response);
            }
            return NotFound();
        }

        [HttpPost]
        public ActionResult Post(Pass pass)
        {
            return Ok(Pass.SavePass(pass));
        }

        [HttpPut]
        public ActionResult Put(Pass pass)
        {
            if (Pass.UpdatePass(pass))
                return Ok();
            return NotFound();
        }
        [HttpDelete("{guid}")]
        public ActionResult Delete(string guid)
        {
            if (Pass.DeletePass(guid))
                return Ok();
            return NotFound();
        }
    }
}
