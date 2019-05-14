import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faPlus } from '@fortawesome/free-solid-svg-icons/faPlus';
import { faEdit } from '@fortawesome/free-solid-svg-icons/faEdit';
import { faTrash } from '@fortawesome/free-solid-svg-icons/faTrash';
import table from 'components/table';

function view({ state }) {
    const { cars } = state;
    const renderResult = (items) => {
        const options = () => m('div.nowrap', [
            m('a.button.dark.connected', {
                href: '/cars/add',
                title: i18next.t('car:AddNewCar'),
                oncreate: m.route.link,
            }, m.trust(icon(faPlus).html.pop()), i18next.t('car:AddNewCar')),
        ]);

        if (items.length) {
            return [
                options(),
                m(table, {
                    items,
                    columns: [
                        {
                            header: {
                                content: () => i18next.t('car:Name'),
                            },
                            body: {
                                content: item => m('a', {
                                    title: item.name,
                                    href: `/cars/${item.id}`,
                                    oncreate: m.route.link,
                                }, item.name),
                            },
                        },
                        {
                            header: {
                                content: () => i18next.t('car:BatteryCapacity'),
                            },
                            body: {
                                content: item => item.batteryCapacity,
                            },
                        },
                        {
                            body: {
                                class: () => 'align-right options',
                                content: item => [
                                    m('button', {
                                        title: i18next.t('ui:Edit'),
                                        onclick: () => m.route.set('/cars/:id/edit', {
                                            id: item.id,
                                        }),
                                    }, m.trust(icon(faEdit).html.pop())),
                                    m('button', {
                                        title: i18next.t('ui:Delete'),
                                        onclick: () => m.route.set('/cars/:id/delete', {
                                            id: item.id,
                                        }),
                                    }, m.trust(icon(faTrash).html.pop())),
                                ],
                            },
                        },
                    ],
                }),
                options(),
            ];
        }

        return m('div', [
            m('p', i18next.t('car:NoCarsAssignedAddNew')),
            options(),
        ]);
    };

    return [
        m('h1', i18next.t('car:MyCars')),
        cars()
            ? renderResult(cars())
            : undefined,
    ];
}

export default view;
