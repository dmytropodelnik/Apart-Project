import { Suggestion } from "./suggestion.item";
import { SurroundingObjectType } from "./surroundingobjecttype.item";

export class SurroundingObject {
  id: number | null = null;
  suggestionObjectType: SurroundingObjectType | null = null;
  suggestion: Suggestion | null = null;

  constructor () {

  }
}
