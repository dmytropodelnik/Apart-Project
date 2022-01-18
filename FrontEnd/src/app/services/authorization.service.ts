export class AuthorizationService {

    private isLogged: boolean = false;
  
    constructor() {
  
    }
  
    getLogCondition(): boolean {
  
        return this.isLogged;
    }
  
    toggleLogCondition(): void {
      this.isLogged = !this.isLogged;
      console.log(this.isLogged);
    }
  
  }