import m from 'mithril';
import stream from 'mithril/stream';
import view from './view';

const component = {
    oninit({ state, attrs }) {
        state.unprotect = stream(attrs.unprotect);
        document.querySelector('html').className = m.route.get().split(/\//)[1];
    },
    view,
};

export default component;
