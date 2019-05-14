import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        state.response = stream();
        state.data = state.response.map(response => response.data);
        app.api.then((urls) => {
            request({
                method: 'GET',
                url: `${urls.charges}api/1.0/charge/costs-per-month`,
            }).then(state.response);
        });
    },
    view,
};

export default component;
