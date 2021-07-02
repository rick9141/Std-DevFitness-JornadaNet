using AutoMapper;
using DevFitness.API.Core.Entities;
using DevFitness.API.Models.InputModels;
using DevFitness.API.Models.ViewModels;
using DevFitness.API.Persistence;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DevFitness.API.Controllers
{
    // api/users/5/meals
    [Route("api/users/{userId}/meals")]
    public class MealsController : ControllerBase
    {
        private readonly DevFitnessDbContext _dbContext;
        private readonly IMapper _mapper;

        public MealsController(DevFitnessDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        // api/users/5/meals - GET
        /// <summary>
        /// Retornar todas as refeicoes atreladas ao usuario
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>Objeto de detalhe das refeicoes</returns>
        /// <response code="200">Usuário e refeicoes encontradas com sucesso.</response>
        [HttpGet]
        public IActionResult GetAll(int userId)
        {
            var allMeals = _dbContext.Meals.Where(m => m.UserId == userId && m.Active);

            var allMealsViewModel = allMeals
            .Select(m => new MealViewModel(m.Id, m.Description, m.Calories, m.Date));

            return Ok(allMealsViewModel);
        }

        // api/users/5/meals/12 - GET
        /// <summary>
        /// Retorna uma refeicao especifica de um usuario especifico
        /// </summary>
        /// <param name="userId">Identificador de usuario</param>
        /// <param name="mealId">Identificador de refeicao</param>
        /// <returns>Objeto de detalhe das refeicoes</returns>
        /// <response code="404">Usuário e refeicoes nao encontrados.</response>
        /// <response code="200">Usuário e refeicoes encontradas.</response>
        [HttpGet("{mealId}")]
        public IActionResult Get(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.Id == mealId && m.UserId == userId);

            if (meal == null)
                return NotFound();

            var mealViewModel = _mapper.Map<MealViewModel>(meal);

            return Ok(mealViewModel);
        }

        // api/users/5/meals - POST
        /// <summary>
        /// Cadastrar uma refeicao
        /// </summary>
        /// <remarks>
        /// Requisição de exemplo: 
        /// {
        /// "description": "Pìzza",
        /// "Calories": 380,
        /// "date": "2021-05-15 00:00:00"
        /// }
        /// </remarks>
        /// <param name="userId">Identificador de usuario</param>
        /// <param name="inputModel">Objeto com dados de cadastro da refeicao</param>
        /// <returns>Objeto recém-criado.</returns>
        /// <response code="201">Objeto criado com sucesso.</response>
        /// <response code="400">Dados inválidos.</response>
        [HttpPost]
        public IActionResult Post(int userId, [FromBody] CreateMealInputModel inputModel)
        {
            var meal = new Meal(inputModel.Description, inputModel.Calories, inputModel.Date, userId);
            //var meal = _mapper.Map<Meal>(inputModel);

            _dbContext.Meals.Add(meal);
            _dbContext.SaveChanges();

            return CreatedAtAction(nameof(Get), new { userId = userId, mealId = meal.Id }, inputModel);
        }

        // api/users/5/meals/12 - PUT
        /// <summary>
        /// Alterar Refeicao
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="mealId"></param>
        /// <param name="inputModel"></param>
        /// <returns></returns>
        /// <response code="404">Refeicao nao encontrada.</response>
        /// <response code="204">Dados alterados.</response>
        [HttpPut("{mealId}")]
        public IActionResult Put(int userId, int mealId, [FromBody] UpdateMealInputModel inputModel)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if (meal == null)
                return NotFound();

            meal.Update(inputModel.Description, inputModel.Calories, inputModel.Date);
            _dbContext.SaveChanges();

            return NoContent();
        }

        // api/users/5/meals/12 - DELETE
        /// <summary>
        /// Remover refeicao
        /// </summary>
        /// <param name="userId">Identificador de usuario</param>
        /// <param name="mealId">Identificador de refeicao</param>
        /// <returns></returns>
        /// <response code="404">Refeicao nao encontrada.</response>
        /// <response code="204">Refeicao removida/desativada.</response>
        [HttpDelete]
        public IActionResult Delete(int userId, int mealId)
        {
            var meal = _dbContext.Meals.SingleOrDefault(m => m.UserId == userId && m.Id == mealId);

            if (meal == null)
                return NotFound();

            meal.Deactivate();
            _dbContext.SaveChanges();

            return NoContent();
        }
    }
}