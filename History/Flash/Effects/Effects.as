package 
{
	import flash.display.*;
	import flash.net.URLRequest;
	import flash.net.URLLoader;
	import flash.media.Sound;
	import flash.media.SoundChannel;
	import flash.events.*;
	import flash.text.*;
	import flash.system.*;


	public class Effects extends MovieClip
	{
		var offsetTimer:int = 0;
		var offsetDir:int = 1;
		var txt:TextField;
		var htxt:TextField;
		var LRCarray:Array=new Array();
		var sc:SoundChannel;
		var loader:Loader = new Loader();
		//var mcBBD4:McBiDeFour = new McBiDeFour();
		var clips:Object = new Object();
		public function Effects()
		{
			txt=new TextField();
			htxt=new TextField();
			System.useCodePage = true;
			clips.mcBBD4 = new McBiDeFour();
			txt.selectable = false;
			//txt.backgroundColor = 0xffffff;
			addChild(txt);
			txt.text = "ok";

			this.stage.frameRate = 30;
			LoadFinish();
			var sound:Sound=new Sound();
			sound.load(new URLRequest("Music/a.mp3"));
			sc = sound.play();
			stage.displayState = StageDisplayState.FULL_SCREEN;
			//fscommand("fullscreen", "true");
			stage.addEventListener(Event.ENTER_FRAME,SoundPlaying);
			stage.addEventListener(MouseEvent.CLICK, onClick);
		}
		function onClick(evt:Event)
		{
			trace("FullScreen");
			fscommand("fullscreen", "true");
		}
		function SoundPlaying(evt:Event):void
		{
			stage.scaleMode = StageScaleMode.SHOW_ALL;

			for (var i=1; i < LRCarray.length; i++)
			{
				var item:Object = LRCarray[i - 1];

				if (sc.position < LRCarray[i].timer && i > 0 && LRCarray[0].timer <= sc.position)
				{
					if (item.lyric.charAt(0) == "$")
					{
						if (! item.mcName)
						{
							item.mcName = item.lyric.substr(1,item.lyric.length - 1);
							this.clips[item.mcName].StartPlay(this.stage);
						}
					}
					else
					{
						item.playedFrames++;
						if (sc.position - item.timer < 3000)
						{
							txt.text = getLyric(i - 1);
							htxt.text = item.lyric;
						}
						else
						{
							txt.text = "";
							htxt.text = "";
						}
					}
					break;
				}
				txt.text = "";
				htxt.text = "";

			}
			htxt.x = this.width / 2;
			htxt.y = this.height / 2;
			htxt.autoSize = "center";

			var txtFormat:TextFormat = new TextFormat();
			txtFormat.font = "Arial";
			txtFormat.size = 44;
			txtFormat.color = 0xFFFFFF;
			txtFormat.bold = true;
			txtFormat.align = "left";
			txt.setTextFormat(txtFormat);

			htxt.setTextFormat(txtFormat);
			txt.wordWrap = true;
			txt.width = htxt.width;

			//txt.border = true;
			//htxt.border = true;

			var w:int = 550;
			var h:int = 400;
			if (txt.width > w)
			{

				txtFormat.align = "center";
				txt.setTextFormat(txtFormat);
				txt.width = 500;
				txt.height = htxt.height * 2;
				txt.x = w / 2 - txt.width / 2;
				txt.y = h / 2 - txt.height / 2;
			}
			else
			{
				txt.x = w / 2 - htxt.width / 2;
				txt.y = h / 2 - htxt.height / 2;
			}
		}
		function LoadFinish():void
		{
			var lyricData:LyricData = new LyricData();
			var list:String = lyricData.Data;

			var listarray:Array = list.split("\n");

			var reg:RegExp = /\[[0-5][0-9]:[0-5][0-9].[0-9][0-9]\]/g;
			for (var i=0; i < listarray.length; i++)
			{
				var info:String = listarray[i];
				var len:int = info.match(reg).length;
				var timeAry:Array = info.match(reg);
				var lyric:String = info.substr(len * 10);
				for (var k:int=0; k < timeAry.length; k++)
				{
					var obj:Object=new Object();
					var ctime:String = timeAry[k];
					var ntime:Number = Number(ctime.substr(1,2)) * 60 + Number(ctime.substr(4,5));
					obj.timer = ntime * 1000 + offsetTimer * offsetDir;
					obj.lyric = lyric;
					obj.playedFrames = 0;

					if (lyric == "++")
					{
						offsetTimer = obj.timer;
						offsetDir = 1;
					}
					else if (lyric == "+")
					{
						offsetTimer = ntime * 1000;
						offsetDir = 1;
					}
					else if (lyric.charAt(0) == "-")
					{
						offsetTimer = ntime * 1000;
						offsetDir = -1;
					}
					else
					{
						trace(obj.timer + "," + obj.lyric);
						LRCarray.push(obj);
					}
				}
			}
			LRCarray.sort(compare);
		}
		private function compare(paraA:Object,paraB:Object):int
		{
			if (paraA.timer > paraB.timer)
			{
				return 1;
			}
			if (paraA.timer < paraB.timer)
			{
				return -1;
			}
			return 0;
		}
		private function getLyric(pos:int):String
		{
			var len:int = calcWords(pos);
			var lyric:String;
			lyric = LRCarray[pos].lyric.substr(0,len);
			return lyric;
		}
		private function calcWords(pos:int):int
		{
			var rlt:int = 0;
			var item:Object = LRCarray[pos];
			var duration:Number = item.timer;
			if (pos < LRCarray.length)
			{
				duration = item.lyric.length * 100;//LRCarray[pos + 1].timer - duration;
			}
			else
			{
				duration = 1000;
			}
			rlt = Math.ceil(item.lyric.length * 1000 * item.playedFrames / this.stage.frameRate / duration);
			if (rlt > (LRCarray[pos].lyric as String).length)
			{
				rlt = (LRCarray[pos].lyric as String).length;
			}
			return rlt;
		}
		private function centerObject(obj:DisplayObject)
		{
			obj.x = this.stage.stageWidth / 2;
			obj.y = this.stage.stageHeight / 2;
		}
	}

}