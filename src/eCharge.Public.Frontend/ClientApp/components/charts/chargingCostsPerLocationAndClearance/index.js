import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        state.response = stream();
        state.data = state.response.map(response => response.data);
        state.labelsResponse = stream();
        state.labels = state.labelsResponse.map(response => response.data);
        app.api.then((urls) => {
            request({
                method: 'GET',
                url: `${urls.charges}api/1.0/charge/costs-per-location-and-clearance`,
            })
                .then(state.response)
                .then(() => request({
                    method: 'GET',
                    url: `${urls.locations}api/1.0/location/by-ids`,
                    data: {
                        id: state.data().map(item => item.key) || [],
                    },
                })
                    .then(state.labelsResponse));
        });
    },
    view,
};

export default component;
