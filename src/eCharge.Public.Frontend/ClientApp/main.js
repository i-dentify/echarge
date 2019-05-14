import m from 'mithril';
import routes from 'routes/routes';
import initializeAuthentication from 'lib/authentication/initialize';
import isAuthenticated from 'lib/authentication/protector';
import i18next from 'i18next';
import { app } from './config';
import uiResources from '../Resources/UIResources.resx';
import carResources from '../../eCharge.Services.Cars.Resources/CarResources.resx';
import chargeResources from '../../eCharge.Services.Charges.Resources/ChargeResources.resx';
import locationResources from '../../eCharge.Services.Locations.Resources/LocationResources.resx';

i18next.init({
    ns: [
        'app',
        'ui',
        'car',
        'charge',
        'location',
    ],
    defaultNS: 'app',
    fallbackLng: app.locale,
    fallbackNS: 'app',
    lng: app.locale,
});

['de', 'en'].forEach((lng) => {
    i18next.addResourceBundle(lng, 'ui', (uiResources[lng] && uiResources[lng].app) || {});
    i18next.addResourceBundle(lng, 'car', (carResources[lng] && carResources[lng].app) || {});
    i18next.addResourceBundle(lng, 'charge', (chargeResources[lng] && chargeResources[lng].app) || {});
    i18next.addResourceBundle(lng, 'location', (locationResources[lng] && locationResources[lng].app) || {});
});

initializeAuthentication.then(() => {
    const root = document.body;
    const routeValues = Object.values(routes).reduce((a, b) => a.concat(b));
    const routeDefinition = {};

    routeValues.forEach((route) => {
        if (route.secure) {
            routeDefinition[route.path] = {
                onmatch: () => {
                    if (isAuthenticated()) {
                        return route.component;
                    }

                    return m.route.set('/login');
                },
            };
        } else {
            routeDefinition[route.path] = route.component;
        }
    });
    m.route(root, '/', routeDefinition);
});
