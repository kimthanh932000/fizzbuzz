import axios from "axios";
import {
    Game,
    GameDto,
    AnswerDto,
    RequestSessionDto,
    ScoreDto
} from "@/types";

const BASE_URL = process.env.VITE_API_URL + "/games";

export const createGame = (game: GameDto) => axios.post<Game>(`${BASE_URL}`, game);

export const fetchGames = () => axios.get<Game[]>(`${BASE_URL}`);

export const startSession = (gameId: number) =>
  axios.post<RequestSessionDto>(`${BASE_URL}/start/${gameId}`);

export const getCurrentSession = (sessionId: number) =>
  axios.get<RequestSessionDto>(`${BASE_URL}/${sessionId}`);

export const getNextRound = (sessionId: number, answer: AnswerDto) =>
  axios.post<RequestSessionDto>(`${BASE_URL}/next-round/${sessionId}`, answer);

export const getScore = (sessionId: number) =>
  axios.get<ScoreDto>(`${BASE_URL}/${sessionId}/score`);