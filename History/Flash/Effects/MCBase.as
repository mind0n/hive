package 
{
	import flash.display.MovieClip;
	import flash.display.Stage;
	import flash.events.Event;

	public class MCBase extends MovieClip
	{
		public var isPlaying:Boolean = false;
		public function MCBase()
		{
			this.addEventListener(Event.EXIT_FRAME, onExitFrame);
		}

		public function StartPlay(stage:Stage)
		{
			if (!this.isPlaying)
			{
				this.isPlaying = true;
				if (!stage.contains(this))
				{
					stage.addChild(this);
				}
				this.x = stage.stageWidth / 2;
				this.y = stage.stageHeight / 2;
				this.gotoAndPlay(2);
			}
		}

		private function onExitFrame(evt:Event)
		{
			if (this.framesLoaded == this.currentFrame)
			{
				isPlaying = false;
				stop();
			}
		}
	}

}