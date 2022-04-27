import { BookedPeriod } from "./BookedPeriod.item";
import { RoomType } from "./roomtype.item";
import { Suggestion } from "./suggestion.item";

export class Apartment {
  id: number | null = null;
  priceInUserCurrency: number | null = null;
  priceInUSD: number | null = null;
  roomsAmount: number | null = null;
  guestsLimit: number | null = null;
  bathroomsAmount: number | null = null;
  name: string = '';
  description: string = '';
  suggestion: Suggestion | null = null;
  bookedPeriods: BookedPeriod[] = [];
  roomTypes: RoomType[] = [];

  constructor () {

  }
}
