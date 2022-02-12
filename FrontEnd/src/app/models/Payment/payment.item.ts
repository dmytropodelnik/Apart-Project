import { CreditCard } from "./creditcard.item";
import { PaymentType } from "./paymenttype.item";

export class Payment {
  id: number | null = null;
  paymentType: PaymentType | null = null;
  creditCard: CreditCard | null = null;

  constructor () {

  }
}
