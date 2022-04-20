import { City } from "./city.item";
import { Country } from "./country.item";
import { District } from "./district.item";
import { Region } from "./region.item";

export class Address {
  id: number = -1;
  zipCode: string | null = null;
  phoneNumber: string | null = null;
  addressText: string = '';
  country: Country = new Country();
  city: City = new City();
  district: District | null = null;
  region: Region | null = null;

  constructor () {

  }
}
