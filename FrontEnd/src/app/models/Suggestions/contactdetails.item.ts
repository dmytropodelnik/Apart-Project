import { Suggestion } from "./suggestion.item";

export class ContactDetails {
  id: number | null = null;
  firstName: string = '';
  lastName: string = '';
  phoneNumber: string = '';
  suggestion: Suggestion | null = null;
  email: string = '';

  constructor () {

  }
}
