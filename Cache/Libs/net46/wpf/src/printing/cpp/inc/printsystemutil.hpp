#ifndef __PRINTSYSTEMUTIL_HPP__
#define __PRINTSYSTEMUTIL_HPP__

/*++
                                                                              
    Copyright (C) 2004 - 2005 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:                                                                
        PrintSystemUtil.hpp
                                                                              
    Abstract:
        Utility help classes.
        
    Author:                                                                     
        Ali Naqvi   (alinaqvi)     Feb 4, 2005                            
                                                                             
    Revision History:                                                           
--*/

namespace System
{
namespace Printing
{
    private ref class InternalExceptionResourceManager : System::Resources::ResourceManager
    {
        public:
        
        InternalExceptionResourceManager(
            void
            );
    };
}
}

#endif
