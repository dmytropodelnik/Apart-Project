import { NgbDate } from '@ng-bootstrap/ng-bootstrap';
import { Address } from '../models/Location/address.item';
import { Currency } from '../models/Payment/currency.item';

export class UserData {
   title : string = '';
   firstName: string = '';
   lastName: string = '';
   displayName: string = '';
   email: string = '';
   newEmail: string = '';
   password: string = '';
   newPassword: string = '';
   phoneNumber: string = '';
   nationality: string = '';
   language: string = '';
   verificationCode: string = '';
   pBirthDate: NgbDate  | null = null;
   birthDate: string = '';
   genderId: number = -1;

   address: Address = new Address();

  constructor () {

  }
}
