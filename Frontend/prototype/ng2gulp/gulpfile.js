var gulp    =   require("gulp");
var mcss    =   require("gulp-clean-css");
var scss    =   require("gulp-sass");
var ts      =   require("gulp-typescript");
var minify  =   require("gulp-minify");
var map     =   require("gulp-sourcemaps");
var web     =   require("gulp-webserver");
var bnd     =   require("gulp-concat");
var nocmt   =   require("gulp-strip-comments");
var jade    =   require("gulp-jade");
var b64     =   require("gulp-base64");
var svg     =   require("gulp-svg-symbols");
var Builder =   require('systemjs-builder');

var tsproj = ts.createProject("tsconfig.json");

gulp.task("assets", function(){
    // gulp.src("assets/**/*.svg")
    //     .pipe(svg())
    //     .pipe(gulp.dest("./dist"));
    // gulp.src("assets/**/*.ico")
    //     .pipe(gulp.dest("./dist"));
    // gulp.src("assets/**/*.html")
    //     .pipe(gulp.dest("./dist"));

    /*
    <script src="node_modules/core-js/client/shim.min.js"></script>
    <script src="node_modules/zone.js/dist/zone.js"></script>
    <script src="node_modules/reflect-metadata/Reflect.js"></script>
    <script src="node_modules/systemjs/dist/system.src.js"></script>
    <script src="systemjs.config.js"></script>
    */
    // gulp.src(["assets/**/*.html", "assets/**/*.css"])
    //     .pipe(gulp.dest("./dist"));
    //gulp.src(["./systemjs.config.js"]).pipe(gulp.dest("./dist/"));

    gulp.src([
        "./node_modules/core-js/client/shim.min.js"
        //"./node_modules/zone.js/dist/zone.js",
        //"./node_modules/reflect-metadata/Reflect.js",
        //"./node_modules/systemjs/dist/system.src.js"
        ])
        //.pipe(bnd('vendor.js'))
        .pipe(gulp.dest("./dist/vendor"));
});

gulp.task("default", ["assets"], function(){
    // gulp.src(["./src/**/*.scss"])
    //     .pipe(scss().on("error", scss.logError))
    //     .pipe(b64({
    //         maxImageSize: 8*1024,
    //         deleteAfterEncoding: false
    //     }))
    //     .pipe(gulp.dest("./dist/themes"));
    // gulp.src("./src/**/*.jade")
    //     .pipe(jade({
    //         pretty:true,
    //         compileDebug:true,
    //         doctype:"html",
    //         locals:{mode:"dev"}
    //     }))
    //     .pipe(gulp.dest("./dist"));

    console.log("Compiling ...");
    var tsResult = tsproj.src()
        .pipe(map.init())
        .pipe(ts(tsproj));

    tsResult.js
        .pipe(map.write())
        .pipe(gulp.dest("./app"));
        //.pipe(gulp.dest("./build"));

    //console.log("Bundling ...");
    // var builder = new Builder('./', './systemjs.config.js', {defaultJSExtensions:true});
    // builder.buildStatic('./node_modules/core-js/shim.js', './dist/vendor/shim.bundle.js', { sourceMaps: false, lowResSourceMaps: false, sourceMapContents: false, minify:false })
    //     .then(function(){
    //         console.log('Bundle Shim.js completed');
    //     });
    // builder.bundle('./build/main.js', './dist/app/main.js', { sourceMaps: true, lowResSourceMaps: true, sourceMapContents: true })
    //     .then(function(){
    //         console.log('Bundle completed');
    //     })
    //     .catch(function(err){
    //         console.error(err);
    //     });
});

gulp.task("dev", ["default"], function(){
    gulp.src('./').pipe(web({
        fallback:"index.html",
        host:"0.0.0.0",
        port:9999,
        livereload:true,
        directoryListing:false,
        open:false
    }));
});

gulp.task("watch", ["default"], function(){
    gulp.watch(["./src/**/*.*", "./assets/**/*.*"], ["default"]);
});

gulp.task("test", ["assets"], function(){
    gulp.src(["./node_modules/jquery/dist/jquery.js", "./src/lib/velocity.js"])
        .pipe(minify({ext:{src:".js", min:"-min.js"}, ignoreFiles:["-min.js"], exclude:["tasks"]}))
        .pipe(gulp.dest("./dist/scripts"))
    gulp.src(["./src/**/*.scss"])
        .pipe(scss().on("error", scss.logError))
        .pipe(mcss({compatibility:"ie8"}))
        .pipe(gulp.dest("./dist/themes"));

    gulp.src("./src/**/*.jade")
        .pipe(jade({
            pretty:false,
            doctype:"html",
            locals:{mode:"test"}
        }))
        .pipe(gulp.dest("./dist"));

    var tsResult = tsproj.src()
        .pipe(ts(tsproj));

    tsResult.js
        .pipe(nocmt())
        .pipe(bnd("wo.js"))
        .pipe(minify({ext:{src:".js", min:"-min.js"}, ignoreFiles:["-min.js"], exclude:["tasks"]}))
        .pipe(gulp.dest("./dist/scripts"));    
});


