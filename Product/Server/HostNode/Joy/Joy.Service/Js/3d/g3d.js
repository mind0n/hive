var g3d = function () {
    var defmat = { specular: '#ffffff', color: '#719978', emissive: '#000000' };
    var darkmat = { specular: '#ffffff', color: '#ff0000', emissive: '#000000' };
    var lightmat = { specular: '#E8C04A', color: '#E8C04A', emissive: '#E8C04A' };
    j3d.materials = {
        gun: darkmat
        , light: lightmat
        , def: defmat
    };
    j3d.creators.cannonTunnelA = function (c, parent, root) {
        var model = {
            $: {
                connector: { mn: 'cylinder', rt: 0.4, rb: 0.4, len: 0.4, rot: [90, 0, 0], material: j3d.materials.def }
                , grpTunnel: {
                    mn: 'group', loc: [0.5, 0, 0], $: {
                        base: { mn: 'cube', gbox: [0.4, 0.3, 0.3], loc: [0, 0, 0], material: j3d.materials.def }
                       , tunnel: { mn: 'cube', gbox: [1, 0.2, 0.2], loc: [0.5, 0, 0], material: j3d.materials.gun }
                    }
                }
            }
        }
        joy.extend(model, c);
        model.mn = null;
        return j3d.build(model, root, parent);
    };
    j3d.creators.cannonTowerA = function (c, parent, root) {
        var model = {
            $: {
                grptower: {
                    mn: 'group', alias: 'tower', loc: [0, 0.9, 0], $: {
                        offset: {
                            mn: 'collection', loc: [0.4, 0, 0], $: {
                                grplr: {
                                    mn: 'collection', loc: [0, -0.04, 0], $: {
                                        towerLeft: { mn: 'cylinder', rt: 0.7, rb: 0.7, len: 0.3, loc: [0.2, -0.15, 0.35], rot: [90, 0, 0], material: j3d.materials.def }
                                        , towerRight: { mn: 'cylinder', rt: 0.7, rb: 0.7, len: 0.3, loc: [0.2, -0.15, -0.35], rot: [90, 0, 0], material: j3d.materials.def }
                                        , gun: { mn: 'cannonTunnelA', alias: 'cannons', loc: [0.4, 0, 0] }
                                    }
                                }
                                , tail: { mn: 'cylinder', segmentsw: 4, rt: 1.1, rb: 0.9, len: 2, gbox: [1.6, 1.2, 1], loc: [-0.8, -0.05, 0], rot: [45, 0, 90], material: j3d.materials.def }
                            }
                        }
                        //, bottom: { mn: 'cube', gbox: [2, 0.2, 0.9], loc: [-0.3, -0.8, 0], mat:'MeshBasicMaterial', material: j3d.materials.def, texture: { url: '/ts/img/crate.gif' } }
                        , bottom: { mn: 'cube', gbox: [2, 0.2, 0.9], loc: [-0.3, -0.8, 0], material: j3d.materials.def }
                    }
                }
            }
        }
        joy.extend(model, c);
        model.mn = null;
        return j3d.build(model, root, parent);
    };

    j3d.creators.cannonTunnelB = function (c, parent, root) {
        var model = {
            $: {
                base: { mn: 'cube', gbox: [0.5, 0.5, 0.5], loc: [0, 0, 0], material: defmat }
                , tunnel: { mn: 'cube', gbox: [1, 0.3, 0.3], loc: [0.75, 0, 0], material: darkmat }
            }
        }
        joy.extend(model, c);
        model.mn = null;
        return j3d.build(model, root, parent);
    };
    j3d.creators.cannonTowerB = function (c, parent, root) {
        var model = {
            $: {
                cannons: {
                    mn: 'group', alias: 'tower', loc:[0, 0, 0], $: {
                        tower: { mn: 'cube', gbox: [0.8, 1, 0.7], loc: [0, 0.5, 0], material: defmat }
                        , cannons: {
                            mn: 'group', alias: 'cannons', loc:[0, 1, 0], $: {
                                rightCannon: { mn: 'cannonTunnelB', alias: 'rc', loc: [0, 0, 0.6] }
                                , leftCannon: { mn: 'cannonTunnelB', alias: 'lc', loc: [0, 0, -0.6] }
                            }
                        }
                    }
                }
            }
        }
        joy.extend(model, c);
        model.mn = null;
        return j3d.build(model, root, parent);
    };
    j3d.creators.jet = function (c, parent, root) {
        var jet = {
            $: {
                body: { mn: 'cube', gbox: [1, 1, 1], loc: [0, 0, 0], material: darkmat }
                , leftWing: { mn: 'cube', gbox: [1, 0.25, 1], loc: [0, -0.3, -0.6], rot: [-20, 45, 0], material: defmat }
                , rightWing: { mn: 'cube', gbox: [1, 0.25, 1], loc: [0, -0.3, 0.6], rot: [20, 45, 0], material: defmat }
                , topWing: { mn: 'cube', gbox: [1, 0.25, 1], loc: [0, 0.5, 0], rot: [90, 45, 0], material: defmat }
                , frontBody: { mn: 'cube', gbox: [1, 0.5, 0.8], loc: [1, 0, 0], material: defmat }
                , tailEngine: { mn: 'cube', gbox: [0.2, 0.2, 0.2], loc: [-0.6, 0, 0], material: lightmat }
            }
        };
        joy.extend(jet, c);
        jet.mn = null;
        return j3d.build(jet, root, parent);
    };
    j3d.creators.fighter = function (c, parent, root) {
        var fighter = {
            $: {
                base: { mn: 'jet' }
                , frontGun: { mn: 'cube', gbox: [1, 0.2, 0.2], loc: [2, 0, 0], material: darkmat }
            }
        };
        joy.extend(fighter, c);
        fighter.mn = null;
        return j3d.build(fighter, root, parent);
    };
    j3d.creators.scout = function (c, parent, root) {

        var fighter = {
            $: {
                base: { mn: 'jet' }
                , radar: { mn: 'ring', r:0.7, rr:0.1, segments:7, tsegments:7, loc: [1.3, 0, 0], rot:[90, 0, 0], material: darkmat }
            }
        };
        joy.extend(fighter, c);
        fighter.mn = null;
        return j3d.build(fighter, root, parent);
    };
    j3d.creators.bomber = function (c, parent, root) {
        var bomber = {
            $: {
                base: { mn: 'jet' }
                , cannonConnector: { mn: 'cube', gbox: [0.3, 0.2, 1.4], loc: [1, 0, 0], material: defmat }
                , leftCannonBase: { mn: 'cube', gbox: [1, 0.5, 0.5], loc: [1.2, 0, 0.8], material: defmat }
                , leftCannon: { mn: 'cube', gbox: [1, 0.3, 0.3], loc: [2, 0, 0.8], material: darkmat }
                , rightCannonBase: { mn: 'cube', gbox: [1, 0.5, 0.5], loc: [1.2, 0, -0.8], material: defmat }
                , rightCannon: { mn: 'cube', gbox: [1, 0.3, 0.3], loc: [2, 0, -0.8], material: darkmat }
            }
        };
        joy.extend(bomber, c);
        bomber.mn = null;
        return j3d.build(bomber, root, parent);
    };
}();