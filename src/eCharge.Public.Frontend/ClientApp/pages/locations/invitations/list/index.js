import m from 'mithril';
import stream from 'mithril/stream';
import request from 'lib/api/request';
import { app } from 'config';
import view from './view';

const component = {
    oninit({ state, attrs }) {
        const id = m.route.param('id');
        state.headerTag = stream(attrs.headerTag || 'h1');
        state.location = stream(id);
        state.response = stream();
        state.invitations = state.response.map(response => response.data);

        if (id) {
            this.loadInvitations();
        }
    },
    loadInvitations() {
        return app.api.then(urls => request({
            method: 'GET',
            url: `${urls.locations}api/1.0/location/${this.location()}/invitation`,
        }))
            .then(this.response)
            .catch(() => {});
    },
    saveInvitation(data) {
        data.new = false;
        m.redraw();
        app.api.then(urls => request({
            method: 'POST',
            url: `${urls.locations}api/1.0/location/:id/invitation`,
            data: {
                id: this.location(),
                email: data.email,
            },
        }))
            .then(() => this.loadInvitations())
            .catch(() => {});
    },
    deleteInvitation(data) {
        this.invitations(this.invitations().filter(invitation => invitation.id !== data.id));
        m.redraw();
        app.api.then(urls => request({
            method: 'DELETE',
            url: `${urls.locations}api/1.0/location/:id/invitation/:invitation`,
            data: {
                id: this.location(),
                invitation: data.id,
            },
        }))
            .then(() => this.loadInvitations())
            .catch(() => this.loadInvitations());
    },
    view,
};

export default component;
