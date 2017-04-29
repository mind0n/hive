'use strict';

var gulp        = require('gulp');
var gutil       = require('gulp-util');
var mocha       = require('gulp-mocha');
var istanbul    = require('gulp-istanbul');
var isparta     = require('isparta');
var runSequence = require('run-sequence');
var factor      = require("factor-bundle");

var browserify  = require("browserify");
var source      = require('vinyl-source-stream');
var tsify       = require("tsify");
var web         = require("gulp-webserver");

// Transform all required files with Babel
require('babel-register');

// Files to process
var TEST_FILES = 'dist/tests/**/*test*.js';
var SRC_FILES = 'dist/tests/**/*test*.js';

gulp.task("ut", function () {
    return browserify({
        basedir: '.',
        debug: true,
        entries: ['spec/tests.ts', 'spec/tests2.ts'],
        cache: {},
        packageCache: {}
    })
    .external(["chai"])
    .plugin(tsify)
    //.plugin(factor, {o:['dist/tests/tests.js', 'dist/tests/tests2.js']})
    .bundle()
    //.pipe(source('common.js'))
    .pipe(source('tests.js'))
    .pipe(gulp.dest("dist/tests"));
});

gulp.task("utwatch", ["ut"], function(){
    gulp.watch("./spec/**/*.*", ["ut"]);
});

/*
 * Instrument files using istanbul and isparta
 */
gulp.task('coverage:instrument', function() {
  return gulp.src(SRC_FILES)
    .pipe(istanbul({
      instrumenter: isparta.Instrumenter // Use the isparta instrumenter (code coverage for ES6)
      // Istanbul configuration (see https://github.com/SBoudrias/gulp-istanbul#istanbulopt)
      // ...
    }))
    .pipe(istanbul.hookRequire()); // Force `require` to return covered files
});

/*
 * Write coverage reports after test success
 */
gulp.task('coverage:report', function(done) {
  return gulp.src(SRC_FILES, {read: false})
    .pipe(istanbul.writeReports({
      // Istanbul configuration (see https://github.com/SBoudrias/gulp-istanbul#istanbulwritereportsopt)
      // ...
    }));
});

/**
 * Run unit tests
 */
gulp.task('test', function() {
  return gulp.src(TEST_FILES, {read: false})
    .pipe(mocha({
      //require: [__dirname + '/lib/jsdom'] // Prepare environement for React/JSX testing
      //require:[__dirname + "\\dist\\tests\\common.js"]
    }));
});

/**
 * Run unit tests with code coverage
 */
gulp.task('test:coverage', function(done) {
  runSequence('coverage:instrument', 'test', 'coverage:report', done);
});

/**
 * Watch files and run unit tests on changes
 */
gulp.task('tdd', function(done) {
  gulp.watch([
    TEST_FILES,
    SRC_FILES
  ], ['test']).on('error', gutil.log);
});