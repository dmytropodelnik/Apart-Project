import { FileModel } from "../filemodel.item";

export class PromoCode {
  id: number | null = null;
  code: string | null = '';
  percentDiscount: number | null = null;
  isActive: boolean | null = null;
  generatingDate: Date | null = null;
  expirationDate: Date | null = null;

  constructor () {

  }
}
