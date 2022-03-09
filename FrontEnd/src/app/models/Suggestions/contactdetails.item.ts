import { Suggestion } from "./suggestion.item";

export class ContactDetails {
  id: number | null = null;
  contactName: string = '';
  phoneNumber: string = '';
  suggestion: Suggestion | null = null;

  constructor () {

  }
}
