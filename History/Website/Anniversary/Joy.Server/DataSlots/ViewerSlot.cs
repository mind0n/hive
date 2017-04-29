using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Joy.Server.DataSlots
{
/*
	var config = {
		styles: {
			uname: { width: '70px' },
			upwd: { width: '70px' },
			utype: { width: '70px' },
			unote: { width: '70px' }
		},
		cols: [
			{ field: 'uname', caption: 'Username', type: 'string' }
			, { field: 'upwd', caption: 'Password', type: 'string' }
			, { field: 'utype', caption: 'User Type', type: 'int' }
			, { field: 'unote', caption: 'Description', type: 'string' }
		], 
		hierarchy:[
			{
				name:'Account Info', 
				$:[
					{col:'uname'},
					{col:'upwd'}
				]
			},
			{col:'utype'},
			{col:'unote'}
 		]
	};
	var data = {
		rows: [
			{ uname: 'admin', upwd: 'nothing', utype: 0, unote: 'System administrator' }
			, { uname: 'temp', upwd: 'nothing' }
			, { uname: 'guest', upwd: 'nothing' }
			, { uname: 'user' }
		]
	};
 */
	public class ViewerSlot : TableSlot
	{
		public ViewerSlot() : base()
		{

		}						   
	}
}
