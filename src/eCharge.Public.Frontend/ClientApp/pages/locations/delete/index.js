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
        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.locations}api/1.0/location/${id}`,
        }))
            .then((response) => {
                if (!response.authenticationHash ||
                    !response.data ||
                    (response.authenticationHash !== response.data.owner)) {
                    m.route.set('/locations');
                }

                return response;
            })
            .then(state.response)
            .catch(() => {
                m.route.set('/locations');
            });
    },
    submit() {
        app.api.then(urls => request({
            method: 'DELETE',
            url: `${urls.locations}api/1.0/location/:id`,
            data: {
                id: this.location().id,
            },
        }))
            .then(() => {
                m.route.set('/locations');
            })
            .catch(() => {
            });
    },
    view,
};

export default component;
