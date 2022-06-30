import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CreditCard } from 'src/app/models/Payment/creditcard.item';
import { AdminContentService } from 'src/app/services/admin-content.service';
import { MainDataService } from 'src/app/services/main-data.service';

import AuthHelper from '../../../utils/authHelper';
import ListHelper from '../../../utils/listHelper';

@Component({
  selector: 'app-credit-cards-list',
  templateUrl: './credit-cards-list.component.html',
  styleUrls: ['./credit-cards-list.component.css'],
})
export class CreditCardsListComponent implements OnInit {
  creditCards: CreditCard[] | null = null;
  card: CreditCard;
  checkedCard: number | null = null;

  page: number = 1;
  pageSize: number = 15;

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private adminContentService: AdminContentService,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {
    this.card = new CreditCard();
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  addCard(): void {
    let card = {
      cardHolder: this.card?.cardHolder,
      cardNumber: this.card?.cardNumber,
      expirationDate: this.card?.expirationDate,
      cvc: this.card?.cvc,
      cardTypeId: this.card?.cardType?.id,
    };

    fetch('https://localhost:44381/api/creditcards/addcreditcard', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(card),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCards();
        } else {
          this.showAlert('Adding error!');
        }
        this.resetCard();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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

    fetch('https://localhost:44381/api/creditcards/editcard', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(card),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCards();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.resetCard();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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

    fetch('https://localhost:44381/api/creditcards/deletecard', {
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json; charset=utf-8',
        Accept: 'application/json',
        Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
      },
      body: JSON.stringify(card),
    })
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.getCards();
          ListHelper.disableButtons();
        } else {
          this.showAlert('Editing error!');
        }
        this.resetCard();
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
    fetch(
      `https://localhost:44381/api/creditcards/getcreditcards?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.creditCards = data.cards;
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
      });
  }

  collectElements(creditCards: CreditCard[]): void {
    for (let item of creditCards) {
      this.creditCards?.push(item);
    }
  }

  loadMore(): void {
    this.page++;

    fetch(
      `https://localhost:44381/api/creditcards/getcreditcards?page=${this.page}&pageSize=${this.pageSize}`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((r) => r.json())
      .then((data) => {
        if (data.code === 200) {
          this.collectElements(data.cards);
        } else {
          this.showAlert('Fetch error!');
        }
      })
      .catch((ex) => {
        this.mainDataService.alertContent = ex;
        this.modalService.open(this.alert);
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
