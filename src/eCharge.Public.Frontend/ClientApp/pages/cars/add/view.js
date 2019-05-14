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

    const { errors } = model();

    return [
        m('h1', i18next.t('car:AddCar')),
        m('form.horizontal', {
            onsubmit: (event) => {
                event.preventDefault();
                this.submit();
            },
        }, [
            m('fieldset', [
                m('legend', i18next.t('car:CarDetails')),
                m('div.form-group', [
                    m('label#name__label.required[for=name]', i18next.t('car:Name')),
                    m('div', [
                        m('input#name.form-control[type=text][required][aria-labelledby=name__label]', {
                            class: classnames({
                                invalid: errors().name.length > 0,
                            }),
                            oninput: m.withAttr('value', model().setName),
                            onchange: model().validateName,
                            value: model().name(),
                            placeholder: i18next.t('car:NameOfTheCar'),
                        }),
                        errors().name.length > 0
                            ? m('ul.errors', errors().name.map(error => m('li', error)))
                            : undefined,
                    ]),
                ]),
                m('div.form-group', [
                    m('label#batteryCapacity__label[for=batteryCapacity]', i18next.t('car:BatteryCapacity')),
                    m('div', [
                        m('input#batteryCapacity.form-control[type=number][min=0][step=1][aria-labelledby=batteryCapacity__label]', {
                            class: classnames({
                                invalid: errors().batteryCapacity.length > 0,
                            }),
                            oninput: m.withAttr('value', model().setBatteryCapacity),
                            onchange: model().validateBatteryCapacity,
                            value: model().batteryCapacity(),
                            placeholder: i18next.t('car:BatteryCapacity'),
                        }),
                        errors().batteryCapacity.length > 0
                            ? m('ul.errors', errors().batteryCapacity.map(error => m('li', error)))
                            : undefined,
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
                        disabled: !model().isValid(),
                        title: i18next.t('ui:Save'),
                    }, m.trust(icon(faAngleRight).html.pop()), i18next.t('ui:Save')),
                ]),
            ]),
        ]),
    ];
}

export default view;
