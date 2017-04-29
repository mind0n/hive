//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
// 
// File: FrameworkAppContextSwitches.cs
//---------------------------------------------------------------------------

using System;
using System.Runtime.CompilerServices;

namespace MS.Internal
{
    internal static class FrameworkAppContextSwitches
    {
        internal const string DoNotApplyLayoutRoundingToMarginsAndBorderThicknessSwitchName = "Switch.MS.Internal.DoNotApplyLayoutRoundingToMarginsAndBorderThickness"; 
        private static int _doNotApplyLayoutRoundingToMarginsAndBorderThickness;
        public static bool DoNotApplyLayoutRoundingToMarginsAndBorderThickness
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return LocalAppContext.GetCachedSwitchValue(DoNotApplyLayoutRoundingToMarginsAndBorderThicknessSwitchName, ref _doNotApplyLayoutRoundingToMarginsAndBorderThickness);
            }
        }
    }
}
