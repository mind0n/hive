//Maya ASCII 2012 scene
//Name: Tire2.ma
//Last modified: Mon, Jan 28, 2013 08:19:20 PM
//Codeset: 936
requires maya "2012";
currentUnit -l meter -a degree -t film;
fileInfo "application" "maya";
fileInfo "product" "Maya 2012";
fileInfo "version" "2012";
fileInfo "cutIdentifier" "001200000000-796618";
fileInfo "osv" "Microsoft Windows 7 Ultimate Edition, 32-bit Windows 7 Service Pack 1 (Build 7601)\n";
createNode transform -s -n "persp";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 4.1390861463896931 1.0053558515051975 1.827717943302855 ;
	setAttr ".r" -type "double3" 349.46164729290717 -655.39999999999611 0 ;
createNode camera -s -n "perspShape" -p "persp";
	setAttr -k off ".v" no;
	setAttr ".fl" 34.999999999999993;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 4.49931395227867;
	setAttr ".ow" 0.1;
	setAttr ".imn" -type "string" "persp";
	setAttr ".den" -type "string" "persp_depth";
	setAttr ".man" -type "string" "persp_mask";
	setAttr ".hc" -type "string" "viewSet -p %camera";
createNode transform -s -n "top";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 1.001 0 ;
	setAttr ".r" -type "double3" -89.999999999999986 0 0 ;
createNode camera -s -n "topShape" -p "top";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 1.001;
	setAttr ".ow" 0.3;
	setAttr ".imn" -type "string" "top";
	setAttr ".den" -type "string" "top_depth";
	setAttr ".man" -type "string" "top_mask";
	setAttr ".hc" -type "string" "viewSet -t %camera";
	setAttr ".o" yes;
createNode transform -s -n "front";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 0 0 1.001 ;
createNode camera -s -n "frontShape" -p "front";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 1.001;
	setAttr ".ow" 0.3;
	setAttr ".imn" -type "string" "front";
	setAttr ".den" -type "string" "front_depth";
	setAttr ".man" -type "string" "front_mask";
	setAttr ".hc" -type "string" "viewSet -f %camera";
	setAttr ".o" yes;
createNode transform -s -n "side";
	setAttr ".v" no;
	setAttr ".t" -type "double3" 1.001 0 0 ;
	setAttr ".r" -type "double3" 0 89.999999999999986 0 ;
createNode camera -s -n "sideShape" -p "side";
	setAttr -k off ".v" no;
	setAttr ".rnd" no;
	setAttr ".ncp" 0.001;
	setAttr ".fcp" 100;
	setAttr ".fd" 0.05;
	setAttr ".coi" 1.001;
	setAttr ".ow" 0.3;
	setAttr ".imn" -type "string" "side";
	setAttr ".den" -type "string" "side_depth";
	setAttr ".man" -type "string" "side_mask";
	setAttr ".hc" -type "string" "viewSet -s %camera";
	setAttr ".o" yes;
createNode transform -n "group3";
createNode transform -n "group1" -p "group3";
createNode transform -n "pCube2" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 1.0186012589334774 0 ;
	setAttr ".r" -type "double3" 18.000000000000004 0 0 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
createNode mesh -n "pCubeShape1" -p "|group3|group1|pCube2";
	setAttr -k off ".v";
	setAttr -s 40 ".iog";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 36 ".pt[0:35]" -type "float3"  -0.12201681 -0.011173462 
		0 0 -0.011173462 0 0 -0.011173462 0 0 -0.011173462 0 0 -0.011173462 0 0.015053959 
		-0.0062148129 0 0.010355307 -0.016602239 0 -0.014363511 -0.044726633 0 -0.05554536 
		-0.2032169 0 -0.12201681 -0.029689822 0 0 -0.013393517 0 0 -0.011167779 0 0 -0.011533394 
		0 0 -0.010482984 0 0.025741868 -0.014680383 0 0.041239008 -0.038476367 0 0.033366371 
		-0.08475291 6.217249e-017 -0.016663861 -0.24779338 6.217249e-017 -0.12201681 -0.029689822 
		0 0 -0.013393517 0 0 -0.011167779 0 0 -0.011533394 0 0 -0.010482984 0 0.025741868 
		-0.014680383 0 0.041239008 -0.038476367 0 0.033366371 -0.08475291 6.217249e-017 -0.016663861 
		-0.24779338 6.217249e-017 -0.12201681 -0.011173462 0 0 -0.011173462 0 0 -0.011173462 
		0 0 -0.011173462 0 0 -0.011173462 0 0.015053959 -0.0062148129 0 0.010355307 -0.016602239 
		0 -0.014363511 -0.044726633 0 -0.05554536 -0.2032169 0;
createNode transform -n "pCube3" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 0.82406572896889929 0.59871879796764493 ;
	setAttr ".r" -type "double3" 36.000000000000014 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999989 0.99999999999999989 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334772 0 ;
	setAttr ".rpt" -type "double3" 0 0.19453552996457796 -0.59871879796764471 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 2.8421709430404003e-016 0 ;
createNode transform -n "pCube4" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 0.5987187979676446 0.82406572896889951 ;
	setAttr ".r" -type "double3" 54.000000000000007 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999989 0.99999999999999989 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334772 0 ;
	setAttr ".rpt" -type "double3" 0 0.41988246096583254 -0.82406572896889918 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 2.8421709430404003e-016 0 ;
createNode transform -n "pCube5" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 0.31476509950216053 0.96874736481513068 ;
	setAttr ".r" -type "double3" 72 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999967 0.99999999999999967 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.018601258933477 0 ;
	setAttr ".rpt" -type "double3" 0 0.70383615943131661 -0.96874736481513046 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 4.2632564145606e-016 0 ;
createNode transform -n "pCube6" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 3.552713678800501e-017 1.0186012589334774 ;
	setAttr ".r" -type "double3" 90.000000000000014 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999967 0.99999999999999967 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.018601258933477 0 ;
	setAttr ".rpt" -type "double3" 0 1.0186012589334772 -1.018601258933477 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 4.2632564145606e-016 0 ;
createNode transform -n "pCube7" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.31476509950216064 0.96874736481513068 ;
	setAttr ".r" -type "double3" 108.00000000000003 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999956 0.99999999999999956 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.018601258933477 0 ;
	setAttr ".rpt" -type "double3" 0 1.3333663584356383 -0.96874736481513024 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 4.2632564145605995e-016 0 ;
createNode transform -n "pCube8" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.59871879796764416 0.82406572896889918 ;
	setAttr ".r" -type "double3" 126.00000000000003 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999967 0.99999999999999967 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.018601258933477 0 ;
	setAttr ".rpt" -type "double3" 0 1.6173200569011217 -0.82406572896889896 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 4.2632564145606e-016 0 ;
createNode transform -n "pCube9" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.82406572896889885 0.59871879796764438 ;
	setAttr ".r" -type "double3" 144.00000000000003 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999978 0.99999999999999978 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334772 0 ;
	setAttr ".rpt" -type "double3" 0 1.8426669879023769 -0.5987187979676446 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 2.8421709430404003e-016 0 ;
createNode transform -n "pCube10" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.96874736481512969 0.31476509950215997 ;
	setAttr ".r" -type "double3" 162.00000000000003 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999978 0.99999999999999978 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334772 0 ;
	setAttr ".rpt" -type "double3" 0 1.9873486237486078 -0.31476509950216036 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 2.8421709430404003e-016 0 ;
createNode transform -n "pCube11" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -1.0186012589334761 -8.1712414612411525e-016 ;
	setAttr ".r" -type "double3" -180 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999967 0.99999999999999967 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.018601258933477 0 ;
	setAttr ".rpt" -type "double3" 0 2.0372025178669539 1.2474267713603468e-016 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 4.2632564145606e-016 0 ;
createNode transform -n "pCube12" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.96874736481512946 -0.31476509950216153 ;
	setAttr ".r" -type "double3" -162 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999967 0.99999999999999967 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.018601258933477 0 ;
	setAttr ".rpt" -type "double3" 0 1.9873486237486078 0.31476509950216081 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 4.2632564145606e-016 0 ;
createNode transform -n "pCube13" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.82406572896889796 -0.59871879796764549 ;
	setAttr ".r" -type "double3" -144 0 0 ;
	setAttr ".s" -type "double3" 1 0.99999999999999967 0.99999999999999967 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.018601258933477 0 ;
	setAttr ".rpt" -type "double3" 0 1.8426669879023769 0.5987187979676446 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 4.2632564145606e-016 0 ;
createNode transform -n "pCube14" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.59871879796764305 -0.82406572896890029 ;
	setAttr ".r" -type "double3" -126 0 0 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".rpt" -type "double3" 0 1.617320056901123 0.82406572896889974 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
createNode transform -n "pCube15" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 -0.31476509950215825 -0.96874736481513146 ;
	setAttr ".r" -type "double3" -108.00000000000001 0 0 ;
	setAttr ".s" -type "double3" 1 1.0000000000000002 1.0000000000000002 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334776 0 ;
	setAttr ".rpt" -type "double3" 0 1.333366358435639 0.96874736481513102 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 -2.8421709430404013e-016 0 ;
createNode transform -n "pCube16" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 3.0198066269804257e-015 -1.0186012589334781 ;
	setAttr ".r" -type "double3" -90.000000000000014 0 0 ;
	setAttr ".s" -type "double3" 1 1.0000000000000004 1.0000000000000004 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334779 0 ;
	setAttr ".rpt" -type "double3" 0 1.0186012589334781 1.0186012589334779 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 -4.2632564145606029e-016 0 ;
createNode transform -n "pCube17" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 0.31476509950216403 -0.96874736481513124 ;
	setAttr ".r" -type "double3" -72.000000000000028 0 0 ;
	setAttr ".s" -type "double3" 1 1.0000000000000004 1.0000000000000004 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334779 0 ;
	setAttr ".rpt" -type "double3" 0 0.70383615943131728 0.96874736481513124 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 -4.2632564145606029e-016 0 ;
createNode transform -n "pCube18" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 0.59871879796764815 -0.82406572896889985 ;
	setAttr ".r" -type "double3" -54.000000000000028 0 0 ;
	setAttr ".s" -type "double3" 1 1.0000000000000004 1.0000000000000004 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334779 0 ;
	setAttr ".rpt" -type "double3" 0 0.41988246096583304 0.82406572896889996 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 -4.2632564145606029e-016 0 ;
createNode transform -n "pCube19" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 0.82406572896890296 -0.59871879796764493 ;
	setAttr ".r" -type "double3" -36.000000000000014 0 0 ;
	setAttr ".s" -type "double3" 1 1.0000000000000004 1.0000000000000004 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334779 0 ;
	setAttr ".rpt" -type "double3" 0 0.19453552996457812 0.59871879796764516 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 -4.2632564145606029e-016 0 ;
createNode transform -n "pCube20" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 0.96874736481513424 -0.31476509950216069 ;
	setAttr ".r" -type "double3" -18.000000000000004 0 0 ;
	setAttr ".s" -type "double3" 1 1.0000000000000002 1.0000000000000002 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334776 0 ;
	setAttr ".rpt" -type "double3" 0 0.049853894118346731 0.31476509950216086 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".spt" -type "double3" 0 -2.8421709430404013e-016 0 ;
createNode transform -n "pCube1" -p "group1";
	setAttr ".t" -type "double3" 0.15107441110063841 1.0186012589334774 0 ;
	setAttr ".rp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
	setAttr ".sp" -type "double3" -0.15107441110063841 -1.0186012589334774 0 ;
createNode transform -n "pCylinder1" -p "group1";
	setAttr ".r" -type "double3" 0 0 90 ;
