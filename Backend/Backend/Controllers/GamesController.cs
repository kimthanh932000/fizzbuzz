using Backend.Wrappers;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GamesController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetAllAsync()
        {
            try
            {
                var entities = await _gameService.GetAllAsync();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _gameService.GetByIdAsync(id);
                return entity == null ? NoContent() : entity;
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult> AddAsync([FromBody] CreateGameDto gameDto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                        .Where(ms => ms.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                return BadRequest(ApiResponse<object>.FailedResponse(errors));
            }
            try
            {
                await _gameService.AddAsync(gameDto);
                return Ok(ApiResponse<object>.SuccessResponse(null, "New game created."));
            }
            catch (Exception ex)
            {
                return BadRequest(ApiResponse<CreateGameDto>.FailedResponse(null, "An error has occurred"));
            }
        }

        // PUT: api/[controller]/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Game>> UpdateAsync(int id, [FromBody] Game entity)
        {
            if (id != entity.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return ValidationProblem(ModelState);
            }

            try
            {
                await _gameService.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

            return Ok(entity);
        }

        // DELETE: api/[controller]/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _gameService.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
            return Ok();
        }
    }
}
