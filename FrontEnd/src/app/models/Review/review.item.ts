import { Suggestion } from "../Suggestions/suggestion.item";
import { User } from "../UserData/user.item";
import { ReviewMessage } from "./reviewmessage.item";

export class Review {
  id: number | null = null;
  user: User | null = null;
  suggestion: Suggestion | null = null;
  reviewedDate: Date | null = null;
  reviewMessage: ReviewMessage | null = null;
  likesAmount: number | null = null;
  dislikesAmount: number | null = null;

  constructor () {

  }
}
