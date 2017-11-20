// webpack.config.js
module.exports = {
    entry: {
        index: ['./src']
    },
    output: {
        path: __dirname + '/dist/',
        filename: 'bundle.js'
    },
    resolve: {
        extensions: [".webpack.js", ".web.js", ".ts", ".tsx", ".js", ".css"]
    },
    devtool: 'source-map',
    module: {
        loaders: [{
                enforce: 'pre',
                test: /\.glsl$/,
                loader: 'webpack-glsl-loader',
                include: __dirname + '/src',
            },
            {
                enforce: 'pre',
                test: /\.tsx?$/, 
                loader: "ts-loader",
                include: __dirname + '/src',
            },
            {
                test: /\.css$/,
                loader: "style-loader!css-loader" 
            }
        ]
    }
};