createNode mesh -n "pCylinderShape1" -p "pCylinder1";
	addAttr -ci true -sn "mso" -ln "miShadingSamplesOverride" -min 0 -max 1 -at "bool";
	addAttr -ci true -sn "msh" -ln "miShadingSamples" -min 0 -smx 8 -at "float";
	addAttr -ci true -sn "mdo" -ln "miMaxDisplaceOverride" -min 0 -max 1 -at "bool";
	addAttr -ci true -sn "mmd" -ln "miMaxDisplace" -min 0 -smx 1 -at "float";
	setAttr -k off ".v";
	setAttr -s 4 ".iog[0].og";
	setAttr ".vir" yes;
	setAttr ".vif" yes;
	setAttr ".uvst[0].uvsn" -type "string" "map1";
	setAttr ".cuvs" -type "string" "map1";
	setAttr ".dcc" -type "string" "Ambient+Diffuse";
	setAttr ".covm[0]"  0 1 1;
	setAttr ".cdvm[0]"  0 1 1;
	setAttr -s 480 ".pt";
	setAttr ".pt[576:961]" -type "float3" 9.536743e-009 0 0  -1.9073486e-008 0 
		-9.0949468e-015  -2.3841858e-008 0 2.2737367e-015  -1.4305114e-008 0 1.1920929e-009  
		9.536743e-009 0 -5.9604646e-009  -2.8610229e-008 0 -1.1920929e-009  9.536743e-009 
		0 4.7683715e-009  9.536743e-009 0 0  -2.8610229e-008 0 4.7683715e-009  0 0 -2.3841857e-009  
		-2.8610229e-008 0 -4.7683715e-009  0 0 -4.7683715e-009  0 0 1.9073486e-008  0 0 1.1920929e-008  
		1.9073486e-008 0 1.9073486e-008  -1.4305114e-008 0 -1.4305114e-008  1.9073486e-008 
		0 -9.536743e-009  1.1920929e-008 0 0  -4.7683715e-009 0 -2.8610229e-008  -4.7683715e-009 
		0 0  9.536743e-009 0 -1.9073486e-008  -7.1525572e-009 0 0  -2.3841857e-009 0 -9.536743e-009  
		0 0 9.536743e-009  0 0 -2.8610229e-008  -1.1920929e-009 0 -9.536743e-009  -4.5474734e-015 
		0 1.9073486e-008  0 0 -4.7683715e-009  1.1920929e-009 0 -1.9073486e-008  -1.1920929e-009 
		0 1.4305114e-008  9.536743e-009 0 9.536743e-009  -2.3841857e-009 0 1.4305114e-008  
		-1.9073486e-008 0 -9.536743e-009  -4.7683715e-009 0 0  -4.7683715e-009 0 -2.8610229e-008  
		0 0 9.536743e-009  -9.536743e-009 0 1.9073486e-008  -4.7683715e-009 0 9.536743e-009  
		2.8610229e-008 0 -2.8610229e-008  0 0 -1.4305114e-008  -1.9073486e-008 0 -5.7220458e-008  
		1.4305114e-008 0 0  0 0 -9.536743e-009  0 0 4.7683715e-009  -9.536743e-009 0 0  4.7683715e-009 
		0 -2.3841857e-009  -1.9073486e-008 0 0  -4.7683715e-009 0 0  1.9073486e-008 0 -1.1920929e-009  
		2.8610229e-008 0 -1.1920929e-009  9.536743e-009 0 2.2737367e-015  2.3841858e-008 
		0 -2.2737367e-015  1.9073486e-008 0 0  2.8610229e-008 0 5.9604643e-010  -3.8146972e-008 
		0 -9.536743e-009  -4.7683715e-009 0 2.3841857e-009  0 0 4.7683715e-009  -4.7683715e-009 
		0 2.3841857e-009  2.8610229e-008 0 1.4305114e-008  0 0 7.1525572e-009  1.9073486e-008 
		0 -2.8610229e-008  4.7683715e-009 0 -1.1920929e-008  -9.536743e-009 0 -1.9073486e-008  
		0 0 1.4305114e-008  0 0 9.536743e-009  0 0 0  -2.3841858e-008 0 2.8610229e-008  -1.4305114e-008 
		0 0  9.536743e-009 0 -9.536743e-009  -4.7683715e-009 0 0  4.7683715e-009 0 -1.9073486e-008  
		-2.3841857e-009 0 -1.4305114e-008  9.536743e-009 0 9.536743e-009  5.9604643e-010 
		0 -1.4305114e-008  -2.728484e-014 0 -9.536743e-009  -1.1368684e-014 0 4.7683715e-009  
		4.7683715e-009 0 9.536743e-009  1.1920929e-009 0 -1.4305114e-008  1.1920929e-008 
		0 -1.9073486e-008  4.7683715e-009 0 -1.4305114e-008  9.536743e-009 0 -9.536743e-009  
		2.3841857e-009 0 0  -4.7683715e-009 0 2.8610229e-008  -2.3841857e-009 0 0  3.8146972e-008 
		0 0  2.3841857e-009 0 0  9.536743e-009 0 -1.9073486e-008  1.9073486e-008 0 0  9.536743e-009 
		0 0  -4.7683715e-009 0 -4.7683715e-009  1.9073486e-008 0 9.536743e-009  0 0 7.1525572e-009  
		0 0 0  4.7683715e-009 0 -4.7683715e-009  -9.536743e-009 0 9.536743e-009  4.7683715e-009 
		0 2.3841857e-009  9.536743e-009 0 0  -9.536743e-009 0 9.536743e-009  4.7683715e-009 
		0 2.3841857e-009  -1.4305114e-008 0 1.1920929e-009  0 0 0  4.7683715e-009 0 -4.7683715e-009  
		1.9073486e-008 0 9.536743e-009  0 0 7.1525572e-009  9.536743e-009 0 0  -4.7683715e-009 
		0 -4.7683715e-009  9.536743e-009 0 -1.9073486e-008  1.9073486e-008 0 0  3.8146972e-008 
		0 0  2.3841857e-009 0 0  -4.7683715e-009 0 2.8610229e-008  -2.3841857e-009 0 0  9.536743e-009 
		0 -9.536743e-009  2.3841857e-009 0 0  1.1920929e-008 0 -1.9073486e-008  4.7683715e-009 
		0 -1.4305114e-008  4.7683715e-009 0 9.536743e-009  1.1920929e-009 0 -1.4305114e-008  
		-2.728484e-014 0 -9.536743e-009  -1.1368684e-014 0 4.7683715e-009  9.536743e-009 
		0 9.536743e-009  5.9604643e-010 0 -1.4305114e-008  4.7683715e-009 0 -1.9073486e-008  
		-2.3841857e-009 0 -1.4305114e-008  9.536743e-009 0 -9.536743e-009  -4.7683715e-009 
		0 0  -2.3841858e-008 0 2.8610229e-008  -1.4305114e-008 0 0  0 0 9.536743e-009  0 
		0 0  -9.536743e-009 0 -1.9073486e-008  0 0 1.4305114e-008  1.9073486e-008 0 -2.8610229e-008  
		4.7683715e-009 0 -1.1920929e-008  2.8610229e-008 0 1.4305114e-008  0 0 7.1525572e-009  
		0 0 4.7683715e-009  -4.7683715e-009 0 2.3841857e-009  -3.8146972e-008 0 -9.536743e-009  
		-4.7683715e-009 0 2.3841857e-009  1.9073486e-008 0 0  2.8610229e-008 0 5.9604643e-010  
		9.536743e-009 0 2.2737367e-015  2.3841858e-008 0 -2.2737367e-015  1.9073486e-008 
		0 -1.1920929e-009  2.8610229e-008 0 -1.1920929e-009  -1.9073486e-008 0 0  -4.7683715e-009 
		0 0  -9.536743e-009 0 0  4.7683715e-009 0 -2.3841857e-009  0 0 -9.536743e-009  0 
		0 4.7683715e-009  -1.9073486e-008 0 -5.7220458e-008  1.4305114e-008 0 0  2.8610229e-008 
		0 -2.8610229e-008  0 0 -1.4305114e-008  -9.536743e-009 0 1.9073486e-008  -4.7683715e-009 
		0 9.536743e-009  -4.7683715e-009 0 -2.8610229e-008  0 0 9.536743e-009  -1.9073486e-008 
		0 -9.536743e-009  -4.7683715e-009 0 0  9.536743e-009 0 9.536743e-009  -2.3841857e-009 
		0 1.4305114e-008  1.1920929e-009 0 -1.9073486e-008  -1.1920929e-009 0 1.4305114e-008  
		-4.5474734e-015 0 1.9073486e-008  0 0 -4.7683715e-009  0 0 -2.8610229e-008  -1.1920929e-009 
		0 -9.536743e-009  -2.3841857e-009 0 -9.536743e-009  0 0 9.536743e-009  9.536743e-009 
		0 -1.9073486e-008  -7.1525572e-009 0 0  -4.7683715e-009 0 -2.8610229e-008  -4.7683715e-009 
		0 0  1.9073486e-008 0 -9.536743e-009  1.1920929e-008 0 0  1.9073486e-008 0 1.9073486e-008  
		-1.4305114e-008 0 -1.4305114e-008  0 0 1.9073486e-008  0 0 1.1920929e-008  -2.8610229e-008 
		0 -4.7683715e-009  0 0 -4.7683715e-009  -2.8610229e-008 0 4.7683715e-009  0 0 -2.3841857e-009  
		9.536743e-009 0 4.7683715e-009  9.536743e-009 0 0  9.536743e-009 0 -5.9604646e-009  
		-2.8610229e-008 0 -1.1920929e-009  -1.9073486e-008 0 -9.0949468e-015  -2.3841858e-008 
		0 2.2737367e-015  -1.9073486e-008 2.3841857e-009 0  -1.9073486e-008 2.3841857e-009 
		0  -9.536743e-009 2.3841857e-009 4.5474734e-015  0 2.3841857e-009 0  5.7220458e-008 
		2.3841857e-009 2.3841857e-009  -9.536743e-009 2.3841857e-009 1.1920929e-009  -1.9073486e-008 
		2.3841857e-009 9.536743e-009  -9.536743e-009 2.3841857e-009 -2.3841857e-009  0 2.3841857e-009 
		0  9.536743e-009 2.3841857e-009 4.7683715e-009  0 2.3841857e-009 9.536743e-009  -1.9073486e-008 
		2.3841857e-009 4.7683715e-009  0 2.3841857e-009 -9.536743e-009  -1.9073486e-008 2.3841857e-009 
		-9.536743e-009  0 2.3841857e-009 0  0 2.3841857e-009 0  -9.536743e-009 2.3841857e-009 
		-3.8146972e-008  -9.536743e-009 2.3841857e-009 -1.9073486e-008  -9.536743e-009 2.3841857e-009 
		0  -4.7683715e-009 2.3841857e-009 -1.9073486e-008  0 2.3841857e-009 1.9073486e-008  
		-9.536743e-009 2.3841857e-009 9.536743e-009  4.7683715e-009 2.3841857e-009 -1.9073486e-008  
		-2.3841857e-009 2.3841857e-009 -9.536743e-009  -2.3841857e-009 2.3841857e-009 0  
		1.1920929e-009 2.3841857e-009 9.536743e-009  9.0949468e-015 2.3841857e-009 0  -4.5474734e-015 
		2.3841857e-009 -9.536743e-009  0 2.3841857e-009 0  0 2.3841857e-009 9.536743e-009  
		4.7683715e-009 2.3841857e-009 -1.9073486e-008  0 2.3841857e-009 -9.536743e-009  9.536743e-009 
		2.3841857e-009 0  0 2.3841857e-009 9.536743e-009  -9.536743e-009 2.3841857e-009 1.9073486e-008  
		-4.7683715e-009 2.3841857e-009 -9.536743e-009  0 2.3841857e-009 -1.9073486e-008  
		-4.7683715e-009 2.3841857e-009 -9.536743e-009  1.9073486e-008 2.3841857e-009 1.9073486e-008  
		0 2.3841857e-009 0  0 2.3841857e-009 -1.9073486e-008  0 2.3841857e-009 0  -3.8146972e-008 
		2.3841857e-009 9.536743e-009  1.9073486e-008 2.3841857e-009 0  0 2.3841857e-009 0  
		9.536743e-009 2.3841857e-009 -9.536743e-009  0 2.3841857e-009 9.536743e-009  0 2.3841857e-009 
		-2.3841857e-009  -5.7220458e-008 2.3841857e-009 2.3841857e-009  9.536743e-009 2.3841857e-009 
		0  1.9073486e-008 2.3841857e-009 -9.0949468e-015  9.536743e-009 2.3841857e-009 -2.2737367e-015  
		-5.7220458e-008 2.3841857e-009 4.7683715e-009  9.536743e-009 2.3841857e-009 2.3841857e-009  
		1.9073486e-008 2.3841857e-009 -4.7683715e-009  0 2.3841857e-009 0  0 2.3841857e-009 
		-1.9073486e-008  0 2.3841857e-009 -4.7683715e-009  1.9073486e-008 2.3841857e-009 
		0  1.9073486e-008 2.3841857e-009 4.7683715e-009  1.9073486e-008 2.3841857e-009 -9.536743e-009  
		9.536743e-009 2.3841857e-009 9.536743e-009  0 2.3841857e-009 0  0 2.3841857e-009 
		0  -9.536743e-009 2.3841857e-009 3.8146972e-008  -1.9073486e-008 2.3841857e-009 1.9073486e-008  
		1.9073486e-008 2.3841857e-009 1.9073486e-008  -4.7683715e-009 2.3841857e-009 1.9073486e-008  
		9.536743e-009 2.3841857e-009 -1.9073486e-008  0 2.3841857e-009 -9.536743e-009  1.4305114e-008 
		2.3841857e-009 1.9073486e-008  0 2.3841857e-009 9.536743e-009  -2.3841857e-009 2.3841857e-009 
		0  -2.3841857e-009 2.3841857e-009 -9.536743e-009  1.8189894e-014 2.3841857e-009 0  
		4.5474734e-015 2.3841857e-009 9.536743e-009  -2.3841857e-009 2.3841857e-009 0  -1.1920929e-009 
		2.3841857e-009 -9.536743e-009  4.7683715e-009 2.3841857e-009 1.9073486e-008  -2.3841857e-009 
		2.3841857e-009 9.536743e-009  9.536743e-009 2.3841857e-009 -1.9073486e-008  9.536743e-009 
		2.3841857e-009 -9.536743e-009  0 2.3841857e-009 0  4.7683715e-009 2.3841857e-009 
		1.9073486e-008  9.536743e-009 2.3841857e-009 0  -4.7683715e-009 2.3841857e-009 1.9073486e-008  
		0 2.3841857e-009 1.9073486e-008  0 2.3841857e-009 0  -1.9073486e-008 2.3841857e-009 
		0  0 2.3841857e-009 -4.7683715e-009  -3.8146972e-008 2.3841857e-009 9.536743e-009  
		0 2.3841857e-009 4.7683715e-009  3.8146972e-008 2.3841857e-009 0  0 2.3841857e-009 
		0  1.9073486e-008 2.3841857e-009 -4.7683715e-009  9.536743e-009 2.3841857e-009 2.3841857e-009  
		-1.9073486e-008 -2.3841857e-009 0  1.9073486e-008 -2.3841857e-009 -4.7683715e-009  
		9.536743e-009 -2.3841857e-009 2.3841857e-009  0 -2.3841857e-009 0  3.8146972e-008 
		-2.3841857e-009 0  0 -2.3841857e-009 0  -3.8146972e-008 -2.3841857e-009 9.536743e-009  
		0 -2.3841857e-009 4.7683715e-009  -1.9073486e-008 -2.3841857e-009 0  0 -2.3841857e-009 
		-4.7683715e-009  0 -2.3841857e-009 1.9073486e-008  0 -2.3841857e-009 0  9.536743e-009 
		-2.3841857e-009 0  -4.7683715e-009 -2.3841857e-009 1.9073486e-008  0 -2.3841857e-009 
		0  4.7683715e-009 -2.3841857e-009 1.9073486e-008  9.536743e-009 -2.3841857e-009 -1.9073486e-008  
		9.536743e-009 -2.3841857e-009 -9.536743e-009  4.7683715e-009 -2.3841857e-009 1.9073486e-008  
		-2.3841857e-009 -2.3841857e-009 9.536743e-009  -2.3841857e-009 -2.3841857e-009 0  
		-1.1920929e-009 -2.3841857e-009 -9.536743e-009  1.8189894e-014 -2.3841857e-009 0  
		4.5474734e-015 -2.3841857e-009 9.536743e-009  -2.3841857e-009 -2.3841857e-009 0  
		-2.3841857e-009 -2.3841857e-009 -9.536743e-009  1.4305114e-008 -2.3841857e-009 1.9073486e-008  
		0 -2.3841857e-009 9.536743e-009  9.536743e-009 -2.3841857e-009 -1.9073486e-008  0 
		-2.3841857e-009 -9.536743e-009  1.9073486e-008 -2.3841857e-009 1.9073486e-008  -4.7683715e-009 
		-2.3841857e-009 1.9073486e-008  -9.536743e-009 -2.3841857e-009 3.8146972e-008  -1.9073486e-008 
		-2.3841857e-009 1.9073486e-008  0 -2.3841857e-009 0  0 -2.3841857e-009 0  1.9073486e-008 
		-2.3841857e-009 -9.536743e-009  9.536743e-009 -2.3841857e-009 9.536743e-009  1.9073486e-008 
		-2.3841857e-009 0  1.9073486e-008 -2.3841857e-009 4.7683715e-009  0 -2.3841857e-009 
		-1.9073486e-008  0 -2.3841857e-009 -4.7683715e-009  1.9073486e-008 -2.3841857e-009 
		-4.7683715e-009  0 -2.3841857e-009 0  -5.7220458e-008 -2.3841857e-009 4.7683715e-009  
		9.536743e-009 -2.3841857e-009 2.3841857e-009  1.9073486e-008 -2.3841857e-009 -9.0949468e-015  
		9.536743e-009 -2.3841857e-009 -2.2737367e-015  -5.7220458e-008 -2.3841857e-009 2.3841857e-009  
		9.536743e-009 -2.3841857e-009 0  0 -2.3841857e-009 9.536743e-009  0 -2.3841857e-009 
		-2.3841857e-009  0 -2.3841857e-009 0  9.536743e-009 -2.3841857e-009 -9.536743e-009  
		-3.8146972e-008 -2.3841857e-009 9.536743e-009  1.9073486e-008 -2.3841857e-009 0  
		0 -2.3841857e-009 -1.9073486e-008  0 -2.3841857e-009 0  1.9073486e-008 -2.3841857e-009 
		1.9073486e-008  0 -2.3841857e-009 0  0 -2.3841857e-009 -1.9073486e-008  -4.7683715e-009 
		-2.3841857e-009 -9.536743e-009  -9.536743e-009 -2.3841857e-009 1.9073486e-008  -4.7683715e-009 
		-2.3841857e-009 -9.536743e-009  9.536743e-009 -2.3841857e-009 0  0 -2.3841857e-009 
		9.536743e-009  4.7683715e-009 -2.3841857e-009 -1.9073486e-008  0 -2.3841857e-009 
		-9.536743e-009  0 -2.3841857e-009 0  0 -2.3841857e-009 9.536743e-009  9.0949468e-015 
		-2.3841857e-009 0  -4.5474734e-015 -2.3841857e-009 -9.536743e-009  -2.3841857e-009 
		-2.3841857e-009 0  1.1920929e-009 -2.3841857e-009 9.536743e-009  4.7683715e-009 -2.3841857e-009 
		-1.9073486e-008  -2.3841857e-009 -2.3841857e-009 -9.536743e-009  0 -2.3841857e-009 
		1.9073486e-008  -9.536743e-009 -2.3841857e-009 9.536743e-009  -9.536743e-009 -2.3841857e-009 
		0  -4.7683715e-009 -2.3841857e-009 -1.9073486e-008  -9.536743e-009 -2.3841857e-009 
		-3.8146972e-008  -9.536743e-009 -2.3841857e-009 -1.9073486e-008  0 -2.3841857e-009 
		0  0 -2.3841857e-009 0  0 -2.3841857e-009 -9.536743e-009  -1.9073486e-008 -2.3841857e-009 
		-9.536743e-009  0 -2.3841857e-009 9.536743e-009  -1.9073486e-008 -2.3841857e-009 
		4.7683715e-009  0 -2.3841857e-009 0  9.536743e-009 -2.3841857e-009 4.7683715e-009  
		-1.9073486e-008 -2.3841857e-009 9.536743e-009  -9.536743e-009 -2.3841857e-009 -2.3841857e-009  
		5.7220458e-008 -2.3841857e-009 2.3841857e-009  -9.536743e-009 -2.3841857e-009 1.1920929e-009  
		-1.9073486e-008 -2.3841857e-009 0  -9.536743e-009 -2.3841857e-009 4.5474734e-015  
		0 -3.5762786e-009 0  0 -3.5762786e-009 0 ;
	setAttr ".pt[964]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[966]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[968]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[970]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[972]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[974]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[976]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[978]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[980]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[982]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[984]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[986]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[988]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[990]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[992]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[994]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[996]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[998]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1000]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1002]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1004]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1006]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1008]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1010]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1012]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1014]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1016]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1018]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1020]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1022]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1024]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1026]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1028]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1030]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1032]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1034]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1036]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1038]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1040]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1042]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1044]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1046]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1048]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1050]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1052]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1054]" -type "float3" 0 -3.5762786e-009 0 ;
	setAttr ".pt[1056:1057]" -type "float3" 0 -1.1920929e-009 0  0 -1.1920929e-009 
		0 ;
	setAttr ".pt[1060]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1062]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1064]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1066]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1068]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1070]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1072]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1074]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1076]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1078]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1080]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1082]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1084]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1086]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1088]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1090]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1092]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1094]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1096]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1098]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1100]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1102]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1104]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1106]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1108]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1110]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1112]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1114]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1116]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1118]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1120]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1122]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1124]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1126]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1128]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1130]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1132]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1134]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1136]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1138]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1140]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1142]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1144]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1146]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1148]" -type "float3" 0 -1.1920929e-009 0 ;
	setAttr ".pt[1150]" -type "float3" 0 -1.1920929e-009 0 ;
