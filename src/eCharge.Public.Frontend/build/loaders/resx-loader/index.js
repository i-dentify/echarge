'use strict';

var path = require('path');
var xml2js = require('xml2js');
var parser = new xml2js.Parser();
var Promise = require('promise');

var readPath = Promise.denodeify(require('glob'));
var readFile = Promise.denodeify(require('fs').readFile);
var parseXml = Promise.denodeify(parser.parseString);

function parseFile(filename) {
    var tokens = path.basename(filename, path.extname(filename)).split('.');

    return readFile(filename)
        .then(parseXml)
        .then(function (result) {
            var keyValues = {};
            var language = (tokens[1] || 'en').split('-').shift().toLocaleLowerCase();

            if (result.root.data) {
                result.root.data.forEach(function (item) {
                    var key = item.$.name;
                    var val = item.value && item.value.length === 1 ? item.value[0] : item.value;
                    keyValues[key] = val || '';
                });
            }

            return {
                language: language,
                module: tokens[0],
                keyValues: keyValues
            };
        });
}

module.exports = function (source) {
    var self = this;
    var callback = this.async();
    var pathParts = this.resourcePath.split('.');
    var extension = pathParts.pop();
    pathParts.push('*', extension);
    var resourcePath = pathParts.join('.');

    readPath(resourcePath,  'utf-8')
        .then(function (files) {
            return Promise.all(files.map(function (file) {
                self.addDependency(file);
                return parseFile(file);
            }));
        })
        .then(function (result) {
            var locales = {};
            result.forEach(function (item) {
                if (!locales[item.language]) {
                    locales[item.language] = {'app': {}};
                }

                locales[item.language]['app'] = item.keyValues;
            });

            var output = JSON.stringify(locales)
                .replace(/\u2028/g, '\\u2028')
                .replace(/\u2029/g, '\\u2029');

            return `module.exports = ${output}`;
        }).nodeify(callback);
};
