import { NgbDate } from "@ng-bootstrap/ng-bootstrap";
import { NgbDateStruct } from "@ng-bootstrap/ng-bootstrap/datepicker/ngb-date-struct";
import { SortState } from "../enums/sortstate.item";
import { Suggestion } from "../models/Suggestions/suggestion.item";
import { FilterViewModel } from "./filterviewmodel.item";

export class SearchViewModel {
  suggestions: Suggestion[] = [];
  filters: FilterViewModel[] = [];

  sortOrder: any = SortState;

  page: number = 1;
  pageSize: number = 25;
  totalPages: number = 1;

  place: string = '';

  pdateIn: NgbDate | null = null;
  pdateOut: NgbDate  | null = null;
  dateIn: string = '';
  dateOut: string = '';

  searchAdultsAmount: number = 2;
  searchChildrenAmount: number = 0;
  searchRoomsAmount: number = 1;

  guestsAmount: number = 2;

  constructor () {

  }
}
