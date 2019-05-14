import m from 'mithril';
import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        const id = m.route.param('id');

        if (!id) {
            m.route.set('/locations');
            return;
        }

        state.response = stream();
        state.location = state.response.map(response => response.data);
        state.authenticationHash = state.response.map(response => response.authenticationHash);
        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.locations}api/1.0/location/${id}`,
        }))
            .then(state.response)
            .catch(() => {
                m.route.set('/locations');
            });
    },
    view,
};

export default component;
