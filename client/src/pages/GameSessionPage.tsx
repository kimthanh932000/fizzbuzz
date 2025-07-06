import { useEffect, useState } from "react";
import { useNavigate, useParams } from "react-router-dom";
import axios from "axios";
import { moveToNextRound, getSession } from "../api/game";
import type { AnswerDto, SessionDto } from "../types";
import Timer from "../components/Timer";

const GameSessionPage = () => {
  const { sessionId } = useParams();
  const navigate = useNavigate();

  const [session, setSession] = useState<SessionDto | null>(null);
  const [currentNumber, setCurrentNumber] = useState<number>(0);
  const [loading, setLoading] = useState<boolean>(true);
  const [error, setError] = useState<string | null>(null);
  const [answer, setAnswer] = useState<string>("");

  useEffect(() => {
    if (sessionId) {
      fetchSession(+sessionId);
    }
  }, [sessionId]);

  const fetchSession = async (id: number): Promise<void> => {
    try {
      setLoading(true);
      const res = await getSession(id);
      if (res.data) {
        setSession(res.data);
        setCurrentNumber(res.data.currentNumber);
      }
    } catch (err) {
      if (axios.isAxiosError(err)) {
        if (err.response?.status === 410) {
          setError(`${err.response.data?.errors.Session[0]} Redirecting to score page in 3 seconds.`);
          setTimeout(() => {
            navigate(`/games/session/${sessionId}/score`);
          }, 3000);
        }
        else {
          setError("Failed to fetch session.");
        }
      }
    } finally {
      setLoading(false);
    }
  };

  const handleSubmit = async () => {
    try {
      const payload: AnswerDto = {
        number: currentNumber,
        value: answer.trim(),
      };
      const res = await moveToNextRound(Number(sessionId), payload);
      setCurrentNumber(res.data);
      setAnswer("");
    } catch (err) {
      if (axios.isAxiosError(err)) {
        if (err.response?.status === 409) {
          setError(`${err.response.data?.errors.Number[0]} Redirecting to score page in 3 seconds.`);
          setTimeout(() => {
            navigate(`/games/session/${sessionId}/score`);
          }, 3000);
        }
        else {
          setError("Failed to submit answer.");
        }
      }
    } finally {
      setLoading(false);
    }
  };

  if (error) return <div className="text-center text-red-500 mt-10">{error}</div>;
  if (loading) return <div className="text-center mt-10">Loading...</div>;

  return (
    <div className="max-w-xl mx-auto mt-10 px-4">
      <div className="bg-white p-6 rounded shadow mb-4">
        <p className="text-lg font-semibold mb-2">Number:</p>
        <div className="text-3xl font-bold text-blue-600">{currentNumber}</div>

        <div className="mt-6">
          <label htmlFor="answer" className="block text-sm font-medium text-gray-700">
            Your Answer
          </label>
          <input
            id="answer"
            type="text"
            className="mt-1 block w-full px-4 py-2 border border-gray-300 rounded"
            value={answer}
            onChange={(e) => setAnswer(e.target.value)}
            onKeyDown={(e) => {
              if (e.key === "Enter") {
                handleSubmit();
              }
            }}
          />
        </div>

        <button
          onClick={handleSubmit}
          className="mt-4 px-4 py-2 bg-green-600 text-white rounded hover:bg-green-700"
        >
          Submit
        </button>

        <div className="mt-4 text-sm text-gray-500">
          Remaining time:
          {session?.remainingSeconds &&
            <Timer
              seconds={session.remainingSeconds}
              onExpire={() => navigate(`/games/session/${sessionId}/score`)} />}
        </div>
      </div>
    </div>
  );
};

export default GameSessionPage;
