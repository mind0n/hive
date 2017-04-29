
namespace ScriptEngine.Core
{
	public enum ElementType
	{
		// Special Type
		Ignore,

		// Basic Type
		Integer,
		Float,
		OpCode,
		ContextBegin,
		ContextEnd,
		ParamenterBegin,
		ParameterEnd,
		LeftCycle,
		RightCycle,
		StatementEnd,
        StringStart,
        String,
        StringEnd,
        MultiLineCommentStart,
        MultiLineComment,
        MultiLineCommentEnd,
		SingleLineCommentStart,
		SingleLineComment,
		SingleLineCommentEnd,

		// Raw Type
		Number,
		Variable,
		Symbol,
        Space,
		Unknown
	}
}
