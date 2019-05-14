import m from 'mithril';
import classnames from 'classnames';

function view({ state, attrs }) {
    const { tabs } = attrs;
    const { tab } = state;
    tabs.forEach((item, index) => {
        item.index = index + 1;
    });
    const renderTabs = items => m('ul',
        items.map(item => m('li', {
            class: classnames({
                active: tab() === item.index,
            }),
        },
        m('a', {
            class: (item.tab.class && item.tab.class(item, tab())) || undefined,
            onclick: () => tab(item.index),
        }, (item.tab.content && item.tab.content()) || undefined))));
    const renderPanels = items => m('div', items.map(item => m('div', {
        class: classnames({
            hidden: tab() !== item.index,
        }),
    }, (item.panel.content && item.panel.content()) || undefined)));
    const renderContainer = items => [
        renderTabs(items),
        renderPanels(items),
    ];

    return m('div.tabs',
        tab() && tabs
            ? renderContainer(tabs)
            : undefined);
}

export default view;
