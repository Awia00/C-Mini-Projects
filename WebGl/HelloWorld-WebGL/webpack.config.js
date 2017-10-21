// webpack.config.js
module.exports = {
    entry: {
        index: ['./index.js', './glsl/renderer.js', './glsl/2d-vertex-shader.glsl', './glsl/2d-fragment-shader.glsl']
    },
    output: {
        path: __dirname + '/dist/',
        filename: 'bundle.js'
    },
    module: {
        loaders: [{
            test: /\.glsl$/,
            loader: 'webpack-glsl-loader'
        }]
    }
};