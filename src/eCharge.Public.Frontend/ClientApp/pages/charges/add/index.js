import m from 'mithril';
import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import model from '../model';
import view from './view';

const component = {
    oninit({ state }) {
        const locationId = m.route.param('location');
        const carId = m.route.param('car');

        if (!locationId || !carId) {
            m.route.set('/charges');
            return;
        }

        state.locationResponse = stream();
        state.carResponse = stream();
        state.location = state.locationResponse.map(response => response.data);
        state.car = state.carResponse.map(response => response.data);
        state.model = stream.merge([state.location, state.car]).map((values) => {
            const [
                location,
                car,
            ] = values;
            model.init({
                location,
                car,
                date: '',
                loadStart: 0,
                loadEnd: 0,
            })
                .then((obj) => {
                    obj.validateLoadStart();
                    obj.validateLoadEnd();
                });
            return model;
        });

        app.api.then((urls) => {
            request({
                method: 'GET',
                url: `${urls.locations}api/1.0/location/${locationId}`,
            })
                .then(state.locationResponse)
                .catch(() => {
                    m.route.set('/charges');
                });
            request({
                method: 'GET',
                url: `${urls.cars}api/1.0/car/${carId}`,
            })
                .then((response) => {
                    if (!response.authenticationHash ||
                        !response.data ||
                        (response.authenticationHash !== response.data.owner)) {
                        m.route.set('/charges');
                    }

                    return response;
                })
                .then(state.carResponse)
                .catch(() => {
                    m.route.set('/charges');
                });
        });
    },
    submit() {
        app.api.then(urls => this.model().submit({
            method: 'POST',
            url: `${urls.charges}api/1.0/charge`,
            data: this.model().data(),
        }))
            .then(() => {
                m.route.set('/charges/:location/:car', {
                    location: this.location().id,
                    car: this.car().id,
                });
            })
            .catch(() => {});
    },
    view,
};

export default component;
