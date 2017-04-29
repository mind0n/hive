#ifndef __PRINTJOBSETTINGS_HPP__
#define __PRINTJOBSETTINGS_HPP__

/*++

    Copyright (C) 2002 - 2003 Microsoft Corporation
    All rights reserved.

    Module Name:
        PrintJobSettings.hpp

    Abstract:
        This file includes the declarations of the PrintJobSettings

    Author:
        Khaled Sedky (khaleds) 1-September-2005

    Revision History:
--*/
namespace System
{
namespace Printing
{
    /// <summary>
    /// This class abstracts the functionality of a print queue.
    /// </summary>
    /// <ExternalAPI/>
    public ref class PrintJobSettings 
    {
        internal:

        /// <summary>
        /// Instantiates a <c>PrintJobSettings</c> object representing the current print settings
        /// on the currently printed Print Job.
        /// </summary>
        /// <SecurityNote>
        ///    Critical     - Uses type from non-APTCA reachframework.dll (PrintTicket)
        ///    TreatAsSafe 	- Type is safe 
        /// <SecurityNote>
        [SecuritySafeCritical]
        PrintJobSettings(
            PrintTicket^ userPrintTicket
            );

        public:

        /// <value>
        /// Current PrintTicket for the currently printed Print Job
        /// </value>
        property
        PrintTicket^
        CurrentPrintTicket
        {
            /// <SecurityNote>
            ///    Critical     - Uses type from non-APTCA reachframework.dll (PrintTicket)
            ///    TreatAsSafe 	- Type is safe 
            /// <SecurityNote>
            [SecuritySafeCritical]
            PrintTicket^ get();

            /// <SecurityNote>
            ///    Critical     - Uses type from non-APTCA reachframework.dll (PrintTicket)
            ///    TreatAsSafe 	- Type is safe 
            /// <SecurityNote>
            [SecuritySafeCritical]
            void set(PrintTicket^ printTicket);
        }

        /// <value>
        /// Current Description for the currently printed Print Job
        /// </value>
        property
        String^
        Description
        {
            String^ get();
            void set(String^ description);
        }


        private:

        void
        VerifyAccess(
            void
        );
      
        PrintSystemDispatcherObject^    _accessVerifier;
        
        /// <SecurityNote>
        ///    Critical     - Field for type from non-APTCA reachframework.dll (PrintTicket)
        /// <SecurityNote>
        [SecurityCritical]
        PrintTicket^    _printTicket;
        String^         _description;
    };
}
}

#endif
