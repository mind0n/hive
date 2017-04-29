var j3d = function () {
    var scene, renderer;
    var creators = {
        cube: createCube
        , group: createGroup
        , collection: createCollection
        , circle: createCircle
        , ring: createRing
        , cylinder: createCylinder
    };
    var phongMaterialCfg = function(){
        var r = {
            // light
            specular: '#a9fcff'
            // intermediate
            , color: '#00abb1'
                // dark
            , emissive: '#006063'
            , shininess: 5
            , wireframe: false
        };
        return r;
    };
    function spaceInit(cube, c) {
        if (!cube) {
            return;
        }
        if (c.loc) {
            cube.position.set(c.loc[0], c.loc[1], c.loc[2]);
        }
        if (c.rot) {
            var r = [c.rot[0] / 180 * Math.PI, c.rot[1] / 180 * Math.PI, c.rot[2] / 180 * Math.PI];
            cube.rotation.set(r[0], r[1], r[2]);
        }
        if (c.scl) {
            cube.scale.set(c.scl[0], c.scl[1], c.scl[2]);
        }
    }
    function createCylinder(c, parent) {
        var matcfg = phongMaterialCfg();
        joy.extend(matcfg, c.material);
        var segmentsWidth = c.segmentsw || 6;
        var openEnded = c.openEnded || false;
        var mat = new THREE.MeshPhongMaterial(matcfg);

        // Dummy settings, replace with proper code:
        var length = c.len;
        var radiusTop = c.rt;
        var radiusBottom = c.rb;
        ////////////////////
        //alert(radiusTop + ',' + radiusBottom + ',' + length + ',' + segmentsWidth + ',' + openEnded);
        var cylGeom = new THREE.CylinderGeometry(radiusTop, radiusBottom, length, segmentsWidth, 1, openEnded);
        var cyl = new THREE.Mesh(cylGeom, mat);
        spaceInit(cyl, c);
        parent.add(cyl);
        return cyl;
    }
    function createRing(c, parent) {
        var matcfg = phongMaterialCfg();
        if (c.material) {
            joy.extend(matcfg, c.material);
        }
        var geometry = new THREE.TorusGeometry(c.r, c.rr, c.segments, c.tsegments);
        var material = new THREE.MeshPhongMaterial(matcfg);
        var torus = new THREE.Mesh(geometry, material);
        spaceInit(torus, c);
        parent.add(torus);
        return torus;
    }
    function createCustom(c, parent) {

    }
    function createCircle(c, parent) {
        var radius = c.r,
            segments = c.segments,
            material = new THREE.LineBasicMaterial({color:c.color}),
            geometry = new THREE.CircleGeometry(radius, segments);
        // alert(c.r + ',' + c.segments + ',' + c.color);
        // Remove center vertex
        geometry.vertices.shift();
        var ln = new THREE.Line(geometry, material);
        spaceInit(ln, c);
        parent.add(ln);
        return ln;
    }
    function createMaterial(c) {
        var material = null;
        var mat = c.mat || 'MeshPhongMaterial';
        var phongcfg = phongMaterialCfg();
        if (c.material) {
            joy.extend(phongcfg, c.material);
        }
        if (c.texture && c.texture.url) {
            var texture = THREE.ImageUtils.loadTexture(c.texture.url);
            texture.wrapS = texture.wrapT = THREE.RepeatWrapping;
            texture.repeat.set(1, 1);
            texture.anisotropy = j3d.curtRenderer().getMaxAnisotropy();
            phongcfg.map = texture;
        }
        material = new THREE[mat](phongcfg);
        return material;
    }
    function createCube(c, parent) {
        var geo = c.geo || 'BoxGeometry';
        var mat = c.mat || 'MeshPhongMaterial';
        var matcfg = phongMaterialCfg();
        if (c.material) {
            joy.extend(matcfg, c.material);
        }
        var sc = parent;
        var geometry = new THREE[geo](c.gbox[0], c.gbox[1], c.gbox[2]);
        //var material = new THREE[mat](matcfg);
        var material = createMaterial(c);
        //var material = new THREE.MeshDepthMaterial({color:0x00ff00});
        var cube = new THREE.Mesh(geometry, material);
        spaceInit(cube, c);
        if (!c.skipAdd && sc) {
            sc.add(cube);
        }
        return cube;
    }
    function createGroup(c, parent) {
        if (!c) {
            c = {};
        }
        var sc = parent;
        var group = new THREE.Object3D();
        group.isgroup = true;
        spaceInit(group, c);
        if (!c.skipAdd && sc) {
            sc.add(group);
        }
        return group;
    }
    function createCollection(c, parent) {
        if (!c) {
            c = {};
        }
        var sc = parent;
        var group = new THREE.Object3D();
        group.isgroup = false;
        spaceInit(group, c);
        if (!c.skipAdd && sc) {
            sc.add(group);
        }
        return group;
    }
    function build(c, root, parent) {
        if (!c.mn) {
            c.mn = 'group';
        }
        if (c.mn == 'group') {
            root = null;
        }
        var create = creators[c.mn];
        if (create) {
            var model = create(c, parent, root);
            model.alias = c.alias;
            if (!root) {
                root = model;
            }
            if (c.$) {
                for (var i in c.$) {
                    var cmodel = build(c.$[i], root, model);
                    if (cmodel.alias) {
                        root['$' + cmodel.alias] = cmodel;
                    }
                }
            }
            if (model.alias) {
                if (root) {
                    root['$' + model.alias] = model;
                }
                else if (parent && parent.$root) {
                    parent.$root['$' + model.alias] = model;
                } else if (parent) {
                    parent['$' + model.alias] = model;
                }
            }
            return model;
        }
        return null;
    }
    function turn(c) {
        var rot = c.rot;
        var mds = c.mds;
        for (var i = 0; i < 3; i++) {
            var r = rot[i];
            var m = mds[i];
            if (i == 0) {
                m.rotation.x += r;
            }
            else if (i == 1) {
                m.rotation.y += r;
            }
            else if (i == 2) {
                m.rotation.z += r;
            }
        }
    }
    var r = {
        createCube: createCube
        , createGroup: createGroup
        , createAxisArrow: function (c, scene) {
            var sourcePos = new THREE.Vector3(0, 0, 0);
            var tPos = new THREE.Vector3(c[0], c[1], c[2]);
            var direction = new THREE.Vector3().sub(tPos, sourcePos);
            var arrow = new THREE.ArrowHelper(direction.clone().normalize(), sourcePos, direction.length(), c[0] ? 0xff0000 : (c[1] ? 0x00ff00 : 0x0000ff));
            scene.add(arrow);
        }
        , createAxisArrows: function (scene) {
            this.createAxisArrow([3, 0, 0], scene);
            this.createAxisArrow([0, 3, 0], scene);
            this.createAxisArrow([0, 0, 3], scene);
        }
        , newScene: function () {
            scene = new THREE.Scene();
            return scene;
        }
        , curtRenderer: function () {
            if (renderer) {
                return renderer;
            }
            return j3d.newRenderer(window.innerWidth, window.innerHeight);
        }
        , newRenderer: function (WIDTH, HEIGHT) {
            renderer = new THREE.WebGLRenderer({ antialias: true });
            renderer.setSize(WIDTH, HEIGHT);
            return renderer;
        }
        , build: build
        , turn: turn
        , use: function (json) {
            var model = build(json); //new THREE.Object3D();
            if (json && json.$ && typeof (json.$) == 'object') {
                if (json.$.length) {
                    for (var i = 0; i < json.$.length; i++) {
                        build(json.$[i], model, model);
                    }
                } else {
                    for (var i in json.$) {
                        build(json.$[i], model, model);
                    }
                }
            }
            return model;
        }
        
    };
    r.creators = creators;
    return r;
}();
