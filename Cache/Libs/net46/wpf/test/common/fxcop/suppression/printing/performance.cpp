//
// FxCop Violation Message Suppressions
//  Approved List
//

#include "win32inc.hpp"

using namespace System::Diagnostics::CodeAnalysis;

//***************************************************************************************************************************
// New Suppressions since 3.0 RTM
//***************************************************************************************************************************

//***************************************************************************************************************************
// Proposed suppressions for Orcas SP1
//***************************************************************************************************************************

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.AttributeNameToInfoLevelMapping.DriverThunk.DriverThunkingProfile.#RegisterAttributeMap()");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="Microsoft.Internal.GDIExporter.GeometryProxy.#ConvertToPathGeometry()");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="Microsoft.Internal.GDIExporter.TrueTypeFont.#write16(System.UInt32,System.UInt16)");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="Microsoft.Internal.GDIExporter.TrueTypeFont.#writeString(System.UInt32,System.UInt32,System.Char[],System.Text.Encoding)");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="Microsoft.Internal.GDIExporter.TrueTypeFont.#writeString(System.UInt32,System.UInt32,System.String,System.Text.Encoding)");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.PrintDocumentImageableArea.#VerifyAccess()");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.PrintJobInfoCollection.#VerifyAccess()");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.PrintJobSettings.#VerifyAccess()");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.PrintQueueCollection.#VerifyAccess()");

//**************************************************************************************************************************************************************
// Bug ID:    202863 in DevDiv bug DB  
// Developer: [....]
// Reason: Wish to avoid accidentally causing a breaking change at this stage in the product cycle. We may remove this method for Dev 10 but for SP1 
// suppression is preferred.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.PrintSystemObject.#CreatePropertiesDelegates()");

// New since V4 RTM:

//**************************************************************************************************************************************************************
// Bad FxCop detection:
// Install calls UpdateFromContext
// ProcessNameTable calls CheckSum
// ReplaceAll calls ReplaceFamilyName
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="Microsoft.Internal.GDIExporter.FontInstallInfo.#UpdateFromContext({modreq(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}Microsoft.Internal.GDIExporter.FontStreamContext)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="Microsoft.Internal.GDIExporter.TrueTypeFont.#CheckSum(System.UInt32,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="Microsoft.Internal.GDIExporter.TrueTypeFont.#ReplaceFamilyName(System.UInt32,System.UInt32,System.Char[],System.Text.Encoding)");
