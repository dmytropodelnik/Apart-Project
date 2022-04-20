import { Component, OnInit } from '@angular/core';
import { CreditCard } from 'src/app/models/Payment/creditcard.item';
import { AdminContentService } from 'src/app/services/admin-content.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-credit-cards-list',
  templateUrl: './credit-cards-list.component.html',
  styleUrls: ['./credit-cards-list.component.css']
})
export class CreditCardsListComponent implements OnInit {

  creditCards: CreditCard[] | null = null;
  card: CreditCard;
  checkedCard: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  constructor(
    private adminContentService: AdminContentService
  ) {
    this.card = new CreditCard();
  }

  addCard(): void {
    let card = {
      cardHolder: this.card?.cardHolder,
      cardNumber: this.card?.cardNumber,
      expirationDate: this.card?.expirationDate,
      cvc: this.card?.cvc,
      cardTypeId: this.card?.cardType?.id,
    };

    fetch('http://apartmain.azurewebsites.net/api/creditcards/addcreditcard', {
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
        this.resetCard();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  editCard(): void {
    let card = {
      cardHolder: this.card?.cardHolder,
      cardNumber: this.card?.cardNumber,
      expirationDate: this.card?.expirationDate,
      cvc: this.card?.cvc,
      cardTypeId: this.card?.cardType?.id,
    };

    fetch('http://apartmain.azurewebsites.net/api/creditcards/editcard', {
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
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.resetCard();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  deleteCard(): void {
    let card = {
      id: this.checkedCard,
      cardHolder: null,
      cardNumber: null,
      expirationDate: null,
      cvc: null,
      cardType: null,
    };

    fetch('http://apartmain.azurewebsites.net/api/creditcards/deletecard', {
      method: 'DELETE',
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
          ListHelper.disableButtons();
        } else {
          alert('Editing error!');
        }
        this.resetCard();
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  resetCard(): void {
    this.card.cardHolder = '';
    this.card.cardNumber = '';
    this.card.expirationDate = null;
    this.card.cvc = '';
    this.card.cardType = null;
  }

  getCards(): void {
    fetch(`http://apartmain.azurewebsites.net/api/creditcards/getcreditcards?page=${this.page}&pageSize=${this.pageSize}`, {
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

  collectElements(creditCards: CreditCard[]): void {
    for (let item of creditCards) {
      this.creditCards?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(`http://apartmain.azurewebsites.net/api/creditcards/getcreditcards?page=${this.page}&pageSize=${this.pageSize}`, {
      method: 'GET',
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.cards);
        } else {
          alert('Fetch error!');
        }
      })
      .catch((ex) => {
        alert(ex);
      });
  }

  setCard(card: CreditCard): void {
    this.checkedCard = card.id;
    this.card.cardHolder = card.cardHolder;
    this.card.cardNumber = card.cardNumber;
    this.card.expirationDate = card.expirationDate;
    this.card.cvc = card.cvc;
    this.card.cardType = card.cardType;

    document.getElementById('editButton')?.removeAttribute('disabled');
    document.getElementById('deleteButton')?.removeAttribute('disabled');
  }

  ngOnInit(): void {
    this.getCards();
  }

}
