export class AuthorizationService {

    private isLogged: boolean = false;
    private token: string | null = sessionStorage.getItem('tokenKey');

    constructor() {

    }

    getLogCondition(): boolean {
      return this.isLogged;
    }

    getTokenKey(): string | null {
      console.log(this.token);
      return this.token;
    }

    toggleLogCondition(): void {
      this.isLogged = !this.isLogged;
      console.log(this.isLogged);
    }

  }
