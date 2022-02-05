import { Country } from "./country.item";

export class City {
  id: number | null = null;
  title: string = '';
  county: Country | null = null;
  image: File | null = null;

  constructor () {

  }
}
