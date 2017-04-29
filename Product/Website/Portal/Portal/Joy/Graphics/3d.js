joy.j3d = {
	use: function (name, cfg) {
		if (joy.j3d.init[name]) {
			return joy.j3d.init[name](cfg);
		}
		return null;
	},
	init: {
		basic:function(cfg) {
			
		},
		simple: function (cfg) {
			var target = cfg.target, onrender = cfg.onrender;
			var g = { models: [], render: onrender };
			var renderer, scene, camera, cube;
			g.run = function () {
				renderer.render(this.scene, this.camera);
				var isSkip = false;
				if (this.render) {
					isSkip = this.render();
				}
				if (!isSkip) {
					for (var i = 0; i < this.models.length; i++) {
						if (this.models[i] && this.models[i].onrender) {
							this.models[i].onrender(this);
						}
					}
				}
				nextFrame(function () { g.run(); });
			};
			g.addObject = function (o) {
				this.models.push(o);
				this.scene.add(o);
			};
			g.addDirectionalLight = function (cfg) {
				var color = cfg.color;
				var dir = cfg.direction;
				var camera = cfg.camera;
				var light = new THREE.DirectionalLight(color, 1.5);
				if (camera) {
					camera.light = light;
					camera.scene = this.scene;
					camera.updateLight(light);
				} else if (dir) {
					light.position.set(dir.x, dir.y, dir.z);
				}
				this.scene.add(light);
			};
			g.addPerspectiveCamera = function(cfg) {
				var camera = new THREE.PerspectiveCamera(45, container.offsetWidth / container.offsetHeight, 0.01, 900000);
				this.camera = camera;
				camera.position.set(cfg.x, cfg.y, cfg.z);
				camera.lookDir = function() {
					var vector = new THREE.Vector3(0, 0, -1);
					vector.applyEuler(this.rotation, this.eulerOrder);
					return vector;
				};
				camera.updateLight = function (light) {
					if (!light) {
						light = this.light;
					}
					if (light && light.position) {
						var p = this.position;
						var q = this.lookDir();
						light.position.x = (p.x - q.x);
						light.position.y = (p.y - q.y);
						light.position.z = (p.z - q.z);
					}
				};
			};
			var container = joy('container');
			renderer = new THREE.WebGLRenderer({ antialias: true });
			g.renderer = renderer;
			renderer.setSize(container.offsetWidth, container.offsetHeight);
			container.appendChild(renderer.domElement);
			g.canvas = renderer.domElement;

			scene = new THREE.Scene();
			//var ambient = new THREE.AmbientLight(0xffffff);
			//scene.add(ambient);
			g.scene = scene;

			return g;
		}
	}
};