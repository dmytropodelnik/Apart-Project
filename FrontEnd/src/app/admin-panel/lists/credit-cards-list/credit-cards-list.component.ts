import { Component, OnInit } from '@angular/core';
import { CreditCard } from 'src/app/models/Payment/creditcard.item';

import AuthHelper from '../../../utils/authHelper';

@Component({
  selector: 'app-credit-cards-list',
  templateUrl: './credit-cards-list.component.html',
  styleUrls: ['./credit-cards-list.component.css']
})
export class CreditCardsListComponent implements OnInit {

  creditCards: CreditCard[] | null = null;
  card: string | null = null;
  checkedCard: number | null = null;

  constructor() {}

  addCard(): void {
    let card = {
      name: this.card,
    };

    fetch('https://localhost:44381/api/creditcards/addcard', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(card),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCards();
        } else {
          alert('Adding error!');
        }
        this.card = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editCard(): void {
    let card = {
      id: this.checkedCard,
      name: this.card,
    };

    fetch('https://localhost:44381/api/creditcards/editcard', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(card),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCards();
        } else {
          alert('Editing error!');
        }
        this.card = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteCard(): void {
    let card = {
      id: this.checkedCard,
      name: this.card,
    };

    fetch('https://localhost:44381/api/cards/deletecard', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: 'Bearer ' + AuthHelper.getToken(),
      },
      body: JSON.stringify(card),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCards();
        } else {
          alert('Editing error!');
        }
        this.card = '';
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  getCards(): void {
    fetch('https://localhost:44381/api/cards/getcards', {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.creditCards = data.cards;
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCard(id: number | null, card: string): void {
    this.checkedCard = id;
    this.card = card;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCards();
  }

}
