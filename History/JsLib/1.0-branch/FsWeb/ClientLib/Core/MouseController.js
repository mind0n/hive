JLib_v1.MouseController = {
	x: 0
	, y: 0
	, target: null
	, delegate: null
	, MouseMoveHandler: new JLib_v1.EventHandler()
	, MouseDownHandler: new JLib_v1.EventHandler()
	, MouseUpHandler: new JLib_v1.EventHandler()
	, OnMouseMove: function(evt) {
		JLib_v1.GetEvent(evt, JLib_v1.MouseController);
		JLib_v1.MouseController.MouseMoveHandler.Invoke(JLib_v1.MouseController);
	}
	, OnMouseDown: function(evt) {
		JLib_v1.GetEvent(evt, JLib_v1.MouseController);
		JLib_v1.MouseController.MouseDownHandler.Invoke(JLib_v1.MouseController);
	}
	, OnMouseUp: function(evt) {
		JLib_v1.GetEvent(evt, JLib_v1.MouseController);
		JLib_v1.MouseController.MouseUpHandler.Invoke(JLib_v1.MouseController);
	}
};
