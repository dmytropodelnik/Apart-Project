//eslint-disable-next-line import/no-anonymous-default-export
export default {
    saveAuth: (userName : string, token : any) => {
        sessionStorage.setItem('tokenKey', JSON.stringify({ userName: userName, accessToken: token }));
    },

    saveGoogleAuth: () => {
      sessionStorage.setItem('googleAuth', JSON.stringify({ isGoogleAuth: true }));
    },

    saveFacebookAuth: () => {
      sessionStorage.setItem('facebookAuth', JSON.stringify({ isFacebookAuth: true }));
    },

    clearAuth: () => {
        sessionStorage.removeItem('tokenKey');
    },

    clearGoogleAuth: () => {
      sessionStorage.removeItem('googleAuth');
    },

    clearFacebookAuth: () => {
      sessionStorage.removeItem('facebookAuth');
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
      let state = false;
      if (item) {
        state = JSON.parse(item).isGoogleAuth;
      }
      return state;
    },

    isFacebookLogin: () => {
      let item = sessionStorage.getItem('facebookAuth');
      let state = false;
      if (item) {
        state = JSON.parse(item).isFacebookAuth;
      }
      return state;
    },

    isLogged: () => {
        let item = sessionStorage.getItem('tokenKey');
        let itemLogin = sessionStorage.getItem('tokenKey');
        let login = '';
        if (itemLogin) {
            login = JSON.parse(itemLogin).userName;
        }

        if (item && login) {
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
