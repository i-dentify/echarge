import m from 'mithril';
import stream from 'mithril/stream';
import { app } from 'config';
import model from '../model';
import view from './view';

const component = {
    oninit({ state }) {
        state.model = stream(model);
        state.model().init({
            name: '',
            batteryCapacity: 0,
        }).then((obj) => {
            obj.validateBatteryCapacity();
        });
    },
    submit() {
        app.api.then(urls => this.model().submit({
            method: 'POST',
            url: `${urls.cars}api/1.0/car`,
            data: this.model().data(),
        }))
            .then(() => {
                m.route.set('/cars');
            })
            .catch(() => {});
    },
    view,
};

export default component;
