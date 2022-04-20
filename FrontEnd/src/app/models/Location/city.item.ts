import { FileModel } from "../filemodel.item";
import { Country } from "./country.item";

export class City {
  id: number = -1;
  title: string = '';
  country: Country | null = null;
  image: FileModel | null = null;

  constructor () {

  }
}
