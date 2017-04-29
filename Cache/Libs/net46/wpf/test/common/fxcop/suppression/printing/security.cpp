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
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Modifying existing ReviewImperativeSecurity suppressions to work with DevDiv code analysis rules. In each case, objects used to create permissions
// are not changed during permission's scope
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2103:ReviewImperativeSecurity", Scope = "member", Target = "Microsoft.Internal.GDIExporter.FontStreamContext.#GetStream()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2103:ReviewImperativeSecurity", Scope = "member", Target = "System.Windows.Xps.XpsDocumentWriter.CreateXPSDocument(System.String):System.Void");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2103:ReviewImperativeSecurity", Scope = "member", Target = "System.Windows.Xps.VisualsToXpsDocument.CreateXPSDocument(System.String):System.Void");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: From CLR 2.0, non-CLS exceptions are wrapped in System.Runtime.CompilerServices.RuntimeWrappedException which derives from System.Exception
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2102:CatchNonClsCompliantExceptionsInGeneralHandlers", Scope = "member", Target = "MS.Internal.PrintWin32Thunk.PropertyCollectionMemorySafeHandle.#AllocPropertyCollectionMemorySafeHandle(System.UInt32)");

//**************************************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression, this code has been reviewed and is not a threat.
//**************************************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2118:ReviewSuppressUnmanagedCodeSecurityUsage", Scope = "member", Target = "MS.Internal.PrintWin32Thunk.Win32ApiThunk.UnsafeNativeMethods.#InvokeClosePrinter(System.IntPtr)");

//**************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression, this code
// is used in print queue to create Packaging Policy packaging process event
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2109:ReviewVisibleEventHandlers", Scope = "member", Target = "System.Printing.PrintQueueStream.#HandlePackagingProgressEvent(System.Object,System.Windows.Xps.Packaging.PackagingProgressEventArgs)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - Avalon PT blessed Assert. See comments in code.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Printing.PrintQueueStream.#AbortOrCancel(System.Boolean)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - PrintQueueStream code
// has been reviewed and does not cause a way to circumvent security protection
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Printing.PrintQueueStream.#CommitDataToPrinter()");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - All asserts were reviewed by PT team.
// No PT code paths call into non-APTCA without showing the Avalon Print UI dialog
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Printing.PrintQueueStream.#HandlePackagingProgressEvent(System.Object,System.Windows.Xps.Packaging.PackagingProgressEventArgs)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - PrintQueueStream code has been
// reviewed and does not cause a way to circumvent security protection
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Printing.PrintQueueStream.#InitializePrintStream()");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - All asserts were reviewed by PT team.
// No PT code paths call into non-APTCA without showing the Avalon Print UI dialog
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Printing.PrintQueueStream.#OpenSpoolFileStream(Microsoft.Win32.SafeHandles.SafeFileHandle)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - All asserts were reviewed by PT team.
// No PT code paths call into non-APTCA without showing the Avalon Print UI dialog
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Printing.PrintQueueStream.#Write(System.Byte[],System.Int32,System.Int32)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - Security critical attribute added.
// The path name only comes from the trusted MXDW driver. Reviewed and approved by PT team.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Windows.Xps.VisualsToXpsDocument.#CreateXPSDocument(System.String)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - Security critical attribute added.
// The path name only comes from the trusted MXDW driver. Reviewed and approved by PT team.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Windows.Xps.XpsDocumentWriter.#CreateXPSDocument(System.String)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - Fix Printing in PT after
// PrintUI dialog optimizations to call EnumPrinters with only name and attribute.Blessed by PT team - Akaza.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Windows.Xps.VisualsToXpsDocument.#MxdwConversionRequired(System.Printing.PrintQueue)");

//**************************************************************************************************************************************************************
// Bug ID:    200292 in DevDiv bug DB
// Developer: [....]
// Reason: Porting existing suppression from 3.0 to work in DevDiv code analysis system. Per 3.0 suppression - Fix Printing in PT after
// PrintUI dialog optimizations to call EnumPrinters with only name and attribute.Blessed by PT team - Akaza.
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security", "CA2106:SecureAsserts", Scope = "member", Target = "System.Windows.Xps.XpsDocumentWriter.#MxdwConversionRequired(System.Printing.PrintQueue)");

