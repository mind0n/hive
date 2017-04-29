var gulp    =   require("gulp");
var ts      =   require("gulp-typescript");
var ut      =   require("gulp-typescript");
var map     =   require("gulp-sourcemaps");
var web     =   require("gulp-webserver");
var bnd     =   require("gulp-concat");
var nocmt   =   require("gulp-strip-comments");

var tsproj = ts.createProject("tsconfig.json");
var utproj = ut.createProject("tsconfig.test.json");

function buildev(){
    gulp.src(["./node_modules/jquery/dist/jquery.js", "./src/lib/velocity.js"])
        .pipe(gulp.dest("./dist/scripts"))

    var tsResult = tsproj.src()
        .pipe(map.init())
        .pipe(ts(tsproj));

    tsResult.js
        .pipe(bnd("src-bundle.js"))
        .pipe(map.write())
        .pipe(gulp.dest("./dist/scripts"));    
}

gulp.task("default", function(){
    buildev();
});

gulp.task("ut", function(){
    buildev();
    var tsResult = utproj.src()
        .pipe(map.init())
        .pipe(ut(utproj));

    tsResult.js
        .pipe(bnd("tests.js"))
        .pipe(map.write())
        .pipe(gulp.dest("./dist/tests"));

});

gulp.task("utreport", function(){
    gulp.src('./').pipe(web({
        fallback:"spec/specrunner.html",
        host:"127.0.0.1",
        port:9999,
        livereload:true,
        directoryListing:false,
        open:true
    }));    
});


gulp.task("dev", function(){
    buildev();
    gulp.src('./dist').pipe(web({
        fallback:"index.html",
        host:"127.0.0.1",
        port:9999,
        livereload:true,
        directoryListing:false,
        open:false
    }));
});

gulp.task("utwatch", ["ut"], function(){
    gulp.watch("./spec/**/*.ts", ["ut"]);
});