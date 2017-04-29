//------------------------------------------------------------------------------
// <copyright file="LocalAppContextSwitches.cs" company="Microsoft">
//     Copyright (c) Microsoft Corporation.  All rights reserved.
// </copyright>                                                                
//------------------------------------------------------------------------------

/*
 */
namespace System.Drawing {
    using System;
    using System.Runtime.CompilerServices;

    internal static class LocalAppContextSwitches {
        private static int dontSupportPngFramesInIcons;
        public static bool DontSupportPngFramesInIcons {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get {
                return LocalAppContext.GetCachedSwitchValue(@"Switch.System.Drawing.DontSupportPngFramesInIcons", ref LocalAppContextSwitches.dontSupportPngFramesInIcons);
            }
        }
    }
}
