/******************************************************************************
 *
 * File: util_watson.h
 *
 * Author: ahoskins
 *
 * Description: utilities for adding Watson support to a module
 *
 * Exports: 
 *    int  ErrorReportingFilter(struct _EXCEPTION_POINTERS* exceptionInfo)
 *    void ErrorReportingInitialize()
 *
 * Instructions:
 * To add watson support to your executable, use
 *    ErrorReportingFilter(GetExceptionInformation())
 * as your top-level exception filter.  If you wish not to handle breakpoints
 * you should handle that before invoking the filter.
 *
 * When your application loads, call
 *    ErrorReportingInitialize()
 * and use the various ErrorReportingSet*() functions to configure.
 *
 * ErrorReportingSetDlls() -
 *   Pass in a null-separated list of names of DLLs that your application uses 
 *   and would like to report errors for. For example, for cl.exe it's
 *     L"c1.dll\0c1xx.dll\0c2.dll\0mspdb110.dll\0p----b110.dll\0msdis170.dll\0msobj110.dll"
 *   It cannot be more than _MAX_PATH characters.
 *
 * ErrorReportingSetFormalAppName() -
 *   Pass in the the formal description of your application, 
 *   i.e. L"Microsoft\x00AE C/C++ Optimizing Compiler"  The maximum length of this
 *   string is 56 characters.  You can get it from a string resource.
 *
 * ErrorReportingSetExit()
 *   Pass in true if your application wants to just exit if error reporting is done.
 *   Default is true.
 *
 * ErrorReportingSetDebug()
 *   Pass in true if your application wants the "Debug" button to be enable in the 
 *   watson dialog.  Default is false.
 *
 * ErrorReportingSetMode() -
 *   Pass in the ErrorReportingMode enum to control which form of error reporting 
 *   your application will use.  ErrorReportingNone is set by default.
 *   If ErrorReportingSend is specified, check with ErrorReportingCheckOptIn() to
 *   see if the user has not consented to sending reports automatically.  If 
 *   not, emit a diagnostic linking the user to the website
 *   ERRORREPORT_DATA_COLLECTION_POLICY_URL, where he can opt-in to 
 *   ErrorReportingSend.  In this case or if ErrorReportingNone is specified, the
 *   filter will return EXCEPTION_EXECUTE_HANDLER. 
 *
 * Note that you can test these flags as well as ErrorReportingCheckOptIn()
 * in your exception filter before calling ErrorReportingFilter.
 *
 * Also note that invoking Dr. Watson takes up enough stack space such that
 * x86 may not provide enough extra to complete this operation.  If you are
 * interested in collecting data about stack overflows use StackExtendingFilter.
 *
 *****************************************************************************/

#define ERRORREPORT_DATA_COLLECTION_POLICY_URL L"http://go.microsoft.com/fwlink/?LinkId=79513"

// defined in util_watson.cpp
LONG _cdecl ErrorReportingFilter(struct _EXCEPTION_POINTERS* exceptionInfo);
void ErrorReportingInitialize();
bool ErrorReportingCheckOptIn();
// returns true if ErroringReportFilter will return without invoking Watson
bool ErrorReportingDisabled();

typedef enum {
    ErrorReportingNone,
    ErrorReportingPrompt,
    ErrorReportingQueue,
    ErrorReportingSend
} ErrorReportingMode;

ErrorReportingMode ErrorReportingGetMode();
bool ErrorReportingSetMode(ErrorReportingMode ErrorReportingMode);
bool ErrorReportingSetDlls(wchar_t const * szErrorReportingDlls);
bool ErrorReportingSetFormalAppName(wchar_t const * szFormalAppName);
bool ErrorReportingSetExit(bool fExit);
bool ErrorReportingSetDebug(bool fDebug);

typedef LONG _cdecl filterfunc_t(PEXCEPTION_POINTERS);
LONG _cdecl StackExtendingFilter(PEXCEPTION_POINTERS pExceptionInfo, filterfunc_t * filterfunc);
