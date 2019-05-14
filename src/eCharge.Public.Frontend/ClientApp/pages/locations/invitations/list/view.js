import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faUserPlus } from '@fortawesome/free-solid-svg-icons/faUserPlus';
import { faSave } from '@fortawesome/free-solid-svg-icons/faSave';
import { faTrash } from '@fortawesome/free-solid-svg-icons/faTrash';
import { faCheck } from '@fortawesome/free-solid-svg-icons/faCheck';
import { faTimes } from '@fortawesome/free-solid-svg-icons/faTimes';
import { faBatteryThreeQuarters } from '@fortawesome/free-solid-svg-icons/faBatteryThreeQuarters';
import table from 'components/table';

function view({ state }) {
    const { invitations, headerTag, location } = state;

    const renderResult = (items) => {
        const options = () => m('div.nowrap', [
            m('a.button.light.connected', {
                title: i18next.t('location:InviteNewUser'),
                onclick: () => {
                    invitations().push({
                        email: '',
                        accepted: false,
                        new: true,
                    });
                },
            }, m.trust(icon(faUserPlus).html.pop()), i18next.t('location:InviteNewUser')),
        ]);

        if (items.length) {
            return [
                m(table, {
                    items,
                    columns: [
                        {
                            header: {
                                content: () => i18next.t('location:Email'),
                            },
                            body: {
                                content: item => (item.new
                                    ? m('input#email.form-control[type=email][required]', {
                                        oninput: m.withAttr('value', (value) => {
                                            item.email = value;
                                        }),
                                        value: item.email,
                                        placeholder: i18next.t('location:EmailAddressOfInvitationRecipient'),
                                        'aria-label': i18next.t('location:EmailAddressOfInvitationRecipient'),
                                    })
                                    : item.email),
                            },
                        },
                        {
                            header: {
                                content: () => i18next.t('location:Accepted'),
                            },
                            body: {
                                content: item => (item.accepted
                                    ? m.trust(icon(faCheck).html.pop())
                                    : m.trust(icon(faTimes).html.pop())),
                            },
                        },
                        {
                            body: {
                                class: () => 'align-right options',
                                content: (item) => {
                                    if (item.accepted) {
                                        return m('a', {
                                            href: `/locations/${location()}/charges/${item.user}`,
                                            title: i18next.t('location:Charges'),
                                            oncreate: m.route.link,
                                        }, m.trust(icon(faBatteryThreeQuarters).html.pop()));
                                    }

                                    if (item.new) {
                                        return m('a', {
                                            title: i18next.t('ui:Save'),
                                            onclick: () => {
                                                this.saveInvitation(item);
                                            },
                                        }, m.trust(icon(faSave).html.pop()));
                                    }

                                    return m('button', {
                                        title: i18next.t('ui:Delete'),
                                        onclick: () => {
                                            this.deleteInvitation(item);
                                        },
                                        disabled: !item.id,
                                    }, m.trust(icon(faTrash).html.pop()));
                                },
                            },
                        },
                    ],
                }),
                options(),
            ];
        }

        return m('div', [
            m('p', i18next.t('location:NoInvitationsInviteSomeone')),
            options(),
        ]);
    };

    return [
        m(headerTag(), i18next.t('location:Invitations')),
        invitations()
            ? renderResult(invitations())
            : undefined,
    ];
}

export default view;
