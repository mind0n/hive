#pragma once

#ifndef __INTEROPINTERFACES_HPP__
#define __INTEROPINTERFACES_HPP__
/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name: 

        InteropInterfaces.hpp
        
    Abstract:

        Interface that the managed objects wrapping PRINTER_INFO or 
        DRIVER_INFO structures expose. The interface allows direct access
        to the unmanaged buffer, it returns the number of structres in the buffer
        and gets and sets data inside the buffer.
        
    Author: 

        Adina Trufinescu (AdinaTru) April 24th 2003
                                                                             
    Revision History:  
--*/
namespace MS
{
namespace Internal
{
namespace PrintWin32Thunk
{
    using namespace System::Security;
    using namespace System::Security::Permissions;
    using namespace System::Drawing::Printing;

    private interface class IPrinterInfo
    {
        public:

        property
        SafeMemoryHandle^
        Win32SafeHandle
        {
            ///<SecurityNote>
            /// Critical               - Returns critical SafeMemoryHandle
            ///</SecurityNote>
            [SecurityCritical]
            SafeMemoryHandle^ get();
        }

        property
        UInt32
        Count
        {
            UInt32 get();
        }
        
        Object^
        GetValueFromName(
            String^         valueName,
            UInt32          index
            );

        bool
        SetValueFromName(
            String^         valueName,
            Object^         value
            );

        ///<SecurityNote>
        /// Critical               - All implementations call Dispose on critical SafeHandle
        ///</SecurityNote>
        [SecurityCritical]
        void
        Release(
            );
    };
}
}
}
#endif
