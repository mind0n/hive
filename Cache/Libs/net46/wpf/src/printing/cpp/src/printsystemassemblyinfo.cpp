/*++
                                                                              
    Copyright (C) 2002 - 2003 Microsoft Corporation                                   
    All rights reserved.                                                        
                                                                              
    Module Name:                                                                
        PrintSystemAssemblyInfo.cpp                                                             
                                                                              
    Abstract:
        
    Author:                                                                     
        Khaled Sedky (khaleds) 29-May-2003
                                                                             
    Revision History:                                                           
--*/

#include "win32inc.hpp"

using namespace System::Reflection;
using namespace System::Runtime::CompilerServices;
using namespace System::Security::Permissions;

//change to use PublicKey 
[assembly:InternalsVisibleTo("ReachFramework, PublicKey=0024000004800000940000000602000000240000525341310004000001000100b5fc90e7027f67871e773a8fde8938c81dd402ba65b9201d60593e96c492651e889cc13f1415ebb53fac1131ae0bd333c5ee6021672d9718ea31a8aebd0da0072f25d87dba6fc90ffd598ed4da35e44c398c454307e8e33b8426143daec9f596836f97c8f74750e5975c64e2189f45def46b2a2b1247adc3652bf5c308055da9")];

[assembly: System::Security::AllowPartiallyTrustedCallers]
[assembly: System::Security::SecurityCritical]


// When objects that do not derive from System.Exception are thrown,
// do not wrap them in a RuntimeWrappedException
[assembly:RuntimeCompatibilityAttribute(WrapNonExceptionThrows = false)];
