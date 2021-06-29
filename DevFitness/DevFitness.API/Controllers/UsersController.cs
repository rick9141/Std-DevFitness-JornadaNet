using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    // api/users
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // api/users/5 - GET
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return NotFound();

            return Ok();
        }

        // api/users - POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel)
        {
            //return BadRequest();

            return CreatedAtAction(nameof(Get), new { id = 10 }, inputModel);
        }

        // api/users/5 - PUT
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UpdateUserInputModel inputModel)
        {
            // return NotFound();
            // return BadRequest();

            return NoContent();
        }
    }
}