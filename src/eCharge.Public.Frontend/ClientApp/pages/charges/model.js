import m from 'mithril';
import stream from 'mithril/stream';
import i18next from 'i18next';
import camelCase from 'lodash/camelCase';
import request from 'lib/api/request';

const model = {
    location: stream(),
    car: stream(),
    date: stream(),
    loadStart: stream(),
    loadEnd: stream(),
    errors: stream({
        date: [],
        loadStart: [],
        loadEnd: [],
    }),
    validated: {
        date: false,
        loadStart: false,
        loadEnd: false,
    },
    setLocation: (value) => {
        model.location(value);
    },
    setCar: (value) => {
        model.car(value);
    },
    setDate: (value) => {
        model.date(value);

        if (model.errors().date.length > 0) {
            model.validateDate();
        }
    },
    validateDate: () => Promise.resolve()
        .then(() => {
            const today = new Date();
            const tomorrow = new Date(today.getFullYear(), today.getMonth(), today.getDate() + 1);
            const errors = {
                date: [],
            };

            if (!/\S/.test(model.date())) {
                errors.date.push(i18next.t('charge:DateRequired'));
            } else {
                const [
                    year,
                    month,
                    day,
                ] = model.date().split(/-/);

                try {
                    const date = new Date(year, month - 1, day);

                    if (date > tomorrow) {
                        errors.date.push(i18next.t('charge:DateCannotBeInFuture'));
                    }
                } catch (err) {
                    errors.date.push(i18next.t('charge:InvalidDate'));
                }
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.date = true;
            m.redraw();
            return errors.date.length > 0;
        }),
    setLoadStart: (value) => {
        model.loadStart(parseInt(value, 10));

        if (model.errors().loadStart.length > 0 || model.errors().loadEnd.length > 0) {
            model.validateLoadStart();
        }
    },
    validateLoadStart: () => Promise.resolve()
        .then(() => {
            const errors = {
                loadStart: [],
            };

            if (!(model.loadStart() >= 0)) {
                errors.loadStart.push(i18next.t('charge:InvalidLoadStart'));
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.loadStart = true;
            model.validateLoadEnd();
            m.redraw();
            return errors.loadStart.length > 0;
        }),
    setLoadEnd: (value) => {
        model.loadEnd(parseInt(value, 10));

        if (model.errors().loadEnd.length > 0) {
            model.validateLoadEnd();
        }
    },
    validateLoadEnd: () => Promise.resolve()
        .then(() => {
            const errors = {
                loadEnd: [],
            };

            if (model.loadEnd() <= (model.loadStart() || 0)) {
                errors.loadEnd.push(i18next.t('charge:LoadEndMustBeHigherThanStart'));
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.loadEnd = true;
            m.redraw();
            return errors.loadEnd.length > 0;
        }),
    isValid: () => model.validated.date &&
        model.validated.loadStart &&
        model.validated.loadEnd &&
        model.errors().date.length === 0 &&
        model.errors().loadStart.length === 0 &&
        model.errors().loadEnd.length === 0,
    init: data => Promise.resolve()
        .then(() => {
            model.errors({
                date: [],
                loadStart: [],
                loadEnd: [],
            });
            model.validated.date = false;
            model.validated.loadStart = false;
            model.validated.loadEnd = false;
            model.setLocation(data.location);
            model.setCar(data.car);
            model.setDate(data.date);
            model.setLoadStart(data.loadStart);
            model.setLoadEnd(data.loadEnd);
            return model;
        }),
    data: () => ({
        location: model.location().id,
        car: model.car().id,
        pricePerKw: model.location().pricePerKw,
        batteryCapacity: model.car().batteryCapacity,
        date: model.date(),
        loadStart: model.loadStart(),
        loadEnd: model.loadEnd(),
    }),
    submit: options => new Promise((resolve, reject) => {
        if (model.isValid()) {
            return request(options)
                .then((data) => {
                    resolve(data);
                })
                .catch(({ data }) => {
                    const errors = {
                        date: [],
                        loadStart: [],
                        loadEnd: [],
                    };

                    Object.keys(data).forEach((key) => {
                        errors[camelCase(key)] = data[key];
                    });

                    model.errors(errors);
                    m.redraw();
                    reject(errors);
                });
        }

        reject(new Error(i18next.t('charge:InvalidChargeData')));
        return undefined;
    }),
};

export default model;
