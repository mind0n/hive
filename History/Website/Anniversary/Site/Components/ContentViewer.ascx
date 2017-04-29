<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentViewer.ascx.cs" Inherits="Site.Components.ContentViewer" %>
<div class="ascx_content_viewer">
<div class="title" runat="server" id="title">搜索结果</div>
<div class="subtitle" runat="server" id="subtitle"></div>
<div class="toolbtn" id="toolbararea"></div>
	<div class="mediacontainer" runat="server" id="mediacontainer" visible="false">
		<div class="videoarea" runat="server" id="itemvideo" visible="false">
			<embed height="400px" type=application/x-shockwave-flash width="100%" 
			src="<%=VideoSrc %>" 
			allowFullScreen="true" 
			align="middle" 
			type="application/x-shockwave-flash" />
		</div>
		<div class="iconarea" runat="server" id="itemicon" visible="false">
			<img src="<%=ImgSrc %>" />
		</div>
	</div>
<div class="details" runat="server" id="details">没有记录</div>
<div class="footer" runat="server" id="footer" visible="false">
</div>
</div>
<asp:PlaceHolder  runat="server" id="MessageToolBar" visible="false">
<script type="text/javascript">
	var formcfg = {
		target: 'services.aspx',
		title: '送祝福',
		rows:
		[
			{
				utext: { label: '姓名：', controlName: 'LabelControl', editor: 'input', className: 'utext' },
				upwd: { label: '密码：', controlName: 'LabelControl', editor: 'input', editortype: 'password', className: 'upwd' }
			},
			{ content: { label: '祝福：', controlName: 'LabelControl', editor: 'textarea', className: 'content'} },
			{ authcode: { label: '验证码：', controlName: 'LabelControl', editor: 'input', className: 'authcode', icon: 'authcode.aspx', iconcfg: { style: { cursor: 'pointer' }, onmousedown: function (evt) { this.src += '1'; } }, autoRefreshIcon: true} }
		]
	};
	var formdata = {
		rows:
		[
			{ utext: '学生', upwd: '2341', content: '祝福寄语' },
			{ utext: '学生2', upwd: '2342', content: '祝福寄语2' }
		]
	};
	var container = Joy('toolbararea');
	var messageToolButton = Joy.make({
		tagName: 'div',
		$:
		[
			{ tagName: 'span', $: '[' },
			{
				tagName: 'a', alias: 'link', href: '#', $: '我要送祝福', onmousedown: function (evt) {
					var fe = Joy.make('FormEditor');
					fe.init(formcfg);
					fe.onsubmit = function (form) {
						var r = Joy.makeRequest({ md: 'SaveMessage', ag: form.getData(), nf: 'field', vf: 'value' });
						Joy.request.send('services.aspx', 'xml=' + escape(Joy.obj2xml(r, 'JsRequest')),
							function (sender, c, s) {
								var msg;
								var rlt = Joy.json(c);
								if (!rlt.Methods[0].Error) {
									msg = rlt.Methods[0].ReturnValue;
									alert(msg);
									sender.hide();
								} else {
									msg = rlt.Methods[0].Error;
									sender.showMsg(msg);
								}
							},
							form
						);
					};
					fe.show();
				}
			}
			, { tagName: 'span', $: ']' }
		]
	});
	if (container){
		container.appendChild(messageToolButton);
	}
</script>
</asp:PlaceHolder>
<div class="item" runat="server" id="itemTemplate" visible="false">
	<div class="itemtitle" runat="server" id="itemTitle">
	</div>
	<div class="itembrief" runat="server" id="itemBrief">brief</div>
	<div class="itemfooter" >
		<span runat="server" id="labelAuthor">发布者：</span><span class="itemauthor" runat="server" id="itemAuthor"></span>
		<span runat="server" id="labelDate">日期：</span><span class="itemdate" runat="server" id="itemDatetime"></span>
	</div>
</div>

