import m from 'mithril';
import i18next from 'i18next';
import classnames from 'classnames';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons/faAngleLeft';
import { faEdit } from '@fortawesome/free-solid-svg-icons/faEdit';
import { faTrash } from '@fortawesome/free-solid-svg-icons/faTrash';
import invitations from '../invitations/list';

function view({ state }) {
    const { location, authenticationHash } = state;

    return location()
        ? [
            m('h1', i18next.t('location:LocationDetails_Name', {
                name: location().name,
            })),
            m('div.container',
                m('div.row', [
                    m('div', {
                        class: classnames({
                            'col-12': authenticationHash() !== location().owner,
                            'col-6': authenticationHash() === location().owner,
                            'pl-0': true,
                            'pr-0': authenticationHash() !== location().owner,
                        }),
                    }, [
                        m('div.form.horizontal', [
                            m('div.form-group', [
                                m('label', i18next.t('location:Name')),
                                m('div', [
                                    m('p.form-control readonly', location().name),
                                ]),
                            ]),
                            m('div.form-group', [
                                m('label', i18next.t('location:Address')),
                                m('div', [
                                    m('p.form-control readonly', location().address),
                                ]),
                            ]),
                            m('div.form-group', [
                                m('label', i18next.t('location:PricePerKw')),
                                m('div', [
                                    m('p.form-control readonly', location().pricePerKw),
                                ]),
                            ]),
                        ]),
                        m('div.nowrap', [
                            m('a.button.dark.connected', {
                                href: '/locations',
                                title: i18next.t('ui:Back'),
                                oncreate: m.route.link,
                            }, m.trust(icon(faAngleLeft).html.pop()), i18next.t('ui:Back')),
                            m('a.button.light.connected', {
                                href: `/locations/${location().id}/edit`,
                                title: i18next.t('ui:Edit'),
                                oncreate: m.route.link,
                            }, m.trust(icon(faEdit).html.pop()), i18next.t('ui:Edit')),
                            m('a.button.light.connected', {
                                href: `/locations/${location().id}/delete`,
                                title: i18next.t('ui:Delete'),
                                oncreate: m.route.link,
                            }, m.trust(icon(faTrash).html.pop()), i18next.t('ui:Delete')),
                        ]),
                    ]),
                    authenticationHash() === location().owner
                        ? m('div.col-6.pr-0.invitations',
                            m('div.form',
                                m(invitations, {
                                    headerTag: 'h2',
                                })))
                        : undefined,
                ])),
        ]
        : undefined;
}

export default view;
