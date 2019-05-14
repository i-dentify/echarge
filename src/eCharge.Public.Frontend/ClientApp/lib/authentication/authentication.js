import { WebAuth } from 'auth0-js';

const webAuth = new WebAuth({
    domain: '',
    clientID: '',
    responseType: 'token id_token',
    audience: '',
    scope: 'openid profile',
    redirectUri: window.location.href,
});

export default webAuth;
