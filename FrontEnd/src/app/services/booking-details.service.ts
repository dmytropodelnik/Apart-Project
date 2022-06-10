import { Suggestion } from "../models/Suggestions/suggestion.item";

export class BookingDetailsService {
  chosenSuggestion: any;
  chosenApartments: {
    name: string;
    amount: number;
    roomsAmount: number;
    guestsLimit: number;
    bathroomsAmount: number;
    apartmentSize: number;
    isSuite: string;
    isSmokingAllowed: string;
  }[] | null = null;

  grade: number = 0;
  diffDays: number = 0;

  dateIn: Date | null = null;
  dateOut: Date | null = null;

  constructor() {}

  setChosenApartmentsAndSuggestion(chosenApartments: any, chosenSuggestion: any, grade: number, diffDays: number) {
    this.chosenApartments = chosenApartments;
    this.chosenSuggestion = chosenSuggestion;
    this.grade = grade;
    this.diffDays = diffDays;
  }

  setChosenDates(dateIn: Date, dateOut: Date) {
    this.dateIn = dateIn;
    this.dateOut = dateOut;
  }

  getChosenApartments(): any {
    return this.chosenApartments;
  }

  getChosenSuggestion(): any {
    return this.chosenSuggestion;
  }

  getGrade(): number {
    return this.grade;
  }

  getDiffDays(): number {
    return this.diffDays;
  }

  getCheckInDate(): Date | null {
    return this.dateIn;
  }

  getCheckOutDate(): Date | null {
    return this.dateOut;
  }

  clearChosenApartmentsAndSuggestions(): void {
    this.chosenApartments = null;
    this.chosenSuggestion = null;
  }
}
