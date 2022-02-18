import { FileModel } from "../filemodel.item";
import { Language } from "../language.item";
import { Address } from "../Location/address.item";
import { Currency } from "../Payment/currency.item";
import { Gender } from "./gender.item";
import { User } from "./user.item";

export class UserProfile {
  id: number | null = null;
  birthDate: string | null = null;
  registerDate: string | null = null;;
  genderId: Gender | null = null;
  addressId: string | null = null;
  currencyId: string | null = null;
  languageId: string | null = null;
  userId: string | null = null;
  image: FileModel | null = null;

  constructor () {

  }
}