createNode transform -n "group2" -p "group3";
	setAttr ".r" -type "double3" 9 0 180 ;
parent -s -nc -r -add "|group3|group1|pCube2" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube1" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube3" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube4" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube5" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube6" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube7" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube8" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube9" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube10" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube11" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube12" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube13" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube14" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube15" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube16" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube17" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube18" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube19" ;
parent -s -nc -r -add "|group3|group1|pCube2|pCubeShape1" "|group3|group1|pCube20" ;
parent -s -nc -r -add "|group3|group1|pCube3" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube4" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube5" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube6" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube7" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube8" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube9" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube10" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube11" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube12" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube13" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube14" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube15" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube16" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube17" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube18" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube19" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube20" "group2" ;
parent -s -nc -r -add "|group3|group1|pCube1" "group2" ;
createNode lightLinker -s -n "lightLinker1";
	setAttr -s 8 ".lnk";
	setAttr -s 8 ".slnk";
createNode displayLayerManager -n "layerManager";
	setAttr ".cdl" 5;
	setAttr -s 6 ".dli[1:5]"  1 2 3 4 5;
	setAttr -s 4 ".dli";
createNode displayLayer -n "defaultLayer";
createNode renderLayerManager -n "renderLayerManager";
createNode renderLayer -n "defaultRenderLayer";
	setAttr ".g" yes;
createNode displayLayer -n "Tire";
	setAttr ".do" 1;
createNode polyCylinder -n "polyCylinder1";
	setAttr ".r" 1;
	setAttr ".h" 0.5;
	setAttr ".sa" 48;
	setAttr ".cuv" 3;
createNode polyExtrudeFace -n "polyExtrudeFace1";
	setAttr ".ics" -type "componentList" 1 "f[48:49]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -4.196167e-007 -3.4332277e-007 ;
	setAttr ".rs" 36438;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.25000000000000022 -1.0000008392333986 -1.0000010681152345 ;
	setAttr ".cbx" -type "double3" 0.25000000000000022 1 1.0000003814697265 ;
createNode polyExtrudeFace -n "polyExtrudeFace2";
	setAttr ".ics" -type "componentList" 1 "f[48:49]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -4.196167e-007 -3.4332277e-007 ;
	setAttr ".rs" 53380;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.25000000000000011 -0.46313648223876963 -0.46313644409179688 ;
	setAttr ".cbx" -type "double3" 0.25000000000000011 0.46313564300537119 0.4631357574462891 ;
