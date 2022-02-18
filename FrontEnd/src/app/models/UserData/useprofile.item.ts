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
  gender: Gender | null = null;
  address: Address | null = null;
  currency: Currency | null = null;
  language: Language | null = null;
  user: User | null = null;
  image: FileModel | null = null;

  constructor () {

  }
}