//**************************************************************************************************************************************************************
// Bug ID:    789126 Dev10 bugs
// Developer: [....]
// Reason: System.Printing.dll depends on types in non-APTCA ReachFramework.dll - all usage has been reviewed by Security team
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#ExtEscGetName()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#StartDocument(System.String,System.String,System.String,System.Byte[])");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#StartDocumentWithoutCreatingDC(System.String,System.String,System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#ThrowOnFailure({modopt(System.Runtime.CompilerServices.IsLong)}System.Int32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.FontInfo.#UsePrivate(System.Windows.Media.GlyphTypeface)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.GdiGeometryConverter.#AddArc(System.Windows.Point,System.Windows.Size,System.Double,System.Boolean,System.Windows.Media.SweepDirection)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.GeometryProxy.#GetPointCount()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.GeometryProxy.#IsRectangle()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="MS.Internal.PrintWin32Thunk.SetDataThunkObject.#CreatePrintCommitAttributesException(System.Int32,System.Collections.ObjectModel.Collection`1<System.String>,System.Collections.ObjectModel.Collection`1<System.String>)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#CalculateImagableArea(System.Printing.PrintTicket,System.Printing.PrintQueue,System.Double,System.Double)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#Commit()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#CreateAsyncSerializationManager(System.Boolean,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#CreatePrintQueueException(System.Int32,System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#CreatePrintSystemException(System.Int32,System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#CreateSerializationManager(System.Boolean,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#get_DefaultPrintTicket()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#set_DefaultPrintTicket(System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#DisposeSerializationManager(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsDriverDocEvent(System.Object,System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentEvent(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentPrintTicket(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentSequenceEvent(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentSequencePrintTicket(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedPageEvent(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedPagePrintTicket(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#GetAlteredPropertiesFilter(System.Collections.Specialized.StringCollection)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#GetPrintCapabilities()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#GetPrintCapabilities(System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#GetPrintCapabilitiesAsXml()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#GetPrintCapabilitiesAsXml(System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#get_MaxPrintSchemaVersion()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#MergeAndValidatePrintTicket(System.Printing.PrintTicket,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#MergeAndValidatePrintTicket(System.Printing.PrintTicket,System.Printing.PrintTicket,System.Printing.PrintTicketScope)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#get_UserPrintTicket()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#set_UserPrintTicket(System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueue.#XpsDocumentEventPrintTicket(System.Windows.Xps.Serialization.XpsDocumentEventType,System.Windows.Xps.Serialization.XpsDocumentEventType,System.Runtime.InteropServices.SafeHandle,System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueueStream.#CreatePrintingCanceledException(System.Int32,System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintQueueStream.#HandlePackagingProgressEvent(System.Object,System.Windows.Xps.Packaging.PackagingProgressEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintServer.#CreatePrintCommitAttributesException(System.Int32,System.String,System.Collections.ObjectModel.Collection`1<System.String>,System.Collections.ObjectModel.Collection`1<System.String>)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintServer.#CreatePrintServerException(System.Int32,System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintServer.#CreatePrintServerException(System.Int32,System.String,System.Exception)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintServer.#IsHResultWin32Error(System.Int32,System.Int32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintSystemJobInfo.#.ctor(System.Printing.PrintQueue,System.String,System.String,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintSystemJobInfo.#CreatePrintJobException(System.Int32,System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintSystemJobInfo.#CreatePrintJobException(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintSystemJobInfo.#CreatePrintJobException(System.String,System.Exception)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Printing.PrintSystemJobInfo.#IsErrorInvalidParameter(System.Int32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#Cancel()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#CancelAsync()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#CreateXPSDocument(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#EndBatchWrite()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#ForwardUserPrintTicket(System.Object,System.Windows.Xps.Serialization.XpsSerializationPrintTicketRequiredEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#MxdwConversionRequired(System.Printing.PrintQueue)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#MxdwInitializeOptimizationConversion(System.Printing.PrintQueue)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#SetPrintTicketEventHandler(System.Windows.Xps.Serialization.PackageSerializationManager)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#WriteVisual(System.Boolean,System.Printing.PrintTicket,System.Windows.Xps.Serialization.PrintTicketLevel,System.Windows.Media.Visual)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#BeginPrintFixedDocumentSequence(System.Windows.Documents.FixedDocumentSequence,System.Int32{modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}*)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#BeginWrite(System.Boolean,System.Boolean,System.Boolean,System.Printing.PrintTicket,System.Windows.Xps.Serialization.PrintTicketLevel,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#CancelAsync()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#CloneSourcePrintTicket(System.Object,System.Windows.Xps.Serialization.XpsSerializationPrintTicketRequiredEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#CreateXPSDocument(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#EndWrite(System.Boolean,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#ForwardProgressChangedEvent(System.Object,System.Windows.Xps.Serialization.XpsSerializationProgressChangedEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#ForwardUserPrintTicket(System.Object,System.Windows.Xps.Serialization.XpsSerializationPrintTicketRequiredEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#ForwardWriteCompletedEvent(System.Object,System.Windows.Xps.Serialization.XpsSerializationCompletedEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#MxdwConversionRequired(System.Printing.PrintQueue)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#MxdwInitializeOptimizationConversion(System.Printing.PrintQueue)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#SaveAsXaml(System.Object,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#SetCompletionEventHandler(System.Windows.Xps.Serialization.PackageSerializationManager,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#SetPrintTicketEventHandler(System.Windows.Xps.Serialization.PackageSerializationManager,System.Windows.Xps.Serialization.XpsSerializationPrintTicketRequiredEventHandler)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#SetProgressChangedEventHandler(System.Windows.Xps.Serialization.PackageSerializationManager,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.String,System.Windows.Xps.XpsDocumentNotificationLevel)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.String,System.Windows.Xps.XpsDocumentNotificationLevel)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.DocumentPaginator)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.DocumentPaginator,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocument)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocument,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocumentSequence)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocumentSequence,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedPage)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedPage,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Media.Visual)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Media.Visual,System.Printing.PrintTicket)");

