import { FacilityType } from "./facilitytype.item";
import { Suggestion } from "./Suggestions/suggestion.item";

export class Facility {
  id: number | null = null;
  text: string = '';
  facilityType: FacilityType | null = null;
  suggestion: Suggestion | null = null;
  image: File | null = null;

  constructor () {

  }
}
