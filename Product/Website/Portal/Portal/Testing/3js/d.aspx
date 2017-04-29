<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="d.aspx.cs" Inherits="Portal.Testing._3js.d" %>

<%@ Register Src="~/Modules/Include.ascx" TagPrefix="uc1" TagName="Include" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<style type="text/css">
		html,body,form{
			width:100%;
			height:100%;
		}
		div {
			position: absolute;
		}
		#main{
			left: 0px;
			top: 0px;
			width:800px;
			height:400px;
			background:green;
		}
		#preview{
			left: 0px;
			top: 400px;
			width:800px;
			height:400px;
			background:blue;
		}
		#test {
			top: 0px;
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
		
		var o = [0, 0];
		function cubeOnRender() {
		    this.rotation.y += 0.03;
		    this.rotation.x += 0.03;
		    this.rotation.z += 0.03;

		    this.rotation.y %= Math.PI * 20;
		    this.rotation.x %= Math.PI * 20;
		    this.rotation.z %= Math.PI * 20;
		    //debugger;
		    o[0] = this.rotation.x / 4;
		    o[1] = this.rotation.y / 4;
		    this.material.map.offset.set(o[0], o[1]);
		    this.material.map.needsUpdate = true;
		}
		
		joy(function () {
			var g = new j3d({ app: 'simple', container: 'main' });
			g.addCube({
			    position: [-1, 0, 0],
			    onrender: cubeOnRender
			});
			g.addCube({ position: [1, 0, 0] });
			g.addCamera({ position: [0, 0, 3] });
			g.addSkybox(imgs);
			g.addLight();
			g.start();

			var h = new j3d({ app: 'simple', container: 'preview' });
			h.addCube({ position: [-1, 0, 0] });
			h.addCube({ position: [1, 0, 0] });
			h.addCamera({ position: [0, 0, 3] });
			h.addSkybox(imgs2);
			h.addLight();
			h.start();

			var i = new j3d({ app: 'simple', container: 'test' });
			i.addCube({ position: [-1, 0, 0] });
			i.addCube({ position: [1, 0, 0] });
			i.addCamera({ position: [0, 0, 3] });
			i.addSkybox(imgs);
			i.addLight();
			i.start();
		});
	</script>
</head>
<body>
	<div id="main"></div>
	<div id="preview"></div>
	<div id="test"></div>
</body>
</html>
