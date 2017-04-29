#pragma once

#define START_USER_HRESULT_IDS  0x0200
#define E_VSCOCREATE_CUST_ERROR( id ) MAKE_HRESULT( 1, FACILITY_ITF, (id) + START_USER_HRESULT_IDS )

//VSW-491950
//Choose a high value for our error code to help avoid conflicts with existing debugger errors.
#define E_VSCOCREATE_COLOADER_UNAVAILABLE E_VSCOCREATE_CUST_ERROR(0x00010000)

#ifdef __cplusplus

HRESULT
GetModulePath (
    REFCLSID rclsid,
    BSTR* pbstrModule
    );

template <class Interface>
HRESULT
VsLoaderCoCreateInstance(
    REFCLSID rclsid, 
    LPUNKNOWN pUnkOuter,
    DWORD dwClsContext, 
    REFIID riid, 
    Interface** ppInterface
    )
{

    HRESULT hr = NOERROR;
    IUnknown* punk = NULL;

    if (FAILED (hr = VsLoaderCoCreateInstanceUnknown(
        rclsid, 
        pUnkOuter,
        dwClsContext, 
        &punk
        )))
    {
        goto Error;
    }

    if (FAILED (hr = punk->QueryInterface (riid, (LPVOID*)ppInterface)))
    {
        goto Error;
    }

Error:

    if (punk)
    {
	    punk->Release();
    }

    return hr;
}

//
// To match ATL::CComPtr<T>::CoCreateInstance
//

template <class Interface>
HRESULT
VsLoaderCoCreateInstance(
    REFCLSID rclsid,
    Interface** ppInterface,
    LPUNKNOWN pUnkOuter = NULL, 
    DWORD dwClsContext = CLSCTX_ALL
    )
{
#ifdef DEBUG
    //
    // Make sure ppInterface is an IUnknown
    //
    static_cast <IUnknown*> (*ppInterface);
#endif

    return VsLoaderCoCreateInstance<Interface> (
        rclsid, 
        pUnkOuter, 
        dwClsContext, 
        __uuidof (Interface), 
        ppInterface
        );
}

//
// To use on an ATL::CComPtr<T>
//
template <class Interface>
HRESULT
VsLoaderCoCreateInstance(
    REFCLSID rclsid,
    CComPtr<Interface>** ppInterface,
    LPUNKNOWN pUnkOuter = NULL, 
    DWORD dwClsContext = CLSCTX_ALL
    )
{
    return VsLoaderCoCreateInstance<Interface> (
        rclsid,
        pUnkOuter,
        dwClsContext,
        __uuidof (Interface),
        &((*ppInterface)->p)
        );
}

#endif // _cplusplus

#ifdef __cplusplus
#define VSCOCO_CLINKAGE	extern "C"
#else
#define VSCOCO_CLINKAGE
#endif

//
// Always returns an IUnknown*
//
VSCOCO_CLINKAGE HRESULT
_cdecl
VsLoaderCoCreateInstanceUnknown(
    REFCLSID rclsid, 
    LPUNKNOWN pUnkOuter,
    DWORD dwClsContext, 
    IUnknown** ppunk
    );

VSCOCO_CLINKAGE HRESULT
_cdecl
VsCoCreateManagedObject
(
    /* [in] */ LPCWSTR codeBase,
    /* [in] */ LPCWSTR assemblyName,
    /* [in] */ LPCWSTR typeName,
    /* [in] */ REFIID riid,
    /* [out] */ void** ppvObj
    );

VSCOCO_CLINKAGE HRESULT
_cdecl
VsCoCreateAggregatedManagedObject
(
    /* [in] */ LPCWSTR codeBase,
    /* [in] */ LPCWSTR assemblyName,
    /* [in] */ LPCWSTR typeName,
    /* [in] */ LPUNKNOWN pUnkOuter,
    /* [in] */ REFIID riid,
    /* [out] */ void** ppvObj
    );

