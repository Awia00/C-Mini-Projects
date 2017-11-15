// webpack.config.js
module.exports = {
    entry: {
        index: ['./index.ts', './glsl/renderer.ts', './glsl/2d-vertex-shader.glsl', './glsl/2d-fragment-shader.glsl']
    },
    output: {
        path: __dirname + '/dist/',
        filename: 'bundle.js'
    },
    resolve: {
        // Add '.ts' and '.tsx' as a resolvable extension.
        extensions: [".webpack.js", ".web.js", ".ts", ".tsx", ".js"]
    },
    module: {
        loaders: [{
                test: /\.glsl$/,
                loader: 'webpack-glsl-loader'
            },
            { test: /\.tsx?$/, loader: "ts-loader" }
        ]
    }
};