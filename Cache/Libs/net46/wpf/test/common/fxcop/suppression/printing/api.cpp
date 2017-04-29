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
// New Suppressions since 3.5 SP 2
//***************************************************************************************************************************

//**************************************************************************************************************************
// Bug ID:    693223
// Developer: [....]
// Reason:    Fixing would be a breaking change.
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="0#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Double&,System.Double&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1702:CompoundWordsShouldBeCasedCorrectly", MessageId="SubSystem", Scope="member", Target="System.Printing.PrintServer.#SubSystemVersion");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="0#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Double&,System.Double&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="1#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Double&,System.Double&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="0#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="0#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="1#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="2#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="1#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="1#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="2#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1045:DoNotPassTypesByReference", MessageId="3#", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="namespace", Target="System.Windows.Xps");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Bidi", Scope="member", Target="System.Printing.EnumeratedPrintQueueTypes.#EnableBidi");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Imageable", Scope="type", Target="System.Printing.PrintDocumentImageableArea");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintJobType.#Xps");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Double&,System.Double&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Imageable", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Imageable", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.Printing.PrintQueue)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Imageable", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Imageable", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintQueue.#CreateXpsDocumentWriter(System.String,System.Printing.PrintDocumentImageableArea&,System.Windows.Controls.PageRangeSelection&,System.Windows.Controls.PageRange&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Bidi", Scope="member", Target="System.Printing.PrintQueue.#IsBidiEnabled");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="member", Target="System.Printing.PrintQueue.#IsXpsDevice");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Bidi", Scope="member", Target="System.Printing.PrintQueueAttributes.#EnableBidi");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.VisualsToXpsDocument");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.XpsDocumentNotificationLevel");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.XpsDocumentWriter");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.XpsWriterException");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1711:IdentifiersShouldNotHaveIncorrectSuffix", Scope="type", Target="System.Printing.PrintQueue");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1716:IdentifiersShouldNotMatchKeywords", MessageId="Resume", Scope="member", Target="System.Printing.PrintQueue.#Resume()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1721:PropertyNamesShouldNotMatchGetMethods", Scope="member", Target="System.Printing.PrintQueueStringProperty.#Type");

//**************************************************************************************************************************
// Bug ID:    693223
// Developer: [....]
// Reason:    PrintSystemObject doesn not own the lifetime of the propertiesCollection field.
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2213:DisposableFieldsShouldBeDisposed", MessageId="propertiesCollection", Scope="member", Target="System.Printing.PrintSystemObject.#Dispose(System.Boolean)");

//**************************************************************************************************************************
// Bug ID:    693223
// Developer: [....]
// Reason:    We have shipped with this design - Filed Dev 10 697099 to re-evaluate the design
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Design","CA1044:PropertiesShouldNotBeWriteOnly", Scope="member", Target="System.Printing.IndexedProperties.PrintProperty.#IsInitialized");

//**************************************************************************************************************************
// Bug ID:    688619
// Developer: [....]
// Reason:    FxCop incorrectly flags these as incorrectly instantiated argument exceptions
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2208:InstantiateArgumentExceptionsCorrectly", Scope="member", Target="System.Printing.PrintSystemUNCPathResolver.#Resolve(System.Printing.IndexedProperties.PrintPropertyDictionary)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2208:InstantiateArgumentExceptionsCorrectly", Scope="member", Target="System.Printing.PrintSystemUNCPathResolver.#ValidateAndCapturePrinterName(System.Object,System.Printing.PrintSystemUNCPathResolver)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2208:InstantiateArgumentExceptionsCorrectly", Scope="member", Target="System.Printing.PrintSystemUNCPathResolver.#ValidateAndCaptureServerName(System.Object,System.Printing.PrintSystemUNCPathResolver)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2208:InstantiateArgumentExceptionsCorrectly", Scope="member", Target="MS.Internal.PrintWin32Thunk.GetDataThunkObject.#PopulatePrintSystemObject(MS.Internal.PrintWin32Thunk.PrinterThunkHandler,System.Printing.PrintSystemObject,System.String[])");

