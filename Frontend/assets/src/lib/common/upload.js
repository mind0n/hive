(function() {
	function upload(fileInput, url, success, error) {
		if (!fileInput || fileInput.files.length < 1 || !url) {
			return;
		}
		var fd = new FormData();
		for (var i = 0; i < fileInput.files.length; i++) {
			var file = fileInput.files[i];
			fd.append(file.name, file);
		}
		$.ajax({
			url: url,
			data: fd,
			type: 'POST',
			xhrFields: {
				withCredentials: true
			},
			processData: false,
			contentType: false,
			success: function (data) {
				console.log(data);
				try {
					var json = eval('(' + data + ')');
					if (success && !json.error) {
						success.call(fileInput, json);
					} else {
						if (error) {
							error.call(fileInput, json);
						}
					}
				} catch (e) {
					if (error) {
						error.call(fileInput, e);
					}
				}
			}, error: function (data) {
				console.log(data);
				if (error) {
					error.call(fileInput, data);
				}
			}
		});
	}
	function ajax(method, url, args, successcb, errorcb, undef) {
		if (url.indexOf('://') == 0){
			var n = location.href.indexOf('://');
			var schema = location.href.substr(0, n);
			url = schema + url;
		}
		$.ajax({
			url: url,
			method: method,
			data: args,
			success: function (data) {
				console.log('AjaxResponse: ' + data);
				if (successcb) {
					var t = typeof (data);
					var rlt = t == 'string' ? fromJson(data) : data;
					successcb(rlt);
				}
			},
			error: function (data) {
				if (data.status == 200 && data.responseText) {
					console.log('AjaxResume: ' + data.responseText);
					if (successcb) {
						var d = data.responseText;
						if (d.indexOf('{') == 0) {
							d = fromJson(d);
						}
						successcb(d);
					}
				} else {
					console.log('AjaxError: ' + data.responseText);
					if (errorcb) {
						errorcb(data);
					}
				}
			}
		});
	}
	window.upload = upload;
	window.get = function(url, args, success, error) {
		return ajax('get', url, args, success, error);
	};
	window.post = function (url, args, success, error) {
		return ajax('post', url, args, success, error);
	};
})();