//------------------------------------------------------------------------------
//  Microsoft Avalon
//  Copyright (c) Microsoft Corporation, 2003
//
//  File:       HitTestResult
//------------------------------------------------------------------------------

using System;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

using System.Collections;
using System.Diagnostics;
using MS.Internal;

namespace System.Windows.Media 
{
    /// <summary>
    /// This base returns the visual that was hit during a hit test pass.
    /// </summary>
    public abstract class HitTestResult
    {
         private DependencyObject _visualHit;

         internal HitTestResult(DependencyObject visualHit)
         {
             _visualHit = visualHit;
         }
    
         /// <summary>
         /// Returns the visual that was hit.  May be a Visual or Visual3D.
         /// </summary>
         public DependencyObject VisualHit
         { 
             get
             {
                 return _visualHit;
             }
         }
    }
}

