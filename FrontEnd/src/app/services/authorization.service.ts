export class AuthorizationService {

    private isLogged: boolean = false;
    private isAdmin: boolean = false;
    private token: string | null = sessionStorage.getItem('tokenKey');

    constructor() {

    }

    getIsAdmin(): boolean {
      return this.isAdmin;
    }

    setIsAdmin(value: boolean): void {
      this.isAdmin = value;
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
