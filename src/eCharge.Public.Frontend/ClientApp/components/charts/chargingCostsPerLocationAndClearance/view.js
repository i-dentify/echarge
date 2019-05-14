import m from 'mithril';
import i18next from 'i18next';
import { GoogleCharts } from 'google-charts';

function view({ state }) {
    const { data, labels } = state;
    const renderChart = (records, locations) => (records.length > 0
        ? m('div.chart', {
            oncreate({ dom }) {
                GoogleCharts.load(() => {
                    const chart = new GoogleCharts.api.visualization.ColumnChart(dom);
                    chart.draw(GoogleCharts.api.visualization.arrayToDataTable([
                        [
                            i18next.t('charge:Location'),
                            i18next.t('charge:Open'),
                            i18next.t('charge:Closed'),
                        ],
                    ].concat(records.filter(record => locations.some(location => location.id === record.key))
                        .map(record => [
                            locations.filter(location => location.id === record.key).pop().name,
                            Number(parseFloat(record.value.open).toFixed(2)),
                            Number(parseFloat(record.value.cleared).toFixed(2)),
                        ]))), {
                        title: i18next.t('charge:ChargingCostsPerClearance'),
                        titlePosition: 'out',
                        titleTextStyle: {
                            fontSize: 18,
                            bold: true,
                        },
                        legend: {
                            position: 'bottom',
                        },
                        chartArea: {
                            left: 40,
                            top: 40,
                            width: 260,
                            height: 200,
                        },
                        width: 300,
                        height: 300,
                        isStacked: true,
                        fontName: 'Helvetica Neue,Helvetica,Arial,sans-serif',
                    });
                });
            },
        })
        : m('div.chart.empty', [
            m('h2', i18next.t('charge:ChargingCostsPerClearance')),
            m('div', i18next.t('charge:NoChargeDataAvailable')),
        ]));

    return m('div',
        data() && labels()
            ? renderChart(data(), labels())
            : undefined);
}

export default view;
