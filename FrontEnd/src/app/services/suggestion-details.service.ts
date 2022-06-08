export class SuggestionDetailsService {
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

  setChosenApartments(chosenApartments: any) {
    this.chosenApartments = chosenApartments;
  }

  getChosenApartments(): any {
    return this.chosenApartments;
  }

  clearChosenApartments(): void {
    this.chosenApartments = null;
  }
}
