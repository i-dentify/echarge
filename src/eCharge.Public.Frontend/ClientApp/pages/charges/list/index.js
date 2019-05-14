import m from 'mithril';
import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        state.carResponse = stream();
        state.locationResponse = stream();
        state.chargesResponse = stream();
        state.locationId = stream(m.route.param('location'));
        state.locations = state.locationResponse.map(response => response.data);
        state.location = stream.merge([state.locations, state.locationId]).map((values) => {
            const [
                locations,
                id,
            ] = values;
            return locations.filter(location => location.id === id).pop();
        });
        state.carId = stream(m.route.param('car'));
        state.cars = state.carResponse.map(response => response.data);
        state.car = stream.merge([state.cars, state.carId]).map((values) => {
            const [
                cars,
                id,
            ] = values;
            return cars.filter(car => car.id === id).pop();
        });
        state.charges = state.chargesResponse.map(response => response.data);
        app.api.then((urls) => {
            request({
                method: 'GET',
                url: `${urls.locations}api/1.0/location`,
            })
                .then(state.locationResponse);
            request({
                method: 'GET',
                url: `${urls.cars}api/1.0/car`,
            }).then(state.carResponse);
        })
            .then(() => {
                this.getCharges();
            });
    },
    getCharges() {
        if (!this.locationId() || !this.carId()) {
            return;
        }

        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.charges}api/1.0/charge/${this.locationId()}/by-car/${this.carId()}`,
        }))
            .then(this.chargesResponse);
    },
    selectLocation(location) {
        this.locationId(location);
        this.getCharges();
    },
    selectCar(car) {
        this.carId(car);
        this.getCharges();
    },
    view,
};

export default component;
