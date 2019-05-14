import m from 'mithril';
import stream from 'mithril/stream';
import profile from 'lib/authentication/profile';
import view from './view';

const component = {
    oninit({ attrs, state }) {
        state.unprotect = stream(attrs.unprotect);
        state.profile = stream();
        state.name = state.profile.map(data => data.name);
        state.avatar = state.profile.map(data => data.picture);
        profile().then(state.profile).then(m.redraw);
    },
    view,
};

export default component;
