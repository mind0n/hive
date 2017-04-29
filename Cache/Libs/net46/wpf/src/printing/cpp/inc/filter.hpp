#ifndef __FILTER_HPP__
#define __FILTER_HPP__

/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:                                                                
        Filter.hpp                                                             
                                                                              
    Abstract:
        This is the header file including the declaration of the 
        base object for a pipeline module ("The Filter") and the 
        The declarations in this file are subject to change as our
        design evolves and as requirements from the new spooler
        architecture comes in

    Author:                                                                     
        Khaled Sedky (KhaledS) 19-December-2002
     
                                                                             
    Revision History:  
--*/

namespace System
{
namespace Printing
{
    /// <summary>
    ///    This is the abstract base class for installable objects in the Print System, 
    ///    such as Driver, PrintProcessors.
    ///    This object is returned by the Print System and cannot be instantiated by the end user.
    ///    It is considered a management object.
    /// </summary>
    /// <ExternalAPI/>
    public ref class PrintFilter abstract :
    public PrintSystemObject
    {
        internal:

        PrintFilter(
            String^ filterName
            );

        protected:
        
        virtual
        void
        InternalDispose(
           bool disposing
           ) override;
    };
}
}

#endif
