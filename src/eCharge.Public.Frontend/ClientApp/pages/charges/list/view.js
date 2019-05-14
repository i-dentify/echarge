import m from 'mithril';
import i18next from 'i18next';
import { icon } from '@fortawesome/fontawesome-svg-core';
import { faPlus } from '@fortawesome/free-solid-svg-icons/faPlus';
import { faLockOpen } from '@fortawesome/free-solid-svg-icons/faLockOpen';
import { faLock } from '@fortawesome/free-solid-svg-icons/faLock';
import table from 'components/table';
import tabs from 'components/tabs';

function view({ state }) {
    const {
        locations,
        cars,
        charges,
        location,
        car,
    } = state;
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
    const renderResult = items => m(tabs, {
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
                        const openCharges = (items || []).filter(item => !item.cleared);
                        const options = () => m('div.nowrap', [
                            m('a.button.dark.connected', {
                                href: `/charges/add/${location().id}/${car().id}`,
                                title: i18next.t('charge:AddNewCharge'),
                                oncreate: m.route.link,
                            }, m.trust(icon(faPlus).html.pop()), i18next.t('charge:AddNewCharge')),
                        ]);
                        return openCharges.length > 0
                            ? [
                                options(),
                                renderTable(openCharges),
                                options(),
                            ]
                            : m('div', [
                                m('p', i18next.t('charge:NoChargesAddNew')),
                                options(),
                            ]);
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
                        const closedCharges = (items || []).filter(item => item.cleared);
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

    return [
        m('h1', i18next.t('charge:MyCharges')),
        locations() && cars()
            ? [
                m('div.form.horizontal', [
                    m('div.form-group', [
                        m('label#location__label.required[for=location]', i18next.t('charge:Location')),
                        m('div', [
                            m('select#location.form-control', {
                                oninput: m.withAttr('value', this.selectLocation.bind(this)),
                            }, [
                                m('option', {
                                    value: null,
                                    selected: !location(),
                                }, i18next.t('charge:SelectLocation')),
                                locations().map(item => m('option', {
                                    value: item.id,
                                    selected: location() && location().id === item.id,
                                }, item.name)),
                            ]),
                        ]),
                    ]),
                    m('div.form-group', [
                        m('label#car__label.required[for=car]', i18next.t('charge:Car')),
                        m('div', [
                            m('select#car.form-control', {
                                oninput: m.withAttr('value', this.selectCar.bind(this)),
                            }, [
                                m('option', {
                                    value: null,
                                    selected: !car(),
                                }, i18next.t('charge:SelectCar')),
                                cars().map(item => m('option', {
                                    value: item.id,
                                    selected: car() && car().id === item.id,
                                }, item.name)),
                            ]),
                        ]),
                    ]),
                ]),
                location() && car() && charges()
                    ? renderResult(charges())
                    : undefined,
            ]
            : undefined,
    ];
}

export default view;
