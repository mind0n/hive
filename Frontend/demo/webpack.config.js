var path = require("path");
var webpack = require("webpack");
var etp = require("extract-text-webpack-plugin");

var ug = new webpack.optimize.UglifyJsPlugin({
	output:{
		comments:false
	}
});

var exjs = new etp("lib.[name].js");
var excss = new etp("[name].bundle.css");

// Path for specified loader
function use(name){
	var r = path.join(__dirname, "extensions/" + name + "-loader.js");
	console.log("Loading: " + r + "-loader.js");
	return r;
}

module.exports = {
    entry: {
        demo: "./src/index.tsx",
        theme: "./src/theme.tsx",
        lib: "./src/lib.js"
    },
    output: {
        path: __dirname + "/dist",
        filename: "[name].bundle.js"
    },
    // Enable sourcemaps for debugging webpack's output.
    devtool: "source-map",
    resolve: {
        // Add '.ts' and '.tsx' as resolvable extensions.
        extensions: ["", ".webpack.js", ".web.js", ".ts", ".tsx", ".js"]
    },
    module: {
        loaders: [
            { test: /\.tsx?$/, loader: "ts-loader" },
            { test: /\.css$/, loader: excss.extract(["css"]) },
            { test: /\.svg$/, loader: "file-loader" },
            //{ test: /jquery/i, loader: exjs.extract([use("content")]) }
        ],
        preLoaders: [
            // All output '.js' files will have any sourcemaps re-processed by 'source-map-loader'.
            { test: /\.js$/, loader: "source-map-loader" },
        ]
    },
    plugins:[
        exjs, excss, new webpack.ProvidePlugin({"window.$": "jquery", "window.jQuery":"jquery", "window.jquery":"jquery"})
    ],
    externals: {
        //"jquery": "jQuery",
        "react": "React",
        "react-dom": "ReactDOM",
    },
    devServer:{
        contentBase: ".",
        host:"localhost",
        port:8888,
        inline:true,
        hot:true,
        watchOptions: {
            aggregateTimeout: 300,
            poll:100
        }
    }
};