//**************************************************************************************************************************************************************
// Bug ID:    828208 Dev10 bugs
// Developer: [....]
// Reason: Methods are annotated with the new [SecuritySafeCritical] which replaces [SecurityCritical, SecurityTreatAsSafe]
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIBitmap.#ColorReduction()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIBitmap.#IsValid()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIBitmap.#Load(System.Windows.Media.Imaging.BitmapSource,System.Byte[],System.Windows.Media.PixelFormat)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIBitmap.#SetBits(tagBITMAPINFO{modopt(System.Runtime.CompilerServices.IsExplicitlyDereferenced)}&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIBitmap.#StretchBlt(Microsoft.Internal.GDIExporter.CGDIDevice,{modopt(System.Runtime.CompilerServices.IsConst)}System.Windows.Int32Rect{modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}*,System.Boolean,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIDevice.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIDevice.#GetDCOrgEx(tagPOINT{modopt(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}*)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#CreateFontW(System.Windows.Media.GlyphRun,System.Double,System.Double)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#DrawBitmap(System.Windows.Media.Imaging.BitmapSource,System.Byte[],System.Windows.Rect)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#DrawBitmap_PassThrough(System.Windows.Media.Imaging.BitmapSource,System.Windows.Int32Rect&,System.Int32,System.Int32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#FillLinearGradient({modreq(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}Microsoft.Internal.GDIExporter.GeometryProxy,System.Windows.Media.Brush)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#Initialize()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#PushClipProxy({modreq(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}Microsoft.Internal.GDIExporter.GeometryProxy)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#RasterizeShape({modreq(System.Runtime.CompilerServices.IsImplicitlyDereferenced)}Microsoft.Internal.GDIExporter.GeometryProxy,System.Windows.Int32Rect&,System.Windows.Media.Brush)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#SetTextWorldTransform(tagXFORM&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="Microsoft.Internal.GDIExporter.GdiFontResourceSafeHandle.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.AttributeValueInteropHandler.#.cctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.EnumDataThunkObject.#GetPrintSystemValuesPerPrintJobs(System.Printing.PrintQueue,System.Collections.Generic.Queue`1<System.Printing.PrintSystemJobInfo>,System.String[],System.UInt32,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.EnumDataThunkObject.#GetPrintSystemValuesPerPrintQueues(System.Printing.PrintServer,System.Printing.EnumeratedPrintQueueTypes[],System.Collections.Generic.Queue`1<System.Printing.PrintQueue>,System.String[])");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterDefaults.#!PrinterDefaults()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterDefaults.#~PrinterDefaults()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterDefaults.#Dispose(System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterInfoEightSafeMemoryHandle.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterInfoNineSafeMemoryHandle.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterInfoOneSafeMemoryHandle.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterInfoSevenSafeMemoryHandle.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterInfoSixSafeMemoryHandle.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterInfoThreeSafeMemoryHandle.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#GetLocalMachineName()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#GetManagedJobInfoObject(System.UInt32,MS.Internal.PrintWin32Thunk.SafeMemoryHandle,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#GetManagedPrinterInfoObject(System.UInt32,MS.Internal.PrintWin32Thunk.SafeMemoryHandle,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkAbortPrinter()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkAddPrinter(System.String,MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoSetter)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkAddPrinter(System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.String,System.Int32,System.Int32,System.Int32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkAddPrinterConnection(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkClosePrinter()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkCloseSpoolFileHandle(System.IntPtr)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkCommitSpoolData(System.IntPtr,System.Int32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkDeletePrinter()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkDeletePrinterConnection(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkDocumentEvent(System.Windows.Xps.Serialization.XpsDocumentEventType,System.UInt32,System.Runtime.InteropServices.SafeHandle,System.UInt32,MS.Internal.PrintWin32Thunk.SafeMemoryHandle)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkDocumentEventPrintTicket(System.Windows.Xps.Serialization.XpsDocumentEventType,System.Windows.Xps.Serialization.XpsDocumentEventType,System.Runtime.InteropServices.SafeHandle,System.IO.MemoryStream&)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkEndDocPrinter()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkEndPagePrinter()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkEnumJobs(System.UInt32,System.UInt32,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkEnumPrinters(System.String,System.UInt32,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkGetDefaultPrinter()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkGetDriver(System.UInt32,System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkGetJob(System.UInt32,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkGetPrinter(System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkGetPrinterDataInt32Internal(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkGetPrinterDataStringInternal(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkGetSpoolFileHandle()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkIsMetroDriverEnabled()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkOpenPrinter(System.String,MS.Internal.PrintWin32Thunk.PrinterDefaults)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkReportJobProgress(System.Int32,System.Printing.JobOperation,System.Windows.Xps.Packaging.PackagingAction)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkSetDefaultPrinter(System.String)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkSetJob(System.UInt32,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkSetPrinter(System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkSetPrinter(System.UInt32,MS.Internal.PrintWin32Thunk.SafeMemoryHandle)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkSetPrinterDataInt32Internal(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkSetPrinterDataStringInternal(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkStartDocPrinter(MS.Internal.PrintWin32Thunk.DocInfoThree)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkStartPagePrinter()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.PropertyCollectionMemorySafeHandle.#AllocPropertyCollectionMemorySafeHandle(System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.SafeMemoryHandle.#get_Null()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForJob.JobInfoOne.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForJob.JobInfoOne.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForJob.JobInfoTwo.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForJob.JobInfoTwo.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoEight.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoEight.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Media.Visual,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Media.Visual)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedPage,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedPage)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocumentSequence,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocumentSequence)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocument,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.FixedDocument)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.DocumentPaginator,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#WriteAsync(System.Windows.Documents.DocumentPaginator)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Media.Visual,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Media.Visual)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.FixedPage,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.FixedPage)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.FixedDocumentSequence,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.FixedDocumentSequence)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.FixedDocument,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.FixedDocument)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.DocumentPaginator,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#Write(System.Windows.Documents.DocumentPaginator)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#CreateVisualsCollator(System.Printing.PrintTicket,System.Printing.PrintTicket)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#CreateVisualsCollator()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#BeginWrite(System.Boolean,System.Boolean,System.Boolean,System.Printing.PrintTicket,System.Windows.Xps.Serialization.PrintTicketLevel,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#WriteVisual(System.Boolean,System.Printing.PrintTicket,System.Windows.Xps.Serialization.PrintTicketLevel,System.Windows.Media.Visual)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#EndBatchWrite()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintSystemJobInfo.#.ctor(System.Printing.PrintQueue,System.String,System.String,System.Boolean)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#IsXpsDocumentEventSupported(System.Windows.Xps.Serialization.XpsDocumentEventType)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedPagePrintTicket(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedPageEvent(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentSequencePrintTicket(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentSequenceEvent(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentPrintTicket(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#ForwardXpsFixedDocumentEvent(System.Windows.Xps.Serialization.XpsSerializationXpsDriverDocEventArgs)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="System.Printing.PrintQueue.#Commit()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoSetter.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoSetter.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoSetter.#.ctor(MS.Internal.PrintWin32Thunk.PrinterThunkHandler)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoSetter.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoGetter.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoTwoGetter.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoThree.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoThree.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSix.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSix.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSeven.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSeven.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSeven.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoSeven.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoOne.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoOne.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoOne.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoNine.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoNine.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoNine.#.ctor()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFourSetter.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFourSetter.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFourGetter.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFourGetter.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFiveSetter.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFiveSetter.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFiveGetter.#RegisterAttributeMaps()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoFiveGetter.#GetValueFromName(System.String,System.UInt32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2129:SecurityTransparentCodeShouldNotReferenceNonpublicSecurityCriticalCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.DirectInteropForPrintQueue.PrinterInfoEight.#SetValueFromName(System.String,System.Object)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2128:SecurityTransparentCodeShouldNotAssert", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIRenderTarget.#DrawBitmap_PassThrough(System.Windows.Media.Imaging.BitmapSource,System.Windows.Int32Rect&,System.Int32,System.Int32)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2128:SecurityTransparentCodeShouldNotAssert", Scope="member", Target="MS.Internal.PrintWin32Thunk.PrinterThunkHandler.#ThunkIsMetroDriverEnabled()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2128:SecurityTransparentCodeShouldNotAssert", Scope="member", Target="System.Printing.PrintQueue.#XpsDocumentEventCancel()");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2128:SecurityTransparentCodeShouldNotAssert", Scope="member", Target="System.Windows.Xps.VisualsToXpsDocument.#MxdwConversionRequired(System.Printing.PrintQueue)");
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2128:SecurityTransparentCodeShouldNotAssert", Scope="member", Target="System.Windows.Xps.XpsDocumentWriter.#MxdwConversionRequired(System.Printing.PrintQueue)");

