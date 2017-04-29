var config = {
	styles: {
		cols: {
			id: { width: '24px', display: 'none' },
			fname: { width: '400px', display: 'none' },
			dname: { width: '400px' },
			utime: { width: '200px' },
			opt: { width: '100px' }
		},
		rows: {

		}
	},
	staticColor: false
			,
	hovable: false
			,
	cols: [
		{ field: 'id', caption: 'Id', type: 'int' }
        , { field: 'fname', caption: '文件路径', type: 'string' }
		, { field: 'dname', caption: '文件名', type: 'string', rowtype: 'ViewerAnchorCell', $: { url: '@fname' } }
		, { field: 'utime', caption: '上传时间', type: 'string' }
        , { field: 'opt', caption: '操作', type: 'string', rowtype: 'ViewerAnchorCell', $: { url: 'FileUploader.aspx?act=del&id=@id', text:'删除' } }
	]
};
//var data = {
//	rows: [
//		{ id: '1', uname: 'admin', upwd: 'nothing', utype: 0, unote: 'This user represents a system administrator.' }
//		, { id: '2', uname: 'temp', upwd: 'nothing' }
//		, { id: '3', uname: 'guest', upwd: 'nothing' }
//		, { id: '4', uname: 'user' }
//	]
//};
function ListFiles() {
    Joy('fviewerarea').innerHTML = '';
	var list = Joy.make('Viewer');
	list.init(config);
	list.setData(data);
	list.append('fviewerarea');
}

$(function () {
	ListFiles();
	var flist = Joy.make('FuList');
	var notification = null;
	flist.init({ startnum: 10 });
	flist.update();
	$('#filelistcontainer').empty().append(flist);
	$('#btnDelAll').click(function () {
	    window.open('/admin/FileUploader.aspx?act=del', '_top');
	});
	$('#btnReset').click(function () {
		flist.reset();
	});
	$('#btnSave').click(function () {
		Joy.model(true);
		var r = Joy.makeRequest({ md: 'SaveFile' });
		var x = Joy.obj2xml(r, 'JsRequest');

		//$("aspnetForm").submit();//.attr('action','services.aspx?xml=' + escape(x)).submit();
		$("#aspnetForm").ajaxSubmit({
			url: '/Services.aspx?xml=' + escape(x),
			beforeSubmit: function () {
				notification = Joy.msg('文件上传中...');
			},
			success: function (data) {
				data = Joy.json(data);
				Joy.model();
				if (data && data.IsAllSuccessful == 'True') {
					$(Joy.msg('文件上传成功！', notification)).delay(1000).fadeOut(1000);
				} else {
					Joy.msg('文件上传失败！', notification);
				}
				Joy("aspnetForm").reset();
			    //ListFiles();
				window.open('/admin/fileuploader.aspx', '_top');
			},
			error: function (data) {
				Joy.model();
				Joy.msg('文件上传失败！错误信息已保存在日志中。', notification);
				Joy("aspnetForm").reset();
			}
		});
	});
});
