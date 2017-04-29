var path = require("path");
var webpack = require("webpack");
var ExtractTextPlugin = require("extract-text-webpack-plugin");

var exCss = new ExtractTextPlugin('[name].bundle.css');
var exScss = new ExtractTextPlugin('[name].bundle.css');
var exTs = new ExtractTextPlugin('[name].bundle.js');
var exJs = new ExtractTextPlugin('[name].bundle.js');

var ugjs = new webpack.optimize.UglifyJsPlugin({
	output:{
		comments:false
	}
});
function load(name){
	var r = path.join(__dirname, "extensions/" + name + "-loader.js");
	console.log("Loading: " + r + "-loader.js");
	return r;
}
module.exports = {
    entry: {
		wo:"./src/wo.script.entry.js",
        themes:"./src/wo.css.entry.js",
        lib:"./src/lib.script.entry.js"
	},
	devtool: "source-map",
    output: {
        path: __dirname + "/bundle",
		publicPath: "/assets/",
        filename: "[name].bundle.js"
    },
    module: {
        loaders: [
			{ test:/\.css$/, loader: exCss.extract(["css"]) },
			{ test:/\.scss$/, loader: exScss.extract(["css", "sass"]) },
            { test:/\.ts$/, loader: exTs.extract([load("content"), "ts-loader"]) },
            { test:/\jquery.js$/, loader: exJs.extract([load("content")]) },
        ]
    },
	plugins: [exCss, exTs, exJs, exScss, ugjs],
    sassLoader:{
        includePaths:[path.resolve(__dirname, "./src")]
    }
};