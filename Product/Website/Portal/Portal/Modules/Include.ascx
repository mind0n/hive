<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Include.ascx.cs" Inherits="Portal.Modules.Include" %>
	<link href="/Themes/Reset.css" rel="stylesheet" type="text/css" media="screen"/>
	<script type="text/javascript" src="/Js/three.js"></script>
	<script type="text/javascript" src="/Js/ColladaLoader.js"></script>
	<script type="text/javascript">
		/**
		 * Provides nextFrame/cancelRequestAnimation in a cross browser way.
		 * from paul irish + jerome etienne
		 * - http://paulirish.com/2011/nextFrame-for-smart-animating/
		 * - http://notes.jetienne.com/2011/05/18/cancelRequestAnimFrame-for-paul-irish-requestAnimFrame.html
		 */

		function nextFrame(callback) {
			var f = function () {
				return window.webkitnextFrame ||
					window.moznextFrame ||
					window.onextFrame ||
					window.msnextFrame ||
					function (/* function FrameRequestCallback */ callback, /* DOMElement Element */ element) {

						return window.setTimeout(callback, 1000 / 60);

					};
			}();
			f(callback);
		}
		function cancelnextFrame() {
			var f = function () {
				return window.webkitCancelnextFrame ||
					window.mozCancelnextFrame ||
					window.oCancelnextFrame ||
					window.msCancelnextFrame ||
					clearTimeout();
			}();
			f();
		}

		window.nextFrame = nextFrame;
		window.cancelnextFrame = cancelnextFrame;
	</script>
	<script src="/joy/joy.js"></script>
    <script src="/Joy/Controls/AdSwitcher.js"></script>
    <script src="/Joy/Controls/Coverer.js"></script>
    <script src="/Joy/Controls/EditableRect.js"></script>
    <script src="/Joy/Controls/FormEditor.js"></script>
    <script src="/Joy/Controls/Grid.js"></script>
    <script src="/Joy/Controls/ImgList.js"></script>
    <script src="/Joy/Controls/LabelControl.js"></script>
    <script src="/Joy/Controls/Menu.js"></script>
    <script src="/Joy/Controls/RowEditor.js"></script>
    <script src="/Joy/Controls/SimpleMenu.js"></script>
    <script src="/Joy/Controls/DropDown.js"></script>
    <script src="/Joy/Controls/VideoPlayer.js"></script>
    <script src="/joy/network/request.js"></script>
	<script src="/joy/Graphics/j3d.js"></script>  
	<script src="/joy/Graphics/j2d.js"></script>  
