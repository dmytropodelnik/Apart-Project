import { SortState } from "../enums/sortstate.item";
import { Suggestion } from "../models/Suggestions/suggestion.item";

export class SearchViewModel {
  suggestions: Suggestion[] = [];

  sortOrder: any = SortState;

  page: number = 1;
  pageSize: number = 25;
  totalPages: number = 1;

  constructor () {

  }
}
