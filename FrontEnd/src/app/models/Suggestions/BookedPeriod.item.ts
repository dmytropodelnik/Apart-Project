import { NgbDate } from "@ng-bootstrap/ng-bootstrap";
import { Apartment } from "./apartment.item";
import { RoomType } from "./roomtype.item";
import { Suggestion } from "./suggestion.item";

export class BookedPeriod {
  id: number | null = null;
  dateIn: string | null = null;
  dateOut: string | null = null;
  apartments: Apartment[] = [];

  pdateIn: NgbDate | null = null;
  pdateOut: NgbDate  | null = null;

  constructor () {

  }
}
