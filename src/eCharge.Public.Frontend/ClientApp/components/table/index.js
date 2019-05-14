import stream from 'mithril/stream';
import view from './view';

const component = {
    oninit({ state, attrs }) {
        state.columns = stream(attrs.columns);
    },
    view,
};

export default component;