createNode polyTweak -n "polyTweak1";
	setAttr ".uopa" yes;
	setAttr -s 96 ".tk[96:191]" -type "float3"  -53.6864357 -2.3841858e-007
		 -1.8431758e-005 -53.22719193 -2.3841858e-007 7.007463932 -53.2271347 -2.3841858e-007
		 -7.0075006485 -51.85712814 -2.3841858e-007 -13.89508438 -49.59978485 -2.3841858e-007
		 -20.54491806 -46.49381638 -2.3841858e-007 -26.8432312 -42.59230042 -2.3841858e-007
		 -32.68223953 -37.96204376 -2.3841858e-007 -37.96204376 -32.68223953 -2.3841858e-007
		 -42.59230804 -26.84323502 -2.3841858e-007 -46.49381638 -20.54491806 -2.3841858e-007
		 -49.59979248 -13.8950901 -2.3841858e-007 -51.8571434 -7.0075035095 -2.3841858e-007
		 -53.22713089 -1.8527764e-005 -2.3841858e-007 -53.6864624 7.0074648857 -2.3841858e-007
		 -53.2271347 13.89504623 -2.3841858e-007 -51.85714722 20.54488754 -2.3841858e-007
		 -49.59981537 26.84320259 -2.3841858e-007 -46.49383545 32.6822052 -2.3841858e-007
		 -42.59231567 37.96203232 -2.3841858e-007 -37.96206665 42.59227753 -2.3841858e-007
		 -32.68224716 46.49378586 -2.3841858e-007 -26.84323692 49.59978485 -2.3841858e-007
		 -20.54491997 51.8571167 -2.3841858e-007 -13.8950882 53.2271347 -2.3841858e-007 -7.0074968338
		 53.6864357 -2.3841858e-007 -1.0431868e-005 53.2271347 -2.3841858e-007 7.0074782372
		 51.85713196 -2.3841858e-007 13.89506721 49.59979248 -2.3841858e-007 20.54491615 46.49380112
		 -2.3841858e-007 26.84322357 42.5922966 -2.3841858e-007 32.68222809 37.96203995 -2.3841858e-007
		 37.96204376 32.68222046 -2.3841858e-007 42.59230804 26.84320259 -2.3841858e-007 46.49382401
		 20.54489136 -2.3841858e-007 49.59980774 13.89504528 -2.3841858e-007 51.85714722 7.0074577332
		 -2.3841858e-007 53.2271347 -2.9727615e-005 -2.3841858e-007 53.6864624 -7.007519722
		 -2.3841858e-007 53.2271347 -13.89510536 -2.3841858e-007 51.85714722 -20.54495049
		 -2.3841858e-007 49.59980774 -26.84325218 -2.3841858e-007 46.49381638 -32.68228149
		 -2.3841858e-007 42.59230042 -37.96210098 -2.3841858e-007 37.96204376 -42.59238815
		 -2.3841858e-007 32.68222809 -46.49387741 -2.3841858e-007 26.84320831 -49.59986877
		 -2.3841858e-007 20.5449028 -51.85713577 -2.3841858e-007 13.89505005 -53.22719193
		 2.3841858e-007 7.007463932 -51.85713577 2.3841858e-007 13.89505005 -49.59986877 2.3841858e-007
		 20.5449028 -46.49387741 2.3841858e-007 26.84320831 -42.59238815 2.3841858e-007 32.68222809
		 -37.96210098 2.3841858e-007 37.96204376 -32.68228149 2.3841858e-007 42.59230042 -26.84325218
		 2.3841858e-007 46.49381638 -20.54495049 2.3841858e-007 49.59980774 -13.89510536 2.3841858e-007
		 51.85714722 -7.007519722 2.3841858e-007 53.2271347 -2.9727615e-005 2.3841858e-007
		 53.6864624 7.0074577332 2.3841858e-007 53.2271347 13.89504528 2.3841858e-007 51.85714722
		 20.54489136 2.3841858e-007 49.59980774 26.84320259 2.3841858e-007 46.49382401 32.68222046
		 2.3841858e-007 42.59230804 37.96203995 2.3841858e-007 37.96204376 42.5922966 2.3841858e-007
		 32.68222809 46.49380112 2.3841858e-007 26.84322357 49.59979248 2.3841858e-007 20.54491615
		 51.85713196 2.3841858e-007 13.89506721 53.2271347 2.3841858e-007 7.0074782372 53.6864357
		 2.3841858e-007 -1.0431868e-005 53.2271347 2.3841858e-007 -7.0074968338 51.8571167
		 2.3841858e-007 -13.8950882 49.59978485 2.3841858e-007 -20.54491997 46.49378586 2.3841858e-007
		 -26.84323692 42.59227753 2.3841858e-007 -32.68224716 37.96203232 2.3841858e-007 -37.96206665
		 32.6822052 2.3841858e-007 -42.59231567 26.84320259 2.3841858e-007 -46.49383545 20.54488754
		 2.3841858e-007 -49.59981537 13.89504623 2.3841858e-007 -51.85714722 7.0074648857
		 2.3841858e-007 -53.2271347 -1.8527764e-005 2.3841858e-007 -53.6864624 -7.0075035095
		 2.3841858e-007 -53.22713089 -13.8950901 2.3841858e-007 -51.8571434 -20.54491806 2.3841858e-007
		 -49.59979248 -26.84323502 2.3841858e-007 -46.49381638 -32.68223953 2.3841858e-007
		 -42.59230804 -37.96204376 2.3841858e-007 -37.96204376 -42.59230042 2.3841858e-007
		 -32.68223953 -46.49381638 2.3841858e-007 -26.8432312 -49.59978485 2.3841858e-007
		 -20.54491806 -51.85712814 2.3841858e-007 -13.89508438 -53.2271347 2.3841858e-007
		 -7.0075006485 -53.6864357 2.3841858e-007 -1.8431758e-005;
createNode polyBevel -n "polyBevel1";
	setAttr ".ics" -type "componentList" 1 "e[0:95]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".oaf" yes;
	setAttr ".o" 0.0007;
	setAttr ".sg" 3;
	setAttr ".at" 180;
	setAttr ".fn" yes;
	setAttr ".mv" yes;
	setAttr ".mvt" 0.0001;
	setAttr ".sa" 30;
	setAttr ".ma" 180;
createNode polyTweak -n "polyTweak2";
	setAttr ".uopa" yes;
	setAttr -s 96 ".tk[192:287]" -type "float3"  -9.074816704 4.8985672 -3.1155898e-006
		 -8.99719524 4.8985672 1.18449831 -8.99718094 4.8985672 -1.18450403 -8.76559925 4.8985672
		 -2.34873748 -8.38403988 4.8985672 -3.47278476 -7.85902309 4.8985672 -4.53740835 -7.19954157
		 4.8985672 -5.5243988 -6.41686821 4.8985672 -6.41686821 -5.5243988 4.8985672 -7.19954157
		 -4.53741121 4.8985672 -7.85902309 -3.472785 4.8985672 -8.38403988 -2.34873748 4.8985672
		 -8.76559925 -1.18450403 4.8985672 -8.99719048 -3.1318166e-006 4.8985672 -9.074816704
		 1.18449867 4.8985672 -8.99718857 2.34873295 4.8985672 -8.76559925 3.47277832 4.8985672
		 -8.38403988 4.53740597 4.8985672 -7.859025 5.52439737 4.8985672 -7.19954491 6.4168644
		 4.8985672 -6.41686821 7.19953871 4.8985672 -5.52440071 7.85902309 4.8985672 -4.53741264
		 8.38403606 4.8985672 -3.4727869 8.76559925 4.8985672 -2.34873748 8.99718094 4.8985672
		 -1.18450356 9.074816704 4.8985672 -1.763337e-006 8.99718094 4.8985672 1.1845001 8.76559925
		 4.8985672 2.34873414 8.38403702 4.8985672 3.47277975 7.85902309 4.8985672 4.53740835
		 7.19953966 4.8985672 5.5243988 6.4168644 4.8985672 6.41686821 5.52439642 4.8985672
		 7.19954157 4.53740692 4.8985672 7.85902309 3.47277832 4.8985672 8.38403988 2.34873271
		 4.8985672 8.76559925 1.18449712 4.8985672 8.99718857 -5.024971e-006 4.8985672 9.074816704
		 -1.18450713 4.8985672 8.99718857 -2.34874415 4.8985672 8.76559925 -3.47278905 4.8985672
		 8.38403988 -4.53741837 4.8985672 7.85902309 -5.52440643 4.8985672 7.19954157 -6.41687536
		 4.8985672 6.41686583 -7.19954491 4.8985672 5.52439737 -7.85903406 4.8985672 4.53740835
		 -8.38404751 4.8985672 3.47277832 -8.76561546 4.8985672 2.34873366 -8.99719524 -4.8985672
		 1.18449831 -8.76561546 -4.8985672 2.34873366 -8.38404751 -4.8985672 3.47277832 -7.85903406
		 -4.8985672 4.53740835 -7.19954491 -4.8985672 5.52439737 -6.41687536 -4.8985672 6.41686583
		 -5.52440643 -4.8985672 7.19954157 -4.53741837 -4.8985672 7.85902309 -3.47278905 -4.8985672
		 8.38403988 -2.34874415 -4.8985672 8.76559925 -1.18450713 -4.8985672 8.99718857 -5.024971e-006
		 -4.8985672 9.074816704 1.18449712 -4.8985672 8.99718857 2.34873271 -4.8985672 8.76559925
		 3.47277832 -4.8985672 8.38403988 4.53740692 -4.8985672 7.85902309 5.52439642 -4.8985672
		 7.19954157 6.4168644 -4.8985672 6.41686821 7.19953966 -4.8985672 5.5243988 7.85902309
		 -4.8985672 4.53740835 8.38403702 -4.8985672 3.47277975 8.76559925 -4.8985672 2.34873414
		 8.99718094 -4.8985672 1.1845001 9.074816704 -4.8985672 -1.763337e-006 8.99718094
		 -4.8985672 -1.18450356 8.76559925 -4.8985672 -2.34873748 8.38403606 -4.8985672 -3.4727869
		 7.85902309 -4.8985672 -4.53741264 7.19953871 -4.8985672 -5.52440071 6.4168644 -4.8985672
		 -6.41686821 5.52439737 -4.8985672 -7.19954491 4.53740597 -4.8985672 -7.859025 3.47277832
		 -4.8985672 -8.38403988 2.34873295 -4.8985672 -8.76559925 1.18449867 -4.8985672 -8.99718857
		 -3.1318166e-006 -4.8985672 -9.074816704 -1.18450403 -4.8985672 -8.99719048 -2.34873748
		 -4.8985672 -8.76559925 -3.472785 -4.8985672 -8.38403988 -4.53741121 -4.8985672 -7.85902309
		 -5.5243988 -4.8985672 -7.19954157 -6.41686821 -4.8985672 -6.41686821 -7.19954157
		 -4.8985672 -5.5243988 -7.85902309 -4.8985672 -4.53740835 -8.38403988 -4.8985672 -3.47278476
		 -8.76559925 -4.8985672 -2.34873748 -8.99718094 -4.8985672 -1.18450403 -9.074816704
		 -4.8985672 -3.1155898e-006;
