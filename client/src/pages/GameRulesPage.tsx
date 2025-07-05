import { useNavigate, useParams } from 'react-router-dom';
import { useEffect, useState } from 'react';
import { fetchGameById, startGame } from '../api/game';
import type { Game } from '../types';

const GameRulesPage = () => {
    const { gameId } = useParams();
    const navigate = useNavigate();
    const [game, setGame] = useState<Game | null>(null);
    const [error, setError] = useState<string | null>(null);
    const [loading, setLoading] = useState(false);

    useEffect(() => {
        if (gameId) {
            getGameById(+gameId);
        }
    }, [gameId]);

    const getGameById = async (id: number): Promise<void> => {
        try {
            const res = await fetchGameById(id);
            if (res.data) {
                setGame(res.data);
            }
        } catch (err) {
            setError('Failed to load game rules.');
        } finally {
            setLoading(false);
        }
    }

    const handlePlay = async (id: number): Promise<void> => {
        try {
            setLoading(true);
            const res = await startGame(id);
            if (res.data) {
                navigate(`/games/session/${res.data.id}`);
            }
        } catch(err) {
            setError('Failed to start game.');
        } finally {
            setLoading(false);
        }
    };

    if (error) return <div className="text-red-500">{error}</div>;
    if (!game) return <div>Loading...</div>;

    return (
        <div className="max-w-3xl mx-auto mt-8 px-4">
            <h1 className="text-2xl font-bold mb-4 uppercase">Rules</h1>
            <ul className="list-disc pl-5 my-4">
                {game.rules.map((rule, idx) => (
                    <li key={idx}>
                      <span className="font-medium text-blue-600">
                        Replace numbers divisible by {rule.divisibleBy}
                      </span>{" "}
                      with <span className="italic text-pink-600">"{rule.word}"</span>
                    </li>
                ))}
            </ul>
            <button
                onClick={() => navigate("/")}
                disabled={loading}
                className="mt-4 mr-2 px-6 py-2 bg-gray-600 text-white rounded hover:bg-gray-700 disabled:opacity-50"
            >
                Back
            </button>
            <button
                onClick={() => handlePlay(game.id)}
                disabled={loading}
                className="mt-4 px-6 py-2 bg-blue-600 text-white rounded hover:bg-blue-700 disabled:opacity-50"
            >
                Play
            </button>
        </div>
    );
};

export default GameRulesPage;
