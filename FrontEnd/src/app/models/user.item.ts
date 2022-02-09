import { Review } from "./Review/review.item";
import { AirportTaxiBooking } from "./Services/airporttaxibooking.item";
import { AttractionBooking } from "./Services/attractionbooking.item";
import { CarRentalBooking } from "./Services/carrentalbooking.item";
import { FlightBooking } from "./Services/flightbooking.item";
import { StayBooking } from "./Services/staybooking.item";
import { Suggestion } from "./Suggestions/suggestion.item";
import { UserProfile } from "./UserData/useprofile.item";

export class User {
  id: number | null = null;
  title: string | null = null;
  firstName: string | null = null;;
  lastName: string | null = null;
  displayName: string | null = null;
  email: string | null = null;
  phoneNumber: string | null = null;
  profile: UserProfile | null = null;
  reviews: Review[] | null = null;
  notifications: Notification[] | null = null;
  suggestions: Suggestion[] | null = null;
  stayBookings: StayBooking[] | null = null;
  flightBookings: FlightBooking[] | null = null;
  carRentalBookings: CarRentalBooking[] | null = null;
  attractionBookings: AttractionBooking[] | null = null;
  airportTaxiBookings: AirportTaxiBooking[] | null = null;
  image: File | null = null;

  constructor () {

  }
}
