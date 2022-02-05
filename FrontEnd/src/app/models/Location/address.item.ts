import { City } from "./city.item";
import { Country } from "./country.item";
import { District } from "./district.item";
import { Region } from "./region.item";

export class Address {
  id: number | null = null;
  zipCode: string | null = null;
  phoneNumber: string | null = null;
  addressText: string = '';
  country: Country | null = null;
  city: City | null = null;
  district: District | null = null;
  region: Region | null = null;
  image: File | null = null;

  constructor () {

  }
}