createNode script -n "uiConfigurationScriptNode";
	setAttr ".b" -type "string" (
		"// Maya Mel UI Configuration File.\n//\n//  This script is machine generated.  Edit at your own risk.\n//\n//\n\nglobal string $gMainPane;\nif (`paneLayout -exists $gMainPane`) {\n\n\tglobal int $gUseScenePanelConfig;\n\tint    $useSceneConfig = $gUseScenePanelConfig;\n\tint    $menusOkayInPanels = `optionVar -q allowMenusInPanels`;\tint    $nVisPanes = `paneLayout -q -nvp $gMainPane`;\n\tint    $nPanes = 0;\n\tstring $editorName;\n\tstring $panelName;\n\tstring $itemFilterName;\n\tstring $panelConfig;\n\n\t//\n\t//  get current state of the UI\n\t//\n\tsceneUIReplacement -update $gMainPane;\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Top View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"top\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n"
		+ "                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n"
		+ "                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n"
		+ "                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Top View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"top\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n"
		+ "            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n"
		+ "            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n"
		+ "            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Side View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"side\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n"
		+ "                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n"
		+ "                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n"
		+ "                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Side View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"side\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n"
		+ "            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n"
		+ "            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Front View\")) `;\n\tif (\"\" == $panelName) {\n"
		+ "\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"front\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"wireframe\" \n                -activeOnly 0\n                -ignorePanZoom 0\n                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n"
		+ "                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n"
		+ "                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Front View\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"front\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"wireframe\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n"
		+ "            -rendererName \"base_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n"
		+ "            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"modelPanel\" (localizedPanelLabel(\"Persp View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `modelPanel -unParent -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            modelEditor -e \n                -camera \"persp\" \n                -useInteractiveMode 0\n                -displayLights \"default\" \n                -displayAppearance \"smoothShaded\" \n                -activeOnly 0\n                -ignorePanZoom 0\n"
		+ "                -wireframeOnShaded 0\n                -headsUpDisplay 1\n                -selectionHiliteDisplay 1\n                -useDefaultMaterial 0\n                -bufferMode \"double\" \n                -twoSidedLighting 1\n                -backfaceCulling 0\n                -xray 0\n                -jointXray 0\n                -activeComponentsXray 0\n                -displayTextures 0\n                -smoothWireframe 0\n                -lineWidth 1\n                -textureAnisotropic 0\n                -textureHilight 1\n                -textureSampling 2\n                -textureDisplay \"modulate\" \n                -textureMaxSize 8192\n                -fogging 0\n                -fogSource \"fragment\" \n                -fogMode \"linear\" \n                -fogStart 0\n                -fogEnd 100\n                -fogDensity 0.1\n                -fogColor 0.5 0.5 0.5 1 \n                -maxConstantTransparency 1\n                -rendererName \"base_OpenGL_Renderer\" \n                -colorResolution 256 256 \n                -bumpResolution 512 512 \n"
		+ "                -textureCompression 0\n                -transparencyAlgorithm \"frontAndBackCull\" \n                -transpInShadows 0\n                -cullingOverride \"none\" \n                -lowQualityLighting 0\n                -maximumNumHardwareLights 1\n                -occlusionCulling 0\n                -shadingModel 0\n                -useBaseRenderer 0\n                -useReducedRenderer 0\n                -smallObjectCulling 0\n                -smallObjectThreshold -1 \n                -interactiveDisableShadows 0\n                -interactiveBackFaceCull 0\n                -sortTransparent 1\n                -nurbsCurves 1\n                -nurbsSurfaces 1\n                -polymeshes 1\n                -subdivSurfaces 1\n                -planes 1\n                -lights 1\n                -cameras 1\n                -controlVertices 1\n                -hulls 1\n                -grid 1\n                -joints 1\n                -ikHandles 1\n                -deformers 1\n                -dynamics 1\n                -fluids 1\n"
		+ "                -hairSystems 1\n                -follicles 1\n                -nCloths 1\n                -nParticles 1\n                -nRigids 1\n                -dynamicConstraints 1\n                -locators 1\n                -manipulators 1\n                -dimensions 1\n                -handles 1\n                -pivots 1\n                -textures 1\n                -strokes 1\n                -motionTrails 1\n                -shadows 0\n                $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tmodelPanel -edit -l (localizedPanelLabel(\"Persp View\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        modelEditor -e \n            -camera \"persp\" \n            -useInteractiveMode 0\n            -displayLights \"default\" \n            -displayAppearance \"smoothShaded\" \n            -activeOnly 0\n            -ignorePanZoom 0\n            -wireframeOnShaded 0\n            -headsUpDisplay 1\n            -selectionHiliteDisplay 1\n            -useDefaultMaterial 0\n"
		+ "            -bufferMode \"double\" \n            -twoSidedLighting 1\n            -backfaceCulling 0\n            -xray 0\n            -jointXray 0\n            -activeComponentsXray 0\n            -displayTextures 0\n            -smoothWireframe 0\n            -lineWidth 1\n            -textureAnisotropic 0\n            -textureHilight 1\n            -textureSampling 2\n            -textureDisplay \"modulate\" \n            -textureMaxSize 8192\n            -fogging 0\n            -fogSource \"fragment\" \n            -fogMode \"linear\" \n            -fogStart 0\n            -fogEnd 100\n            -fogDensity 0.1\n            -fogColor 0.5 0.5 0.5 1 \n            -maxConstantTransparency 1\n            -rendererName \"base_OpenGL_Renderer\" \n            -colorResolution 256 256 \n            -bumpResolution 512 512 \n            -textureCompression 0\n            -transparencyAlgorithm \"frontAndBackCull\" \n            -transpInShadows 0\n            -cullingOverride \"none\" \n            -lowQualityLighting 0\n            -maximumNumHardwareLights 1\n"
		+ "            -occlusionCulling 0\n            -shadingModel 0\n            -useBaseRenderer 0\n            -useReducedRenderer 0\n            -smallObjectCulling 0\n            -smallObjectThreshold -1 \n            -interactiveDisableShadows 0\n            -interactiveBackFaceCull 0\n            -sortTransparent 1\n            -nurbsCurves 1\n            -nurbsSurfaces 1\n            -polymeshes 1\n            -subdivSurfaces 1\n            -planes 1\n            -lights 1\n            -cameras 1\n            -controlVertices 1\n            -hulls 1\n            -grid 1\n            -joints 1\n            -ikHandles 1\n            -deformers 1\n            -dynamics 1\n            -fluids 1\n            -hairSystems 1\n            -follicles 1\n            -nCloths 1\n            -nParticles 1\n            -nRigids 1\n            -dynamicConstraints 1\n            -locators 1\n            -manipulators 1\n            -dimensions 1\n            -handles 1\n            -pivots 1\n            -textures 1\n            -strokes 1\n            -motionTrails 1\n"
		+ "            -shadows 0\n            $editorName;\nmodelEditor -e -viewSelected 0 $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"outlinerPanel\" (localizedPanelLabel(\"Outliner\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `outlinerPanel -unParent -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels `;\n\t\t\t$editorName = $panelName;\n            outlinerEditor -e \n                -showShapes 0\n                -showAttributes 0\n                -showConnected 0\n                -showAnimCurvesOnly 0\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 1\n                -showAssets 1\n                -showContainedOnly 1\n                -showPublishedAsConnected 0\n                -showContainerContents 1\n                -ignoreDagHierarchy 0\n                -expandConnections 0\n"
		+ "                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 0\n                -highlightActive 1\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"defaultSetFilter\" \n                -showSetMembers 1\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n"
		+ "                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\toutlinerPanel -edit -l (localizedPanelLabel(\"Outliner\")) -mbv $menusOkayInPanels  $panelName;\n\t\t$editorName = $panelName;\n        outlinerEditor -e \n            -showShapes 0\n            -showAttributes 0\n            -showConnected 0\n            -showAnimCurvesOnly 0\n            -showMuteInfo 0\n            -organizeByLayer 1\n            -showAnimLayerWeight 1\n            -autoExpandLayers 1\n            -autoExpand 0\n            -showDagOnly 1\n            -showAssets 1\n            -showContainedOnly 1\n            -showPublishedAsConnected 0\n            -showContainerContents 1\n            -ignoreDagHierarchy 0\n            -expandConnections 0\n            -showUpstreamCurves 1\n            -showUnitlessCurves 1\n            -showCompounds 1\n            -showLeafs 1\n            -showNumericAttrsOnly 0\n            -highlightActive 1\n"
		+ "            -autoSelectNewObjects 0\n            -doNotSelectNewObjects 0\n            -dropIsParent 1\n            -transmitFilters 0\n            -setFilter \"defaultSetFilter\" \n            -showSetMembers 1\n            -allowMultiSelection 1\n            -alwaysToggleSelect 0\n            -directSelect 0\n            -displayMode \"DAG\" \n            -expandObjects 0\n            -setsIgnoreFilters 1\n            -containersIgnoreFilters 0\n            -editAttrName 0\n            -showAttrValues 0\n            -highlightSecondary 0\n            -showUVAttrsOnly 0\n            -showTextureNodesOnly 0\n            -attrAlphaOrder \"default\" \n            -animLayerFilterOptions \"allAffecting\" \n            -sortOrder \"none\" \n            -longNames 0\n            -niceNames 1\n            -showNamespace 1\n            -showPinIcons 0\n            -mapMotionTrails 0\n            $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"graphEditor\" (localizedPanelLabel(\"Graph Editor\")) `;\n"
		+ "\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"graphEditor\" -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n"
		+ "                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n"
		+ "                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Graph Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n"
		+ "            outlinerEditor -e \n                -showShapes 1\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 1\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 1\n                -showCompounds 0\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 1\n                -doNotSelectNewObjects 0\n                -dropIsParent 1\n                -transmitFilters 1\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n"
		+ "                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 1\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"GraphEd\");\n            animCurveEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 1\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n"
		+ "                -snapValue \"none\" \n                -showResults \"off\" \n                -showBufferCurves \"off\" \n                -smoothness \"fine\" \n                -resultSamples 1\n                -resultScreenSamples 0\n                -resultUpdate \"delayed\" \n                -showUpstreamCurves 1\n                -stackedCurves 0\n                -stackedCurvesMin -1\n                -stackedCurvesMax 1\n                -stackedCurvesSpace 0.2\n                -displayNormalized 0\n                -preSelectionHighlight 0\n                -constrainDrag 0\n                -classicMode 1\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dopeSheetPanel\" (localizedPanelLabel(\"Dope Sheet\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dopeSheetPanel\" -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n"
		+ "                -showShapes 1\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n"
		+ "                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n"
		+ "                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dope Sheet\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"OutlineEd\");\n            outlinerEditor -e \n                -showShapes 1\n                -showAttributes 1\n                -showConnected 1\n                -showAnimCurvesOnly 1\n                -showMuteInfo 0\n                -organizeByLayer 1\n                -showAnimLayerWeight 1\n                -autoExpandLayers 1\n                -autoExpand 0\n                -showDagOnly 0\n                -showAssets 1\n                -showContainedOnly 0\n                -showPublishedAsConnected 0\n                -showContainerContents 0\n                -ignoreDagHierarchy 0\n"
		+ "                -expandConnections 1\n                -showUpstreamCurves 1\n                -showUnitlessCurves 0\n                -showCompounds 1\n                -showLeafs 1\n                -showNumericAttrsOnly 1\n                -highlightActive 0\n                -autoSelectNewObjects 0\n                -doNotSelectNewObjects 1\n                -dropIsParent 1\n                -transmitFilters 0\n                -setFilter \"0\" \n                -showSetMembers 0\n                -allowMultiSelection 1\n                -alwaysToggleSelect 0\n                -directSelect 0\n                -displayMode \"DAG\" \n                -expandObjects 0\n                -setsIgnoreFilters 1\n                -containersIgnoreFilters 0\n                -editAttrName 0\n                -showAttrValues 0\n                -highlightSecondary 0\n                -showUVAttrsOnly 0\n                -showTextureNodesOnly 0\n                -attrAlphaOrder \"default\" \n                -animLayerFilterOptions \"allAffecting\" \n                -sortOrder \"none\" \n"
		+ "                -longNames 0\n                -niceNames 1\n                -showNamespace 1\n                -showPinIcons 0\n                -mapMotionTrails 1\n                $editorName;\n\n\t\t\t$editorName = ($panelName+\"DopeSheetEd\");\n            dopeSheetEditor -e \n                -displayKeys 1\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"integer\" \n                -snapValue \"none\" \n                -outliner \"dopeSheetPanel1OutlineEd\" \n                -showSummary 1\n                -showScene 0\n                -hierarchyBelow 0\n                -showTicks 1\n                -selectionWindow 0 0 0 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"clipEditorPanel\" (localizedPanelLabel(\"Trax Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n"
		+ "\t\t\t$panelName = `scriptedPanel -unParent  -type \"clipEditorPanel\" -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Trax Editor\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = clipEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n"
		+ "                -snapValue \"none\" \n                -manageSequencer 0 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"sequenceEditorPanel\" (localizedPanelLabel(\"Camera Sequencer\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"sequenceEditorPanel\" -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Camera Sequencer\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\t\t$editorName = sequenceEditorNameFromPanel($panelName);\n            clipEditor -e \n                -displayKeys 0\n                -displayTangents 0\n                -displayActiveKeys 0\n                -displayActiveKeyTangents 0\n                -displayInfinities 0\n                -autoFit 0\n                -snapTime \"none\" \n                -snapValue \"none\" \n                -manageSequencer 1 \n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph Hierarchy\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n"
		+ "                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph Hierarchy\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n"
		+ "            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 0\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -opaqueContainers 0\n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperShadePanel\" (localizedPanelLabel(\"Hypershade\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperShadePanel\" -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypershade\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"visorPanel\" (localizedPanelLabel(\"Visor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"visorPanel\" -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Visor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"createNodePanel\" (localizedPanelLabel(\"Create Node\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"createNodePanel\" -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Create Node\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"polyTexturePlacementPanel\" (localizedPanelLabel(\"UV Texture Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"polyTexturePlacementPanel\" -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"UV Texture Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n"
		+ "\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"renderWindowPanel\" (localizedPanelLabel(\"Render View\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"renderWindowPanel\" -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Render View\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextPanel \"blendShapePanel\" (localizedPanelLabel(\"Blend Shape\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\tblendShapePanel -unParent -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels ;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tblendShapePanel -edit -l (localizedPanelLabel(\"Blend Shape\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n"
		+ "\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynRelEdPanel\" (localizedPanelLabel(\"Dynamic Relationships\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynRelEdPanel\" -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Dynamic Relationships\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"relationshipPanel\" (localizedPanelLabel(\"Relationship Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"relationshipPanel\" -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Relationship Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"referenceEditorPanel\" (localizedPanelLabel(\"Reference Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"referenceEditorPanel\" -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Reference Editor\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"componentEditorPanel\" (localizedPanelLabel(\"Component Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"componentEditorPanel\" -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Component Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"dynPaintScriptedPanelType\" (localizedPanelLabel(\"Paint Effects\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"dynPaintScriptedPanelType\" -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Paint Effects\")) -mbv $menusOkayInPanels  $panelName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"scriptEditorPanel\" (localizedPanelLabel(\"Script Editor\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"scriptEditorPanel\" -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels `;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Script Editor\")) -mbv $menusOkayInPanels  $panelName;\n"
		+ "\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\t$panelName = `sceneUIReplacement -getNextScriptedPanel \"hyperGraphPanel\" (localizedPanelLabel(\"Hypergraph\")) `;\n\tif (\"\" == $panelName) {\n\t\tif ($useSceneConfig) {\n\t\t\t$panelName = `scriptedPanel -unParent  -type \"hyperGraphPanel\" -l (localizedPanelLabel(\"Hypergraph\")) -mbv $menusOkayInPanels `;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 1\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -currentNode \"pCylinder1\" \n                -opaqueContainers 0\n                -dropTargetNode \"group1\" \n                -dropNode \"pCylinder1\" \n"
		+ "                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\t}\n\t} else {\n\t\t$label = `panel -q -label $panelName`;\n\t\tscriptedPanel -edit -l (localizedPanelLabel(\"Hypergraph\")) -mbv $menusOkayInPanels  $panelName;\n\n\t\t\t$editorName = ($panelName+\"HyperGraphEd\");\n            hyperGraph -e \n                -graphLayoutStyle \"hierarchicalLayout\" \n                -orientation \"horiz\" \n                -mergeConnections 1\n                -zoom 1\n                -animateTransition 0\n                -showRelationships 1\n                -showShapes 0\n                -showDeformers 0\n                -showExpressions 0\n"
		+ "                -showConstraints 0\n                -showUnderworld 0\n                -showInvisible 0\n                -transitionFrames 1\n                -currentNode \"pCylinder1\" \n                -opaqueContainers 0\n                -dropTargetNode \"group1\" \n                -dropNode \"pCylinder1\" \n                -freeform 0\n                -imagePosition 0 0 \n                -imageScale 1\n                -imageEnabled 0\n                -graphType \"DAG\" \n                -heatMapDisplay 0\n                -updateSelection 1\n                -updateNodeAdded 1\n                -useDrawOverrideColor 0\n                -limitGraphTraversal -1\n                -range 0 0 \n                -iconSize \"smallIcons\" \n                -showCachedConnections 0\n                $editorName;\n\t\tif (!$useSceneConfig) {\n\t\t\tpanel -e -l $label $panelName;\n\t\t}\n\t}\n\n\n\tif ($useSceneConfig) {\n        string $configName = `getPanel -cwl (localizedPanelLabel(\"Current Layout\"))`;\n        if (\"\" != $configName) {\n\t\t\tpanelConfiguration -edit -label (localizedPanelLabel(\"Current Layout\")) \n"
		+ "\t\t\t\t-defaultImage \"\"\n\t\t\t\t-image \"\"\n\t\t\t\t-sc false\n\t\t\t\t-configString \"global string $gMainPane; paneLayout -e -cn \\\"single\\\" -ps 1 100 100 $gMainPane;\"\n\t\t\t\t-removeAllPanels\n\t\t\t\t-ap false\n\t\t\t\t\t(localizedPanelLabel(\"Persp View\")) \n\t\t\t\t\t\"modelPanel\"\n"
		+ "\t\t\t\t\t\"$panelName = `modelPanel -unParent -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels `;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 8192\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t\t\"modelPanel -edit -l (localizedPanelLabel(\\\"Persp View\\\")) -mbv $menusOkayInPanels  $panelName;\\n$editorName = $panelName;\\nmodelEditor -e \\n    -cam `findStartUpCamera persp` \\n    -useInteractiveMode 0\\n    -displayLights \\\"default\\\" \\n    -displayAppearance \\\"smoothShaded\\\" \\n    -activeOnly 0\\n    -ignorePanZoom 0\\n    -wireframeOnShaded 0\\n    -headsUpDisplay 1\\n    -selectionHiliteDisplay 1\\n    -useDefaultMaterial 0\\n    -bufferMode \\\"double\\\" \\n    -twoSidedLighting 1\\n    -backfaceCulling 0\\n    -xray 0\\n    -jointXray 0\\n    -activeComponentsXray 0\\n    -displayTextures 0\\n    -smoothWireframe 0\\n    -lineWidth 1\\n    -textureAnisotropic 0\\n    -textureHilight 1\\n    -textureSampling 2\\n    -textureDisplay \\\"modulate\\\" \\n    -textureMaxSize 8192\\n    -fogging 0\\n    -fogSource \\\"fragment\\\" \\n    -fogMode \\\"linear\\\" \\n    -fogStart 0\\n    -fogEnd 100\\n    -fogDensity 0.1\\n    -fogColor 0.5 0.5 0.5 1 \\n    -maxConstantTransparency 1\\n    -rendererName \\\"base_OpenGL_Renderer\\\" \\n    -colorResolution 256 256 \\n    -bumpResolution 512 512 \\n    -textureCompression 0\\n    -transparencyAlgorithm \\\"frontAndBackCull\\\" \\n    -transpInShadows 0\\n    -cullingOverride \\\"none\\\" \\n    -lowQualityLighting 0\\n    -maximumNumHardwareLights 1\\n    -occlusionCulling 0\\n    -shadingModel 0\\n    -useBaseRenderer 0\\n    -useReducedRenderer 0\\n    -smallObjectCulling 0\\n    -smallObjectThreshold -1 \\n    -interactiveDisableShadows 0\\n    -interactiveBackFaceCull 0\\n    -sortTransparent 1\\n    -nurbsCurves 1\\n    -nurbsSurfaces 1\\n    -polymeshes 1\\n    -subdivSurfaces 1\\n    -planes 1\\n    -lights 1\\n    -cameras 1\\n    -controlVertices 1\\n    -hulls 1\\n    -grid 1\\n    -joints 1\\n    -ikHandles 1\\n    -deformers 1\\n    -dynamics 1\\n    -fluids 1\\n    -hairSystems 1\\n    -follicles 1\\n    -nCloths 1\\n    -nParticles 1\\n    -nRigids 1\\n    -dynamicConstraints 1\\n    -locators 1\\n    -manipulators 1\\n    -dimensions 1\\n    -handles 1\\n    -pivots 1\\n    -textures 1\\n    -strokes 1\\n    -motionTrails 1\\n    -shadows 0\\n    $editorName;\\nmodelEditor -e -viewSelected 0 $editorName\"\n"
		+ "\t\t\t\t$configName;\n\n            setNamedPanelLayout (localizedPanelLabel(\"Current Layout\"));\n        }\n\n        panelHistory -e -clear mainPanelHistory;\n        setFocus `paneLayout -q -p1 $gMainPane`;\n        sceneUIReplacement -deleteRemaining;\n        sceneUIReplacement -clear;\n\t}\n\n\ngrid -spacing 1 -size 10 -divisions 10 -displayAxes yes -displayGridLines yes -displayDivisionLines yes -displayPerspectiveLabels no -displayOrthographicLabels no -displayAxesBold yes -perspectiveLabelPosition axis -orthographicLabelPosition edge;\nviewManip -drawCompass 0 -compassAngle 0 -frontParameters \"\" -homeParameters \"\" -selectionLockParameters \"\";\n}\n");
	setAttr ".st" 3;
