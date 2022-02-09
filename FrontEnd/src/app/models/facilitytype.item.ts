import { Facility } from "./facility.item";

export class FacilityType {
  id: number | null = null;
  type: string = '';
  facilities: Facility[] | null = null;

  constructor () {

  }
}
