#pragma once

#define PREFAST_SUPPRESS(x,y)        __pragma(warning(suppress: x))
#define PREFAST_DISABLE(x,y)         __pragma(warning(disable: x))
#define PREFAST_DISABLE_PUSH(x,y)    __pragma(warning(push)); __pragma(warning(disable: x))
#define PREFAST_POP                  __pragma(warning(pop))
#define PREFAST_ENABLE(x,y)          __pragma(warning(default: x))

PREFAST_DISABLE(6011 6014 6031 6054 6069 6216 6237 6239 6240 6246 6255 6262 6263 6295 6325 6326 6385 6386 6387 6419 6535 22011, "We don't care about these warnings")

/* The following IDs represent a ?minimum bar? for PreFast issues 
   and should be addressed before we start the security push 
   (as well as ongoing until we ship):
 
   6029;6053;6056;6057;6059;6063;6067;6200;6201;6202;6203;6204;
   6205;6207;6241;6243;6248;6252;6259;6260;6268;6276;6277;6281;
   6282;6287;6288;6289;6290;6291;6296;6298;6299;6305;6306;6308;
   6334;6383;22008;22009. 

   Note that these a DevDiv PreFast (LKG8 and on) warning numbers.

   Basically, we should never disable these warnings globally in
   this file (like we do with others at line 6).
*/
#include <ddsecbar.h>
#include <CodeAnalysis\Warnings.h>
#include <CodeAnalysis\IntegerOverflowWarnings.h>
PREFAST_ENABLE( ALL_DEVDIV_MINBAR_WARNINGS, "Enable all minbar warnings." )
