import m from 'mithril';
import isAuthenticated from 'lib/authentication/protector';

const generator = (path, layout, view, secure = true) => ({
    path,
    secure,
    component: {
        view: () => m(layout, {
            unprotect: !secure || (secure && isAuthenticated()),
        }, m(view)),
    },
});

export default generator;
