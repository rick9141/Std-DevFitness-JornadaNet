using DevFitness.API.Models.InputModels;
using Microsoft.AspNetCore.Mvc;

namespace DevFitness.API.Controllers
{
    // api/users/5/meals
    [Route("api/users/{userId}/meals")]
    public class MealsController : ControllerBase
    {
        // api/users/5/meals - GET
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            return Ok();
        }

        // api/users/5/meals/12 - GET
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            return Ok();
        }

        // api/users/5/meals
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            return Ok();
        }

        // api/users/5/meals/12 - PUT
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            return NoContent();
        }

        // api/users/5/meals/12 - DELETE
        [HttpDelete]
        public IActionResult Delete(int userId, int mealId)
        {
            return NoContent();
        }
    }
}