//eslint-disable-next-line import/no-anonymous-default-export
export default {
    saveAuth: (userName : string, token : any) => {
        sessionStorage.setItem('tokenKey', JSON.stringify({ userName: userName, accessToken: token }));
    },

    saveGoogleAuth: () => {
      sessionStorage.setItem('googleAuth', JSON.stringify({ isGoogleAuth: true }));
    },

    clearAuth: () => {
        sessionStorage.removeItem('tokenKey');
    },

    clearGoogleAuth: () => {
      sessionStorage.removeItem('googleAuth');
    },

    getLogin: () => {
        let item = sessionStorage.getItem('tokenKey');
        let login = '';
        if (item) {
            login = JSON.parse(item).userName;
        }
        return login;
    },

    isGoogleLogin: () => {
      let item = sessionStorage.getItem('googleAuth');
      let login = false;
      if (item) {
          login = JSON.parse(item).isGoogleAuth;
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
            token = JSON.parse(item).accessToken;
        }
        return token;
    }
  }
