import { FacilityType } from "./facilitytype.item";
import { FileModel } from "./filemodel.item";
import { Suggestion } from "./Suggestions/suggestion.item";

export class Facility {
  id: number | null = null;
  text: string = '';
  facilityType: FacilityType = new FacilityType();
  suggestion: Suggestion | null = null;
  image: FileModel | null = null;

  constructor () {

  }
}
