import { Router } from '@angular/router';
import { AuthorizationService } from '../services/authorization.service';

export function appInitializer(
  authService: AuthorizationService,
  router: Router
) {
  return () =>
    new Promise<void>((resolve) => {
      window.fbAsyncInit = function () {
        FB.init({
          appId: '347521480376594',
          cookie: true,
          xfbml: true,
          version: 'v13.0',
        });

        // auto authenticate with the api if already logged in with facebook
        FB.getLoginStatus(async ({ authResponse }) => {
          if (authResponse) {
            alert('FACEBOOK OK');
            //await authService.userSignIn(authResponse.accessToken);
            router.navigate(['']);
            resolve();
          } else {
            alert('FACEBOOK ERROR!');
            resolve();
          }
        });

        FB.AppEvents.logPageView();
      };

      (function (d, s, id) {
        var js,
          fjs = d.getElementsByTagName(s)[0];
        if (d.getElementById(id)) {
          return;
        }
        js = d.createElement(s) as HTMLImageElement;
        js.id = id;
        js.src = 'https://connect.facebook.net/en_US/sdk.js';
        fjs.parentNode?.insertBefore(js, fjs);
      })(document, 'script', 'facebook-jssdk');
    });
}
