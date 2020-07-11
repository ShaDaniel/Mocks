using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Mocks.Models.Pass;

namespace Mocks.Controllers
{
    [Route("pass/validate")]
    [ApiController]
    public class PassValidationController : ControllerBase
    {
        [HttpGet("{guid}")]
        public ActionResult Get(string guid)
        {
            Valid res = ValidatePass(guid);
            switch (res)
            {
                case Valid.Valid:
                    return Ok();
                case Valid.Invalid:
                    return new StatusCodeResult(410);
            }
            return NotFound();
        }
    }
}