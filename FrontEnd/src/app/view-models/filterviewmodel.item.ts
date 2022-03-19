import { SortState } from "../enums/sortstate.item";
import { Suggestion } from "../models/Suggestions/suggestion.item";

export class FilterViewModel {
  filter: string | null = null;
  value: number | null = null;

  constructor (_filter: string | null = null, _value: number | null = null) {
    this.filter = _filter;
    this.value = _value;
  }
}
