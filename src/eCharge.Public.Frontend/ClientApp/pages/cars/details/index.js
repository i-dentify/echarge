import m from 'mithril';
import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        const id = m.route.param('id');

        if (!id) {
            m.route.set('/cars');
            return;
        }

        state.response = stream();
        state.car = state.response.map(response => response.data);
        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.cars}api/1.0/car/${id}`,
        }))
            .then(state.response)
            .catch(() => {
                m.route.set('/cars');
            });
    },
    view,
};

export default component;
