using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class BuggyController : ControllerBase
    {
        [HttpGet("notfound")]
        public IActionResult GetNotFoundRequest()
        {
            return NotFound(); // 404
        }
        [HttpGet( "servererror")]
        public IActionResult GetServerErrrorRequest()
        {
            throw new Exception();
            return Ok();
        }

        [HttpGet(template:"badrequest")]
        public IActionResult GetBadRequest()
        {
            return BadRequest();
        }

        [HttpGet(template:"badrequest/{id}")]
        public IActionResult GetBadRequest(int id)
        {
            return BadRequest();
        }

        [HttpGet(template: "unauthorized")]
        public IActionResult GetUnauthorizedRequest()
        {
            return Unauthorized();
        }

    }
}
