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

  constructor() {}

  setChosenApartmentsAndSuggestion(chosenApartments: any, chosenSuggestion: any) {
    this.chosenApartments = chosenApartments;
    this.chosenSuggestion = chosenSuggestion;
  }

  getChosenApartments(): any {
    return this.chosenApartments;
  }

  getChosenSuggestion(): any {
    return this.chosenSuggestion;
  }

  clearChosenApartmentsAndSuggestions(): void {
    this.chosenApartments = null;
    this.chosenSuggestion = null;
  }
}
