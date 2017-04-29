#ifndef __TEXT_FORMATTING_MODE_H
#define __TEXT_FORMATTING_MODE_H

namespace System { namespace Windows { namespace Media
{
    /// <summary>
    /// The text formatting mode enumeration describes how text will be laid out on the screen.
    /// </summary>
    public enum class TextFormattingMode
    {
        /// <summary>
        /// Text is measured using glyph ideal metrics whose values are independent to the current display resolution.
        /// </summary>
        Ideal = 0,

        /// <summary>
        /// Text is measured using glyph display compatible metrics whose values tuned for the current display resolution.
        /// </summary>
        Display = 1
    };

}}}//System.Windows.Media

#endif //__TEXT_FORMATTING_MODE_H
