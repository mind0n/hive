<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="a.aspx.cs" Inherits="Portal.Testing._3js.a" %>

<%@ Register Src="~/Modules/Include.ascx" TagPrefix="uc1" TagName="Include" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<uc1:Include runat="server" ID="Include" />
	
	<script type="text/javascript">
		var renderer, scene, camera, cube;

		function run() {
			renderer.render(scene, camera);
			cube.rotation.y += 0.02;
			nextFrame(run);
		}
		joy(function() {
			var container = joy('container');
			renderer = new THREE.WebGLRenderer({ antialias: true });
			renderer.setSize(container.offsetWidth, container.offsetHeight);
			container.appendChild(renderer.domElement);

			scene = new THREE.Scene();
			camera = new THREE.PerspectiveCamera(45, container.offsetWidth / container.offsetHeight, 1, 4000);
			camera.position.set(0, 0, 3);

			var light = new THREE.DirectionalLight(0xffffff, 1.5);
			light.position.set(0, 0, 1);
			scene.add(light);

			var mapurl = '/Themes/Imgs/CubeSkin.png';
			var map = THREE.ImageUtils.loadTexture(mapurl);

			var material = new THREE.MeshPhongMaterial({ map: map });
			var geometry = new THREE.CubeGeometry(1, 1, 1);

			cube = new THREE.Mesh(geometry, material);

			cube.rotation.x = Math.PI / 5;
			cube.rotation.y = Math.PI / 5;

			scene.add(cube);
			run();

		});
	</script>
</head>
<body>
    <form id="mainform" runat="server">
    <div id="container" style="width: 100%; height: 100%;">
    
    </div>
    </form>
</body>
</html>
