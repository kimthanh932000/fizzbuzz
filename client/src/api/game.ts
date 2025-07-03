import type {
  AnswerDto,
  Game,
  GameDto,
  ScoreDto,
  SessionDto,
} from "../types";
import { get, post } from "./axiosInstance";

export const createGame = (game: GameDto): Promise<Game> => post<Game, GameDto>("/games", game);

export const fetchGames = (): Promise<Game[]> => get<Game[]>("/games");

export const startGame = async (gameId: number): Promise<SessionDto> => post<SessionDto>(`/games/start/${gameId}`);

export const getSession = async (sessionId: number): Promise<SessionDto> => get<SessionDto>(`/games/session/${sessionId}`);

export const moveToNextRound = async (sessionId: number, answer: AnswerDto): Promise<SessionDto> => post<SessionDto, AnswerDto>(`/games/session/${sessionId}/next-round`, answer);

export const getScore = async (sessionId: number): Promise<ScoreDto> => get<ScoreDto>(`/games/session/${sessionId}/score`);