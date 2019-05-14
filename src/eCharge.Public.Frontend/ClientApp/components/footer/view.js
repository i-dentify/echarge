import m from 'mithril';
import i18next from 'i18next';

function view() {
    return m('footer',
        m('span', i18next.t('ui:Copyright')));
}

export default view;
