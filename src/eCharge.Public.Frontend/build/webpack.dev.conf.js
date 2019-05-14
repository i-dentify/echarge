var merge = require('webpack-merge');
var baseWebpackConfig = require('./webpack.base.conf');

module.exports = merge(baseWebpackConfig, {
    devtool: '#eval-source-map',
});
