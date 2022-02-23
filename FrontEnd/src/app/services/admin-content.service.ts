export class AdminContentService {

  private content: string = '';

  constructor() {

  }

  getContent(): string {

      return this.content;
  }

  setData(newContent: string): void {
    this.content = newContent;
  }
}
