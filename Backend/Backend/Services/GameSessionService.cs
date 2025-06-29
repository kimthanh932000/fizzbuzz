﻿using Backend.Helpers;

namespace Backend.Services
{
    public class GameSessionService : IGameSessionService
    {
        private readonly IGameSessionRepo _gameSessionRepo;
        private readonly IGameService _gameService;
        private readonly IRuleService _ruleService;
        private readonly IGameSessionNumberService _gameSessionNumberService;

        public GameSessionService(
            IGameSessionRepo gameSessionRepo,
            IGameService gameService,
            IRuleService ruleService,
            IGameSessionNumberService gameSessionNumberService)
        {
            _gameSessionRepo = gameSessionRepo;
            _gameService = gameService;
            _ruleService = ruleService;
            _gameSessionNumberService = gameSessionNumberService;
        }

        public async Task<GameSession?> GetByIdAsync(int id)
        {
            var session = await _gameSessionRepo.GetByIdAsync(id);
            if (session == null || session.IsExpired)
            {
                throw new InvalidOperationException("Session not found or already expired.");
            }

            // Calculate remaining seconds
            session.RemainingSeconds = TimeHelper.GetReminingTimeInSeconds(session.StartTime, session.Game.DurationInSeconds);

            // Expire session if time is up
            session.IsExpired = session.RemainingSeconds == 0;

            await _gameSessionRepo.UpdateAsync(session);

            return session;
        }

        public async Task ValidateAnswerAsync(int sessionId, int number, string answer)
        {
            var session = await _gameSessionRepo.GetByIdAsync(sessionId);

            if (session == null || session.IsExpired)
                throw new InvalidOperationException("Session not found or already expired.");

            string correctAnswer = "";

            var gameRules = await _ruleService.GetByGameIdAsync(session.Game.Id);

            foreach (var rule in gameRules)
            {
                if (number % rule.DivisibleBy == 0)
                {
                    correctAnswer += rule.Word;
                }
            }
            
            if (correctAnswer == answer)
            {
                session.TotalCorrect += 1;
            } 
            else
            {
                session.TotalIncorrect += 1;
            }
            await _gameSessionRepo.UpdateAsync(session);
        }

        public async Task<GameSession> StartSessionAsync(int gameId)
        {
            var game = await _gameService.GetByIdAsync(gameId);

            if (game == null)
            {
                throw new KeyNotFoundException("Game was not found.");
            }

            var session = await _gameSessionRepo.AddAsync(new GameSession()
            {
                GameId = gameId,
                StartTime = DateTime.UtcNow,
                RemainingSeconds = game.DurationInSeconds,
                IsExpired = false,
                TotalCorrect = 0,
                TotalIncorrect = 0
            });

            return session;
        }

        public async Task<int> GetRandomNumber(int sessionId, int range)
        {
            var usedNumbers = await _gameSessionNumberService.GetUsedNumbersBySessionIdAsync(sessionId);
            if (usedNumbers.ToList().Count >= range)
            {
                throw new InvalidOperationException("All possible numbers have been used for this session.");
            }

            int randNumber;
            do
            {
                randNumber = RandomHelper.Generate(1, range);
            }
            while (usedNumbers.Contains(randNumber));

            // Save the new number to DB
            var sessionNumber = new GameSessionNumber
            {
                GameSessionId = sessionId,
                Value = randNumber
            };

            await _gameSessionNumberService.AddNewNumberAsync(sessionNumber);

            return sessionNumber.Value;
        }
    }
}
