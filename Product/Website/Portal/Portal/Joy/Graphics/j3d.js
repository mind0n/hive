(function () {
	function List() {
		var list = new Array();
		list.selectedIndex = -1;
		list.add = function (o) {
			this[this.length] = o;
		};
		list.selection = function () {
			if (this.selectedIndex >= 0 && this.selectedIndex < this.length) {
				return this[this.selectedIndex];
			}
			return null;
		};
		list.select = function (v, f) {
			if (!f) {
				f = '_key';
			}
			if (!v) {
				return this.selection();
			}
			if (typeof (v) == 'number') {

			} else if (typeof (v) == 'string') {
				for (var i = 0; i < this.length; i++) {
					var o = this[i];
					if (o[f] == v) {
						return o;
					}
				}
			}
			return null;
		};
		list.enum = function (delegate) {
			if (delegate) {
				for (var i = 0; i < this.length; i++) {
					if (this[i] !== null) {
						delegate(this[i]);
					}
				}
			}
		};
		return list;
	}
	function Dict() {
	    var dict = List();
	    dict.addItem = function (key, val) {
	        val._key = key;
	        this.add(val);
	    };
		dict.retrieve = function (noi) {
			var kv = this.select(noi);
			return kv;
		};
		return dict;
	}
	function Cameras() {
		var cams = Dict();
	    cams.add = function(cam) {
	        this[this.length] = cam;
	        if (this.selectedIndex < 0) {
	            this.selectedIndex = 0;
	        }
	    };
		return cams;
	}
	function Scenes() {
		var scenes = Dict();
		scenes.add = function(scene) {
			//var scene = this.select(scene);
			var t = typeof (scene);
			var s = scene;
			if (t == 'string') {
				s = new THREE.Scene();
				s._key = scene;
			}
			if (s) {
				this[this.length] = s;
			}
			s.cameras = Cameras();
			s.render = function (renderer) {
				var cam = this.cameras.selection();
				renderer.render(this, cam);
			};
			return s;
		};
		return scenes;
	}

    function MouseControl() {
        this.prevPos = [0, 0];
        this.curtPos = [0, 0];
        this.isActive = false;
        this.mode = 0;
        this.targets = List();
        this.activate = function(e) {
            e = e || event;
            this.mode = e.button;
            this.isActive = !this.isActive;
            this.addInfo(e);
            this.addInfo(e);
            return false;
        };
        this.getDelta = function() {
            if (this.isActive) {
                //return [this.mX; this.mY];
                var r = [0, 0];
                for (var i = 0; i < this.prevPos.length; i++) {
                    r[i] = this.curtPos[i] - this.prevPos[i];
                }
                return r;
            }
            return [0, 0];
        };
        this.addInfo = function(e) {
            if (this.isActive && e && e.clientX && e.clientY) {
                this.prevPos[0] = this.curtPos[0];
                this.prevPos[1] = this.curtPos[1];
                this.curtPos[0] = e.clientX;
                this.curtPos[1] = e.clientY;
            }
            this.mX = e.movementX ||
                e.mozMovementX ||
                e.webkitMovementX ||
                0;
            this.mY = e.movementY ||
                e.mozMovementY ||
                e.webkitMovementY ||
                0;
        };
        this.trigger = function() {
            //this.moveHandlers.enum(function(i) {
            //    //i.f(i.c, i.info);
            //    //this.handlers[]
            //});
            this.targets.enum(function(i) {
                if (i && i.o && i.info) {
                    //debugger;
                    var info = i.info;
                    var h = i.info.activateController(i.h);
                    if (h) {
                        var n = h[info.mode];
                        var f = info.handlers.retrieve(n);
                        f(i.o, info.getDelta());
                    }
                }
            });
        };
        this.activateController = function(name) {
            var c = this.controllers.retrieve(name || 'default');
            if (c) {
                this.activeController = c;
            }
            return c;
        };
        this.moveHandlers = List();
        this.handlers = Dict();
        this.handlers.addItem('lb-translate', function (o, delta) {
            o.position.x += delta[0] / 100;
            o.position.y -= delta[1] / 100;
        });
        this.handlers.addItem('rb-rotateFixedY', function(o, delta) {
            j3d.rotate(o, new THREE.Vector3(1, 0, 0), -delta[1] / 1000, true);
            j3d.rotate(o, new THREE.Vector3(0, 1, 0), -delta[0] / 1000);
        });
        this.controllers = Dict();
        this.controllers.addItem('default', ['lb-translate', null, 'rb-rotateFixedY']);
        this.activateController('default');
    }

    var verMajor = 0, verMinor = 1;
	function j3d(cfg) {
	    var renderer, scenes, container;

		var sceneSize = 900000;

		var mouseInfo = new MouseControl();
		var models = {
			active: List(),
			inactive: List()
		};
		var apps = {
		    simple: function (app, appCfg) {
		        app.cfg = appCfg;
				app.addLight = function (lightCfg) {
					if (!lightCfg) {
						lightCfg = {};
					}
					var color = lightCfg.color || 0xffffff;
					var dir = lightCfg.direction || [0, 1, 1];
					var scene = scenes.retrieve(lightCfg.scene);
					var light = new THREE.DirectionalLight(color, 1.5);
					light.position.set(dir[0], dir[1], dir[2]);
					if (scene) {
						scene.add(light);
					}
				};
				app.line = function (lineCfg) {
					var p = lineCfg.begin;
					var q = lineCfg.end;
					if (lineCfg.color) {
						lineCfg.colorb = lineCfg.colorb || lineCfg.color;
						lineCfg.colore = lineCfg.colore || lineCfg.color;
					}
					var geometry = new THREE.Geometry();
					var material = lineCfg.dashed ? new THREE.LineDashedMaterial({ linewidth: 1, color: lineCfg.color, dashSize: 1, gapSize: 1, depthTest: false }) : new THREE.LineBasicMaterial({ vertexColors: THREE.VertexColors, depthTest: false });
					var cp = new THREE.Color(lineCfg.colorb);
					var cq = new THREE.Color(lineCfg.colore);

					// 线的材质可以由2点的颜色决定
					geometry.vertices.push(j3d.V3(p));
					geometry.vertices.push(j3d.V3(q));
					geometry.colors.push(cp,cq);
					geometry.computeLineDistances();

					var line = new THREE.Line(geometry, material, THREE.LinePieces);
					this.addObj(line, lineCfg.scene);
				};
				app.addSquare = function(c) {
					var pos = c.pos;
					
				};
				app.addModel = function (modelCfg) {
					if (!modelCfg) {
						modelCfg = {};
					}
					var loader = new THREE.ColladaLoader();
					var app = this;
					loader.load('/Testing/3js/Models/CannonBase.DAE', function colladaReady(collada) {
						target = collada.scene;
						target.setMaterial = function (material, n) {
						    var node = n || this;
						    if (node.material) {
						        node.material = material;
						    }
						    if (node.children) {
								for (var i = 0; i < node.children.length; i++) {
									this.setMaterial(material, node.children[i]);
								}
							}
						};
						skin = collada.skins[0];
						app.addObj(target, modelCfg.scene);
						if (modelCfg.onload) {
							modelCfg.onload(target);
						}
					});
				};
				app.addCube = function (cubeCfg) {
					if (!cubeCfg) {
						return;
					}
					var mapurl = '/Themes/Imgs/brick001.jpg';
					var map = THREE.ImageUtils.loadTexture(mapurl);
					map.wrapS = map.wrapT = THREE.RepeatWrapping;
					map.repeat.set(1, 1);
					var material = new THREE.MeshPhongMaterial({ map: map });
					var geometry = new THREE.CubeGeometry(1, 1, 1);
					var cube = new THREE.Mesh(geometry, material);
					cube.onrender = cubeCfg.onrender;
					cube.rotation.x = Math.PI / 5;
					cube.rotation.y = Math.PI / 5;
				    cube.map = map;
					cube.position.x = cubeCfg.position[0];
					cube.position.y = cubeCfg.position[1];
					cube.position.z = cubeCfg.position[2];
					this.addObj(cube, cubeCfg.scene);
				    return cube;
				};
				app.addCamera = function (cameraCfg) {
					if (!cameraCfg) {
						cameraCfg = {};
					}
					var s = scenes.retrieve(cameraCfg.scene);
					if (!cameraCfg.camera) {
						var camera = new THREE.PerspectiveCamera(45, container.offsetWidth / container.offsetHeight, 0.01, sceneSize);
						var p = cameraCfg.position || [10, 10, 10];
						if (s && camera) {
							camera.lookDir = function () {
								var vector = new THREE.Vector3(0, 0, -1);
								vector.applyEuler(this.rotation, this.eulerOrder);
								return vector;
							};
							camera.updateLight = function (light) {
								if (!light) {
									return;
								}
								if (light && light.position) {
									var pp = this.position;
									var q = this.lookDir();
									light.position.x = (pp.x - q.x);
									light.position.y = (pp.y - q.y);
									light.position.z = (pp.z - q.z);
								}
							};
							camera.position.set(p[0], p[1], p[2]);
							camera.lookAt(j3d.V3([0, 0, 0]));
							s.cameras.add(camera);
							//if (cfg.mouseMove) {
							//    mouseInfo.moveHandlers.add({ c: camera, f: cfg.mouseMove, info: mouseInfo });
						    //}
							return camera;
						}
					} else {
						s.cameras.add(cameraCfg.camera);
						return cameraCfg.camera;
					}
					return null;
				};
				return app;
			}
		};

		function run() {
			//var s = scenes.selection();
			//renderer.render(s, s.cameras.selection());
			renderer.clear();
			models.active.enum(function (item) {
				if (item.onrender) {
					item.onrender();
				}
			});
			for (var i = 0; i < scenes.length; i++) {
				var s = scenes[i];
				s.render(renderer);
			}
			//renderer.render(s, s.cameras.selection());
			requestAnimationFrame(run);
		}

		this.addObj = function (o, sceneObject, type) {
			var t = type || (o.onrender ? 'active' : 'inactive');
			var s = scenes.retrieve(sceneObject);
			if (o && s) {
				models[t].add(o);
				s.add(o);
			}
		};
		this.addSkybox = function(imgs) {

			var skyTexture = THREE.ImageUtils.loadTextureCube(imgs);
			skyTexture.format = THREE.RGBFormat;
	
			var shader = THREE.ShaderLib["cube"];
			shader.uniforms["tCube"].value = skyTexture;

			var skyMaterial = new THREE.ShaderMaterial({
				fragmentShader: shader.fragmentShader,
				vertexShader: shader.vertexShader,
				uniforms: shader.uniforms,
				depthWrite: false,
				side: THREE.BackSide
			});
			//skyMaterial.color.setHex(0x00ff00);

			var skyGeometry = new THREE.CubeGeometry(4000, 4000, 4000);

			var skyBox = new THREE.Mesh(skyGeometry, skyMaterial);
			this.activeScene().add(skyBox);
		};
		this.addAxis = function (axisCfg) {
			var x = true, y = true, z = true;
			if (axisCfg.axis) {
				x = axisCfg.axis[0];
				y = axisCfg.axis[1];
				z = axisCfg.axis[2];
			}
			if (x) {
				this.line({ begin: [0, 0, 0], end: [sceneSize, 0, 0], color: 0xff0000, scene: axisCfg.scene });
				this.line({ begin: [0, 0, 0], end: [-sceneSize, 0, 0], color: 0xff0000, dashed: true, scene: axisCfg.scene });
			}
			if (y) {
				this.line({ begin: [0, 0, 0], end: [0, sceneSize, 0], color: 0x00ff00, scene: axisCfg.scene });
				this.line({ begin: [0, 0, 0], end: [0, -sceneSize, 0], color: 0x00ff00, dashed: true, scene: axisCfg.scene });
			}
			if (z) {
				this.line({ begin: [0, 0, 0], end: [0, 0, sceneSize], color: 0x0000ff, scene: axisCfg.scene });
				this.line({ begin: [0, 0, 0], end: [0, 0, -sceneSize], color: 0x0000ff, dashed: true, scene: axisCfg.scene });
			}
			//this.lines({ material: { name: 'LineBasicMaterial', vertexColors: THREE.VertexColors }, points: [[0, 0, 0], [sceneSize, 0, 0], [0, 0, 0], [0, sceneSize, 0], [0, 0, 0], [0, 0, sceneSize]], color: 0x00ff00 });
		};
		this.lines = function (lineCfg) {
			var ps = lineCfg.points;
			if (lineCfg.color) {
				lineCfg.colorb = lineCfg.colorb || lineCfg.color;
				lineCfg.colore = lineCfg.colore || lineCfg.color;
			}
			var geometry = new THREE.Geometry();
			var material = j3d.Material(lineCfg.material, lineCfg.dashed ? new THREE.LineDashedMaterial({ linewidth: 3, color: lineCfg.color, dashSize: 3, gapSize: 3, depthTest: false }) : new THREE.LineBasicMaterial({ vertexColors: THREE.VertexColors, depthTest: false }));
			var cp = new THREE.Color(lineCfg.colorb);
			var cq = new THREE.Color(lineCfg.colore);

			if (!lineCfg.mode) {
				for (var i = 0; i < ps.length; i++) {
					var p = ps[i];
					geometry.vertices.push(j3d.V3(p));
					geometry.colors.push(cp, cq);
				}
			}

			var r = new THREE.Line(geometry, material, THREE.LinePieces);
			this.addObj(r, lineCfg.scene);
		};

		this.activeScene = function() {
			return scenes.selection();
		};
		this.start = function () {
			run();
		};
		this.attachObject = function (o, h) {
		    mouseInfo.targets.add({ o: o, h: h, info:mouseInfo });
		};
		container = joy(cfg.container);
		if (!container) {
			container = document.createElement('div');
			document.body.appendChild(container);
			container.style.width = '100%';
			container.style.height = '100%';
		}
		renderer = new THREE.WebGLRenderer({ antialias: true });
		renderer.setSize(container.offsetWidth, container.offsetHeight);
		renderer.autoClear = false;
		renderer.domElement.oncontextmenu = function (e) {
		    e = e || event;
		    if (e.preventDefault) {
		        e.preventDefault();
		    }
		    return false;
		};
		renderer.domElement.onmousemove = function (evt) {
		    var e = evt || event;
		    mouseInfo.addInfo(e);
		    if (mouseInfo.isActive) {
		        mouseInfo.trigger();
		    }
		};
		renderer.domElement.onmouseup = function (evt) {
		    mouseInfo.activate(evt || event);
		};
	    container.appendChild(renderer.domElement);

		scenes = Scenes();
		
		scenes.add('default');
		scenes.add('front');

		scenes.selectedIndex = 0;
		if (cfg.app && apps[cfg.app]) {
			apps[cfg.app](this, cfg);
		}
	};
	j3d.rotate = function (o, axis, r, islocal) {
	    if (!islocal) {
	        var rotWorldMatrix = new THREE.Matrix4();
	        rotWorldMatrix.makeRotationAxis(axis.normalize(), r);
	        rotWorldMatrix.multiply(o.matrix); // pre-multiply
	        o.matrix = rotWorldMatrix;
	        o.rotation.setFromRotationMatrix(o.matrix);
	    } else {
	        var rotObjectMatrix = new THREE.Matrix4();
	        rotObjectMatrix.makeRotationAxis(axis.normalize(), r);
	        o.matrix.multiply(rotObjectMatrix);
	        o.rotation.setFromRotationMatrix(o.matrix);
	    }
	};
	j3d.version = function () {
		return verMajor + '.' + verMinor;
	};
	j3d.V3 = function(list) {
		return new THREE.Vector3(list[0], list[1], list[2]);
	};
	j3d.Material = function (cfg, def) {
		var t = typeof (cfg);
		if (t == 'object' && cfg.name) {
			return new THREE[cfg.name](cfg);
		}
		return def;
	};
	self.j3d = j3d;
})();