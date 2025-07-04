import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { fetchGames, startGame } from '../api/game';
import type { Game, SessionDto } from '../types';

const GameListPage: React.FC = () => {
  const [games, setGames] = useState<Game[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    getAllGames();
  }, []);

  const getAllGames = async () => {
    try {
      const res = await fetchGames();
      setGames(res.data);
    } catch (err) {
      console.error('Error fetching games:', err);
      setError('Failed to fetch games.');
    }
  };

  const handleStart = async (gameId: number) => {
    try {
      setLoading(true);
      const data: SessionDto = await startGame(gameId);
      navigate(`/session/${data.id}`);
    } catch (err) {
      setError('Failed to start game.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="max-w-4xl mx-auto mt-8 px-4">
      <h1 className="text-3xl font-bold mb-6">Available Games</h1>

      {error && <div className="text-red-500 mb-4">{error}</div>}

      <div className="grid gap-6">
        {games.length === 0 && !error ? (
          <div className="text-gray-500 text-center py-8">Loading games...</div>
        ) : (
          games.map((game) => (
            <div
              key={game.id}
              className="border border-gray-200 p-6 rounded-xl shadow-lg bg-gradient-to-br from-white to-blue-50 hover:shadow-2xl transition-shadow duration-200"
            >
              <h2 className="text-2xl font-bold mb-2 text-blue-700">{game.name}</h2>
              <p className="text-sm text-gray-500 mb-1">
                <span className="font-medium text-gray-700">Author:</span> {game.authorName}
              </p>
              <div className="flex flex-wrap gap-4 mb-2">
                <span className="inline-block bg-blue-100 text-blue-800 text-xs px-2 py-1 rounded">
                  Range: 1 to {game.range}
                </span>
                <span className="inline-block bg-green-100 text-green-800 text-xs px-2 py-1 rounded">
                  Duration: {game.durationInSeconds} seconds
                </span>
              </div>
              <div className="mt-3">
                <h3 className="font-semibold text-gray-700 mb-1">Rules:</h3>
                <ul className="list-disc pl-6 text-sm text-gray-700 space-y-1">
                  {game.rules.map((rule, index) => (
                    <li key={index}>
                      <span className="font-medium text-blue-600">
                        Replace numbers divisible by {rule.divisibleBy}
                      </span>{" "}
                      with <span className="italic text-pink-600">"{rule.word}"</span>
                    </li>
                  ))}
                </ul>
              </div>
              <button
                onClick={() => handleStart(game.id)}
                disabled={loading}
                className="mt-6 w-full px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white font-semibold rounded-lg shadow disabled:opacity-50 transition-colors duration-150"
              >
                Start Game
              </button>
            </div>
          ))
        )}
      </div>

    </div>
  );
};

export default GameListPage;
