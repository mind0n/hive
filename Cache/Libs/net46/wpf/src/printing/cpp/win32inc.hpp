#pragma once

#ifndef __WIN32INC_HPP__
#define __WIN32INC_HPP__

/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:                                                                
        Win32Inc.hpp                                                             
                                                                              
    Abstract:
        
    Author:                                                                     
        Adina Trufinescu (AdinaTru) 24-April-2003                                        
                                                                             
    Revision History:  
        Khaled Sedky (KhaledS) 30-April-2003
--*/

#include <wpfsdl.h>
#include <stddef.h>
#include <codeanalysis\sourceannotations.h>  //TEMPORARY INCLUDE
#include <windows.h>
#include <winspool.h>
#include "winddiui.h"
#include <assert.h>
#include <xpsprint.h>
#include <..\..\Native\XpsPrintHelper\xpsprinthelper.h>

#define DOWNLEVEL_BAD_ALLOC

#using REACHFRAMEWORK_DLL as_friend
#using PRESENTATIONCORE_DLL as_friend

#endif
