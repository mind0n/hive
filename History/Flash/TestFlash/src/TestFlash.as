package
{
	import flash.display.Loader;
	import flash.display.MovieClip;
	import flash.display.Sprite;
	import flash.events.*;
	import flash.net.URLRequest;
	import flash.text.engine.TextElement;
	import flash.text.engine.TextLine;
	public class TestFlash extends Sprite
	{
		private var angle:Number = 0;
		private var length:Number = 100;
		private var speed:Number = 0.05;
		private var loader:Loader = new Loader();
		
//		[Embeded("a.swf")]
//		private var a:Class;
		
		public function TestFlash()
		{
			this.stage.frameRate=30;
			this.stage.color=0x000000;
			this.addEventListener(MouseEvent.CLICK, OnHit);
			this.addEventListener(Event.ENTER_FRAME, OnEnterFrame);
			loader.load(new URLRequest("Ani.swf"));
			addChild(loader);

//			var m:MovieClip = new a as MovieClip;
//			Object((m.getChildAt(0) as Loader).content).tt = "ok";
		}
		
		public function OnHit(event:Event):void{
		 	var mc:MovieClip = loader.content as MovieClip;
			var n:int = mc.continueEffect();
		}
		
		public function OnEnterFrame(event:Event):void{
			
			
//			this.graphics.clear();
//			this.graphics.lineStyle(1, 0xffffff);
//			this.graphics.drawCircle(this.stage.stageWidth/2, this.stage.stageHeight/2 + length * Math.sin(angle), 10);
//			angle+=speed;
			
		}
	}
}