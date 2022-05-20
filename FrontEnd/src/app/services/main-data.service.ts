import { BookingCategory } from "../models/bookingcategory.item";

export class MainDataService {

  private currentCountry: string = 'Ukraine';
  private bookingCategories: BookingCategory[] = [];

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
}
