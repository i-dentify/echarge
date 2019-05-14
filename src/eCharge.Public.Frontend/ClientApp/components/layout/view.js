import m from 'mithril';
import header from '../header';
import footer from '../footer';

function view({ state, children }) {
    const { unprotect } = state;
    return [
        m(header, { unprotect }),
        m('main', children),
        m(footer),
    ];
}

export default view;
