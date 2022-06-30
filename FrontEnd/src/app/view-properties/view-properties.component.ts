import { Component, OnInit, TemplateRef, ViewChild } from '@angular/core';
import { Suggestion } from '../models/Suggestions/suggestion.item';

import { Router } from '@angular/router';
import { ActivatedRoute } from '@angular/router';

import AuthHelper from '../utils/authHelper';
import ImageHelper from '../utils/imageHelper';
import MathHelper from '../utils/mathHelper';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { MainDataService } from '../services/main-data.service';

@Component({
  selector: 'app-view-property',
  templateUrl: './view-properties.component.html',
  styleUrls: ['./view-properties.component.css'],
})
export class ViewPropertyComponent implements OnInit {
  imageHelper: any = ImageHelper;

  inProgressSuggestions: any[] = [];
  activeSuggestions: any[] = [];

  activeSuggestionsAmount: number = 0;
  inProgressSuggestionsAmount: number = 0;

  activeFilter: string = '';
  inProgressFilter: string = '';

  @ViewChild('alert', { static: true })
  alert!: TemplateRef<any>;
  constructor(
    private router: Router,
    private activatedRouter: ActivatedRoute,
    public mainDataService: MainDataService,
    private modalService: NgbModal
  ) {}

  getProperties(): void {
    fetch(
      'https://apartmain.azurewebsites.net/api/suggestions/getusersuggestions?email=' +
        AuthHelper.getLogin(),
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.inProgressSuggestions = response.inProgressSuggestions;
          this.activeSuggestions = response.activeSuggestions;
          this.activeSuggestionsAmount = response.activeSuggestionsAmount;
          this.inProgressSuggestionsAmount =
            response.inProgressSuggestionsAmount;
        } else {
          this.showAlert('Get properties fetching error!');
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  showAlert(value: string): void {
    this.mainDataService.alertContent = value;
    this.modalService.open(this.alert);
  }

  continueRegistration(suggestion: any): void {
    if (suggestion.progress == 5) {
      this.router.navigate(['lp/nameandlocation'], {
        queryParams: {
          id: suggestion.id,
          choice: 0,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 10) {
      this.router.navigate(['lp/nameandlocation'], {
        queryParams: {
          id: suggestion.id,
          choice: 1,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 15) {
      this.router.navigate(['lp/apartments'], {
        queryParams: {
          id: suggestion.id,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 25) {
      this.router.navigate(['/lp/propertysetup'], {
        queryParams: {
          id: suggestion.id,
          choice: 1,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 30) {
      this.router.navigate(['/lp/propertysetup'], {
        queryParams: {
          id: suggestion.id,
          choice: 2,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 35) {
      this.router.navigate(['lp/propertysetup'], {
        queryParams: {
          id: suggestion.id,
          choice: 3,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 40) {
      this.router.navigate(['lp/propertysetup'], {
        queryParams: {
          id: suggestion.id,
          choice: 4,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 45) {
      this.router.navigate(['lp/propertysetup'], {
        queryParams: {
          id: suggestion.id,
          choice: 5,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 50) {
      this.router.navigate(['lp/propertysetup'], {
        queryParams: {
          id: suggestion.id,
          choice: 6,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 50) {
      this.router.navigate(['lp/photos'], {
        queryParams: {
          id: suggestion.id,
          toSaveId: true,
        },
      });
    } else if (suggestion.progress == 65) {
      this.router.navigate(['lp/reviewandcomplete'], {
        queryParams: {
          id: suggestion.id,
          toSaveId: true,
        },
      });
    }
  }

  filterActiveSuggestions(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/suggestions/getusersuggestions?email=${AuthHelper.getLogin()}&filter=${
        this.activeFilter
      }`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.activeSuggestions = response.activeSuggestions;
          this.activeSuggestionsAmount = response.activeSuggestionsAmount;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  filterInProgressSuggestions(): void {
    fetch(
      `https://apartmain.azurewebsites.net/api/suggestions/getusersuggestions?email=${AuthHelper.getLogin()}&filter=${
        this.inProgressFilter
      }`,
      {
        method: 'GET',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
      }
    )
      .then((response) => response.json())
      .then((response) => {
        if (response.code === 200) {
          this.inProgressSuggestions = response.inProgressSuggestions;
          this.inProgressSuggestionsAmount =
            response.inProgressSuggestionsAmount;
        } else {
          this.showAlert(response.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  deleteSuggestion(id: number): void {
    if (!confirm('Are you sure to delete this suggestion?')) {
      return;
    }

    fetch(
      `https://apartmain.azurewebsites.net/api/suggestions/deletesuggestion?id=${id}`,
      {
        method: 'DELETE',
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
          this.showAlert('Suggestion was deleted successfully!');
          this.getProperties();
        } else {
          this.showAlert(data.message);
        }
      })
      .catch((ex) => {
        this.showAlert(ex);
      });
  }

  showSuggestion(uniqueCode: number, id: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {},
    });
  }

  editSuggestion(uniqueCode: number, id: number): void {
    this.router.navigate(['suggestion', uniqueCode], {
      queryParams: {},
    });
  }

  ngOnInit(): void {
    this.getProperties();
  }
}
