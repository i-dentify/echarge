import auth from 'lib/authentication/authentication';

const view = {
    view: () => auth.authorize(),
};

export default view;
