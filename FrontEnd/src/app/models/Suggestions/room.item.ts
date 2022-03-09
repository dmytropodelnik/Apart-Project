import { RoomType } from "./roomtype.item";
import { SuggestionHighlight } from "./suggestionhighlight.item";

export class Room {
  id: number | null = null;
  sleeps: number | null = null;
  roomSize: number | null = null;
  isSmokingAllowed: boolean = false;
  isSuite: boolean = false;
  description: string = '';
  roomType: RoomType | null = null;
  priceInUserCurrency: number | null = null;
  priceInUSD: number | null = null;
  facilities: SuggestionHighlight[] | null = null;

  constructor () {

  }
}
