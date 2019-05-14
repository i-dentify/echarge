import m from 'mithril';
import i18next from 'i18next';
import classnames from 'classnames';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faAngleLeft } from '@fortawesome/free-solid-svg-icons/faAngleLeft';
import { faAngleRight } from '@fortawesome/free-solid-svg-icons/faAngleRight';

function view({ state }) {
    const { location, model } = state;

    if (!location() || !model()) {
        return undefined;
    }

    const { errors } = model();

    return [
        m('h1', i18next.t('location:EditLocation_Name', {
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
                    m('label#name__label.required[for=name]', i18next.t('location:Name')),
                    m('div', [
                        m('input#name.form-control[type=text][required][aria-labelledby=name__label]', {
                            class: classnames({
                                invalid: errors().name.length > 0,
                            }),
                            oninput: m.withAttr('value', model().setName),
                            onchange: model().validateName,
                            value: model().name(),
                            placeholder: i18next.t('location:NameOfTheLocation'),
                        }),
                        errors().name.length > 0
                            ? m('ul.errors', errors().name.map(error => m('li', error)))
                            : undefined,
                    ]),
                ]),
                m('div.form-group', [
                    m('label#address__label.required[for=address]', i18next.t('location:Address')),
                    m('div', [
                        m('input#address.form-control[type=text][required][aria-labelledby=address__label]', {
                            class: classnames({
                                invalid: errors().address.length > 0,
                                busy: model().isResolvingAddress,
                            }),
                            oninput: m.withAttr('value', model().setAddress),
                            onchange: model().validateAddress,
                            value: model().address(),
                            placeholder: i18next.t('location:AddressOfTheLocation'),
                        }),
                        errors().address.length > 0
                            ? m('ul.errors', errors().address.map(error => m('li', error)))
                            : undefined,
                    ]),
                ]),
                m('div.form-group', [
                    m('label#pricePerKw__label[for=pricePerKw]', i18next.t('location:PricePerKw')),
                    m('div', [
                        m('input#pricePerKw.form-control[type=number][min=0][step=any][aria-labelledby=pricePerKw__label]', {
                            class: classnames({
                                invalid: errors().pricePerKw.length > 0,
                            }),
                            oninput: m.withAttr('value', model().setPricePerKw),
                            onchange: model().validatePricePerKw,
                            value: model().pricePerKw(),
                            placeholder: i18next.t('location:PricePerKw'),
                        }),
                        errors().pricePerKw.length > 0
                            ? m('ul.errors', errors().pricePerKw.map(error => m('li', error)))
                            : undefined,
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
                        disabled: !model().isValid(),
                        title: i18next.t('ui:Save'),
                    }, m.trust(icon(faAngleRight).html.pop()), i18next.t('ui:Save')),
                ]),
            ]),
        ]),
    ];
}

export default view;
