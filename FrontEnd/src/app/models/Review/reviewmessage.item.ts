import { FileModel } from "../filemodel.item";

export class ReviewMessage {
  id: number | null = null;
  title: string | null = '';
  text: string | null = '';
  image: FileModel | null = null;

  constructor () {

  }
}
