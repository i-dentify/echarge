import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        state.response = stream();
        state.locations = state.response.map(response => response.data);
        state.authenticationHash = state.response.map(response => response.authenticationHash);
        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.locations}api/1.0/location`,
        }))
            .then(state.response);
    },
    view,
};

export default component;
