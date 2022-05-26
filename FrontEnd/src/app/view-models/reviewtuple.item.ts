export class ReviewTuple {
  reviewCategoryId: number | null = null;
  grade: number | null = null;

  constructor (reviewCategoryId: number | null = null, grade: number | null = null) {
    this.reviewCategoryId = reviewCategoryId;
    this.grade = grade;
  }
}
