import { FacilityType } from "./facilitytype.item";
import { Suggestion } from "./Suggestions/suggestion.item";

export class Facility {
  id: number | null = null;
  text: string = '';
  facilityTypeId: number | null = null;
  suggestion: Suggestion | null = null;
  image: string | null = null;

  constructor () {

  }
}
