var gulp = require('gulp');
var shell = require('gulp-shell');
var clean = require('gulp-clean');
var htmlreplace = require('gulp-html-replace');

var runSequence = require('run-sequence');
var Builder = require('systemjs-builder');
var builder = new Builder('', 'systemjs.config.js');

var bundleHash = new Date().getTime();
var mainBundleName = 'main.bundle.js'; // bundleHash + '.main.bundle.js';
var vendorBundleName = 'vendor.min.js'; //bundleHash + '.vendor.bundle.js';

var web     =   require("gulp-webserver");
var b64     =   require("gulp-base64");
var jade    =   require("gulp-jade");
var scss    =   require("gulp-sass");
var mcss    =   require("gulp-clean-css");

// This is main task for production use
gulp.task('default', function(done) {
    runSequence('scss_transpile', 'jade_transpile', 'compile_ts', 'bundle', 'copy_assets', 'postclean', function() {
        done();
    });
});

gulp.task('test', function(done) {
    runSequence('scss_transpile', 'jade_transpile', 'compile_ts', 'bundle:test', 'copy_assets', 'postclean', function() {
        done();
    });
});

gulp.task('scss_transpile', function(){
    return gulp.src(["./themes/**/*.scss"])
        .pipe(scss().on("error", scss.logError))
        .pipe(b64({
            maxImageSize: 8*1024,
            deleteAfterEncoding: false
        }))
        .pipe(mcss({compatibility:"ie8"}))
        .pipe(gulp.dest("./dist/themes"));
});

gulp.task("watch", function(){
    gulp.watch(["./app/**/*.ts", "./app/**/*.html", "./assets/**/*.*","./themes/**/*.*", "./*.*"], ["default"]);
});

gulp.task('jade_transpile', function(){
    return gulp.src('./**/*.jade')
        .pipe(jade({
            pretty:true,
            compileDebug:true,
            doctype:'html',
            locals:{mode:'dev'}
        }))
        .pipe(gulp.dest('./dist'));
});
gulp.task('jade_transpile:test', function(){
    return gulp.src('./**/*.jade')
        .pipe(jade({
            pretty:false,
            compileDebug:false,
            doctype:'html',
            locals:{mode:'test'}
        }))
        .pipe(gulp.dest('./dist'));
});
gulp.task("host", function(){
    gulp.src('./dist').pipe(web({
        fallback:"index.html",
        host:"0.0.0.0",
        port:9999,
        livereload:true,
        directoryListing:false,
        open:false
    }));
});

gulp.task('bundle', ['bundle:vendor', 'bundle:app'], function () {
    console.log(mainBundleName);
    console.log(vendorBundleName);
    return gulp.src('./dist/index.html')
        .pipe(htmlreplace({
            'app': mainBundleName,
            'vendor': vendorBundleName
        }))
        .pipe(gulp.dest('./dist'));
});

gulp.task('bundle:test', ['bundle:vendor', 'bundle:app_test'], function () {
    console.log(mainBundleName);
    console.log(vendorBundleName);
    return gulp.src('./dist/index.html')
        .pipe(htmlreplace({
            'app': mainBundleName,
            'vendor': vendorBundleName
        }))
        .pipe(gulp.dest('./dist'));
});

gulp.task('bundle:vendor', function () {
    return builder
        .buildStatic('app/vendor.js', './dist/' + vendorBundleName, { sourceMaps: false, lowResSourceMaps: false, sourceMapContents: false, minify:true })
        .catch(function (err) {
            console.log('Vendor bundle error');
            console.log(err);
        });
});

gulp.task('bundle:app', function () {
    return builder
        .buildStatic('app/main.js', './dist/' + mainBundleName, { sourceMaps: true, lowResSourceMaps: true, sourceMapContents: true, minify:false })
        .catch(function (err) {
            console.log('App bundle error');
            console.log(err);
        });
});

gulp.task('bundle:app_test', function () {
    return builder
        .buildStatic('app/main.js', './dist/' + mainBundleName, { sourceMaps: false, lowResSourceMaps: false, sourceMapContents: false, minify:true })
        .catch(function (err) {
            console.log('App bundle error');
            console.log(err);
        });
});

gulp.task('compile_ts', ['clean:ts'], shell.task([
    'tsc'
]));

gulp.task('copy_assets', function() {
     return gulp.src(['./assets/**/*'], {base:"."})
        .pipe(gulp.dest('./dist'));
});

gulp.task('clean', ['clean:ts', 'clean:dist']);

gulp.task('clean:dist', function () {
    return gulp.src(['./dist'], {read: false})
        .pipe(clean());
});

gulp.task('clean:ts', function () {
    return gulp.src(['./app/**/*.js', './app/**/*.js.map'], {read: false})
        .pipe(clean());
});

gulp.task('postclean', function(){
    return gulp.src(['./app/**/*.js', './app/**/*.js.map']).pipe(clean());
});

