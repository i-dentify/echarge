// https://eslint.org/docs/user-guide/configuring

module.exports = {
    root: true,
    parserOptions: {
        parser: 'babel-eslint'
    },
    env: {
        browser: true,
    },
    // https://github.com/vuejs/eslint-plugin-vue#priority-a-essential-error-prevention
    // consider switching to `plugin:vue/strongly-recommended` or `plugin:vue/recommended` for stricter rules.
    extends: [
        'airbnb-base'
    ],
    // check if imports actually resolve
    settings: {
        'import/resolver': {
            webpack: {
                config: './build/webpack.prod.conf.js'
            }
        }
    },
    // add your custom rules here
    rules: {
        // don't require .vue extension when importing
        'import/extensions': [
            'error',
            'always',
            {
                js: 'never',
            }
        ],
        // disallow reassignment of function parameters
        // disallow parameter object manipulation except for specific exclusions
        'no-param-reassign': [
            'error',
            {
                props: false,
            }
        ],
        // allow optionalDependencies
        'import/no-extraneous-dependencies': [
            'error',
            {
                optionalDependencies: [
                    'test/unit/index.js'
                ]
            }
        ],
        'indent': [
            'error',
            4
        ],
        'max-len': [
            'off',
        ],
        'function-paren-newline': [
            'error',
            'never'
        ],
        'prefer-destructuring': [
            'error',
            {
                'object': false,
                'array': false
            }
        ],
        // allow debugger during development
        'no-debugger': process.env.NODE_ENV === 'production' ? 'error' : 'off'
    }
};