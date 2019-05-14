import m from 'mithril';
import stream from 'mithril/stream';
import { app } from 'config';
import model from '../model';
import view from './view';

const component = {
    oninit({ state }) {
        state.model = stream(model);
        state.model().init({
            code: '',
        });
    },
    submit() {
        app.api.then(urls => this.model().submit({
            method: 'PATCH',
            url: `${urls.locations}api/1.0/location/invitation/:code/accept`,
            data: this.model().data(),
        }))
            .then(() => {
                m.route.set('/locations');
            })
            .catch(() => {});
    },
    view,
};

export default component;
