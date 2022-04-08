import { Review } from "../Review/review.item";
import { AirportTaxiBooking } from "../Services/airporttaxibooking.item";
import { AttractionBooking } from "../Services/attractionbooking.item";
import { CarRentalBooking } from "../Services/carrentalbooking.item";
import { FlightBooking } from "../Services/flightbooking.item";
import { StayBooking } from "../Services/staybooking.item";
import { Suggestion } from "../Suggestions/suggestion.item";
import { Role } from "./role.item";
import { UserProfile } from "./useprofile.item";

export class User {
  id: number | null = null;
  title: string | null = null;
  firstName: string | null = null;;
  lastName: string | null = null;
  displayName: string | null = null;
  email: string | null = null;
  passwordHash: string | null = null;
  phoneNumber: string | null = null;
  role: Role = new Role();
  profile: UserProfile = new UserProfile();
  reviews: Review[] | null = null;
  notifications: Notification[] | null = null;
  suggestions: Suggestion[] | null = null;
  stayBookings: StayBooking[] | null = null;
  flightBookings: FlightBooking[] | null = null;
  carRentalBookings: CarRentalBooking[] | null = null;
  attractionBookings: AttractionBooking[] | null = null;
  airportTaxiBookings: AirportTaxiBooking[] | null = null;
  image: string | null = null;

  constructor () {

  }
}
