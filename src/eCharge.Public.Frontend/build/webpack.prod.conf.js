var merge = require('webpack-merge');
var webpack = require('webpack');
var UglifyJsPlugin = require('uglifyjs-webpack-plugin');
var WebpackCdnPlugin = require('webpack-cdn-plugin');
var baseWebpackConfig = require('./webpack.base.conf');

module.exports = merge(baseWebpackConfig, {
    devtool: '#source-map',
    plugins: [
        new UglifyJsPlugin({
            parallel: true,
            uglifyOptions: {
                sourceMap: true,
                compress: {
                    warnings: false
                }
            }
        }),
        new webpack.LoaderOptionsPlugin({
            minimize: true
        }),
        new WebpackCdnPlugin({
            modules: [
                {
                    name: 'auth0-js',
                    var: 'auth0',
                    path: 'build/auth0.js',
                },
            ],
            publicPath: '/node_modules'
        }),
    ],
});
