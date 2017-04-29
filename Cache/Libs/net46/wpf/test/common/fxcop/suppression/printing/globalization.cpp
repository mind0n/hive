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
// Bug ID:    784228 in DevDiv bug DB  
// Developer: [....]
// Reason: Thread::CurrentCulture is necessary in some cases. We throw exceptions based on UICulture which is usually language of the OS.  
// CurrentCulture is usually used when you need to format a date, currency, number, etc based on user settings on their machine.
// The OS will usually have the appropriate langpack for WPF which will have the localized strings for WPF and then can be loaded via UICulture.  
// CurrentCulture is easily changed by user in the control panel and in WPF world it is not used for resource loading.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Globalization","CA1305:SpecifyIFormatProvider", MessageId="System.String.Format(System.IFormatProvider,System.String,System.Object[])", Scope="member", Target="System.Printing.PrintQueue.#.cctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Globalization","CA1305:SpecifyIFormatProvider", MessageId="System.String.Format(System.IFormatProvider,System.String,System.Object[])", Scope="member", Target="System.Printing.PrintSystemJobInfo.#.cctor()");

