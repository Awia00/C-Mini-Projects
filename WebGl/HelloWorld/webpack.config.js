// webpack.config.js
module.exports = {
    entry: {
        index: ['./index.js', './renderer.js']
    },
    output: {
        path: __dirname + '/dist/',
        filename: 'bundle.js'
    }
};