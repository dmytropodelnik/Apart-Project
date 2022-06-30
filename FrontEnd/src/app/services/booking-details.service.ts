import { Suggestion } from '../models/Suggestions/suggestion.item';
import { Guest } from '../models/UserData/guest.item';

export class BookingDetailsService {
  chosenSuggestion: any;
  chosenApartments:
    | {
        id: number;
        name: string;
        amount: number;
        roomsAmount: number;
        guestsLimit: number;
        bathroomsAmount: number;
        apartmentSize: number;
        isSuite: string;
        isSmokingAllowed: string;
      }[]
    | null = null;

  grade: number = 0;
  diffDays: number = 0;

  dateIn: string = '';
  dateOut: string = '';

  guestsData: string[] = [];

  constructor() {}

  setChosenApartmentsAndSuggestion(
    chosenApartments: any,
    chosenSuggestion: any,
    grade: number,
    diffDays: number
  ): void {
    this.chosenApartments = chosenApartments;
    this.chosenSuggestion = chosenSuggestion;
    this.grade = grade;
    this.diffDays = diffDays;
  }

  setChosenDates(dateIn: string, dateOut: string) {
    this.dateIn = dateIn;
    this.dateOut = dateOut;
  }

  setGuestsData(guestsData: string[]): void {
    this.guestsData = guestsData;
  }

  getGuestsData(): string[] {
    return this.guestsData;
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

  getCheckInDate(): string {
    return this.dateIn;
  }

  getCheckOutDate(): string {
    return this.dateOut;
  }

  clearChosenApartmentsAndSuggestions(): void {
    this.chosenApartments = null;
    this.chosenSuggestion = null;
  }
}
