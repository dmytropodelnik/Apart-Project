import AuthHelper from '../utils/authHelper';

export class AuthorizationService {
  private isLogged: boolean = false;
  private isAdmin: boolean = false;
  private token: string | null = null;

  constructor() {
    let model = {
      username: AuthHelper.getLogin(),
      accessToken: AuthHelper.getToken(),
    };

    if (AuthHelper.getToken()) {
      fetch('https://apartmain.azurewebsites.net/api/codes/refreshauth', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json; charset=utf-8',
          Accept: 'application/json',
          Authorization: AuthHelper.getLogin() + ';' + AuthHelper.getToken(),
        },
        body: JSON.stringify(model),
      })
        .then((response) => response.json())
        .then((response) => {
          if (response.code === 200) {
            this.setLogCondition(true);
          } else {
            alert("Refresh auth error!");
          }
        })
        .catch((ex) => {
          alert(ex);
        });
    }
  }

  getIsAdmin(): boolean {
    return this.isAdmin;
  }

  setIsAdmin(value: boolean): void {
    this.isAdmin = value;
  }

  setLogCondition(value: boolean): void {
    this.isLogged = value;
  }

  getLogCondition(): boolean {
    return this.isLogged;
  }

  getTokenKey(): string | null {
    return this.token;
  }

  setTokenKey(value: string): void {
    this.token = value;
  }

  toggleLogCondition(): void {
    this.isLogged = !this.isLogged;
  }
}
