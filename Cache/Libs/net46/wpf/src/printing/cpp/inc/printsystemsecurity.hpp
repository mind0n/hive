#ifndef __PRINTSYSTEMSECURITY_HPP__
#define __PRINTSYSTEMSECURITY_HPP__

/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:                                                                
        PrintSystemSecurity.hpp                                                             
                                                                              
    Abstract:
        
    Author:                                                                     
        Khaled Sedky (khaleds) 10-May-2002                                        
                                                                             
    Revision History:                                                           
--*/
namespace System
{
namespace Printing
{

    public enum class PrintSystemDesiredAccess
    {
        None                  = 0x00000000,
        AdministrateServer    = 0x000f0001,
        EnumerateServer       = 0x00020002,
        UsePrinter            = 0x00020008,
        AdministratePrinter   = 0x000f000c
    };
}
}

#endif

