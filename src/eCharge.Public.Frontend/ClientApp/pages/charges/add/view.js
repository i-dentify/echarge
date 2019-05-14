import m from 'mithril';
import i18next from 'i18next';
import classnames from 'classnames';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons/faAngleLeft';
import { faAngleRight } from '@fortawesome/free-solid-svg-icons/faAngleRight';

function view({ state }) {
    const { model } = state;

    if (!model()) {
        return undefined;
    }

    const { errors, location, car } = model();

    return [
        m('h1', i18next.t('charge:AddChargeForLocationAndCar', {
            location: location().name,
            car: car().name,
        })),
        m('form.horizontal', {
            onsubmit: (event) => {
                event.preventDefault();
                this.submit();
            },
        }, [
            m('fieldset', [
                m('legend', i18next.t('charge:ChargeDetails')),
                m('div.form-group', [
                    m('label#date__label.required[for=date]', i18next.t('charge:Date')),
                    m('div', [
                        m('input#date.form-control[type=date][required][aria-labelledby=date__label]', {
                            class: classnames({
                                invalid: errors().date.length > 0,
                            }),
                            oninput: m.withAttr('value', model().setDate),
                            onchange: model().validateDate,
                            value: model().date(),
                            placeholder: i18next.t('charge:Date'),
                        }),
                        errors().date.length > 0
                            ? m('ul.errors', errors().date.map(error => m('li', error)))
                            : undefined,
                    ]),
                ]),
                m('div.form-group', [
                    m('label#loadStart__label.required[for=batteryCapacity]', i18next.t('charge:InitialBatteryLoad')),
                    m('div', [
                        m('input#loadStart.form-control[type=number][required][min=0][step=1][aria-labelledby=loadStart__label]', {
                            class: classnames({
                                invalid: errors().loadStart.length > 0,
                            }),
                            oninput: m.withAttr('value', model().setLoadStart),
                            onchange: model().validateLoadStart,
                            value: model().loadStart(),
                            placeholder: i18next.t('charge:InitialBatteryLoad'),
                        }),
                        errors().loadStart.length > 0
                            ? m('ul.errors', errors().loadStart.map(error => m('li', error)))
                            : undefined,
                    ]),
                ]),
                m('div.form-group', [
                    m('label#loadEnd__label.required[for=batteryCapacity]', i18next.t('charge:FinalBatteryLoad')),
                    m('div', [
                        m('input#loadEnd.form-control[type=number][required][min=0][step=1][aria-labelledby=loadEnd__label]', {
                            class: classnames({
                                invalid: errors().loadEnd.length > 0,
                            }),
                            oninput: m.withAttr('value', model().setLoadEnd),
                            onchange: model().validateLoadEnd,
                            value: model().loadEnd(),
                            placeholder: i18next.t('charge:FinalBatteryLoad'),
                        }),
                        errors().loadEnd.length > 0
                            ? m('ul.errors', errors().loadEnd.map(error => m('li', error)))
                            : undefined,
                    ]),
                ]),
            ]),
            m('fieldset', [
                m('legend', i18next.t('ui:FormActions')),
                m('div.nowrap', [
                    m('button.connected.light[type=reset]', {
                        onclick: () => {
                            m.route.set('/charges');
                        },
                        title: i18next.t('ui:Cancel'),
                    }, m.trust(icon(faAngleLeft).html.pop()), i18next.t('ui:Cancel')),
                    m('button.connected.dark[type=submit]', {
                        disabled: !model().isValid(),
                        title: i18next.t('ui:Save'),
                    }, m.trust(icon(faAngleRight).html.pop()), i18next.t('ui:Save')),
                ]),
            ]),
        ]),
    ];
}

export default view;
