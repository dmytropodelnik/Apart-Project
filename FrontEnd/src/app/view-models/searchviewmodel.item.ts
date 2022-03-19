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

  constructor () {

  }
}
