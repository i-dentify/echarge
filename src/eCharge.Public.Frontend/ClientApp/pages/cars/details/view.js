import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons/faAngleLeft';
import { faEdit } from '@fortawesome/free-solid-svg-icons/faEdit';
import { faTrash } from '@fortawesome/free-solid-svg-icons/faTrash';

function view({ state }) {
    const { car } = state;

    return car()
        ? [
            m('h1', i18next.t('car:CarDetails', {
                name: car().name,
            })),
            m('div.form.horizontal', [
                m('div.form-group', [
                    m('label', i18next.t('car:Name')),
                    m('div', [
                        m('p.form-control readonly', car().name),
                    ]),
                ]),
                m('div.form-group', [
                    m('label', i18next.t('car:BatteryCapacity')),
                    m('div', [
                        m('p.form-control readonly', car().batteryCapacity),
                    ]),
                ]),
            ]),
            m('div.nowrap', [
                m('a.button.dark.connected', {
                    href: '/cars',
                    title: i18next.t('ui:Back'),
                    oncreate: m.route.link,
                }, m.trust(icon(faAngleLeft).html.pop()), i18next.t('ui:Back')),
                m('a.button.light.connected', {
                    href: `/cars/${car().id}/edit`,
                    title: i18next.t('ui:Edit'),
                    oncreate: m.route.link,
                }, m.trust(icon(faEdit).html.pop()), i18next.t('ui:Edit')),
                m('a.button.light.connected', {
                    href: `/cars/${car().id}/delete`,
                    title: i18next.t('ui:Delete'),
                    oncreate: m.route.link,
                }, m.trust(icon(faTrash).html.pop()), i18next.t('ui:Delete')),
            ]),
        ]
        : undefined;
}

export default view;
