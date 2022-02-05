import { Address } from "./address.item";

export class Airport {
  id: number | null = null;
  title: string = '';
  abbreviation: string = '';
  address: Address | null = null;
  image: File | null = null;

  constructor () {

  }
}
