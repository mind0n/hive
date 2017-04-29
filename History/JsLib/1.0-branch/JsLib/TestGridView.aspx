<%@ Page Language="C#" 
AutoEventWireup="true" 
CodeBehind="TestGridView.aspx.cs" 
Inherits="TestWebApp.TestGridView"
EnableEventValidation="false"
 %>

<%@ Register Assembly="FsWeb" Namespace="FsWeb.ClientLib" TagPrefix="cc1" %>
<%@ Register src="/FsWeb/Components/Pager.ascx" tagname="Pager" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<cc1:ScriptHeaders ID="ScriptHeaders1" runat="server" />
	<script type="text/javascript">
		pageConfig.data.beatNameDropDown = J.parseJson("<%=BeatNameDropDownData %>");
		var externConfig = new J.EditRowConfig();
		externConfig.hiddenBoxId = 'box';
		externConfig.hiddenBox = null;
		externConfig.behaviors.MakeEditor= function (cell, config) {
			var editEl = config.behaviors[cell.field].MakeEditor(cell, config);
			return editEl;
		};
		externConfig.behaviors.BeatName = {
			MakeEditor: function (cell, config) {
				var rowEl = cell.rowEl;
				var select = document.createElement('select');
				for (var i = 0; i < pageConfig.data.beatNameDropDown.length; i++) {
					var item = pageConfig.data.beatNameDropDown[i];
					var option = document.createElement('option');
					option.value = item.value;
					option.innerHTML = item.display;
					if (item.display == cell.innerHTML) {
						option.selected = true;
					}
					select.appendChild(option);
				}
				select.field = cell.field;
				select.onchange = function () {
					if (!config.data[rowEl.rowid]) {
						config.data[rowEl.rowid] = {};
					}
					config.data[rowEl.rowid][this.field] = this.value;
					if (rowEl.primaryCell) {
						config.data[rowEl.rowid][rowEl.primaryCell.field] = rowEl.primaryCell.innerHTML;
					}
					config.behaviors.UpdateData(config);
				};
				return select;
			}
		};

		function origin() {
			this.a = 'origin';
		}
		function test(content) {
			alert(content);
		}
		function test2(content) {
			alert(content + " :)");
		}
		function out(content) {
			var box = document.getElementById('box');
			box.value += content;
		}
	</script>
	<title></title>
	<style type="text/css">
		#box
		{
			height: 158px;
			width: 494px;
		}
	</style>
