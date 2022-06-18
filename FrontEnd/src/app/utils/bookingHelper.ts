//eslint-disable-next-line import/no-anonymous-default-export
export default {
  saveBookingData: (chosenSuggestion: any, chosenApartments: any, bookingGrade: number, bookingDiffDays: number, checkIn: Date, checkOut: Date) => {
    sessionStorage.setItem('chosenSuggestion', JSON.stringify({ chosenSuggestion }));
    sessionStorage.setItem('chosenApartments', JSON.stringify({ chosenApartments }));
    sessionStorage.setItem('bookingGrade', JSON.stringify({ bookingGrade }));
    sessionStorage.setItem('bookingDiffDays', JSON.stringify({ bookingDiffDays }));
    sessionStorage.setItem('checkIn', JSON.stringify({ checkIn }));
    sessionStorage.setItem('checkOut', JSON.stringify({ checkOut }));
  },

  saveGuestsData: (guestsData: any) => {
    sessionStorage.setItem('guestsData', JSON.stringify({ guestsData }));
  },

  getBookingData: () => {
    let item1 = sessionStorage.getItem('chosenSuggestion');
    let item2 = sessionStorage.getItem('chosenApartments');
    let item3 = sessionStorage.getItem('bookingGrade');
    let item4 = sessionStorage.getItem('bookingDiffDays');
    let item5 = sessionStorage.getItem('checkIn');
    let item6 = sessionStorage.getItem('checkOut');
    let chosenSuggestion = '';
    let chosenApartments = '';
    let bookingGrade: number = -1;
    let bookingDiffDays: number = -1;
    let checkIn: any;
    let checkOut: any;
    if (item1 && item2 && item3 && item4 && item5 && item6) {
      chosenSuggestion = JSON.parse(item1).chosenSuggestion;
      chosenApartments = JSON.parse(item2).chosenApartments;
      bookingGrade = JSON.parse(item3).bookingGrade;
      bookingDiffDays = JSON.parse(item4).bookingDiffDays;
      checkIn = JSON.parse(item5 as string).checkIn;
      checkOut = JSON.parse(item6 as string).checkOut;
    }
    return { chosenSuggestion, chosenApartments, bookingGrade, bookingDiffDays, checkIn, checkOut, };
  },

  getGuestsData: () => {
    let item = sessionStorage.getItem('guestsData');
    let guestsData = '';

    if (item) {
      guestsData = JSON.parse(item as string).guestsData;
    }
    return guestsData;
  },

  clearBookingData: () => {
    sessionStorage.removeItem('chosenSuggestion');
    sessionStorage.removeItem('chosenApartments');
    sessionStorage.removeItem('bookingGrade');
    sessionStorage.removeItem('bookingDiffDays');
    sessionStorage.removeItem('guestsData');
  },
}