createNode script -n "sceneConfigurationScriptNode";
	setAttr ".b" -type "string" "playbackOptions -min 1 -max 24 -ast 1 -aet 48 ";
	setAttr ".st" 6;
createNode displayLayer -n "Trend";
	setAttr ".do" 2;
createNode polyCube -n "Trend1";
	setAttr ".w" 0.3;
	setAttr ".h" 0.044096544026543671;
	setAttr ".d" 0.11415269042970692;
	setAttr ".sw" 8;
	setAttr ".cuv" 4;
createNode displayLayer -n "Trend_Inner";
	setAttr ".do" 3;
createNode polyExtrudeFace -n "polyExtrudeFace3";
	setAttr ".ics" -type "componentList" 1 "f[50:145]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -4.196167e-007 -3.4332277e-007 ;
	setAttr ".rs" 52751;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.25000000000000022 -0.98250259399414064 -0.98250282287597657 ;
	setAttr ".cbx" -type "double3" 0.25000000000000022 0.9825017547607422 0.98250213623046878 ;
createNode polyExtrudeFace -n "polyExtrudeFace4";
	setAttr ".ics" -type "componentList" 1 "f[50:145]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -4.196167e-007 -3.4332277e-007 ;
	setAttr ".rs" 50138;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.25000000000000022 -0.98250259399414064 -0.98250282287597657 ;
	setAttr ".cbx" -type "double3" 0.25000000000000022 0.9825017547607422 0.98250213623046878 ;
createNode polyExtrudeFace -n "polyExtrudeFace5";
	setAttr ".ics" -type "componentList" 1 "f[50:145]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -4.196167e-007 -3.4332277e-007 ;
	setAttr ".rs" 52450;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.25000000000000022 -0.98250259399414064 -0.98250282287597657 ;
	setAttr ".cbx" -type "double3" 0.25000000000000022 0.9825017547607422 0.98250213623046878 ;
createNode polyMoveFace -n "polyMoveFace1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 1 "f[50:145]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -4.196167e-007 -3.4332277e-007 ;
	setAttr ".rs" 49735;
	setAttr ".ls" -type "double3" 0.73461919173146151 0.73461919173146151 1 ;
createNode polyExtrudeFace -n "polyExtrudeFace6";
	setAttr ".ics" -type "componentList" 1 "f[50:145]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.8146973e-007 -3.4332277e-007 ;
	setAttr ".rs" 42563;
	setAttr ".ls" -type "double3" 0.59788352193459593 0.59788352193459593 1 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.25000000000000022 -0.9165639495849609 -0.91656417846679694 ;
	setAttr ".cbx" -type "double3" 0.25000000000000022 0.91656318664550784 0.91656349182128904 ;
createNode polyExtrudeFace -n "polyExtrudeFace7";
	setAttr ".ics" -type "componentList" 1 "f[50:145]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.4332277e-007 -3.4332277e-007 ;
	setAttr ".rs" 43578;
	setAttr ".ls" -type "double3" 0.37964768440614444 0.37964768440614444 1 ;
	setAttr ".c[0]"  0 1 1;
	setAttr ".cbn" -type "double3" -0.25000000000000017 -0.84260475158691406 -0.84260498046874999 ;
	setAttr ".cbx" -type "double3" 0.25000000000000017 0.84260406494140627 0.8426042938232422 ;
createNode polyMoveVertex -n "polyMoveVertex1";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 94 "vtx[960:961]" "vtx[964]" "vtx[966]" "vtx[968]" "vtx[970]" "vtx[972]" "vtx[974]" "vtx[976]" "vtx[978]" "vtx[980]" "vtx[982]" "vtx[984]" "vtx[986]" "vtx[988]" "vtx[990]" "vtx[992]" "vtx[994]" "vtx[996]" "vtx[998]" "vtx[1000]" "vtx[1002]" "vtx[1004]" "vtx[1006]" "vtx[1008]" "vtx[1010]" "vtx[1012]" "vtx[1014]" "vtx[1016]" "vtx[1018]" "vtx[1020]" "vtx[1022]" "vtx[1024]" "vtx[1026]" "vtx[1028]" "vtx[1030]" "vtx[1032]" "vtx[1034]" "vtx[1036]" "vtx[1038]" "vtx[1040]" "vtx[1042]" "vtx[1044]" "vtx[1046]" "vtx[1048]" "vtx[1050]" "vtx[1052]" "vtx[1054]" "vtx[1056:1057]" "vtx[1060]" "vtx[1062]" "vtx[1064]" "vtx[1066]" "vtx[1068]" "vtx[1070]" "vtx[1072]" "vtx[1074]" "vtx[1076]" "vtx[1078]" "vtx[1080]" "vtx[1082]" "vtx[1084]" "vtx[1086]" "vtx[1088]" "vtx[1090]" "vtx[1092]" "vtx[1094]" "vtx[1096]" "vtx[1098]" "vtx[1100]" "vtx[1102]" "vtx[1104]" "vtx[1106]" "vtx[1108]" "vtx[1110]" "vtx[1112]" "vtx[1114]" "vtx[1116]" "vtx[1118]" "vtx[1120]" "vtx[1122]" "vtx[1124]" "vtx[1126]" "vtx[1128]" "vtx[1130]" "vtx[1132]" "vtx[1134]" "vtx[1136]" "vtx[1138]" "vtx[1140]" "vtx[1142]" "vtx[1144]" "vtx[1146]" "vtx[1148]" "vtx[1150]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.8146973e-007 -3.4332277e-007 ;
	setAttr ".rs" 46888;
	setAttr ".lt" -type "double3" -1.4210854715202004e-016 -8.9227550979418812e-018 
		0.022349303317325955 ;
