import { Component, OnInit } from '@angular/core';
import { MailLetter } from 'src/app/models/mailletter.item';

@Component({
  selector: 'app-letter-creator',
  templateUrl: './letter-creator.component.html',
  styleUrls: ['./letter-creator.component.css']
})
export class LetterCreatorComponent implements OnInit {
  sentLetters: MailLetter[] = [];

  newLetter: MailLetter = new MailLetter();

  choice: boolean = false;

  constructor() { }

  setChoice(): void {
    this.choice = !this.choice;
  }

  ngOnInit(): void {
  }

}
