import type { Game } from "./Game";
import type { Rule } from "./Rule";

// When a session starts or is queried
export interface SessionDto {
  id: number;
  game: Game;
  startTime: string; // ISO string
  remainingSeconds: number;
  isExpired: boolean;
  currentNumber: number;
  totalCorrect: number;
  totalIncorrect: number;
  rules: Rule[];
}