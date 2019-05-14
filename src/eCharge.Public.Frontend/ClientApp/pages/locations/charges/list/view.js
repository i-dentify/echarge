import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faLockOpen } from '@fortawesome/free-solid-svg-icons/faLockOpen';
import { faLock } from '@fortawesome/free-solid-svg-icons/faLock';
import table from 'components/table';
import tabs from 'components/tabs';

function view({ state }) {
    const { location, user, charges } = state;
    const renderTable = items => m(table, {
        items,
        columns: [
            {
                header: {
                    content: () => i18next.t('charge:Date'),
                },
                body: {
                    content: item => (new Date(Date.parse(item.date))).toLocaleDateString(),
                },
                footer: {
                    colspan: 5,
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
                header: {
                    content: () => i18next.t('location:PricePerKw'),
                },
                body: {
                    content: item => item.pricePerKw,
                },
            },
            {
                header: {
                    content: () => i18next.t('charge:InitialBatteryLoad'),
                },
                body: {
                    content: item => item.loadStart,
                },
            },
            {
                header: {
                    content: () => i18next.t('charge:FinalBatteryLoad'),
                },
                body: {
                    content: item => item.loadEnd,
                },
            },
            {
                header: {
                    class: () => 'align-right',
                    content: () => i18next.t('charge:UsedLoad'),
                },
                body: {
                    class: () => 'align-right',
                    content: item => parseFloat(((item.loadEnd - item.loadStart) / 100) * item.batteryCapacity).toFixed(2),
                },
                footer: {
                    class: () => 'align-right sum',
                    colspan: 1,
                    content: () => parseFloat(items.reduce((acc, item) => acc + parseFloat(((item.loadEnd - item.loadStart) / 100) * item.batteryCapacity), 0)).toFixed(2),
                },
            },
            {
                header: {
                    class: () => 'align-right',
                    content: () => i18next.t('charge:PriceInEur'),
                },
                body: {
                    class: () => 'align-right',
                    content: item => parseFloat(((item.loadEnd - item.loadStart) / 100) * item.batteryCapacity * item.pricePerKw).toFixed(2),
                },
                footer: {
                    class: () => 'align-right sum',
                    colspan: 1,
                    content: () => parseFloat(items.reduce((acc, item) => acc + parseFloat(((item.loadEnd - item.loadStart) / 100) * item.batteryCapacity * item.pricePerKw), 0)).toFixed(2),
                },
            },
        ],
    });
    const renderResult = (items = []) => m(tabs, {
        tabs: [
            {
                tab: {
                    content: () => [
                        m.trust(icon(faLockOpen).html.pop()),
                        i18next.t('charge:Open'),
                    ],
                },
                panel: {
                    content: () => {
                        const openCharges = items.filter(item => !item.cleared);
                        const options = m('div.nowrap', [
                            m('button.button.dark.connected', {
                                title: i18next.t('charge:CloseCharges'),
                                onclick: () => this.closeCharges(openCharges),
                            }, m.trust(icon(faLock).html.pop()), i18next.t('charge:CloseCharges')),
                        ]);
                        return openCharges.length > 0
                            ? [
                                options,
                                renderTable(openCharges),
                                options,
                            ]
                            : m('p', i18next.t('charge:NoOpenCharges'));
                    },
                },
            },
            {
                tab: {
                    content: () => [
                        m.trust(icon(faLock).html.pop()),
                        i18next.t('charge:Closed'),
                    ],
                },
                panel: {
                    content: () => {
                        const closedCharges = items.filter(item => item.cleared);
                        return closedCharges.length > 0
                            ? [
                                renderTable(closedCharges),
                            ]
                            : m('p', i18next.t('charge:NoClosedCharges'));
                    },
                },
            },
        ],
    });

    return location() && user()
        ? [
            m('h1', i18next.t('charge:ChargesForUserAtLocation', {
                user: user().email,
                location: location().name,
            })),
            charges()
                ? renderResult(charges())
                : undefined,
        ]
        : undefined;
}

export default view;
