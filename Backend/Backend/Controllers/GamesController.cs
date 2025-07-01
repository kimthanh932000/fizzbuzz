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
        public async Task<ActionResult> GetAllAsync()
        {
            try
            {
                var result = await _gameService.GetAllAsync();
                var gamesDto = result.Select(r => r.ToRequestGameDto()).ToList();
                return Ok(ApiResponse<IEnumerable<RequestGameDto>>.SuccessResponse(gamesDto, "All games retrieved."));
            }
            catch (Exception ex)
            {
                var generalErrors = new Dictionary<string, string[]>
                {
                    { "Server", new[] { ex.Message } }
                };
                return BadRequest(ApiResponse<object>.FailedResponse(generalErrors));
            }
        }

        // GET: api/[controller]/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetByIdAsync(int id)
        {
            try
            {
                var entity = await _gameService.GetByIdAsync(id);
                return Ok(ApiResponse<RequestGameDto>.SuccessResponse(entity.ToRequestGameDto(), "Game retrieved."));
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
                var result = await _gameService.AddAsync(gameDto);
                return Ok(ApiResponse<RequestGameDto>.SuccessResponse(result.ToRequestGameDto(), "New game created."));
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
                return StatusCode(500, ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "Server", new[] { ex.Message } } }));
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
                var result = await _gameSessionService.StartSessionAsync(gameId);
                return Ok(ApiResponse<RequestSessionDto>.SuccessResponse(result, "Session started."));
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

        // GET: api/[controller]/session/{id}
        [HttpGet("session/{id}")]
        public async Task<ActionResult> GetSessionByIdAsync(int id)
        {
            try
            {
                var result = await _gameSessionService.GetByIdAsync(id);
                return Ok(ApiResponse<RequestSessionDto>.SuccessResponse(result, "Session retrieved."));
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

        // POST: api/[controller]/session/{sessionId}/next-round
        [HttpPost("session/{sessionId}/next-round")]
        public async Task<ActionResult> NextRoundAsync(int sessionId, [FromBody] AnswerDto answer)
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
                // Validate answer
                await _gameSessionService.ValidateAnswerAsync(sessionId, answer.Number, answer.Value);

                // Get new number
                var newNumber = await _gameSessionService.GetRandomNumber(sessionId);

                return Ok(ApiResponse<int>.SuccessResponse(newNumber, "Next round."));
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException notFoundEx)
                {
                    return NotFound(ApiResponse<object>.FailedResponse(new Dictionary<string, string[]>
                    {
                        { "Session", new[] { notFoundEx.Message } }
                    }));
                }
                if (ex is SessionExpiredException expiredEx)
                {
                    return StatusCode(410, ApiResponse<object>.FailedResponse(new Dictionary<string, string[]>
                    {
                        { "Session", new[] { expiredEx.Message } }
                    }));
                }
                if (ex is InvalidOperationException invalidEx)
                {
                    return Conflict(ApiResponse<object>.FailedResponse(new Dictionary<string, string[]>
                    {
                        { "Number", new[] { invalidEx.Message } }
                    }));
                }
                return StatusCode(500, ApiResponse<object>.FailedResponse(new Dictionary<string, string[]>
                {
                    { "Server", new[] { ex.Message } }
                }));
            }
        }

        // GET: api/[controller]/session/{sessionId}/score
        [HttpGet("session/{sessionId}/score")]
        public async Task<ActionResult> GetScoreBySessionIdAsync(int sessionId)
        {
            try
            {
                var entity = await _gameSessionService.GetScoreBySessionIdAsync(sessionId);
                return Ok(ApiResponse<GameScoreDto>.SuccessResponse(entity, "Score retrieved."));
            }
            catch (Exception ex)
            {
                if (ex is KeyNotFoundException keyNotFoundEx)
                {
                    return NotFound(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "SessionId", new[] { keyNotFoundEx.Message } } }));
                }
                return BadRequest(ApiResponse<object>.FailedResponse(
                                   new Dictionary<string, string[]> { { "Server", new[] { ex.Message } } }));
            }
        }
    }
}
