import m from 'mithril';

const request = (options) => {
    options.config = (xhr) => {
        xhr.setRequestHeader('Authorization', `Bearer ${localStorage.getItem('access_token')}`);
        xhr.setRequestHeader('X-Requested-With', 'XMLHttpRequest');
    };
    options.extract = (xhr) => {
        try {
            const data = xhr.responseText !== '' ? JSON.parse(xhr.responseText) : null;

            return {
                data,
                authenticationHash: xhr.getResponseHeader('x-authentication-hash'),
            };
        } catch (e) {
            throw new Error(xhr.responseText);
        }
    };
    return m.request(options);
};

export default request;
