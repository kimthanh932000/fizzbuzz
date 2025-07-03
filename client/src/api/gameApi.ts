import type {
  Game,
  GameDto,
  AnswerDto,
  SessionDto,
  ScoreDto
} from "../types";
import axiosInstance from "./axiosInstance";

export const createGame = async (game: GameDto): Promise<Game> => {
  const res = await axiosInstance.post<Game>("/games", game);
  return res.data;
}

export const fetchGames = async (): Promise<Game[]> => {
  const res = await axiosInstance.get<Game[]>("/games");
  return res.data;
}

export const startSession = async (gameId: number): Promise<SessionDto> => {
  const res = await axiosInstance.post<SessionDto>(`/games/start/${gameId}`);
  return res.data;
}

export const getSession = async (sessionId: number): Promise<SessionDto> => {
  const res = await axiosInstance.get<SessionDto>(`/games/session/${sessionId}`);
  return res.data;
}

export const getNextRound = async (sessionId: number, answer: AnswerDto): Promise<SessionDto> => {
  const res = await axiosInstance.post<SessionDto>(`/games/session/${sessionId}/next-round`, answer);
  return res.data;
}

export const getScore = async (sessionId: number): Promise<ScoreDto> => {
  const res = await axiosInstance.get<ScoreDto>(`/games/session/${sessionId}/score`);
  return res.data;
}