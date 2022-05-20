import { BookingCategory } from "../models/bookingcategory.item";
import { City } from "../models/Location/city.item";
import { Country } from "../models/Location/country.item";

export class MainDataService {

  private currentCountry: string = 'Ukraine';
  private bookingCategories: BookingCategory[] = [];
  private searchingCities: City[] = [];
  private searchingCountries: string[] = [];

  constructor() {

  }

  getCurrentCountry(): string {
      return this.currentCountry;
  }

  setCurrentCountry(newCurrentCountry: string): void {
    this.currentCountry = newCurrentCountry;
  }

  getBookingCategories(): BookingCategory[] {
    return this.bookingCategories;
  }

  setBookingCategories(categories: BookingCategory[]): void {
    this.bookingCategories = categories;
  }

  getSearchingCities(): City[] {
    return this.searchingCities;
  }

  setSearchingCities(cities: City[]): void {
    this.searchingCities = cities;
  }

  getSearchingCountries(): string[] {
    return this.searchingCountries;
  }

  setSearchingCountries(countries: string[]): void {
    this.searchingCountries = countries;
  }
}
