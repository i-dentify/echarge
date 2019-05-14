import stream from 'mithril/stream';
import view from './view';

const component = {
    oninit({ state }) {
        state.tab = stream(1);
    },
    view,
};

export default component;
