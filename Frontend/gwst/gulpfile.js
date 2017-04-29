// https://zhongsp.gitbooks.io/typescript-handbook/content/doc/handbook/tutorials/Gulp.html

var gulp        = require("gulp");
var browserify  = require("browserify");
var source      = require('vinyl-source-stream');
var tsify       = require("tsify");
var web         = require("gulp-webserver");
var factor      = require("factor-bundle");
var jade        = require("gulp-jade");
var env         = require("./env.js");

var paths = {
    pages: ['src/*.html'],
    specs: ['spec/*.jade']
};

gulp.task("copy-html", function () {
    return gulp.src(paths.pages)
        .pipe(gulp.dest("dist"));
});

gulp.task("default", ["copy-html"], function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['src/main.ts'],
        cache: {},
        packageCache: {}
    })
    .plugin(tsify)
    .bundle()
    .pipe(source('bundle.js'))
    .pipe(gulp.dest("dist/scripts"));
});

gulp.task("dev", ["default"], function(){
    gulp.src('./dist').pipe(web({
        fallback:"index.html",
        host:"127.0.0.1",
        port:9999,
        livereload:true,
        directoryListing:false,
        open:false
    }));
});

gulp.task("watch", ["default"], function(){
    gulp.watch("./src/**/*.ts", ["default"]);
});

gulp.task("copy-ut-html", function () {
    return gulp.src(paths.specs)
        .pipe(jade({
            pretty:true,
            compileDebug:true,
            doctype:"html",
            locals:{env:env}
        }))
        .pipe(gulp.dest("dist/tests"));
});

gulp.task("ut", ["copy-ut-html"], function () {
    return browserify({
        basedir: env.gulp.browserify.basedir,
        debug: true,
        entries: env.gulp.browserify.entries(),
        cache: {},
        packageCache: {}
    })
    .external(env.gulp.browserify.externals)
    .plugin(tsify)
    //.plugin(factor, {o:["dist/tests/1st.tests.js", "dist/tests/2nd.tests.js"]})
    .bundle()
    .pipe(source(env.gulp.browserify.outname))
    //.pipe(source('common.js'))
    .pipe(gulp.dest(env.gulp.browserify.outdir));
});
gulp.task("spec", ["ut"], function(){
    gulp.src(env.gulp.browserify.basedir + '/')
    //gulp.src('../../../')
    .pipe(web({
        fallback: 
            env.gulp.browserify.dir("dist/tests/specrunner.html"), 
            //'/' + env.gulp.browserify.dir("dist/tests/specrunner.html"),
        host:"127.0.0.1",
        port:9999,
        livereload:false,
        directoryListing:false,
        open:true
    }));
});

gulp.task("utwatch", ["ut"], function(){
    gulp.watch("./spec/**/*.ts", ["ut"]);
});
