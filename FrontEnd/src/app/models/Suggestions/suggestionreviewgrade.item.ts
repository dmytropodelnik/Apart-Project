import { ReviewCategory } from "../Review/reviewcategory.item";
import { Suggestion } from "./suggestion.item";

export class SuggestionReviewGrade {
  id: number | null = null;
  value: number | null = null;
  reviewCategory: ReviewCategory | null = null;
  suggestion: Suggestion | null = null;

  constructor () {

  }
}
