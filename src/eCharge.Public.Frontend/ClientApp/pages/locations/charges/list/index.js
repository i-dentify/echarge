import m from 'mithril';
import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state }) {
        const id = m.route.param('id');
        const user = m.route.param('user');

        if (!id || !user) {
            m.route.set('/locations');
            return;
        }

        state.locationResponse = stream();
        state.location = state.locationResponse.map(response => response.data);
        state.invitationsResponse = stream();
        state.invitations = state.invitationsResponse.map(response => response.data);
        state.user = state.invitations.map(invitations => invitations.filter(invitation => invitation.user === user).pop());
        state.chargesResponse = stream();
        state.charges = state.chargesResponse.map(response => response.data);
        app.api.then(urls => request({
            method: 'GET',
            url: `${urls.locations}api/1.0/location/${id}`,
        })
            .then((response) => {
                if (!response.authenticationHash ||
                    !response.data ||
                    (response.authenticationHash !== response.data.owner)) {
                    m.route.set('/locations/:id', {
                        id,
                    });
                }

                return response;
            })
            .then(state.locationResponse)
            .then(() => request({
                method: 'GET',
                url: `${urls.locations}api/1.0/location/${state.location().id}/invitation`,
            })
                .then(state.invitationsResponse))
            .then(this.getCharges.bind(this))
            .catch(() => {
                m.route.set('/locations/:id', {
                    id,
                });
            }));
    },
    getCharges() {
        return app.api.then(urls => request({
            method: 'GET',
            url: `${urls.charges}api/1.0/charge/${this.location().id}/by-user/${this.user().user}`,
        }))
            .then(this.chargesResponse);
    },
    closeCharges(charges) {
        app.api.then(urls => request({
            method: 'PATCH',
            url: `${urls.charges}api/1.0/charge`,
            data: {
                charges: charges.map(charge => charge.id),
            },
        }))
            .then(this.getCharges.bind(this))
            .catch(() => {});
    },
    view,
};

export default component;
