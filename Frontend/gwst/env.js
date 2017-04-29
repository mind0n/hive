if (!exports){
    exports = {};
}
exports.karma = {
    files: ['dist/tests/*tests.js']
    ,preprocessors: {
        // "dist/scripts/*.js":["coverage"],
        "dist/tests/*tests.js":["coverage"]
    }
    ,frameworks: ['angular','jasmine']
    ,basepath: ''
    ,reporters: {
        names:['spec', 'coverage']
        ,coverageReporter:{
            type:'html',
            dir:'test-coverage/'
        }
    }
    ,browsers: ["Chrome"]
};

exports.gulp = {
    browserify:{
        basedir: '../../..'
        ,root: 'git/widgets/gwst'
        ,entries: function(){
            var rlt = [
                this.dir('typings/index.d.ts'),
                this.dir('spec/1st.tests.ts'), 
                this.dir('spec/2nd.tests.ts'),
                this.dir('spec/component.tests.ts'),
            ];
            return rlt;
        }
        ,externals: ["jquery","jasmine","react","react-dom"]
        ,outname: 'tests.js'
        ,outdir: 'dist/tests'
        ,dir:function(path){
            var rlt = this.root + "/" + path;
            return rlt;
        }
    }
};

exports.ts = {
    root: "ld-76/DotNet/Web_Solutions/Gdc.Web.SPA"
};

exports.srcroot = function(){
    var rlt = this.ts.root;
    return rlt;
};

exports.testroot = function(){
    var rlt = this.gulp.browserify.root;
    return rlt;
};

// function t(file){
//     var rlt = exports.gulp.browserify.basedir + "/" + exports.ts.root + "/" + file;
//     return rlt;
// };