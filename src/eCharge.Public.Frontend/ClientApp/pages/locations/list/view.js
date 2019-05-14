import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faPlus } from '@fortawesome/free-solid-svg-icons/faPlus';
import { faEdit } from '@fortawesome/free-solid-svg-icons/faEdit';
import { faTrash } from '@fortawesome/free-solid-svg-icons/faTrash';
import table from 'components/table';

function view({ state }) {
    const { locations, authenticationHash } = state;
    const renderResult = (items) => {
        const options = () => m('div.nowrap', [
            m('a.button.dark.connected', {
                href: '/locations/add',
                title: i18next.t('location:AddNewLocation'),
                oncreate: m.route.link,
            }, m.trust(icon(faPlus).html.pop()), i18next.t('location:AddNewLocation')),
            m('a.button.light.connected', {
                href: '/locations/invitations/accept',
                title: i18next.t('location:AddFromInvitation'),
                oncreate: m.route.link,
            }, m.trust(icon(faPlus).html.pop()), i18next.t('location:AddFromInvitation')),
        ]);

        if (items.length) {
            return [
                options(),
                m(table, {
                    items,
                    columns: [
                        {
                            header: {
                                content: () => i18next.t('location:Name'),
                            },
                            body: {
                                content: item => m('a', {
                                    title: item.name,
                                    href: `/locations/${item.id}`,
                                    oncreate: m.route.link,
                                }, item.name),
                            },
                        },
                        {
                            header: {
                                content: () => i18next.t('location:Address'),
                            },
                            body: {
                                content: item => item.address,
                            },
                        },
                        {
                            header: {
                                content: () => i18next.t('location:PricePerKw'),
                            },
                            body: {
                                content: item => item.pricePerKw,
                            },
                        },
                        {
                            body: {
                                class: () => 'align-right options',
                                content: item => ((authenticationHash() === item.owner)
                                    ? [
                                        m('button', {
                                            title: i18next.t('ui:Edit'),
                                            onclick: () => m.route.set('/locations/:id/edit', {
                                                id: item.id,
                                            }),
                                        }, m.trust(icon(faEdit).html.pop())),
                                        m('button', {
                                            title: i18next.t('ui:Delete'),
                                            onclick: () => m.route.set('/locations/:id/delete', {
                                                id: item.id,
                                            }),
                                        }, m.trust(icon(faTrash).html.pop())),
                                    ]
                                    : undefined),
                            },
                        },
                    ],
                }),
                options(),
            ];
        }

        return m('div', [
            m('p', i18next.t('location:NoLocationsAssignedAddNew')),
            options(),
        ]);
    };

    return [
        m('h1', i18next.t('location:MyLocations')),
        locations()
            ? renderResult(locations())
            : undefined,
    ];
}

export default view;
