#pragma once

#ifndef __GENERICTHUNKINGINC_HPP__
#define __GENERICTHUNKINGINC_HPP__

/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:                                                                
        GenericThunkingInc.hpp                                                             
                                                                              
    Abstract:
        Win32 thunking code include file
        
    Author:                                                                     
        Adina Trufinescu (AdinaTru) 24-April-2003                                        
                                                                             
    Revision History:  
        Khaled Sedky (KhaledS) 30-April-2003
--*/
#ifndef __GENERICTYPEMAPPINGS_HPP__ 
#include "GenericTypeMappings.hpp"
#endif

#ifndef  __GENERICPRINTERLEVELTHUNK_HPP__
#include <GenericPrinterLevelThunk.hpp>
#endif

#ifndef __GENERICPRINTERTHUNKFILTER_HPP__
#include "GenericPrinterThunkFilter.hpp"
#endif

#ifndef __GENERICDRIVERLEVELTHUNK_HPP__
#include "GenericDriverLevelThunk.hpp"
#endif

#ifndef __GENERICDRIVERTHUNKFILTER_HPP__
#include "GenericDriverThunkFilter.hpp"
#endif


#ifndef __GENERICJOBLEVELTHUNK_HPP__
#include "GenericJobLevelThunk.hpp"
#endif

#ifndef __GENERICJOBTHUNKFILTER_HPP__
#include "GenericJobThunkFilter.hpp"
#endif

#endif
