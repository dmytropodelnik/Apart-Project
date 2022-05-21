import { FileModel } from "./filemodel.item";
import { User } from "./UserData/user.item";

export class MailLetter {
  id: number = -1;
  title: string = '';
  text: string = '';
  sendingDate: Date = new Date();
  receiversAmount: number | null = null;
  sentCount: number | null = null;
  sender: User = new User();
  file: FileModel | null = null;

  constructor () {

  }
}
