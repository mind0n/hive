//---------------------------------------------------------------------------
//
// Copyright (C) Microsoft Corporation.  All rights reserved.
// 
// File: AppContextDefaultValues.cs
//---------------------------------------------------------------------------

using MS.Internal;

namespace System
{
    internal static partial class AppContextDefaultValues
    {
        static partial void PopulateDefaultValuesPartial(string platformIdentifier, string profile, int targetFrameworkVersion)
        {
            switch (platformIdentifier)
            {
                case ".NETFramework":
                    {
                        if (targetFrameworkVersion <= 40502)
                        {
                            LocalAppContext.DefineSwitchDefault(FrameworkAppContextSwitches.DoNotApplyLayoutRoundingToMarginsAndBorderThicknessSwitchName, true);
                        }

                        break;
                    }
            }
        }
    }
}
