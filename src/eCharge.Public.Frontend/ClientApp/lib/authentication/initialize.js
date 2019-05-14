import auth from 'lib/authentication/authentication';

let tokenRenewalTimeout;

const renewToken = () => {
    auth.checkSession({}, (err, result) => {
        if (!err) {
            // eslint-disable-next-line no-use-before-define
            setSession(result);
        }
    });
};

const scheduleRenewal = () => {
    const expiresAt = JSON.parse(localStorage.getItem('expires_at'));
    const delay = expiresAt - Date.now();

    if (delay > 0) {
        tokenRenewalTimeout = setTimeout(() => {
            renewToken();
        }, delay);
    }
};

const setSession = (authResult) => {
    const expiresAt = JSON.stringify((authResult.expiresIn * 1000) + new Date().getTime());
    localStorage.setItem('access_token', authResult.accessToken);
    localStorage.setItem('id_token', authResult.idToken);
    localStorage.setItem('expires_at', expiresAt);
    scheduleRenewal();
};

const promise = new Promise((resolve, reject) => {
    auth.parseHash((err, authResult) => {
        if (authResult && authResult.accessToken && authResult.idToken) {
            setSession(authResult);
            resolve(authResult);
        } else if (err) {
            reject(err);
        } else {
            resolve(null);
        }
    });
});

export default promise;
