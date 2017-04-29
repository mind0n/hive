<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestViewer.aspx.cs" Inherits="Site.Tests.TestViewer" %>

<%@ Register Src="~/Joy/JoyEntry.ascx" TagPrefix="uc1" TagName="JoyEntry" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="x-ua-compatible" content="ie=7" />
    <title>Test Viewer</title>
	<uc1:JoyEntry runat="server" ID="JoyEntry" />
	<link href="../Joy/Theme/Default/Viewer.css" rel="stylesheet" />
	<style type="text/css">	  
		#grid1 {
			position: absolute;
			top: 0px;
			left: 0px;
			width: 500px;
			min-height: 200px;
			background: silver;
			border: solid 1px black;
			overflow: auto;
		}
		#grid2 {
			position: absolute;
			top: 0px;
			left: 500px;
			width: 500px;
			min-height: 200px;
			background: silver;
			border: solid 1px black;
			overflow: auto;
		}

	</style>
	<script type="text/javascript">
		var config = {
			styles: {
				cols: {
					id: { width: '24px', display:'none' },
					uname: { width: '100px' },
					upwd: { width: '100px' },
					utype: { width: '100px' },
					unote: { width: '200px' }
				},
				rows: {
					
				}
			},
			hovable: false
			,
			cols: [
				{ field: 'id', caption: 'id', type: 'int' }
				, { field: 'uname', caption: 'Username', type: 'string', rowtype: 'ViewerAnchorCell', $: { url:'testviewer.aspx?id=@id', field:'id' } }
				, { field: 'upwd', caption: 'Password', type: 'string' }
				, { field: 'utype', caption: 'User Type', type: 'int' }
				, { field: 'unote', caption: 'Description', type: 'string' }
			],
			hierarchy: [
				{
					name: 'Account Info',
					$: [
						{ col: 'uname' },
						{ col: 'upwd' }
					]
				},
				{ col: 'utype' },
				{ col: 'unote' }
			]
		};
		var data = {
			rows: [
				{id:'1', uname: 'admin', upwd: 'nothing', utype: 0, unote: 'This user represents a system administrator.' }
				, {id:'2', uname: 'temp', upwd: 'nothing' }
				, {id:'3', uname: 'guest', upwd: 'nothing' }
				, {id:'4', uname: 'user' }
			]
		};
		Joy(function () {
			//for (var i = 0; i < 5; i++) {
			//	var cell = Joy.make('ViewerDataCell');
			//	cell.init({
			//		className: 'cell',
			//		style: { width: '70px' }
			//	});
			//	cell.setData({ data: 'header_' + i });
			//	Joy('headrow').appendChild(cell);
			//}

			var hr = Joy.make('ViewerHeadRow');
			hr.init(config);
			hr.setData();
			Joy('headarea').appendChild(hr);
			
			//for (var i = 0; i < 5; i++) {
			//	var cell = Joy.make('ViewerDataCell');
			//	cell.init({
			//		className: 'cell',
			//		style: { width: '70px' }
			//	});
			//	cell.setData({ data: 'body_' + i });
			//	Joy('recordarea').appendChild(cell);
			//}
			for (var i = 0; i < data.rows.length; i++) {
				var row = Joy.make('ViewerBodyRow');
				row.init(hr);
				row.setData(data.rows[i]);
				Joy('bodyarea').appendChild(row);
			}

			var g = Joy.make('Viewer');
			g.init(config);
			g.setData(data);
			//Joy(g).addClass('solid');
			g.append('grid2');
			//Joy('grid2').appendChild(g);
		});

	</script>
</head>
<body>
    <form id="mainform" runat="server">
	   
	<div id="grid1">
		<div class="viewer">
			<div id="headarea" class="headarea">
		 
			</div>
			<div id="bodyarea" class="bodyarea">
			</div>		 
			<div id="footrow" class="footrow"></div>
		</div> 	 
	</div> 
	<div id="grid2">
		
	</div>
    </form>
</body>
</html>
