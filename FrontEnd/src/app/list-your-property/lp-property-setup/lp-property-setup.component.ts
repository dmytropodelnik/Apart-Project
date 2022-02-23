import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-lp-property-setup',
  templateUrl: './lp-property-setup.component.html',
  styleUrls: ['./lp-property-setup.component.css'],
})
export class LpPropertySetupComponent implements OnInit {
  constructor() {}
  choice: number = 0;

  incrementChoice() {
    let firstLine = document.getElementById('firstLine');
    if (firstLine !== null) {
      firstLine.classList.remove('navstep__container--active');
      firstLine.classList.add('navstep__container--after');
    }

    let secondLine = document.getElementById('secondLine');
    if (secondLine !== null) {
      secondLine.classList.add('navstep__container--active');
    }
    ++this.choice;
  }

  incrementChoice1() {
    let secondLine = document.getElementById('secondLine');
    if (secondLine !== null) {
      secondLine.classList.remove('navstep__container--active');
      secondLine.classList.add('navstep__container--after');
    }

    let thirdLine = document.getElementById('thirdLine');
    if (thirdLine !== null) {
      thirdLine.classList.add('navstep__container--active');
    }
    ++this.choice;
  }

  ngOnInit(): void {}
}
