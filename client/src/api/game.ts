import type {
  AnswerDto,
  Game,
  GameDto,
  ScoreDto,
  SessionDto,
} from "../types";
import { get, post } from "./axiosInstance";

export const createGame = (game: GameDto): Promise<Game> => post<Game, GameDto>("/games", game);

export const fetchGames = (): Promise<{ data: Game[]; message: string }> =>
  get<{ data: Game[]; message: string }>("/games");

export const fetchGameById = (gameId: number): Promise<{ data: Game; message: string }> =>
  get<{ data: Game; message: string }>(`/games/${gameId}`);

export const startGame = async (gameId: number): Promise<{ data: SessionDto; message: string }> =>
  post<{ data: SessionDto; message: string }>(`/games/start/${gameId}`);

export const getSession = async (sessionId: number): Promise<{ data: SessionDto; message: string }> =>
  get<{ data: SessionDto; message: string }>(`/games/session/${sessionId}`);

export const moveToNextRound = async (sessionId: number, answer: AnswerDto): Promise<{ data: number, message: string }> =>
  post<{ data: number, message: string }, AnswerDto>(`/games/session/${sessionId}/next-round`, answer);

export const getScore = async (sessionId: number): Promise<{ data: ScoreDto; message: string }> =>
  get<{ data: ScoreDto; message: string }>(`/games/session/${sessionId}/score`);