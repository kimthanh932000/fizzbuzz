// Rule entity (when retrieved from server)
export interface Rule {
  id: number;
  divisibleBy: number;
  word: string;
  gameId: number;
}