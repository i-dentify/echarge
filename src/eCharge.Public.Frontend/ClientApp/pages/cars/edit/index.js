import m from 'mithril';
import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import model from '../model';
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
        state.model = state.car.map((car) => {
            model.init(car)
                .then((obj) => {
                    obj.validateName();
                    obj.validateBatteryCapacity();
                });
            return model;
        });
        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.cars}api/1.0/car/${id}`,
        }))
            .then((response) => {
                if (!response.authenticationHash ||
                    !response.data ||
                    (response.authenticationHash !== response.data.owner)) {
                    m.route.set('/cars');
                }

                return response;
            })
            .then(state.response)
            .catch(() => {
                m.route.set('/cars');
            });
    },
    submit() {
        app.api.then(urls => this.model().submit({
            method: 'PUT',
            url: `${urls.cars}api/1.0/car/:id`,
            data: Object.assign({}, this.model().data(), {
                id: this.car().id,
            }),
        }))
            .then(() => {
                m.route.set('/cars');
            })
            .catch(() => {});
    },
    view,
};

export default component;
