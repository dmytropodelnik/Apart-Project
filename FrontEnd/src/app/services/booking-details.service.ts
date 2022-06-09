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

  constructor() {}

  setChosenApartmentsAndSuggestion(chosenApartments: any, chosenSuggestion: any, grade: number) {
    this.chosenApartments = chosenApartments;
    this.chosenSuggestion = chosenSuggestion;
    this.grade = grade;
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

  clearChosenApartmentsAndSuggestions(): void {
    this.chosenApartments = null;
    this.chosenSuggestion = null;
  }
}
