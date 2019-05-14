import m from 'mithril';
import i18next from 'i18next';
import costsPerMonth from 'components/charts/chargingCostsPerMonth';
import costsPerLocation from 'components/charts/chargingCostsPerLocation';
import costsPerLocationAndClearance from 'components/charts/chargingCostsPerLocationAndClearance';
import costsPerCar from 'components/charts/chargingCostsPerCar';

function view() {
    return [
        m('h1', i18next.t('ui:ApplicationTitle')),
        m('div.dashboard', [
            m('div',
                m(costsPerMonth)),
            m('div',
                m(costsPerCar)),
            m('div',
                m(costsPerLocation)),
            m('div',
                m(costsPerLocationAndClearance)),
        ]),
    ];
}

const component = {
    view,
};

export default component;
