// Karma configuration

var env = require("./env.js");
module.exports = function(config) {
  config.set({

    // base path that will be used to resolve all patterns (eg. files, exclude)
    basePath: env.karma.basepath,


    // frameworks to use
    // available frameworks: https://npmjs.org/browse/keyword/karma-adapter
    frameworks: env.karma.frameworks,


    // list of files / patterns to load in the browser
    files: env.karma.files,


    // list of files to exclude
    exclude: [
      "node_modules"
    ],


    // preprocess matching files before serving them to the browser
    // available preprocessors: https://npmjs.org/browse/keyword/karma-preprocessor
    preprocessors: env.karma.preprocessors,

    // test results reporter to use
    // possible values: 'dots', 'progress'
    // available reporters: https://npmjs.org/browse/keyword/karma-reporter
    // reporters: ['progress','coverage', 'jasmine-spec-runner'],
    // jasmineSpecRunnerReporter:{
    //   jasmineCoreDir:'jasmine-core'
    // },
    reporters: env.karma.reporters.names,
    specReporter: {
        maxLogLines: 5,         // limit number of lines logged per test
        suppressErrorSummary: true,  // do not print error summary
        suppressFailed: false,  // do not print information about failed tests
        suppressPassed: false,  // do not print information about passed tests
        suppressSkipped: true,  // do not print information about skipped tests
        showSpecTiming: false // print the time elapsed for each spec
    },
    //plugins: ["karma-spec-reporter"],

    coverageReporter:env.karma.reporters.coverageReporter,


    // web server port
    port: 9900,


    // enable / disable colors in the output (reporters and logs)
    colors: true,


    // level of logging
    // possible values: config.LOG_DISABLE || config.LOG_ERROR || config.LOG_WARN || config.LOG_INFO || config.LOG_DEBUG
    logLevel: config.LOG_INFO,


    // enable / disable watching file and executing tests whenever any file changes
    autoWatch: true,


    // start these browsers
    // available browser launchers: https://npmjs.org/browse/keyword/karma-launcher
    browsers: env.karma.browsers,


    // Continuous Integration mode
    // if true, Karma captures browsers, runs the tests and exits
    singleRun: false,

    // Concurrency level
    // how many browser should be started simultaneous
    concurrency: Infinity
  })
}
