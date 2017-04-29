<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="b.aspx.cs" Inherits="Portal.Testing._3js.b" %>

<%@ Register Src="~/Modules/Include.ascx" TagPrefix="uc1" TagName="Include" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<uc1:Include runat="server" ID="Include" />
	
	<script type="text/javascript">

		joy(function () {
			var g = joy.j3d.use('simple', {
					target: 'container',
					onrender: function () {
						//joy.debug.log(this.camera.rotation.x);
						this.camera.rotation.y += 0.005;
						//this.camera.updateLight();
					}
				}
			);
			var mapurl = '/Themes/Imgs/CubeSkin.png';
			var texture = THREE.ImageUtils.loadTexture(mapurl);
			var imgs = [
    			'Imgs/pos-x.png',
    			'Imgs/neg-x.png',
    			'Imgs/pos-y.png',
    			'Imgs/neg-y.png',
    			'Imgs/pos-z.png',
    			'Imgs/neg-z.png'
			];

			var skyTexture = THREE.ImageUtils.loadTextureCube(imgs);
			skyTexture.format = THREE.RGBFormat;
			
			var shader = THREE.ShaderLib["cube"];
			shader.uniforms["tCube"].value = skyTexture;
			
			var material = new THREE.MeshPhongMaterial({ map: texture });
			var skyMaterial = new THREE.ShaderMaterial({
				fragmentShader: shader.fragmentShader,
				vertexShader: shader.vertexShader,
				uniforms: shader.uniforms,
				//lights:true,
				depthWrite: false,
				side: THREE.BackSide
			});

			var geometry = new THREE.CubeGeometry(1, 1, 1);
			var skyGeometry = new THREE.CubeGeometry(4000, 4000, 4000);

			var skyBox = new THREE.Mesh(skyGeometry, skyMaterial);
			var cube2 = new THREE.Mesh(geometry, material);

			cube2.rotation.x = Math.PI / 5;
			cube2.rotation.y = Math.PI / 5;
			skyBox.onrender = function(g) {
				//this.rotation.y += 0.01;
			};
			cube2.onrender = function(g) {
				this.rotation.y -= 0.01;
				this.position.z = Math.sin(this.rotation.y) * 1.2;
				this.position.x = Math.cos(this.rotation.y) * 1.2;
			};
			g.addObject(skyBox);
			g.addObject(cube2);

			g.addPerspectiveCamera({ x: 0, y: 0, z: 3 });
			g.addDirectionalLight({ color: 0xffffff, camera: g.camera });

			g.run();
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
