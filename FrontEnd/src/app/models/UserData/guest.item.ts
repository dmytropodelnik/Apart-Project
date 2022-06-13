import { StayBooking } from "../Services/staybooking.item";

export class Guest {
  id: number | null = null;
  fullName: string = '';
  stayBookings: StayBooking[] = [];

  constructor () {

  }
}
