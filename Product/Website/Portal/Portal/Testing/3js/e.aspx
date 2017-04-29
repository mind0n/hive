<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="e.aspx.cs" Inherits="Portal.Testing._3js.e" %>

<%@ Register Src="~/Modules/Include.ascx" TagPrefix="uc1" TagName="Include" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<style type="text/css">
		* {
			border: 0;
			padding: 0;
			margin: 0;
		}
		html,body,form{
			width:100%;
			height:100%;
		}
		body {
			overflow: hidden;
		}
		div {
			position: absolute;
		}
		#main{
			left: 0;
			top: 0;
			width: 100%;
			height: 100%;
			background:green;
		}
		#preview{
			left: 0;
			top: 400px;
			width:800px;
			height:400px;
			background:blue;
		}
		#test {
			top: 0;
			left: 800px;
			width: 400px;
			height: 800px;
			background: red;
		}
	</style>
    <title></title>
	<uc1:Include runat="server" ID="Include" />
	<script type="text/javascript">
		var imgs = [
			'Imgs/pos-x.png',
			'Imgs/neg-x.png',
			'Imgs/pos-y.png',
			'Imgs/neg-y.png',
			'Imgs/pos-z.png',
			'Imgs/neg-z.png'
		];
		var imgs2 = [
			'/Themes/Imgs/pos-x.png',
			'/Themes/Imgs/neg-x.png',
			'/Themes/Imgs/pos-y.png',
			'/Themes/Imgs/neg-y.png',
			'/Themes/Imgs/pos-z.png',
			'/Themes/Imgs/neg-z.png'
		];

		function mm(camera, info) {
		    var r = info.getDelta();
		    var dx = r[0] / 100;
		    var dy = r[1] / 100;
		    if (info.mode == 0) {
		        camera.position.x += dx;
		        camera.position.z += dy;
		    } else if (info.mode == 2) {
		        j3d.rotate(camera, new THREE.Vector3(1, 0, 0), -dy / 10, true);
		        j3d.rotate(camera, new THREE.Vector3(0, 1, 0), -dx / 10);
		    }
		}
		var o = [0, 0];
		function cubeOnRender() {
			this.rotation.y += 0.03;
			this.rotation.x += 0.03;
			this.rotation.z += 0.03;

			this.rotation.y %= Math.PI * 20;
			this.rotation.x %= Math.PI * 20;
			this.rotation.z %= Math.PI * 20;
			//debugger;
			o[0] = this.rotation.x;
			o[1] = this.rotation.y;
			this.material.map.offset.set(o[0], o[1]);
			this.material.map.needsUpdate = true;
			debugger;
		}

		joy(function () {
			var g = new j3d({ app: 'simple', container: 'main' });
			g.addAxis({ scene: 'front' });
			var cam = g.addCamera({ position: [0, 0, 5], scene: 'default', mouseMove: mm });
			var cam2 = g.addCamera({ position: [0, 0, 5], scene: 'front', mouseMove: mm });
		    //g.attachObject(cam, 'default');
		    //g.attachObject(cam2, 'default');
		    g.addSkybox(imgs);
		    //var cube = g.addCube({ position: [0, 0, 0], onrender: cubeOnRender });
			//g.attachObject(cube, 'default');
		    g.addModel({
		    	onload: function (model) {
		    		model.setMaterial(new THREE.MeshPhongMaterial({ color: 0xff0000 }));
		    		g.attachObject(model, 'default');
		    	}
		    });
			g.addLight();
			g.start();
		});
	</script>
</head>
<body>
	<div id="main"></div>
</body>
</html>

