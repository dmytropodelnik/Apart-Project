export class AdminContentService {
  private content: string = '';
  private pageSize: number = 25;

  constructor() {}

  getContent(): string {
    return this.content;
  }

  setData(newContent: string): void {
    this.content = newContent;
  }

  getPageSize(): number {
    return this.pageSize;
  }

  setPageSize(value: number): void {
    this.pageSize = value;
  }
}