//**************************************************************************************************************************
// Bug ID:    688619
// Developer: [....]
// Reason:    SafeHandle.GetDangerousHandle call is required for MC++ language interop. All calls are protected by DangerousAddRef, DangerousRelease
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForJob.JobInfoOne.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForJob.JobInfoTwo.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoEight.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoEight.#InternalDispose(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoEight.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFiveGetter.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFiveSetter.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFourGetter.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFourSetter.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoNine.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoNine.#InternalDispose(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoNine.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoOne.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoOne.#InternalDispose(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSeven.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSeven.#InternalDispose(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSeven.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSix.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSix.#InternalDispose(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoThree.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoThree.#InternalDispose(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoGetter.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoSetter.#.ctor(MS.Internal.PrintWin32Thunk.PrinterThunkHandler)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Reliability","CA2001:AvoidCallingProblematicMethods", MessageId="System.Runtime.InteropServices.SafeHandle.DangerousGetHandle", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoSetter.#SetValueFromName(System.String,System.Object)");

//**************************************************************************************************************************
// Bug ID:    688619
// Developer: [....]
// Reason:    Fixing would be a breaking change.
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="namespace", Target="System.Windows.Xps");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.VisualsToXpsDocument");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.XpsDocumentNotificationLevel");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.XpsDocumentWriter");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId="Xps", Scope="type", Target="System.Windows.Xps.XpsWriterException");

//**************************************************************************************************************************
// Bug ID:    688619
// Developer: [....]
// Reason:    Xps is a standard acronym for Xml Paper Specification
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Naming","CA1703:ResourceStringsShouldBeSpelledCorrectly", MessageId="Xps", Scope="resource", Target="System.Printing.resources");

//**************************************************************************************************************************
// Bug ID:    none
// Developer: [....]
// Reason:    These are referenced private methods flagged as uncalled by FxCop. 
//            Surpressing until FxCop is fixed
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.AttributeValueInteropHandler.#get_SyncRoot()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.EnumDataThunkObject.#TweakTheFlags(System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.PrintSystemUNCPathResolver.#set_PrinterName(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.PrintSystemUNCPathResolver.#set_ServerName(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.Activation.ObjectsAttributesValuesFactory.#get_SyncRoot()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.Activation.PrintPropertyFactory.#get_SyncRoot()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="System.Printing.Activation.PrintSystemObjectFactory.#get_SyncRoot()");

//**************************************************************************************************************************
// Bug ID:    Dev 11 280791
// Developer: [....]
// Reason:    Conversion is available via alternate public API's. Do not want to introduce new public API's
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2225:OperatorOverloadsHaveNamedAlternates", Scope="member", Target="System.Printing.IndexedProperties.PrintQueueAttributeProperty.#op_Implicit({modreq(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}System.Printing.IndexedProperties.PrintQueueAttributeProperty):System.Printing.PrintQueueAttributes");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2225:OperatorOverloadsHaveNamedAlternates", Scope="member", Target="System.Printing.IndexedProperties.PrintQueueAttributeProperty.#op_Implicit(System.Printing.IndexedProperties.PrintQueueAttributeProperty):System.Printing.PrintQueueAttributes");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2225:OperatorOverloadsHaveNamedAlternates", Scope="member", Target="System.Printing.IndexedProperties.PrintQueueStatusProperty.#op_Implicit({modreq(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}System.Printing.IndexedProperties.PrintQueueStatusProperty):System.Printing.PrintQueueStatus");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Usage","CA2225:OperatorOverloadsHaveNamedAlternates", Scope="member", Target="System.Printing.IndexedProperties.PrintQueueStatusProperty.#op_Implicit(System.Printing.IndexedProperties.PrintQueueStatusProperty):System.Printing.PrintQueueStatus");
