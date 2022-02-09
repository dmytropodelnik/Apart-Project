import { Suggestion } from "./suggestion.item";
import { SuggestionRuleType } from "./suggestionruletype.item";

export class SuggestionRule {
  id: number | null = null;
  text: string = '';
  suggestionRuleType: SuggestionRuleType | null = null;
  suggestions: Suggestion[] | null = null;

  constructor () {

  }
}
