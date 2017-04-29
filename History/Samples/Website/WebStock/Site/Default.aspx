<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Site._Default" %>

<!doctype html>
<html lang="en">
    <head>
        <meta charset="utf-8" />
        <title>Web Stock</title>
        <style>
            html, body {
                width:100%;
                height:100%;
                margin:0px;
                padding:0px;
            }
            canvas {
                width:100%;
                height:100%;
                background:silver;
            }
        </style>
    </head>
    <body>
        <script type="text/javascript" >
            document.body.onload = function () {
                var cvs = document.getElementById('cvs');
                var ctx = cvs.getContext("2d");
                ctx.fillStyle = "#ffffaa";
                var x = 10, y = 10;
                ctx.fillRect(x, y, cvs.width - x * 2, cvs.height - y * 2);
                var img = new Image();
                img.onload = function () {
                    ctx.drawImage(this, 100, -100, this.naturalWidth, this.naturalHeight);
                    ctx.setTransform(1, 0, 0, 1, 0, 0);
                    var angle = 45 * Math.PI / 180;
                    ctx.rotate(angle);
                    ctx.drawImage(this, 100, -100, this.naturalWidth, this.naturalHeight);
                }
                img.src = "http://cn.bing.com/fd/s/a/k_zh_cn.png";
            };
        </script>
        <div style="width:600px; height:400px; margin:100px auto; border:solid 1px silver;">
            <canvas id="cvs" width="600" height="400">Not supported!</canvas>
        </div>
    </body>
</html>