createNode polyMoveVertex -n "polyMoveVertex2";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 93 "vtx[1152:1153]" "vtx[1156]" "vtx[1158]" "vtx[1160]" "vtx[1162]" "vtx[1164]" "vtx[1166]" "vtx[1168]" "vtx[1170]" "vtx[1172]" "vtx[1174]" "vtx[1176]" "vtx[1178]" "vtx[1180]" "vtx[1182]" "vtx[1184]" "vtx[1186]" "vtx[1188]" "vtx[1190]" "vtx[1192]" "vtx[1194]" "vtx[1196]" "vtx[1198]" "vtx[1200]" "vtx[1202]" "vtx[1204]" "vtx[1206]" "vtx[1208]" "vtx[1210]" "vtx[1212]" "vtx[1214]" "vtx[1216]" "vtx[1218]" "vtx[1220]" "vtx[1222]" "vtx[1224]" "vtx[1226]" "vtx[1228]" "vtx[1230]" "vtx[1232]" "vtx[1234]" "vtx[1236]" "vtx[1238]" "vtx[1240]" "vtx[1242]" "vtx[1244]" "vtx[1246]" "vtx[1248:1249]" "vtx[1252]" "vtx[1254]" "vtx[1256]" "vtx[1258]" "vtx[1260]" "vtx[1262]" "vtx[1264]" "vtx[1266]" "vtx[1268]" "vtx[1270]" "vtx[1272]" "vtx[1274]" "vtx[1276]" "vtx[1278]" "vtx[1280]" "vtx[1282]" "vtx[1284]" "vtx[1286]" "vtx[1288]" "vtx[1290]" "vtx[1292]" "vtx[1294]" "vtx[1296]" "vtx[1298]" "vtx[1300]" "vtx[1302]" "vtx[1304]" "vtx[1306]" "vtx[1308]" "vtx[1310]" "vtx[1312]" "vtx[1314]" "vtx[1316]" "vtx[1318]" "vtx[1320]" "vtx[1322]" "vtx[1324]" "vtx[1326]" "vtx[1328]" "vtx[1330]" "vtx[1332]" "vtx[1334]" "vtx[1336]" "vtx[1340]" "vtx[1342]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.4332277e-007 -3.4332277e-007 ;
	setAttr ".rs" 52588;
createNode polyMoveVertex -n "polyMoveVertex3";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 94 "vtx[1152:1153]" "vtx[1156]" "vtx[1158]" "vtx[1160]" "vtx[1162]" "vtx[1164]" "vtx[1166]" "vtx[1168]" "vtx[1170]" "vtx[1172]" "vtx[1174]" "vtx[1176]" "vtx[1178]" "vtx[1180]" "vtx[1182]" "vtx[1184]" "vtx[1186]" "vtx[1188]" "vtx[1190]" "vtx[1192]" "vtx[1194]" "vtx[1196]" "vtx[1198]" "vtx[1200]" "vtx[1202]" "vtx[1204]" "vtx[1206]" "vtx[1208]" "vtx[1210]" "vtx[1212]" "vtx[1214]" "vtx[1216]" "vtx[1218]" "vtx[1220]" "vtx[1222]" "vtx[1224]" "vtx[1226]" "vtx[1228]" "vtx[1230]" "vtx[1232]" "vtx[1234]" "vtx[1236]" "vtx[1238]" "vtx[1240]" "vtx[1242]" "vtx[1244]" "vtx[1246]" "vtx[1248:1249]" "vtx[1252]" "vtx[1254]" "vtx[1256]" "vtx[1258]" "vtx[1260]" "vtx[1262]" "vtx[1264]" "vtx[1266]" "vtx[1268]" "vtx[1270]" "vtx[1272]" "vtx[1274]" "vtx[1276]" "vtx[1278]" "vtx[1280]" "vtx[1282]" "vtx[1284]" "vtx[1286]" "vtx[1288]" "vtx[1290]" "vtx[1292]" "vtx[1294]" "vtx[1296]" "vtx[1298]" "vtx[1300]" "vtx[1302]" "vtx[1304]" "vtx[1306]" "vtx[1308]" "vtx[1310]" "vtx[1312]" "vtx[1314]" "vtx[1316]" "vtx[1318]" "vtx[1320]" "vtx[1322]" "vtx[1324]" "vtx[1326]" "vtx[1328]" "vtx[1330]" "vtx[1332]" "vtx[1334]" "vtx[1336]" "vtx[1338]" "vtx[1340]" "vtx[1342]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.4332277e-007 -3.4332277e-007 ;
	setAttr ".rs" 40895;
	setAttr ".lt" -type "double3" -1.8676900304104295e-016 7.0082828429463007e-018 0.025386556229577271 ;
createNode polyMoveVertex -n "polyMoveVertex4";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 94 "vtx[1344:1345]" "vtx[1348]" "vtx[1350]" "vtx[1352]" "vtx[1354]" "vtx[1356]" "vtx[1358]" "vtx[1360]" "vtx[1362]" "vtx[1364]" "vtx[1366]" "vtx[1368]" "vtx[1370]" "vtx[1372]" "vtx[1374]" "vtx[1376]" "vtx[1378]" "vtx[1380]" "vtx[1382]" "vtx[1384]" "vtx[1386]" "vtx[1388]" "vtx[1390]" "vtx[1392]" "vtx[1394]" "vtx[1396]" "vtx[1398]" "vtx[1400]" "vtx[1402]" "vtx[1404]" "vtx[1406]" "vtx[1408]" "vtx[1410]" "vtx[1412]" "vtx[1414]" "vtx[1416]" "vtx[1418]" "vtx[1420]" "vtx[1422]" "vtx[1424]" "vtx[1426]" "vtx[1428]" "vtx[1430]" "vtx[1432]" "vtx[1434]" "vtx[1436]" "vtx[1438]" "vtx[1440:1441]" "vtx[1444]" "vtx[1446]" "vtx[1448]" "vtx[1450]" "vtx[1452]" "vtx[1454]" "vtx[1456]" "vtx[1458]" "vtx[1460]" "vtx[1462]" "vtx[1464]" "vtx[1466]" "vtx[1468]" "vtx[1470]" "vtx[1472]" "vtx[1474]" "vtx[1476]" "vtx[1478]" "vtx[1480]" "vtx[1482]" "vtx[1484]" "vtx[1486]" "vtx[1488]" "vtx[1490]" "vtx[1492]" "vtx[1494]" "vtx[1496]" "vtx[1498]" "vtx[1500]" "vtx[1502]" "vtx[1504]" "vtx[1506]" "vtx[1508]" "vtx[1510]" "vtx[1512]" "vtx[1514]" "vtx[1516]" "vtx[1518]" "vtx[1520]" "vtx[1522]" "vtx[1524]" "vtx[1526]" "vtx[1528]" "vtx[1530]" "vtx[1532]" "vtx[1534]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.0517577e-007 -3.8146973e-007 ;
	setAttr ".rs" 48340;
	setAttr ".lt" -type "double3" 6.1894933622852479e-016 -1.4432899320127036e-017 0.02897999003058634 ;
createNode polyMoveVertex -n "polyMoveVertex5";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 94 "vtx[1346:1347]" "vtx[1349]" "vtx[1351]" "vtx[1353]" "vtx[1355]" "vtx[1357]" "vtx[1359]" "vtx[1361]" "vtx[1363]" "vtx[1365]" "vtx[1367]" "vtx[1369]" "vtx[1371]" "vtx[1373]" "vtx[1375]" "vtx[1377]" "vtx[1379]" "vtx[1381]" "vtx[1383]" "vtx[1385]" "vtx[1387]" "vtx[1389]" "vtx[1391]" "vtx[1393]" "vtx[1395]" "vtx[1397]" "vtx[1399]" "vtx[1401]" "vtx[1403]" "vtx[1405]" "vtx[1407]" "vtx[1409]" "vtx[1411]" "vtx[1413]" "vtx[1415]" "vtx[1417]" "vtx[1419]" "vtx[1421]" "vtx[1423]" "vtx[1425]" "vtx[1427]" "vtx[1429]" "vtx[1431]" "vtx[1433]" "vtx[1435]" "vtx[1437]" "vtx[1439]" "vtx[1442:1443]" "vtx[1445]" "vtx[1447]" "vtx[1449]" "vtx[1451]" "vtx[1453]" "vtx[1455]" "vtx[1457]" "vtx[1459]" "vtx[1461]" "vtx[1463]" "vtx[1465]" "vtx[1467]" "vtx[1469]" "vtx[1471]" "vtx[1473]" "vtx[1475]" "vtx[1477]" "vtx[1479]" "vtx[1481]" "vtx[1483]" "vtx[1485]" "vtx[1487]" "vtx[1489]" "vtx[1491]" "vtx[1493]" "vtx[1495]" "vtx[1497]" "vtx[1499]" "vtx[1501]" "vtx[1503]" "vtx[1505]" "vtx[1507]" "vtx[1509]" "vtx[1511]" "vtx[1513]" "vtx[1515]" "vtx[1517]" "vtx[1519]" "vtx[1521]" "vtx[1523]" "vtx[1525]" "vtx[1527]" "vtx[1529]" "vtx[1531]" "vtx[1533]" "vtx[1535]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.0517577e-007 -3.4332277e-007 ;
	setAttr ".rs" 52141;
	setAttr ".lt" -type "double3" -1.5266463879688161e-016 3.4908980613428293e-019 0.019867760459345081 ;
createNode polyMoveVertex -n "polyMoveVertex6";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 94 "vtx[1154:1155]" "vtx[1157]" "vtx[1159]" "vtx[1161]" "vtx[1163]" "vtx[1165]" "vtx[1167]" "vtx[1169]" "vtx[1171]" "vtx[1173]" "vtx[1175]" "vtx[1177]" "vtx[1179]" "vtx[1181]" "vtx[1183]" "vtx[1185]" "vtx[1187]" "vtx[1189]" "vtx[1191]" "vtx[1193]" "vtx[1195]" "vtx[1197]" "vtx[1199]" "vtx[1201]" "vtx[1203]" "vtx[1205]" "vtx[1207]" "vtx[1209]" "vtx[1211]" "vtx[1213]" "vtx[1215]" "vtx[1217]" "vtx[1219]" "vtx[1221]" "vtx[1223]" "vtx[1225]" "vtx[1227]" "vtx[1229]" "vtx[1231]" "vtx[1233]" "vtx[1235]" "vtx[1237]" "vtx[1239]" "vtx[1241]" "vtx[1243]" "vtx[1245]" "vtx[1247]" "vtx[1250:1251]" "vtx[1253]" "vtx[1255]" "vtx[1257]" "vtx[1259]" "vtx[1261]" "vtx[1263]" "vtx[1265]" "vtx[1267]" "vtx[1269]" "vtx[1271]" "vtx[1273]" "vtx[1275]" "vtx[1277]" "vtx[1279]" "vtx[1281]" "vtx[1283]" "vtx[1285]" "vtx[1287]" "vtx[1289]" "vtx[1291]" "vtx[1293]" "vtx[1295]" "vtx[1297]" "vtx[1299]" "vtx[1301]" "vtx[1303]" "vtx[1305]" "vtx[1307]" "vtx[1309]" "vtx[1311]" "vtx[1313]" "vtx[1315]" "vtx[1317]" "vtx[1319]" "vtx[1321]" "vtx[1323]" "vtx[1325]" "vtx[1327]" "vtx[1329]" "vtx[1331]" "vtx[1333]" "vtx[1335]" "vtx[1337]" "vtx[1339]" "vtx[1341]" "vtx[1343]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.4332277e-007 -3.4332277e-007 ;
	setAttr ".rs" 57271;
	setAttr ".lt" -type "double3" 1.9180035655580244e-016 2.5443408603685167e-018 0.0110164297609904 ;
