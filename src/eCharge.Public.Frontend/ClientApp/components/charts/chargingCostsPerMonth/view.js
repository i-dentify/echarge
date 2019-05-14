import m from 'mithril';
import i18next from 'i18next';
import { GoogleCharts } from 'google-charts';

function view({ state }) {
    const { data } = state;
    const renderChart = records => (records.length > 0
        ? m('div.chart', {
            oncreate({ dom }) {
                GoogleCharts.load(() => {
                    const chart = new GoogleCharts.api.visualization.LineChart(dom);
                    chart.draw(GoogleCharts.api.visualization.arrayToDataTable([
                        [
                            i18next.t('charge:Month'),
                            i18next.t('charge:Expenses'),
                        ],
                    ].concat(records.map(record => [
                        record.key,
                        Number(parseFloat(record.value).toFixed(2)),
                    ]))), {
                        title: i18next.t('charge:ChargingCostsPerMonth'),
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
                        fontName: 'Helvetica Neue,Helvetica,Arial,sans-serif',
                    });
                });
            },
        })
        : m('div.chart.empty', [
            m('h2', i18next.t('charge:ChargingCostsPerMonth')),
            m('div', i18next.t('charge:NoChargeDataAvailable')),
        ]));

    return m('div',
        data()
            ? renderChart(data())
            : undefined);
}

export default view;
