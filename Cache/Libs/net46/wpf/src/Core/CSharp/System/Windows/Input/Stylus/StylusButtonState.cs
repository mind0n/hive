using System;

namespace System.Windows.Input
{
    /////////////////////////////////////////////////////////////////////////
    /// <summary>
    ///		The state of the StylusDevice hardware button.
    /// </summary>
    /// <ExternalAPI/>
    public enum StylusButtonState
    {
        /// <summary>
        ///  The StylusDevice button is not pressed, and is in the up position.
        /// </summary>
        Up = 0,
        /// <summary>
        ///  The StylusDevice button is pressed, and is in the down position.
        /// </summary>
        Down = 1,
    }
}
