
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
