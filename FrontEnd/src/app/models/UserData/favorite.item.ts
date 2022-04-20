import { Suggestion } from "../Suggestions/suggestion.item";
import { User } from "./user.item";

export class Favorite {
  id: number | null = null;
  suggestions: Suggestion[] = [];
  user: User | null = null;

  constructor () {

  }
}
