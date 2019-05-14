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
        m('h1', i18next.t('location:AcceptInvitationToLocation')),
        m('form.horizontal', {
            onsubmit: (event) => {
                event.preventDefault();
                this.submit();
            },
        }, [
            m('fieldset', [
                m('legend', i18next.t('location:InvitationDetails')),
                m('div.form-group', [
                    m('label#code__label.required[for=code]', i18next.t('location:Code')),
                    m('div', [
                        m('input#code.form-control[type=text][required][aria-labelledby=code__label]', {
                            class: classnames({
                                invalid: errors().code.length > 0,
                            }),
                            placeholder: i18next.t('location:InvitationCode'),
                            oninput: m.withAttr('value', model().setCode),
                            onchange: model().validateCode,
                            value: model().code(),
                        }),
                        errors().code.length > 0
                            ? m('ul.errors', errors().code.map(error => m('li', error)))
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
