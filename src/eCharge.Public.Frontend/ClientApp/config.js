import m from 'mithril';
import trimEnd from 'lodash/trimEnd';

const baseURI = document.baseURI.endsWith('/') ? document.baseURI : `${document.baseURI}/`;
const hash = window.location.hash;
const hashIndex = baseURI.indexOf(hash) || baseURI.length;
const hashlessBaseURI = baseURI.substr(0, hashIndex);
const configUrl = `${trimEnd(hashlessBaseURI, '#!/')}/configuration`;

export const app = {
    locale: document.querySelector('html').getAttribute('lang'),
    api: m.request({
        url: configUrl,
    }),
};
export const mapsApiKey = '';
