//------------------------------------------------------------------------------
//
// <copyright file="VisualDiagnostics.cs" company="Microsoft">
//    Copyright (C) Microsoft Corporation.  All rights reserved.
// </copyright>
//
// Description:
//      Visual tree diagnostic API.
//
//------------------------------------------------------------------------------

using System.Windows.Media;
using System.Windows.Media.Media3D;

namespace System.Windows.Diagnostics
{
    public static class VisualDiagnostics
    {
        private static bool s_isDebuggerCheckDisabledForTestPurposes; // for test purposes
        private static event EventHandler<VisualTreeChangeEventArgs> s_visualTreeChanged;

        #region Public

        /// <summary>
        /// Visual tree change notification. Fires for any visual change regardless whether it is
        /// connected to any root visual, e.g. Window. Subscribers are responsible for performing
        /// necessary filtering. Enabled under Managed debugger only.
        /// </summary>
        public static event EventHandler<VisualTreeChangeEventArgs> VisualTreeChanged
        {
            add
            {
                if (VisualDiagnostics.IsVisualTreeChangeEnabled)
                {
                    s_visualTreeChanged += value;
                }
            }
            remove
            {
                s_visualTreeChanged -= value;
            }
        }

        /// <summary>
        /// Provides object source info which will be available for objects created from BAML or XAML.
        /// Source info will not be available if diagnostics have not been enabled at the time of
        /// loading BAML or XAML.
        /// </summary>
        public static XamlSourceInfo GetXamlSourceInfo(object obj)
        {
            return XamlSourceInfoHelper.GetXamlSourceInfo(obj);
        }

        #endregion Public

        internal static void OnVisualChildChanged(DependencyObject parent, DependencyObject child, bool isAdded)
        {
            EventHandler<VisualTreeChangeEventArgs> visualTreeChanged = VisualDiagnostics.s_visualTreeChanged;
            if (visualTreeChanged != null && VisualDiagnostics.IsVisualTreeChangeEnabled)
            {
                int index;
                VisualTreeChangeType changeType;
                if (isAdded)
                {
                    index = VisualDiagnostics.GetChildIndex(parent, child);
                    changeType = VisualTreeChangeType.Add;
                }
                else
                {
                    // We cannot reliably get correct child index for a removed child. We'll force it to be -1;
                    index = -1;
                    changeType = VisualTreeChangeType.Remove;
                }
                VisualTreeChangeEventArgs args = new VisualTreeChangeEventArgs(parent, child, index, changeType);
                visualTreeChanged(null, args);
            }
        }

        private static bool IsVisualTreeChangeEnabled
        {
            get
            {
                return (System.Diagnostics.Debugger.IsAttached || s_isDebuggerCheckDisabledForTestPurposes);
            }
        }

        private static int GetChildIndex(DependencyObject parent, DependencyObject child)
        {
            int index = -1;
            Visual asVisual = child as Visual;
            if (asVisual != null)
            {
                index = asVisual._parentIndex;
            }
            else
            {
                Visual3D asVisual3D = child as Visual3D;
                if (asVisual3D != null)
                {
                    index = asVisual3D.ParentIndex;
                }
            }

            // Sometimes index is not up to date. We'll have to find it manually.
            if (index < 0)
            {
                int count = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < count; i++)
                {
                    DependencyObject obj = VisualTreeHelper.GetChild(parent, i);
                    if (obj == child)
                    {
                        index = i;
                        break;
                    }
                }
            }

            return index;
        }
    }
}
