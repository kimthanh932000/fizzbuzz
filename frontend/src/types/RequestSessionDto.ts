import { Game } from "./Game";
import { Rule } from "./Rule";

// When a session starts or is queried
export interface RequestSessionDto {
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