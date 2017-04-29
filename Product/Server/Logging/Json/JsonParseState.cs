using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreJson
{
	public enum State
	{
		BeforeObj,
		Obj, Array,
		BeforeKey,
		Key,
		KeyEnd,
		BeforeColon,
		Colon,
		BeforeValue,
		BeginStrValue,
		PureValue,
		PureValueEnd,
		StrValue,
		StrValueEnd,
		NumValue,
		NumValueEnd,
		ObjPostValueEnd,
		ArrayPostValueEnd,
		ObjEnd, ArrayEnd,
		PostObjEnd
	}

}
