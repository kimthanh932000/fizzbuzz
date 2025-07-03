import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { fetchGames, startSession } from '../api/gameApi';
import type { Game } from '../types';

const GameListPage = () => {
  const [games, setGames] = useState<Game[]>([]);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);
  const navigate = useNavigate();

  useEffect(() => {
    getAllGames();
  }, []);

  const getAllGames = async () => {
    try {
      const { data } = await fetchGames();
      setGames(data);
    } catch (err) {
      console.error('Error fetching games:', err);
      setError('Failed to fetch games.');
    }
  };

  const handleStart = async (gameId: number) => {
    try {
      setLoading(true);
      const { data } = await startSession(gameId);
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

      <div className="grid gap-4">
        {games.map((game) => (
          <div key={game.id} className="border p-4 rounded shadow-sm bg-white">
            <h2 className="text-xl font-semibold">{game.name}</h2>
            <p className="text-sm text-gray-600">Author: {game.authorName}</p>
            <p className="text-sm">Range: 1 to {game.range}</p>
            <p className="text-sm">Duration: {game.durationInSeconds} seconds</p>
            <div className="mt-2">
              <h3 className="font-medium">Rules:</h3>
              <ul className="list-disc pl-6 text-sm">
                {game.rules.map((rule, index) => (
                  <li key={index}>
                    Replace numbers divisible by {rule.divisibleBy} with "{rule.word}"
                  </li>
                ))}
              </ul>
            </div>
            <button
              onClick={() => handleStart(game.id)}
              disabled={loading}
              className="mt-4 px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded disabled:opacity-50"
            >
              Start Game
            </button>
          </div>
        ))}
      </div>
    </div>
  );
};

export default GameListPage;
