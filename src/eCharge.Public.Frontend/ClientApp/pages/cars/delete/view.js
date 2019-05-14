import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons/faAngleLeft';
import { faAngleRight } from '@fortawesome/free-solid-svg-icons/faAngleRight';

function view({ state }) {
    const { car } = state;

    return car()
        ? [
            m('h1', i18next.t('car:ConfirmDeleteCar', {
                name: car().name,
            })),
            m('form.horizontal', {
                onsubmit: (event) => {
                    event.preventDefault();
                    this.submit();
                },
            }, [
                m('fieldset', [
                    m('legend', i18next.t('car:CarDetails')),
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
                m('fieldset', [
                    m('legend', i18next.t('ui:FormActions')),
                    m('div.nowrap', [
                        m('button.connected.light[type=reset]', {
                            onclick: () => {
                                m.route.set('/cars');
                            },
                            title: i18next.t('ui:Cancel'),
                        }, m.trust(icon(faAngleLeft).html.pop()), i18next.t('ui:Cancel')),
                        m('button.connected.dark[type=submit]', {
                            title: i18next.t('car:YesDeleteCar'),
                        }, m.trust(icon(faAngleRight).html.pop()), i18next.t('car:YesDeleteCar')),
                    ]),
                ]),
            ]),
        ]
        : undefined;
}

export default view;
