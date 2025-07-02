import { Rule } from "./Rule";

export interface Game {
  id: number;
  name: string;
  authorName: string;
  range: number;
  durationInSeconds: number;
  rules: Rule[];
}