import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-flights',
  templateUrl: './flights.component.html',
  styleUrls: ['./flights.component.css'],
})
export class FlightsComponent implements OnInit {
  card = [{ card: 1 }, { card: 1 }, { card: 1 }, { card: 1 }, { card: 1 }];

  displayMonths = 2;
  navigation = 'arrows';
  showWeekNumbers = false;
  outsideDays = 'hidden';

  slides = [
    { text: 'Educational Consulting', img: 'assets/images/21.png' },
    { text: 'University and Higher Education', img: 'assets/images/21.png' },
    { text: 'Migration Consulting', img: 'assets/images/21.png' },
    { text: 'NAATI Translations', img: 'assets/images/21.png' },
    { text: 'English Courses', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
    { text: 'Tax Return', img: 'assets/images/21.png' },
  ];

  slideConfig = {
    slidesToShow: 6,
    slidesToScroll: 1,
    dots: false,
    infinite: true,
    arrows: true,
    autoplay: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          arrows: true,
        },
      },
    ],
  };

  slideConfig1 = {
    slidesToShow: 4,
    slidesToScroll: 1,
    dots: false,
    infinite: true,
    arrows: true,
    autoplay: true,
    responsive: [
      {
        breakpoint: 1024,
        settings: {
          slidesToShow: 3,
          slidesToScroll: 1,
        },
      },
      {
        breakpoint: 600,
        settings: {
          slidesToShow: 1,
          slidesToScroll: 1,
          arrows: true,
        },
      },
    ],
  };

  constructor() {}

  ngOnInit(): void {}
}
