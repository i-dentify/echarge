import m from 'mithril';
import stream from 'mithril/stream';
import camelCase from 'lodash/camelCase';
import i18next from 'i18next';
import resolveAddress from 'lib/geocoding/geocoder';
import request from 'lib/api/request';

let geocodeTimeout;
const resolvedAddress = stream();

const model = {
    name: stream(),
    address: stream(),
    formattedAddress: resolvedAddress.map(data => (data && data.address) || undefined),
    latitude: resolvedAddress.map(data => (data && data.latitude) || undefined),
    longitude: resolvedAddress.map(data => (data && data.longitude) || undefined),
    pricePerKw: stream(),
    errors: stream({
        name: [],
        address: [],
        pricePerKw: [],
    }),
    validated: {
        name: false,
        address: false,
        pricePerKw: false,
    },
    isResolvingAddress: false,
    setName: (value) => {
        model.name(value);

        if (model.errors().name.length > 0) {
            model.validateName();
        }
    },
    validateName: () => Promise.resolve()
        .then(() => {
            const errors = {
                name: [],
            };

            if (!/\S/.test(model.name())) {
                errors.name.push(i18next.t('location:LocationNameRequired'));
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.name = true;
            m.redraw();
            return errors.name.length > 0;
        }),
    setAddress: (value) => {
        model.address(value);

        if (geocodeTimeout) {
            clearTimeout(geocodeTimeout);
        }
    },
    resolveAddress: value => new Promise((resolve, reject) => {
        if (/\S/.test(value)) {
            setTimeout(() => {
                model.isResolvingAddress = true;

                resolveAddress(value)
                    .then((data) => {
                        model.isResolvingAddress = false;

                        if (data) {
                            resolvedAddress(data);
                            resolve();
                        } else {
                            resolvedAddress(undefined);
                            reject(new Error(i18next.t('location:InvalidAddress')));
                        }
                    })
                    .catch(() => {
                        model.isResolvingAddress = false;
                        resolvedAddress(undefined);
                        reject(new Error(i18next.t('location:InvalidAddress')));
                    });
            }, 50);
        } else {
            reject(new Error(i18next.t('location:AddressRequired')));
        }
    }),
    validateAddress: () => model.resolveAddress(model.address())
        .then(() => {
            const errors = {
                address: [],
            };

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.address = true;
            m.redraw();
            return errors.address.length > 0;
        })
        .catch((error) => {
            const errors = {
                address: [
                    error.message,
                ],
            };

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.address = true;
            m.redraw();
            return errors.address.length > 0;
        }),
    setPricePerKw: (value) => {
        model.pricePerKw(parseFloat(value));
    },
    validatePricePerKw: () => Promise.resolve()
        .then(() => {
            const errors = {
                pricePerKw: [],
            };

            if (!(model.pricePerKw() >= 0)) {
                errors.pricePerKw.push(i18next.t('location:InvalidPricePerKw'));
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.pricePerKw = true;
            m.redraw();
            return errors.pricePerKw.length > 0;
        }),
    isValid: () => model.validated.name &&
        model.validated.address &&
        model.validated.pricePerKw &&
        model.errors().name.length === 0 &&
        model.errors().address.length === 0 &&
        model.errors().pricePerKw.length === 0,
    init: data => Promise.resolve()
        .then(() => {
            model.errors({
                name: [],
                address: [],
                pricePerKw: [],
            });
            model.validated.name = false;
            model.validated.address = false;
            model.validated.pricePerKw = false;
            model.setName(data.name);
            model.setAddress(data.address);
            model.setPricePerKw(data.pricePerKw);
            return model;
        }),
    data: () => ({
        name: model.name(),
        address: model.formattedAddress().replace(/^\s+|\s+$/g, ''),
        latitude: model.latitude(),
        longitude: model.longitude(),
        pricePerKw: model.pricePerKw(),
    }),
    submit: options => new Promise((resolve, reject) => {
        if (model.isValid()) {
            return model.validateAddress()
                .then(() => request(options)
                    .then((data) => {
                        resolve(data);
                    })
                    .catch(({ data }) => {
                        const errors = {
                            name: [],
                            address: [],
                            pricePerKw: [],
                        };

                        Object.keys(data).forEach((key) => {
                            errors[camelCase(key)] = data[key];
                        });

                        model.errors(errors);
                        m.redraw();
                        reject(errors);
                    }));
        }

        reject(new Error(i18next.t('location:InvalidLocationData')));
        return undefined;
    }),
};

export default model;