</head>
<body>
	<form id="form1" runat="server">
	<asp:GridView ID="gvMain" runat="server">
	</asp:GridView>
	<uc1:Pager ID="pager" runat="server" />
	<select runat="server" id="sel" onchange='alert("ch")'>
		<option>1</option>
		<option>2</option>
	</select>
	<textarea id='box'>

	</textarea>
	<div id="area" style='position: fixed; right: 0px; bottom: 0px; z-index: 100;'>
		<script>			//this is script</script>
		<!-- this is comment -->
		<input id="mx" type="text" value="x" />
		<input id="my" type="text" value="y" />
	</div>
	<div>
		{ o:'ok' }</div>
	<!------------------------------------------------------------------------------------------------------>
	<div class='a' style='display: none;'>
		<div class='b'>
			<span id='c' class='c'>ok</span>
			<div id='a' class='a'>
				<div id='b' class='b'>
					<div class='c'>
						SUCCESS!
					</div>
				</div>
			</div>
			<div class='g'>
				~~~~~~</div>
		</div>
		<div class='e'>
			<div class='b'>
				<div class='f'>
					<div class='c'>
						error</div>
				</div>
			</div>
		</div>
	</div>
	<!--================================================================================================-->
	<div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span1' class='c'>ok</span>
				<div id='Div1' class='a'>
					<div id='Div2' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span2' class='c'>ok</span>
				<div id='Div3' class='a'>
					<div id='Div4' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span3' class='c'>ok</span>
				<div id='Div5' class='a'>
					<div id='Div6' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span4' class='c'>ok</span>
				<div id='Div7' class='a'>
					<div id='Div8' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span5' class='c'>ok</span>
				<div id='Div9' class='a'>
					<div id='Div10' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span6' class='c'>ok</span>
				<div id='Div11' class='a'>
					<div id='Div12' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span7' class='c'>ok</span>
				<div id='Div13' class='a'>
					<div id='Div14' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span8' class='c'>ok</span>
				<div id='Div15' class='a'>
					<div id='Div16' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span9' class='c'>ok</span>
				<div id='Div17' class='a'>
					<div id='Div18' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span10' class='c'>ok</span>
				<div id='Div19' class='a'>
					<div id='Div20' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span11' class='c'>ok</span>
				<div id='Div21' class='a'>
					<div id='Div22' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span12' class='c'>ok</span>
				<div id='Div23' class='a'>
					<div id='Div24' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span13' class='c'>ok</span>
				<div id='Div25' class='a'>
					<div id='Div26' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span14' class='c'>ok</span>
				<div id='Div27' class='a'>
					<div id='Div28' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span15' class='c'>ok</span>
				<div id='Div29' class='a'>
					<div id='Div30' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span16' class='c'>ok</span>
				<div id='Div31' class='a'>
					<div id='Div32' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span17' class='c'>ok</span>
				<div id='Div33' class='a'>
					<div id='Div34' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span18' class='c'>ok</span>
				<div id='Div35' class='a'>
					<div id='Div36' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span19' class='c'>ok</span>
				<div id='Div37' class='a'>
					<div id='Div38' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span20' class='c'>ok</span>
				<div id='Div39' class='a'>
					<div id='Div40' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span21' class='c'>ok</span>
				<div id='Div41' class='a'>
					<div id='Div42' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span22' class='c'>ok</span>
				<div id='Div43' class='a'>
					<div id='Div44' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span23' class='c'>ok</span>
				<div id='Div45' class='a'>
					<div id='Div46' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span24' class='c'>ok</span>
				<div id='Div47' class='a'>
					<div id='Div48' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span25' class='c'>ok</span>
				<div id='Div49' class='a'>
					<div id='Div50' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span26' class='c'>ok</span>
				<div id='Div51' class='a'>
					<div id='Div52' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span27' class='c'>ok</span>
				<div id='Div53' class='a'>
					<div id='Div54' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span28' class='c'>ok</span>
				<div id='Div55' class='a'>
					<div id='Div56' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span29' class='c'>ok</span>
				<div id='Div57' class='a'>
					<div id='Div58' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span30' class='c'>ok</span>
				<div id='Div59' class='a'>
					<div id='Div60' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span31' class='c'>ok</span>
				<div id='Div61' class='a'>
					<div id='Div62' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span32' class='c'>ok</span>
				<div id='Div63' class='a'>
					<div id='Div64' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span33' class='c'>ok</span>
				<div id='Div65' class='a'>
					<div id='Div66' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span34' class='c'>ok</span>
				<div id='Div67' class='a'>
					<div id='Div68' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span35' class='c'>ok</span>
				<div id='Div69' class='a'>
					<div id='Div70' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span36' class='c'>ok</span>
				<div id='Div71' class='a'>
					<div id='Div72' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span37' class='c'>ok</span>
				<div id='Div73' class='a'>
					<div id='Div74' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span38' class='c'>ok</span>
				<div id='Div75' class='a'>
					<div id='Div76' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span39' class='c'>ok</span>
				<div id='Div77' class='a'>
					<div id='Div78' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span40' class='c'>ok</span>
				<div id='Div79' class='a'>
					<div id='Div80' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span41' class='c'>ok</span>
				<div id='Div81' class='a'>
					<div id='Div82' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span42' class='c'>ok</span>
				<div id='Div83' class='a'>
					<div id='Div84' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span43' class='c'>ok</span>
				<div id='Div85' class='a'>
					<div id='Div86' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span18' class='c'>ok</span>
				<div id='Div35' class='a'>
					<div id='Div36' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span19' class='c'>ok</span>
				<div id='Div37' class='a'>
					<div id='Div38' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span20' class='c'>ok</span>
				<div id='Div39' class='a'>
					<div id='Div40' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span21' class='c'>ok</span>
				<div id='Div41' class='a'>
					<div id='Div42' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span22' class='c'>ok</span>
				<div id='Div43' class='a'>
					<div id='Div44' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span23' class='c'>ok</span>
				<div id='Div45' class='a'>
					<div id='Div46' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span24' class='c'>ok</span>
				<div id='Div47' class='a'>
					<div id='Div48' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span25' class='c'>ok</span>
				<div id='Div49' class='a'>
					<div id='Div50' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
		<div class='a' style='display: none;'>
			<div class='b'>
				<span id='Span26' class='c'>ok</span>
				<div id='Div51' class='a'>
					<div id='Div52' class='b'>
						<div class='c'>
							SUCCESS!
						</div>
					</div>
				</div>
				<div class='g'>
					~~~~~~</div>
			</div>
			<div class='e'>
				<div class='b'>
					<div class='f'>
						<div class='c'>
							error</div>
					</div>
				</div>
			</div>
		</div>
	</div>
	</form>
	<script type="text/javascript">
		J.ScriptReady(function () {
			J.debuging = true;
			J.MouseController.MouseMoveHandler.Add(function (evt) {
				var x = J('#mx');
				var y = J('#my');
				x.value = evt.ax;
				y.value = evt.ay;
			});
			Tests();
		});
	</script>
</body>
</html>
