using Backend.Wrappers;

namespace Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly IGameService _gameService;
        private readonly IGameSessionService _gameSessionService;

        public GamesController(IGameService gameService, IGameSessionService gameSessionService)
        {
            _gameService = gameService;
            _gameSessionService = gameSessionService;
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
                if (ex is FieldValidateException fieldEx)
                {
                    var errors = new Dictionary<string, string[]>
                    {
                        { fieldEx.FieldName, new[] { fieldEx.Message } }
                    };
                    return BadRequest(ApiResponse<object>.FailedResponse(errors));
                }

                var generalErrors = new Dictionary<string, string[]>
                {
                    { "Server", new[] { ex.Message } }
                };
                return BadRequest(ApiResponse<object>.FailedResponse(generalErrors));
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
        public async Task<ActionResult> DeleteAsync(int id)
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

        // POST: api/[controller]/start/{gameId}
        [HttpPost("start/{gameId}")]
        public async Task<ActionResult> StartSession(int gameId)
        {
            try
            {
                await _gameSessionService.StartSessionAsync(gameId);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Game session started."));
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException keyNotFoundEx)
                {
                    return NotFound(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "GameId", new[] { keyNotFoundEx.Message } } }));
                }
                return BadRequest(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "Server", new[] { ex.Message } } }));
            }
        }

        // POST: api/[controller]/session/validate-answer/{sessionId}
        [HttpPost("session/validate-answer/{sessionId}")]
        public async Task<ActionResult> ValidateAnswerAsync(int sessionId, [FromBody] AnswerDto answer)
        {
            try
            {
                await _gameSessionService.ValidateAnswerAsync(sessionId, answer.Number, answer.Answer);
                return Ok(ApiResponse<object>.SuccessResponse(null, "Answer submitted."));
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException invalidEx)
                {
                    return NotFound(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "SessionId", new[] { invalidEx.Message } } }));
                }
                return BadRequest(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "Server", new[] { ex.Message } } }));
            }
        }

        // GET: api/[controller]/session/{id}
        [HttpGet("session/{id}")]
        public async Task<ActionResult<GameSession>> GetSessionByIdAsync(int id)
        {
            try
            {
                var session = await _gameSessionService.GetByIdAsync(id);
                return Ok(ApiResponse<GameSession>.SuccessResponse(session, "Session retrieved."));
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException invalidEx)
                {
                    return NotFound(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "SessionId", new[] { invalidEx.Message } } }));
                }
                return BadRequest(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "Server", new[] { ex.Message } } }));
            }
        }

        // GET: api/[controller]/session/generate-number/{sessionId}
        [HttpGet("session/generate-number/{sessionId}")]
        public async Task<ActionResult> GenerateUniqueNumber(int sessionId, [FromBody] int range)
        {
            try
            {
                var number = await _gameSessionService.GetRandomNumber(sessionId, range);

                return Ok(ApiResponse<int>.SuccessResponse(number, "Generated unique number."));
            }
            catch (Exception ex)
            {
                if (ex is InvalidOperationException invalidEx)
                {
                    return BadRequest(ApiResponse<object>.FailedResponse(new Dictionary<string, string[]>
                    {
                        { "Number", new[] { ex.Message } }
                    }));
                }
                return StatusCode(500, ApiResponse<object>.FailedResponse(new Dictionary<string, string[]>
                {
                    { "Server", new[] { ex.Message } }
                });

            }
        }
    }
}
