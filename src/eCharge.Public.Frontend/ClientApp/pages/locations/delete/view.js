import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons/faAngleLeft';
import { faAngleRight } from '@fortawesome/free-solid-svg-icons/faAngleRight';

function view({ state }) {
    const { location } = state;

    return location()
        ? [
            m('h1', i18next.t('location:ConfirmDeleteLocation', {
                name: location().name,
            })),
            m('form.horizontal', {
                onsubmit: (event) => {
                    event.preventDefault();
                    this.submit();
                },
            }, [
                m('fieldset', [
                    m('legend', i18next.t('location:LocationDetails')),
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
                m('fieldset', [
                    m('legend', i18next.t('ui:FormActions')),
                    m('div.nowrap', [
                        m('button.connected.light[type=reset]', {
                            onclick: () => {
                                m.route.set('/locations');
                            },
                            title: i18next.t('ui:Cancel'),
                        }, m.trust(icon(faAngleLeft).html.pop()), i18next.t('ui:Cancel')),
                        m('button.connected.dark[type=submit]', {
                            title: i18next.t('location:YesDeleteLocation'),
                        },
                        m.trust(icon(faAngleRight).html.pop()), i18next.t('location:YesDeleteLocation')),
                    ]),
                ]),
            ]),
        ]
        : undefined;
}

export default view;
