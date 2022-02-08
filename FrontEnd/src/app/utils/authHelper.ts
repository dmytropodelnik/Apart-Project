//eslint-disable-next-line import/no-anonymous-default-export
export default {
    saveAuth: (userName : string, token : any) => {
        sessionStorage.setItem('tokenKey', JSON.stringify({ userName: userName, access_token: token }));
    },

    clearAuth: () => {
        sessionStorage.removeItem('tokenKey');
    },

    getLogin: () => {
        let item = sessionStorage.getItem('tokenKey');
        let login = '';
        if (item) {
            login = JSON.parse(item).userName;
        }
        return login;
    },

    isLogged: () => {
        let item = sessionStorage.getItem('tokenKey');
        if (item) {
            return true;
        } else {
            return false;
        }
    }, 

    getToken: () => {
        let item = sessionStorage.getItem('tokenKey');
        let token = null;
        if (item) {
            token = JSON.parse(item).access_token;
        }
        return token;
    }
  }