//**************************************************************************************************************************************************************
// Bug ID:    789126 Dev10 bugs
// Developer: [....]
// Reason: The compiler has inlined all calls to MS.Internal.PrintWin32Thunk.AttributeValueInteropHandler.#AllocateUnmanagedPrintPropertiesCollection(System.Printing.IndexedProperties.PrintPropertyDictionary)
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Performance","CA1811:AvoidUncalledPrivateCode", Scope="member", Target="MS.Internal.PrintWin32Thunk.AttributeValueInteropHandler.#AllocateUnmanagedPrintPropertiesCollection(System.Printing.IndexedProperties.PrintPropertyDictionary)");

// New suppressions since V4 RTM:

//**************************************************************************************************************************
// We have an equivalent full demand in place on this method since LinkDemands are no longer allowed.
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2123:OverrideLinkDemandsShouldBeIdenticalToBase", Scope="member", Target="System.Printing.IndexedProperties.PrintPropertyDictionary.#GetObjectData(System.Runtime.Serialization.SerializationInfo,System.Runtime.Serialization.StreamingContext)");


//**************************************************************************************************************************
// HrEndPage is marked as SecurityCritical in the header, so this should be OK.  It is also only used in DBG builds.
//**************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="Microsoft.Internal.GDIExporter.CGDIDevice.#HrEndPage()");


//**************************************************************************************************************************************************************
// Bug ID:    402890 Dev11 bugs
// Developer: [....]
// Reason: System.Printing.dll depends on types in non-APTCA ReachFramework.dll - all usage has been reviewed by Security team
//**************************************************************************************************************************************************************
CA_GLOBAL_SUPPRESS_MESSAGE("Microsoft.Security","CA2116:AptcaMethodsShouldOnlyCallAptcaMethods", Scope="member", Target="MS.Internal.PrintWin32Thunk.XpsDeviceSimulatingPrintThunkHandler.#ThunkStartDocPrinter(MS.Internal.PrintWin32Thunk.DocInfoThree,System.Printing.PrintTicket)");

