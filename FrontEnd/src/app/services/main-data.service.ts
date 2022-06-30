import { BookingCategory } from '../models/bookingcategory.item';
import { City } from '../models/Location/city.item';
import { Region } from '../models/Location/region.item';

export class MainDataService {
  private currentCountry: string = 'Ukraine';
  private bookingCategories: BookingCategory[] = [];
  private searchingCities: City[] = [];
  private searchingCountries: string[] = [];
  private searchingRegions: Region[] = [];
  private isGotData: boolean = true;

  public alertContent: string = 'Example';

  constructor() {}

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

  getSearchingRegions(): Region[] {
    return this.searchingRegions;
  }

  setSearchingRegions(regions: Region[]): void {
    this.searchingRegions = regions;
  }

  getIsGotData(): boolean {
    return this.isGotData;
  }

  setIsGotData(value: boolean): void {
    this.isGotData = value;
  }
}
