J.MouseController = {
	x: 0
	, y: 0
	, target: null
	, delegate: null
	, MouseMoveHandler: new J.EventHandler()
	, MouseDownHandler: new J.EventHandler()
	, MouseUpHandler: new J.EventHandler()
	, OnMouseMove: function(evt) {
		J.GetEvent(evt, J.MouseController);
		J.MouseController.MouseMoveHandler.Invoke(J.MouseController);
	}
	, OnMouseDown: function(evt) {
		J.GetEvent(evt, J.MouseController);
		J.MouseController.MouseDownHandler.Invoke(J.MouseController);
	}
	, OnMouseUp: function(evt) {
		J.GetEvent(evt, J.MouseController);
		J.MouseController.MouseUpHandler.Invoke(J.MouseController);
	}
};
