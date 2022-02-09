import { Suggestion } from "../Suggestions/suggestion.item";
import { SuggestionReviewGrade } from "../Suggestions/suggestionreviewgrade.item";

export class ReviewCategory {
  id: number | null = null;
  category: string = '';
  suggestion: Suggestion | null = null;
  suggestionReviewGrades: SuggestionReviewGrade[] | null = null;

  constructor () {

  }
}
