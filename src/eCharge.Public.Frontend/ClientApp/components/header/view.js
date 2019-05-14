import m from 'mithril';
import i18next from 'i18next';
import { faPlug } from '@fortawesome/free-solid-svg-icons/faPlug';
import { faHome } from '@fortawesome/free-solid-svg-icons/faHome';
import { faCar } from '@fortawesome/free-solid-svg-icons/faCar';
import { faBolt } from '@fortawesome/free-solid-svg-icons/faBolt';
import { icon } from '@fortawesome/fontawesome-svg-core';

function view({ state }) {
    const { unprotect, avatar, name } = state;
    const navigationItems = [
        m('li',
            m('a.brand', {
                href: '/',
                title: i18next.t('ui:Home'),
                oncreate: m.route.link,
            },
            m.trust(icon(faPlug).html.pop()))),
    ];

    if (unprotect()) {
        navigationItems.push([
            m('li', [
                m('a.locations', {
                    href: '/locations',
                    title: i18next.t('ui:Locations'),
                    oncreate: m.route.link,
                }, m.trust(icon(faHome).html.pop())),
            ]),
            m('li', [
                m('a.cars', {
                    href: '/cars',
                    title: i18next.t('ui:Cars'),
                    oncreate: m.route.link,
                }, m.trust(icon(faCar).html.pop())),
            ]),
            m('li', [
                m('a.charges', {
                    href: '/charges',
                    title: i18next.t('ui:Charges'),
                    oncreate: m.route.link,
                }, m.trust(icon(faBolt).html.pop())),
            ]),
            m('li', [
                m('a.account', {
                    title: i18next.t('ui:Account'),
                }, avatar()
                    ? m('img', {
                        src: avatar(),
                        alt: name(),
                        title: name(),
                        width: 60,
                        height: 60,
                    })
                    : undefined),
            ]),
        ]);
    }

    return m('header',
        m('nav',
            m('ul', navigationItems)));
}

export default view;
