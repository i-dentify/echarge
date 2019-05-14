import m from 'mithril';
import stream from 'mithril/stream';
import i18next from 'i18next';
import camelCase from 'lodash/camelCase';
import request from 'lib/api/request';

const model = {
    name: stream(),
    batteryCapacity: stream(),
    errors: stream({
        name: [],
        batteryCapacity: [],
    }),
    validated: {
        name: false,
        batteryCapacity: false,
    },
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
                errors.name.push(i18next.t('car:CarNameRequired'));
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.name = true;
            m.redraw();
            return errors.name.length > 0;
        }),
    setBatteryCapacity: (value) => {
        model.batteryCapacity(parseInt(value, 10));

        if (model.errors().batteryCapacity.length > 0) {
            model.validateBatteryCapacity();
        }
    },
    validateBatteryCapacity: () => Promise.resolve()
        .then(() => {
            const errors = {
                batteryCapacity: [],
            };

            if (!(model.batteryCapacity() > 0)) {
                errors.batteryCapacity.push(i18next.t('car:InvalidBatteryCapacity'));
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.batteryCapacity = true;
            m.redraw();
            return errors.batteryCapacity.length > 0;
        }),
    isValid: () => model.validated.name &&
        model.validated.batteryCapacity &&
        model.errors().name.length === 0 &&
        model.errors().batteryCapacity.length === 0,
    init: data => Promise.resolve()
        .then(() => {
            model.errors({
                name: [],
                batteryCapacity: [],
            });
            model.validated.name = false;
            model.validated.batteryCapacity = false;
            model.setName(data.name);
            model.setBatteryCapacity(data.batteryCapacity);
            return model;
        }),
    data: () => ({
        name: model.name(),
        batteryCapacity: model.batteryCapacity(),
    }),
    submit: options => new Promise((resolve, reject) => {
        if (model.isValid()) {
            return request(options)
                .then((data) => {
                    resolve(data);
                })
                .catch(({ data }) => {
                    const errors = {
                        name: [],
                        batteryCapacity: [],
                    };

                    Object.keys(data).forEach((key) => {
                        errors[camelCase(key)] = data[key];
                    });

                    model.errors(errors);
                    m.redraw();
                    reject(errors);
                });
        }

        reject(new Error(i18next.t('car:InvalidCarData')));
        return undefined;
    }),
};

export default model;
