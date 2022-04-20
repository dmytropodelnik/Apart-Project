import { Address } from "./address.item";
import { City } from "./city.item";

export class Region {
  id: number | null = null;
  title: string = '';
  city: City | null = null;
  image: File | null = null;

  constructor () {

  }
}
