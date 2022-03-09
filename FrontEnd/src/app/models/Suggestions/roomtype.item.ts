import { Room } from "./room.item";
import { Suggestion } from "./suggestion.item";
import { SuggestionHighlight } from "./suggestionhighlight.item";

export class RoomType {
  id: number | null = null;
  title: string | null = null;
  suggestion: Suggestion | null = null;
  rooms: Room[] | null = null;
  highlights: SuggestionHighlight[] | null = null;

  constructor () {

  }
}
