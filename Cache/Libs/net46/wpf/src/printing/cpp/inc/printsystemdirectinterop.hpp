#pragma once

#ifndef __PRINTSYSTEMDIRECTINTEROPINC_HPP__
#define __PRINTSYSTEMDIRECTINTEROPINC_HPP__

/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:                                                                
        PrintSystemDirectInteropInc.hpp                                                             
                                                                              
    Abstract:
        
    Author:                                                                     
        Adina Trufinescu (AdinaTru) 24-April-2003                                        
                                                                             
    Revision History:  
        Khaled Sedky (KhaledS) 30-April-2003
--*/

#ifndef  __INTEROPDEVMODE_HPP__ 
#include <InteropDevMode.hpp>
#endif 

#ifndef  __PRINTSYSTEMSECURITY_HPP__
#include <PrintSystemSecurity.hpp>
#endif

#ifndef  __INTEROPPRINTERDEFAULTS_HPP__
#include <InteropPrinterDefaults.hpp>
#endif

#ifndef  __INTEROPDOCINFO_HPP__
#include <InteropDocInfo.hpp>
#endif

#ifndef  __INTEROPINTERFACES_HPP__
#include <InteropInterfaces.hpp>
#endif 

#ifndef  __INTEROPPRINTERINFO_HPP__
#include <InteropPrinterInfo.hpp>
#endif 

#ifndef  __INTEROPJOBINFO_HPP__
#include <InteropJobInfo.hpp>
#endif 

using namespace System::Windows::Xps::Serialization;

#ifndef  __INTEROPPRINTERHANDLERBASE_HPP__
#include <InteropPrinterHandlerBase.hpp>
#endif 

#ifndef  __INTEROPPRINTERHANDLER_HPP__
#include <InteropPrinterHandler.hpp>
#endif 

#ifndef  __XPSDEVSIMINTEROPPRINTERHANDLER_HPP__
#include <XpsDeviceSimulatingInteropPrinterHandler.hpp>
#endif 

using namespace MS::Internal::PrintWin32Thunk;

#ifndef  __INTEROPPRINTERINFOUNMANAGEDBUILDER_HPP__
#include <InteropPrinterInfoUnmanagedBuilder.hpp>
#endif 

using namespace MS::Internal::PrintWin32Thunk::Win32ApiThunk;
#endif
