export class ArticleItem {
  id: number | undefined;
  title: string;
  content: string;
  username: string;
  date: string;
  comments: any;
  tags: string;
  // image: File = null;

  constructor () {
    this.content = "";
    this.title = "";
    this.username = "";
    this.date = "";
    this.tags = "";
  }
}
