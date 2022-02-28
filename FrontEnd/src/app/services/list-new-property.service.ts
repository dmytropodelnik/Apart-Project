export class ListNewPropertyService {

  private currentCountry: string = 'Ukraine';

  constructor() {

  }

  getCurrentCountry(): string {

      return this.currentCountry;
  }

  setCurrentCountry(newCurrentCountry: string): void {
    this.currentCountry = newCurrentCountry;
  }
}
