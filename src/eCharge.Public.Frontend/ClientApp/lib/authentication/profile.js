import auth from 'lib/authentication/authentication';

let profile;

const promise = () => new Promise((resolve, reject) => {
    const accessToken = localStorage.getItem('access_token');

    if (profile) {
        resolve(profile);
    } else if (!accessToken) {
        reject();
    } else {
        auth.client.userInfo(accessToken, (err, data) => {
            if (data) {
                profile = data;
                resolve(data);
            } else {
                reject(err);
            }
        });
    }
});

export default promise;
