import AuthHelper from '../utils/authHelper';

export class ListNewPropertyService {

  private currentCountry: string = 'Ukraine';
  private savedPropertyId: string = '';

  constructor() {
    if (AuthHelper.getToken()) {
      fetch('https://localhost:44381/api/users/userexists' + AuthHelper.getLogin(), {
        method: 'GET',
      })
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.savedPropertyId = response.userId;
          } else {
            alert("Refresh getting userId error!");
          }
        })
        .catch((ex) => {
          alert(ex);
        });
    }
  }

  getCurrentCountry(): string {
      return this.currentCountry;
  }

  setCurrentCountry(newCurrentCountry: string): void {
    this.currentCountry = newCurrentCountry;
  }

  getSavedPropertyId(): string {

    return this.savedPropertyId;
}

  setSavedPropertyId(value: string): void {
  this.savedPropertyId = value;
}
}
