import { CardType } from "./cardtype.item";

export class CreditCard {
  id: number | null = null;
  cardHolder: string = '';
  cardNumber: string = '';
  expirationDate: Date | null = null;
  cvc: string = '';
  cardType: CardType | null = null;

  constructor () {

  }
}
