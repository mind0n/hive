// https://zhongsp.gitbooks.io/typescript-handbook/content/doc/handbook/tutorials/Gulp.html

var gulp        = require("gulp");
var browserify  = require("browserify");
var source      = require('vinyl-source-stream');
var tsify       = require("tsify");
var web         = require("gulp-webserver");
var factor      = require("factor-bundle");

var paths = {
    pages: ['src/*.html'],
    specs: ['spec/*.html']
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
    gulp.watch("./src/**/*.*", ["default"]);
});

gulp.task("copy-ut-html", function () {
    return gulp.src(paths.specs)
        .pipe(gulp.dest("dist/tests"));
});

gulp.task("ut", ["copy-ut-html"], function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['spec/1st.tests.ts', 'spec/2nd.tests.ts'],
        cache: {},
        packageCache: {}
    })
    .external(["jquery","jasmine","react","react-dom"])
    .plugin(tsify)
    //.plugin(factor, {o:["dist/tests/1st.tests.js", "dist/tests/2nd.tests.js"]})
    .bundle()
    .pipe(source('tests.js'))
    //.pipe(source('common.js'))
    .pipe(gulp.dest("dist/tests"));
});

gulp.task("spec", ["ut"], function(){
    gulp.src('./').pipe(web({
        fallback:"dist/tests/specrunner.html",
        host:"127.0.0.1",
        port:9999,
        livereload:true,
        directoryListing:false,
        open:true
    }));
});

gulp.task("utwatch", ["ut"], function(){
    gulp.watch("./spec/**/*.*", ["ut"]);
});
