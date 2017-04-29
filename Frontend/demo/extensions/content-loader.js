module.exports = function(source){
    var src = source;
    src = src.replace(/\\/ig, "\\\\");
    src = src.replace(/\'/ig, "\\'");
    src = src.replace(/\"/ig, "\\\"");
    src = src.replace(/\r/ig, "\\r");
    src = src.replace(/\n/ig, "\\n");
    return 'var content = "'+src+'";module.exports = content;';
};

