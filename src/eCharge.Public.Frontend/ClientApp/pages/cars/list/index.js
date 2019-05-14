import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        state.response = stream();
        state.cars = state.response.map(response => response.data);
        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.cars}api/1.0/car`,
        }))
            .then(state.response);
    },
    view,
};

export default component;
