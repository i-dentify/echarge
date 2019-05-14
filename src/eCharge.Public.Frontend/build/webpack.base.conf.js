var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');

module.exports = {
    entry: {
        'main': './ClientApp/main.js',
        'styles': './Styles/site.scss',
    },
    output: {
        path: path.resolve(__dirname, '../wwwroot/dist'),
        publicPath: '/dist/',
        filename: '[name].bundle.js',
    },
    module: {
        rules: [
            {
                test: /\.resx$/,
                use: [
                    {
                        loader: 'resx-loader',
                    }
                ],
            },
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: /node_modules/,
            },
            {
                test: /\.(jpe?g|png|gif|svg)$/i,
                use: [
                    'file-loader?name=images/[name].[ext]',
                    //'image-webpack-loader?bypassOnDebug',
                ]
            },
            {
                test: /\.scss$/,
                use: ExtractTextPlugin.extract({
                    use: [
                        {
                            loader: 'css-loader',
                            options: {
                                minimize: true,
                                sourceMap: process.env.NODE_ENV === 'development',
                            },
                        },
                        {
                            loader: 'postcss-loader',
                            options: {
                                plugins: function () {
                                    return [
                                        require('precss'),
                                        require('autoprefixer'),
                                    ];
                                },
                            },
                        },
                        {
                            loader: 'sass-loader',
                        },
                    ],
                }),
            },
        ],
    },
    resolve: {
        extensions: [
            '*',
            '.js',
            '.json',
        ],
        modules: [
            path.resolve(__dirname, "../ClientApp"),
            path.resolve(__dirname, "../node_modules"),
        ],
    },
    resolveLoader: {
        modules: [
            'node_modules',
            path.resolve(__dirname, './loaders')
        ],
    },
    performance: {
        hints: false,
    },
    plugins: [
        new ExtractTextPlugin("[name].css"),
        new webpack.optimize.AggressiveMergingPlugin(),
        new webpack.optimize.CommonsChunkPlugin({
            name: 'vendor',
            minChunks: function (module) {
                return module.context && module.context.indexOf('node_modules') !== -1;
            },
        }),
        new webpack.optimize.CommonsChunkPlugin({
            name: 'manifest',
        }),
    ],
};
