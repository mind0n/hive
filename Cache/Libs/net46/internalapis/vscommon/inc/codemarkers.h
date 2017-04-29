// Code Markers for Visual Studio

#pragma once

// Don't place code marker values in here. Put them in Microsoft.Internal.Performance.CodeMarkerValues.h instead
// For C++ we include all code markers and the App enumeration
#define Codemarkers_IncludeAppEnum 1
#define Codemarkers_IncludeAllMarkers 1

__interface IStream; // Forward declaring instead of including Objidl.h

#include "Microsoft.Internal.Performance.CodeMarkerValues.h"

using namespace Microsoft::Internal::Performance;

// APIs provided by the CodeMarkers static LIB

// Initialize Code Markers. You must call this early on in WinMain. If Code Markers are
// enabled (via a registry setting) this will load the requested DLL
// param nApp:
//    One of the CodeMarkerApp IDs defined in CodeMarkerValues.txt
// NOTE: Do not call this function from a DLL entry point (DllMain) because it might end up
// calling LoadLibrary. Instead, use AttachPerformanceDLL to attach a newly loaded DLL to
// an already loaded Code Marker DLL.
// param szRegRoot:
// The key "Performance" will be appended to this root and the {Default} value
// for  that key will contain the name of the DLL to be loaded.
// e.g. If szRegRoot is "SOFTWARE\Microsoft\Foo\1.0", the {Default} value under
// HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Foo\1.0\Performance" will be examined.
// For CodeMarkers to be enabled for this process, the key must exist and the
// Default value must equal "Microsoft.Internal.Performance.CodeMarkers.dll"
void __stdcall InitPerformanceDLL(CodeMarkerApp nApp, LPCWSTR szRegRoot);

// InitPerformanceDllEx provides applications like Rascal ability to enable code markers
// without a registry key entry. This is different from InitPerformanceDLL as 
// InitPeformanceDll will enable only if Registry key is present.
void __stdcall InitPerformanceDLLEx(CodeMarkerApp nApp, LPCWSTR szRegRoot, bool fEnableByDefault, CodeMarkerEvent nStopTimerId);

// Attach a newly-loaded DLL to an already-loaded code markers DLL. This is safe to call from
// within your DLL entry point. See also DetachPerformanceDLL. Note that if your application
// hasn't already called InitializePerformanceDLL, this will fail.
// Note: Most code doesn't need to call this directly because an automatic Attach will
// occur when the first CodeMarker from this DLL is fired. This means that the first
// CodeMarker can incur a slight additional perf cost to do the auto-attach. If you care about
// that, then you can explicitly call AttachPerformanceDLL in DllMain
void __stdcall AttachPerformanceDLL();

// Fire a Code Marker event
// Note that there is a small but measurable performance hit for firing code markers.
// Therefore you do not want to fire CodeMarkers from inside tight loops.
void __stdcall CodeMarker(CodeMarkerEvent nID);

// Write logged data to a stream and stop further logging.
HRESULT __stdcall WriteCodeMarkerLog(_In_ IStream* pStream);

// Start Code Marker logging.
HRESULT __stdcall BeginCodeMarkerLogging(CodeMarkerEvent nID);

// Fire a Code Marker event with a user-defined parameter block. Note: The interpretation of the
// buffer is specific to the event ID.
// SECURITY: Be cautious of exposing user-specific or private data (such as filenames)
// through this API. This is not meant as a general purpose logging mechanism. Please consider
// using ActivityLog or SQM for that.
void __stdcall CodeMarkerEx(CodeMarkerEvent nID, const void* pUserParams, size_t cbUserParams);

template<typename T> void CodeMarkerEx(CodeMarkerEvent nID, const T& user)
{
    CodeMarkerEx(nID, &user, sizeof(T));
}

// Fire a Code Marker event with a user-defined parameter block which is a null-terminated Unicode
// string
void __stdcall CodeMarkerEx(CodeMarkerEvent nID, const LPCWSTR exData);

// Uninitialize Code Markers. Call this on exit from WinMain to unload any loaded DLL
void __stdcall UninitializePerformanceDLL(CodeMarkerApp nApp);

// Equivalent of UnitializePerformanceDLL for DLL entry points.
void __stdcall DetachPerformanceDLL(void);

// check to see if code marker logging is enabled.
bool __stdcall IsCodeMarkerLoggingEnabled();

//Utility class that fires a code marker event in the constructor and a different event
//in the destructor. Use it to fire code markers when entering and exiting a scope.
template<CodeMarkerEvent markerStart, CodeMarkerEvent markerEnd> class CCodeMarkerStartEnd
{
public:
    CCodeMarkerStartEnd(void)
    {
        CodeMarker( markerStart );
    }

    ~CCodeMarkerStartEnd(void)
    {
        CodeMarker( markerEnd );
    }
};

//Utility class that fires a code marker event in the constructor and a different event
//in the destructor. Use it to fire code markers when entering and exiting a scope.
template<CodeMarkerEvent markerStart, CodeMarkerEvent markerEnd, typename T> class CCodeMarkerExStartEnd
{
private:
    const T& m_data;

public:
    CCodeMarkerExStartEnd(const T& data) : m_data(data)
    {
        CodeMarkerEx( markerStart, &m_data, sizeof(m_data) );
    }

    ~CCodeMarkerExStartEnd(void)
    {
        CodeMarkerEx( markerEnd, &m_data, sizeof(m_data) );
    }
};

// Partial specialization for LPCWSTR
template<CodeMarkerEvent markerStart, CodeMarkerEvent markerEnd> class CCodeMarkerExStartEnd <markerStart, markerEnd, LPCWSTR>
{
private:
    LPCWSTR &m_data;

public:
    CCodeMarkerExStartEnd(LPCWSTR& data) : m_data(data)
    {
        CodeMarkerEx( markerStart, m_data);
    }

    ~CCodeMarkerExStartEnd(void)
    {
        CodeMarkerEx( markerEnd, m_data);
    }
};

// Utility class that initializes CodeMarkers in the constructor and
// uninitializes in the destructor. Use this within your WinMain routine.
template<
    CodeMarkerApp app,
    CodeMarkerEvent markerFirst = perfBootStart,
    CodeMarkerEvent markerFinal = perfExitEnd
>
class CCodeMarkersApplication
{
public:
    // Default constructor does nothing. You must call Init.
    CCodeMarkersApplication()
    {
    }

    CCodeMarkersApplication( LPCWSTR szRegRoot, bool fEnableByDefault = false, CodeMarkerEvent nStopTimerId = static_cast<CodeMarkerEvent>( 0 ))
    {
        Init( szRegRoot, fEnableByDefault, nStopTimerId );
    }

    ~CCodeMarkersApplication()
    {
        // Safe to call, even if Initialize has never been called
        CodeMarker( markerFinal );
        UninitializePerformanceDLL( app );
    }

    void Init( LPCWSTR szRegRoot, bool fEnableByDefault = false, CodeMarkerEvent nStopTimerId = static_cast<CodeMarkerEvent>( 0 ) )
    {
        // Safe to call multiple times (although, not recommended)
        InitPerformanceDLLEx( app, szRegRoot, fEnableByDefault, nStopTimerId );
        CodeMarker( markerFirst );
    }
};
