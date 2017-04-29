/***************************************************************************
 * module: ttfdcnfg.h
 *
 * author: Paul Linnerud
 * date:   Jan 1998
 * Copyright 1990-1998. Microsoft Corporation.
 *
 * Provides some platform configuration for ttfdelta.
 *
 **************************************************************************/
#ifndef TTFDCNFG_DOT_H_DEFINED
#define TTFDCNFG_DOT_H_DEFINED  

// Using Microsoft C, callback functions for qsort and bsearch must be __cdecl
// Under C++/CLI, they are declared with __clrcall, which is the only option.
#ifndef _M_CEE
#define CRTCB __cdecl 
#else
#define CRTCB
#endif

#ifndef NO_CRT_ASSERT

#include <assert.h>

#endif

#endif  TTFDCNFG_DOT_H_DEFINED    
