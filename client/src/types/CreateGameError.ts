export interface RuleError {
  word?: string;
  divisibleBy?: string;
  [key: string]: string | undefined;
}

export interface ValidationError {
  name?: string;
  authorName?: string;
  range?: string;
  durationInSeconds?: string;
  rules?: RuleError[];
  [key: string]: string | RuleError[] | undefined;
}