//-----------------------------------------------------------------------------
// Get the class object (factory) for the given locally-registered COM object
// If the object is managed, then REFIID must be IID_IClassFactory.
// If the class object cannot be found in the local registry, this will fail.
// It will not fall-back to the global registry.
// This does not respect ThreadingModel.
//-----------------------------------------------------------------------------
STDMETHODIMP VsGetLocalClassObjectOfClsid( 
    /*[in]*/ REFCLSID clsid,        // CLSID of COM object
    /*[in]*/ DWORD dwClsCtx,         // This must be INPROC_SERVER
    /*[in]*/ REFIID riid,           // IID_IClassFactory or IID_IPSFactoryBuffer
    /*[out]*/ void** ppvClassObject // Interface of class object
    );

//-----------------------------------------------------------------------------
// Get the class object (factory) for the given managed object
// The interface (riid) must be IClassFactory
// Only one of the codeBase or assemblyName need be specified. If both are
// specified, then codeBase is used.
// The typeName must be specified.
//-----------------------------------------------------------------------------
STDMETHODIMP VsGetLocalClassObjectOfManagedClass(
    /* [in,opt] */ LPCWSTR codeBase,
    /* [in,opt] */ LPCWSTR assemblyName,
    /* [in] */ LPCWSTR typeName,
    /* [in] */ REFIID riid,
    /* [out] */ void** ppvClassObject
    );

//-----------------------------------------------------------------------------
// If the CLSID is registered locally, then CoCreate it and register it with
// COM. This allows subsequent calls to CoCreateInstance to succeed even though
// the CLSID isn't registered under HKEY_CLASSES_ROOT\CLSID
//-----------------------------------------------------------------------------
STDMETHODIMP VsRegisterLocalClassObject( REFCLSID rclsid, /*out*/ DWORD* pdwCookie );

//-----------------------------------------------------------------------------
// Revoke a previously registered class object
//-----------------------------------------------------------------------------
STDMETHODIMP VsRevokeLocalClassObject( DWORD dwCookie );

//-----------------------------------------------------------------------------
// If the interface is registered locally, then look for a local proxy-stub
// and, if there is one, then register it with COM.
// You should call this on each thread that will need to marshal the given
// interface. The best place to do this is just after CoInitialize.
// The riid may be GUID_NULL, in which case we will attempt to register all
// interfaces with listed under HKLM\<regroot>\Interface but note that this
// can be expensive if many local interfaces are registered.
//-----------------------------------------------------------------------------
STDMETHODIMP VsRegisterLocalProxy( REFIID riid );

//-----------------------------------------------------------------------------
// Unloads any unused DLLs. For each DLL that was loaded via VsCoCreate,
// VsRegisterLocalClassObject, VsRegisterLocalProxy or
// VsGetLocalClassObjectOfClsid we will call DllCanUnloadNow. If that returns
// S_OK, then we will free that library so that it can be unloaded.
//-----------------------------------------------------------------------------
STDMETHODIMP VsFreeUnusedLocalLibraries();

VSCOCO_CLINKAGE HRESULT
_cdecl
VsCoMarshalInterThreadInterfaceInStream( REFIID riid, IUnknown* punk, LPSTREAM* ppStm );

VSCOCO_CLINKAGE HRESULT
_cdecl
VsCoGetInterfaceAndReleaseStream( LPSTREAM pStm, REFIID riid, LPVOID * ppv );


VSCOCO_CLINKAGE void
_cdecl
VsLoaderSetRegistryRootA(
    LPCSTR sz
    );

VSCOCO_CLINKAGE void
_cdecl
VsLoaderSetRegistryRootW(
    LPCWSTR sz
    );

#ifdef _UNICODE
#define VsLoaderSetRegistryRoot VsLoaderSetRegistryRootW
#else // UNICODE
#define VsLoaderSetRegistryRoot VsLoaderSetRegistryRootA
#endif