createNode polyMoveVertex -n "polyMoveVertex7";
	setAttr ".uopa" yes;
	setAttr ".ics" -type "componentList" 94 "vtx[962:963]" "vtx[965]" "vtx[967]" "vtx[969]" "vtx[971]" "vtx[973]" "vtx[975]" "vtx[977]" "vtx[979]" "vtx[981]" "vtx[983]" "vtx[985]" "vtx[987]" "vtx[989]" "vtx[991]" "vtx[993]" "vtx[995]" "vtx[997]" "vtx[999]" "vtx[1001]" "vtx[1003]" "vtx[1005]" "vtx[1007]" "vtx[1009]" "vtx[1011]" "vtx[1013]" "vtx[1015]" "vtx[1017]" "vtx[1019]" "vtx[1021]" "vtx[1023]" "vtx[1025]" "vtx[1027]" "vtx[1029]" "vtx[1031]" "vtx[1033]" "vtx[1035]" "vtx[1037]" "vtx[1039]" "vtx[1041]" "vtx[1043]" "vtx[1045]" "vtx[1047]" "vtx[1049]" "vtx[1051]" "vtx[1053]" "vtx[1055]" "vtx[1058:1059]" "vtx[1061]" "vtx[1063]" "vtx[1065]" "vtx[1067]" "vtx[1069]" "vtx[1071]" "vtx[1073]" "vtx[1075]" "vtx[1077]" "vtx[1079]" "vtx[1081]" "vtx[1083]" "vtx[1085]" "vtx[1087]" "vtx[1089]" "vtx[1091]" "vtx[1093]" "vtx[1095]" "vtx[1097]" "vtx[1099]" "vtx[1101]" "vtx[1103]" "vtx[1105]" "vtx[1107]" "vtx[1109]" "vtx[1111]" "vtx[1113]" "vtx[1115]" "vtx[1117]" "vtx[1119]" "vtx[1121]" "vtx[1123]" "vtx[1125]" "vtx[1127]" "vtx[1129]" "vtx[1131]" "vtx[1133]" "vtx[1135]" "vtx[1137]" "vtx[1139]" "vtx[1141]" "vtx[1143]" "vtx[1145]" "vtx[1147]" "vtx[1149]" "vtx[1151]";
	setAttr ".ix" -type "matrix" 2.2204460492503131e-016 1 0 0 -1 2.2204460492503131e-016 0 0
		 0 0 1 0 0 0 0 1;
	setAttr ".ws" yes;
	setAttr ".pvt" -type "float3" 0 -3.8146973e-007 -3.6239624e-007 ;
	setAttr ".rs" 40687;
	setAttr ".lt" -type "double3" 5.5866200369420224e-017 4.9907932993964063e-018 0.0087104022972645089 ;
createNode shadingMap -n "shadingMap1";
createNode shadingEngine -n "shadingMap1SG";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo1";
createNode surfaceShader -n "surfaceShader1";
createNode shadingEngine -n "surfaceShader1SG";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo2";
createNode useBackground -n "useBackground1";
createNode shadingEngine -n "useBackground1SG";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo3";
createNode lambert -n "lambert2";
createNode shadingEngine -n "lambert2SG";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo4";
createNode lambert -n "lambert3";
	setAttr ".c" -type "float3" 0.14633402 0.14633402 0.14633402 ;
createNode shadingEngine -n "lambert3SG";
	setAttr ".ihi" 0;
	setAttr -s 42 ".dsm";
	setAttr ".ro" yes;
	setAttr -s 2 ".gn";
createNode materialInfo -n "materialInfo5";
createNode shadingMap -n "shadingMap2";
createNode shadingEngine -n "shadingMap2SG";
	setAttr ".ihi" 0;
	setAttr ".ro" yes;
createNode materialInfo -n "materialInfo6";
createNode groupId -n "groupId1";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts1";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 2 "f[0:47]" "f[50:1489]";
	setAttr ".irc" -type "componentList" 1 "f[48:49]";
createNode groupId -n "groupId2";
	setAttr ".ihi" 0;
createNode groupId -n "groupId3";
	setAttr ".ihi" 0;
createNode groupParts -n "groupParts2";
	setAttr ".ihi" 0;
	setAttr ".ic" -type "componentList" 1 "f[48:49]";
select -ne :time1;
	setAttr ".o" 1;
	setAttr ".unw" 1;
select -ne :renderPartition;
	setAttr -s 8 ".st";
select -ne :initialShadingGroup;
	setAttr ".ro" yes;
select -ne :initialParticleSE;
	setAttr ".ro" yes;
select -ne :defaultShaderList1;
	setAttr -s 8 ".s";
select -ne :postProcessList1;
	setAttr -s 2 ".p";
select -ne :defaultRenderingList1;
select -ne :renderGlobalsList1;
select -ne :hardwareRenderGlobals;
	setAttr ".ctrs" 256;
	setAttr ".btrs" 512;
select -ne :defaultHardwareRenderGlobals;
	setAttr ".fn" -type "string" "im";
	setAttr ".res" -type "string" "ntsc_4d 646 485 1.333";
select -ne :ikSystem;
	setAttr -s 4 ".sol";
select -ne :strokeGlobals;
	setAttr ".cch" yes;
	setAttr ".ihi" 0;
connectAttr "Trend.di" "group1.do";
connectAttr "Trend1.out" "|group3|group1|pCube2|pCubeShape1.i";
connectAttr "Tire.di" "pCylinder1.do";
connectAttr "groupParts2.og" "pCylinderShape1.i";
connectAttr "groupId1.id" "pCylinderShape1.iog.og[0].gid";
connectAttr "lambert3SG.mwc" "pCylinderShape1.iog.og[0].gco";
connectAttr "groupId3.id" "pCylinderShape1.iog.og[1].gid";
connectAttr "shadingMap2SG.mwc" "pCylinderShape1.iog.og[1].gco";
connectAttr "groupId2.id" "pCylinderShape1.ciog.cog[0].cgid";
connectAttr "Trend_Inner.di" "group2.do";
relationship "link" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "shadingMap1SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "surfaceShader1SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "useBackground1SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "lambert3SG.message" ":defaultLightSet.message";
relationship "link" ":lightLinker1" "shadingMap2SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialShadingGroup.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" ":initialParticleSE.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "shadingMap1SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "surfaceShader1SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "useBackground1SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert2SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "lambert3SG.message" ":defaultLightSet.message";
relationship "shadowLink" ":lightLinker1" "shadingMap2SG.message" ":defaultLightSet.message";
connectAttr "layerManager.dli[0]" "defaultLayer.id";
connectAttr "renderLayerManager.rlmi[0]" "defaultRenderLayer.rlid";
connectAttr "layerManager.dli[1]" "Tire.id";
connectAttr "polyCylinder1.out" "polyExtrudeFace1.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace1.mp";
connectAttr "polyTweak1.out" "polyExtrudeFace2.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace2.mp";
connectAttr "polyExtrudeFace1.out" "polyTweak1.ip";
connectAttr "polyTweak2.out" "polyBevel1.ip";
connectAttr "pCylinderShape1.wm" "polyBevel1.mp";
connectAttr "polyExtrudeFace2.out" "polyTweak2.ip";
connectAttr "layerManager.dli[3]" "Trend.id";
connectAttr "layerManager.dli[4]" "Trend_Inner.id";
connectAttr "polyBevel1.out" "polyExtrudeFace3.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace3.mp";
connectAttr "polyExtrudeFace3.out" "polyExtrudeFace4.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace4.mp";
connectAttr "polyExtrudeFace4.out" "polyExtrudeFace5.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace5.mp";
connectAttr "polyExtrudeFace5.out" "polyMoveFace1.ip";
connectAttr "pCylinderShape1.wm" "polyMoveFace1.mp";
connectAttr "polyMoveFace1.out" "polyExtrudeFace6.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace6.mp";
connectAttr "polyExtrudeFace6.out" "polyExtrudeFace7.ip";
connectAttr "pCylinderShape1.wm" "polyExtrudeFace7.mp";
connectAttr "polyExtrudeFace7.out" "polyMoveVertex1.ip";
connectAttr "pCylinderShape1.wm" "polyMoveVertex1.mp";
connectAttr "polyMoveVertex1.out" "polyMoveVertex2.ip";
connectAttr "pCylinderShape1.wm" "polyMoveVertex2.mp";
connectAttr "polyMoveVertex2.out" "polyMoveVertex3.ip";
connectAttr "pCylinderShape1.wm" "polyMoveVertex3.mp";
connectAttr "polyMoveVertex3.out" "polyMoveVertex4.ip";
connectAttr "pCylinderShape1.wm" "polyMoveVertex4.mp";
connectAttr "polyMoveVertex4.out" "polyMoveVertex5.ip";
connectAttr "pCylinderShape1.wm" "polyMoveVertex5.mp";
connectAttr "polyMoveVertex5.out" "polyMoveVertex6.ip";
connectAttr "pCylinderShape1.wm" "polyMoveVertex6.mp";
connectAttr "polyMoveVertex6.out" "polyMoveVertex7.ip";
connectAttr "pCylinderShape1.wm" "polyMoveVertex7.mp";
connectAttr "shadingMap1.oc" "shadingMap1SG.ss";
connectAttr "shadingMap1SG.msg" "materialInfo1.sg";
connectAttr "shadingMap1.msg" "materialInfo1.m";
connectAttr "shadingMap1.msg" "materialInfo1.t" -na;
connectAttr "surfaceShader1.oc" "surfaceShader1SG.ss";
connectAttr "surfaceShader1SG.msg" "materialInfo2.sg";
connectAttr "surfaceShader1.msg" "materialInfo2.m";
connectAttr "surfaceShader1.msg" "materialInfo2.t" -na;
connectAttr "useBackground1.oc" "useBackground1SG.ss";
connectAttr "useBackground1SG.msg" "materialInfo3.sg";
connectAttr "useBackground1.msg" "materialInfo3.m";
connectAttr "useBackground1.msg" "materialInfo3.t" -na;
connectAttr "lambert2.oc" "lambert2SG.ss";
connectAttr "lambert2SG.msg" "materialInfo4.sg";
connectAttr "lambert2.msg" "materialInfo4.m";
connectAttr "lambert3.oc" "lambert3SG.ss";
connectAttr "|group3|group2|pCube1|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube20|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube19|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube18|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube17|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube16|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube15|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube14|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube13|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube12|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube11|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube10|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube9|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube8|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube7|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube6|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube5|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube4|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube3|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group2|pCube2|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube1|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube20|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube19|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube18|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube17|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube16|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube15|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube14|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube13|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube12|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube11|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube10|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube9|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube8|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube7|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube6|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube5|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube4|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube3|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "|group3|group1|pCube2|pCubeShape1.iog" "lambert3SG.dsm" -na;
connectAttr "pCylinderShape1.iog.og[0]" "lambert3SG.dsm" -na;
connectAttr "pCylinderShape1.ciog.cog[0]" "lambert3SG.dsm" -na;
connectAttr "groupId1.msg" "lambert3SG.gn" -na;
connectAttr "groupId2.msg" "lambert3SG.gn" -na;
connectAttr "lambert3SG.msg" "materialInfo5.sg";
connectAttr "lambert3.msg" "materialInfo5.m";
connectAttr "shadingMap2.oc" "shadingMap2SG.ss";
connectAttr "groupId3.msg" "shadingMap2SG.gn" -na;
connectAttr "pCylinderShape1.iog.og[1]" "shadingMap2SG.dsm" -na;
connectAttr "shadingMap2SG.msg" "materialInfo6.sg";
connectAttr "shadingMap2.msg" "materialInfo6.m";
connectAttr "shadingMap2.msg" "materialInfo6.t" -na;
connectAttr "polyMoveVertex7.out" "groupParts1.ig";
connectAttr "groupId1.id" "groupParts1.gi";
connectAttr "groupParts1.og" "groupParts2.ig";
connectAttr "groupId3.id" "groupParts2.gi";
connectAttr "shadingMap1SG.pa" ":renderPartition.st" -na;
connectAttr "surfaceShader1SG.pa" ":renderPartition.st" -na;
connectAttr "useBackground1SG.pa" ":renderPartition.st" -na;
connectAttr "lambert2SG.pa" ":renderPartition.st" -na;
connectAttr "lambert3SG.pa" ":renderPartition.st" -na;
connectAttr "shadingMap2SG.pa" ":renderPartition.st" -na;
connectAttr "shadingMap1.msg" ":defaultShaderList1.s" -na;
connectAttr "surfaceShader1.msg" ":defaultShaderList1.s" -na;
connectAttr "useBackground1.msg" ":defaultShaderList1.s" -na;
connectAttr "lambert2.msg" ":defaultShaderList1.s" -na;
connectAttr "lambert3.msg" ":defaultShaderList1.s" -na;
connectAttr "shadingMap2.msg" ":defaultShaderList1.s" -na;
connectAttr "defaultRenderLayer.msg" ":defaultRenderingList1.r" -na;
// End of Tire2.ma
