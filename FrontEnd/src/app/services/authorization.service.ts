export class AuthorizationService {

    private isLogged: boolean = false;
    private token: string | null = sessionStorage.getItem('tokenKey');

    constructor() {

    }

    getLogCondition(): boolean {
      return this.isLogged;
    }

    getTokenKey(): string | null {
      return this.token;
    }

    toggleLogCondition(): void {
      this.isLogged = !this.isLogged;
    }

  }
