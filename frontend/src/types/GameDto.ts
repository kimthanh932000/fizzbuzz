import { RuleDto } from "./RuleDto";

// Game creation DTO (used when creating a game)
export interface GameDto {
    name: string;
    authorName: string;
    range: number;
    durationInSeconds: number;
    rules: RuleDto[];
}