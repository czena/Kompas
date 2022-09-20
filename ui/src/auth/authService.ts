import {OidcClient, User, UserManager, WebStorageStateStore} from "oidc-client-ts";
import env from "../../environment";

export default class AuthService {
    private _userManager: UserManager;

    constructor() {
        const settings: any = {
            userStore: new WebStorageStateStore({ store: window.localStorage }),
            /*authority: env.LoginConnect.Authority,
            client_id: env.LoginConnect.ClientId,*/
            redirect_uri: 'http://localhost:8080/oidc-login-redirect',
            response_type: 'code',
            scope: 'openid profile dataEventRecords',
            post_logout_redirect_uri: 'https://localhost:7251/',
        };
        this._userManager = new UserManager(settings);
    }

    public getUser(): Promise<User | null> {
        return this._userManager.getUser();
    }

    public login(): Promise<void> {
        return this._userManager.signinRedirect();
    }

    public logout(): Promise<void> {
        return this._userManager.signoutRedirect();
    }

    public getAccessToken(): Promise<string> {
        return this._userManager.getUser().then((data: any) => {
            return data.access_token;
        });
    }
}