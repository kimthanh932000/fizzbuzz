import { useEffect, useState } from 'react';
import { useNavigate, useParams } from 'react-router-dom';
import { getScore } from '../api/game';
import type { ScoreDto } from '../types';

const GameScorePage = () => {
  const { sessionId } = useParams();
  const navigate = useNavigate();

  const [score, setScore] = useState<ScoreDto | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    if (sessionId) {
      fetchScore();
    }
  }, [sessionId]);

  const fetchScore = async () => {
    try {
      setLoading(true);
      const res = await getScore(Number(sessionId));
      setScore(res.data);
    } catch (err) {
      setError('Failed to retrieve score.');
    } finally {
      setLoading(false);
    }
  };

  if (loading) return <p>Loading...</p>;

  return (
    <div className="max-w-xl mx-auto mt-10 px-4 text-center">
      <h1 className="text-3xl font-bold mb-6">Game Score</h1>

      {error && <p className="text-red-500">{error}</p>}

      {score && (
        <div className="bg-white shadow-md rounded p-6">
          <p className="text-lg">
            Correct: <span className="font-bold">{score.totalCorrect}</span>
          </p>
          <p className="text-lg">
            Incorrect: <span className="font-bold">{score.totalIncorrect}</span>
          </p>
        </div>
      )}

      <button
        onClick={() => navigate('/')}
        className="mt-6 px-6 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded"
      >
        Back to Game List
      </button>
    </div>
  );
};

export default GameScorePage;
