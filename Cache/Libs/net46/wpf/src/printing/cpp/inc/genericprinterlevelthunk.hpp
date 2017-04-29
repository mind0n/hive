#pragma once

#ifndef __GENERICPRINTEREVELTHUNK_HPP__
#define __GENERICPRINTEREVELTHUNK_HPP__
/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:  

        GenericPrinterLevelThunk.hpp
        
    Abstract:

        Win32PrinterThunk - This is object that does the Win32 thunking for a PrintQueue
        based on the level specified in the constructor. The object has the knowledge of calling
        the thunked GetPrinter, SetPrinter and EnumPrinters APIs. 

    Author: 
 
        Adina Trufinescu (adinatru) April 24th 2003

    Revision History:  
--*/
namespace MS
{
namespace Internal
{
namespace PrintWin32Thunk
{
namespace AttributeNameToInfoLevelMapping
{
namespace PrintQueueThunk
{
    using namespace System::Security;

    private ref class Win32PrinterThunk : public InfoLevelThunk
    {
        public:
        
        Win32PrinterThunk(
            UInt32                  infoLevel,
            InfoLevelMask           infoCoverageMask
            );

        virtual
        void
        CallWin32ApiToGetPrintInfoData(
            PrinterThunkHandler^    printThunkHandler,
            Object^                 cookie
            ) override;

        virtual
        UInt32
        CallWin32ApiToEnumeratePrintInfoData(
            String^                 serverName,
            UInt32                  flags
            );

        virtual
        void
        BeginCallWin32ApiToSetPrintInfoData(
            PrinterThunkHandler^    printThunkHandler
            ) override;

        ///<SecurityNote>
        /// Critical    - Calls critical IPrinterInfo.get_Win32SafeHandle().
        /// TreatAsSafe - Does not call critical SafeMemoryHandle.Dispose, does not expose critical SafeMemoryHandle
        ///</SecurityNote>
        [SecurityCritical, SecurityTreatAsSafe]
        virtual
        void
        EndCallWin32ApiToSetPrintInfoData(
            PrinterThunkHandler^    printThunkHandler
            ) override;
    };
}
}
}
}
}
#endif
