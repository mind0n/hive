<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TopMenu.ascx.cs" Inherits="Site.Components.TopMenu" %>
<div class="topmenu">
	<div id="topmenu"></div>
	<div class="counter" id="counter"></div>
</div>
<script type="text/javascript" src="<%=TopMenuJsUrl %>">"></script>
<script type="text/javascript">

    var menuCfg = {
        displayMember: 'Caption',
        valueMember: 'CategoryId',
        parentMember: 'ParentId',
        url: 'content.aspx?cid=',
        menucfg:
        [
            { autoHide: false, className: 'toplevelmenu' },
            { autoHide: true, className: 'sublevelmenu' }
        ],
        itemcfg:
        [
            {
                style: { float: 'left' },
                onmouseover: function (evt) {
                    this.activate();
                }
            },
            { style: { float: 'none' } }
        ]
    };
    Joy.ready(function (completed) {
        topMenuInit(completed)
    });
</script>