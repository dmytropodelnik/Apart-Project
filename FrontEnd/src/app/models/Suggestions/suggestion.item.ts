import { BookingCategory } from "../bookingcategory.item";
import { Facility } from "../facility.item";
import { FileModel } from "../filemodel.item";
import { InterestPlace } from "../interestplace.item";
import { Language } from "../language.item";
import { Address } from "../Location/address.item";
import { Review } from "../Review/review.item";
import { ReviewCategory } from "../Review/reviewcategory.item";
import { ServiceCategory } from "../servicecategory.item";
import { StayBooking } from "../Services/staybooking.item";
import { Favorite } from "../UserData/favorite.item";
import { User } from "../UserData/user.item";
import { Bed } from "./bed.item";
import { RoomType } from "./roomtype.item";
import { SuggestionHighlight } from "./suggestionhighlight.item";
import { SuggestionReviewGrade } from "./suggestionreviewgrade.item";
import { SuggestionRule } from "./suggestionrule.item";
import { SurroundingObject } from "./surroundingobject.item";

export class Suggestion {
  id: number | null = null;
  name: string = '';
  guestsAmount: number | null = null;
  bathroomsAmount: number | null = null;
  roomsAmount: number | null = null;
  starRating: number | null = null;
  progress: number | null = null;
  description: string = '';
  priceInUserCurrency: number | null = null;
  priceInUSD: number | null = null;
  IsParkingAvailable: boolean = false;
  address: Address | null = null;
  user: User | null = null;
  serviceCategory: ServiceCategory | null = null;
  bookingCategory: BookingCategory | null = null;
  interestPlaces: InterestPlace[] | null = null;
  reviews: Review[] | null = null;
  stayBookings: StayBooking[] | null = null;
  additionalServices: ReviewCategory[] | null = null;
  suggestionReviewGrades: SuggestionReviewGrade[] | null = null;
  favorites: Favorite[] | null = null;
  highlights: SuggestionHighlight[] | null = null;
  roomTypes: RoomType[] | null = null;
  languages: Language[] | null = null;
  facilities: Facility[] | null = null;
  beds: Bed[] | null = null;
  suggestionRules: SuggestionRule[] | null = null;
  surroundingObjects: SurroundingObject[] | null = null;

  images: FileModel[] | null = null;

  constructor () {

  }
}
