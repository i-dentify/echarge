import m from 'mithril';
import stream from 'mithril/stream';
import i18next from 'i18next';
import camelCase from 'lodash/camelCase';
import request from 'lib/api/request';

const model = {
    code: stream(),
    errors: stream({
        code: [],
    }),
    validated: {
        code: false,
    },
    setCode: (value) => {
        model.code(value);

        if (model.errors().code.length > 0) {
            model.validateCode();
        }
    },
    validateCode: () => Promise.resolve()
        .then(() => {
            const errors = {
                code: [],
            };

            if (!/\S/.test(model.code())) {
                errors.code.push(i18next.t('location:InvitationCodeRequired'));
            }

            model.errors(Object.assign({}, model.errors(), errors));
            model.validated.code = true;
            m.redraw();
            return errors.code.length > 0;
        }),
    isValid: () => model.validated.code && model.errors().code.length === 0,
    init: data => Promise.resolve()
        .then(() => {
            model.errors({
                code: [],
            });
            model.validated.code = false;
            model.setCode(data.code);
            return model;
        }),
    data: () => ({
        code: model.code(),
    }),
    submit: options => new Promise((resolve, reject) => {
        if (model.isValid()) {
            return request(options)
                .then((data) => {
                    resolve(data);
                })
                .catch(({ data }) => {
                    const errors = {
                        code: [],
                    };

                    Object.keys(data).forEach((key) => {
                        errors[camelCase(key)] = data[key];
                    });

                    model.errors(errors);
                    m.redraw();
                    reject(errors);
                });
        }

        reject(new Error(i18next.t('location:InvalidInvitationData')));
        return undefined;
    }),
};

export default model;
