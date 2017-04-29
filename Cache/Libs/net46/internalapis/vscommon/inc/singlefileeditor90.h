

/* this ALWAYS GENERATED file contains the definitions for the interfaces */


 /* File created by MIDL compiler version 8.00.0601 */
/* @@MIDL_FILE_HEADING(  ) */

#pragma warning( disable: 4049 )  /* more than 64k source lines */


/* verify that the <rpcndr.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCNDR_H_VERSION__
#define __REQUIRED_RPCNDR_H_VERSION__ 475
#endif

/* verify that the <rpcsal.h> version is high enough to compile this file*/
#ifndef __REQUIRED_RPCSAL_H_VERSION__
#define __REQUIRED_RPCSAL_H_VERSION__ 100
#endif

#include "rpc.h"
#include "rpcndr.h"

#ifndef __RPCNDR_H_VERSION__
#error this stub requires an updated version of <rpcndr.h>
#endif // __RPCNDR_H_VERSION__

#ifndef COM_NO_WINDOWS_H
#include "windows.h"
#include "ole2.h"
#endif /*COM_NO_WINDOWS_H*/

#ifndef __singlefileeditor90_h__
#define __singlefileeditor90_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IVsContainedLanguageHost2_FWD_DEFINED__
#define __IVsContainedLanguageHost2_FWD_DEFINED__
typedef interface IVsContainedLanguageHost2 IVsContainedLanguageHost2;

#endif 	/* __IVsContainedLanguageHost2_FWD_DEFINED__ */


#ifndef __IVsContainedLanguage2_FWD_DEFINED__
#define __IVsContainedLanguage2_FWD_DEFINED__
typedef interface IVsContainedLanguage2 IVsContainedLanguage2;

#endif 	/* __IVsContainedLanguage2_FWD_DEFINED__ */


#ifndef __IVsProjectIntellisenseEvents_FWD_DEFINED__
#define __IVsProjectIntellisenseEvents_FWD_DEFINED__
typedef interface IVsProjectIntellisenseEvents IVsProjectIntellisenseEvents;

#endif 	/* __IVsProjectIntellisenseEvents_FWD_DEFINED__ */


#ifndef __IVsProjectIntellisenseSupportSvc_FWD_DEFINED__
#define __IVsProjectIntellisenseSupportSvc_FWD_DEFINED__
typedef interface IVsProjectIntellisenseSupportSvc IVsProjectIntellisenseSupportSvc;

#endif 	/* __IVsProjectIntellisenseSupportSvc_FWD_DEFINED__ */


#ifndef __IVsIntellisenseProjectEventSink2_FWD_DEFINED__
#define __IVsIntellisenseProjectEventSink2_FWD_DEFINED__
typedef interface IVsIntellisenseProjectEventSink2 IVsIntellisenseProjectEventSink2;

#endif 	/* __IVsIntellisenseProjectEventSink2_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "oleidl.h"
#include "textmgr.h"
#include "textmgr2.h"
#include "vsshell.h"
#include "containedlanguage.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_singlefileeditor90_0000_0000 */
/* [local] */ 

typedef 
enum _CONTAINEDLANGUAGECOMPILERREADY
    {
        CLCR_INITIALIZED	= 0,
        CLCR_DECLARED	= ( CLCR_INITIALIZED + 1 ) ,
        CLCR_BOUND	= ( CLCR_DECLARED + 1 ) 
    } 	ContainedLanguageCompilerReady;



extern RPC_IF_HANDLE __MIDL_itf_singlefileeditor90_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_singlefileeditor90_0000_0000_v0_0_s_ifspec;

#ifndef __IVsContainedLanguageHost2_INTERFACE_DEFINED__
#define __IVsContainedLanguageHost2_INTERFACE_DEFINED__

/* interface IVsContainedLanguageHost2 */
/* [object][uuid] */ 


EXTERN_C const IID IID_IVsContainedLanguageHost2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("F2BDA2B3-FF1E-45f1-876A-6591840D2EED")
    IVsContainedLanguageHost2 : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetLineIndent( 
            /* [in] */ long lLineNumber,
            /* [in] */ long lColNumber,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrIndentString,
            /* [out] */ __RPC__out long *plParentIndentLevel,
            /* [out] */ __RPC__out long *plIndentSize,
            /* [out] */ __RPC__out BOOL *pfTabs,
            /* [out] */ __RPC__out long *plTabSize) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE OnCompilerReady( 
            ContainedLanguageCompilerReady clcr) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsContainedLanguageHost2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsContainedLanguageHost2 * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsContainedLanguageHost2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsContainedLanguageHost2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetLineIndent )( 
            __RPC__in IVsContainedLanguageHost2 * This,
            /* [in] */ long lLineNumber,
            /* [in] */ long lColNumber,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrIndentString,
            /* [out] */ __RPC__out long *plParentIndentLevel,
            /* [out] */ __RPC__out long *plIndentSize,
            /* [out] */ __RPC__out BOOL *pfTabs,
            /* [out] */ __RPC__out long *plTabSize);
        
        HRESULT ( STDMETHODCALLTYPE *OnCompilerReady )( 
            __RPC__in IVsContainedLanguageHost2 * This,
            ContainedLanguageCompilerReady clcr);
        
        END_INTERFACE
    } IVsContainedLanguageHost2Vtbl;

    interface IVsContainedLanguageHost2
    {
        CONST_VTBL struct IVsContainedLanguageHost2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsContainedLanguageHost2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsContainedLanguageHost2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsContainedLanguageHost2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsContainedLanguageHost2_GetLineIndent(This,lLineNumber,lColNumber,pbstrIndentString,plParentIndentLevel,plIndentSize,pfTabs,plTabSize)	\
    ( (This)->lpVtbl -> GetLineIndent(This,lLineNumber,lColNumber,pbstrIndentString,plParentIndentLevel,plIndentSize,pfTabs,plTabSize) ) 

#define IVsContainedLanguageHost2_OnCompilerReady(This,clcr)	\
    ( (This)->lpVtbl -> OnCompilerReady(This,clcr) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsContainedLanguageHost2_INTERFACE_DEFINED__ */


#ifndef __IVsContainedLanguage2_INTERFACE_DEFINED__
#define __IVsContainedLanguage2_INTERFACE_DEFINED__

/* interface IVsContainedLanguage2 */
/* [object][uuid] */ 


EXTERN_C const IID IID_IVsContainedLanguage2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("4DA92F3A-3552-4a80-B14E-3EBAE25C5D02")
    IVsContainedLanguage2 : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE SupportsOnCompilerReady( 
            __RPC__in BOOL *pfSupportsOnCompilerReady) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsContainedLanguage2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsContainedLanguage2 * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsContainedLanguage2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsContainedLanguage2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *SupportsOnCompilerReady )( 
            __RPC__in IVsContainedLanguage2 * This,
            __RPC__in BOOL *pfSupportsOnCompilerReady);
        
        END_INTERFACE
    } IVsContainedLanguage2Vtbl;

    interface IVsContainedLanguage2
    {
        CONST_VTBL struct IVsContainedLanguage2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsContainedLanguage2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsContainedLanguage2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsContainedLanguage2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsContainedLanguage2_SupportsOnCompilerReady(This,pfSupportsOnCompilerReady)	\
    ( (This)->lpVtbl -> SupportsOnCompilerReady(This,pfSupportsOnCompilerReady) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsContainedLanguage2_INTERFACE_DEFINED__ */


#ifndef __IVsProjectIntellisenseEvents_INTERFACE_DEFINED__
#define __IVsProjectIntellisenseEvents_INTERFACE_DEFINED__

/* interface IVsProjectIntellisenseEvents */
/* [uuid][object] */ 


EXTERN_C const IID IID_IVsProjectIntellisenseEvents;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("0FEF491A-8F4A-4198-88BE-24BD341907E1")
    IVsProjectIntellisenseEvents : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE OnProjectReferenceChange( 
            /* [in] */ DWORD dwChangeType,
            __RPC__in LPCWSTR pszAssemblyPath) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE OnConfigFileChange( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsProjectIntellisenseEventsVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsProjectIntellisenseEvents * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsProjectIntellisenseEvents * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsProjectIntellisenseEvents * This);
        
        HRESULT ( STDMETHODCALLTYPE *OnProjectReferenceChange )( 
            __RPC__in IVsProjectIntellisenseEvents * This,
            /* [in] */ DWORD dwChangeType,
            __RPC__in LPCWSTR pszAssemblyPath);
        
        HRESULT ( STDMETHODCALLTYPE *OnConfigFileChange )( 
            __RPC__in IVsProjectIntellisenseEvents * This);
        
        END_INTERFACE
    } IVsProjectIntellisenseEventsVtbl;

    interface IVsProjectIntellisenseEvents
    {
        CONST_VTBL struct IVsProjectIntellisenseEventsVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsProjectIntellisenseEvents_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsProjectIntellisenseEvents_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsProjectIntellisenseEvents_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsProjectIntellisenseEvents_OnProjectReferenceChange(This,dwChangeType,pszAssemblyPath)	\
    ( (This)->lpVtbl -> OnProjectReferenceChange(This,dwChangeType,pszAssemblyPath) ) 

#define IVsProjectIntellisenseEvents_OnConfigFileChange(This)	\
    ( (This)->lpVtbl -> OnConfigFileChange(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsProjectIntellisenseEvents_INTERFACE_DEFINED__ */


#ifndef __IVsProjectIntellisenseSupportSvc_INTERFACE_DEFINED__
#define __IVsProjectIntellisenseSupportSvc_INTERFACE_DEFINED__

/* interface IVsProjectIntellisenseSupportSvc */
/* [uuid][object] */ 


EXTERN_C const IID IID_IVsProjectIntellisenseSupportSvc;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("1BF5F81B-6FFC-41d6-8136-389E696947A6")
    IVsProjectIntellisenseSupportSvc : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE AdviseProjectIntellisenseEvents( 
            /* [in] */ __RPC__in_opt IVsProjectIntellisenseEvents *pSink,
            /* [out] */ __RPC__out VSCOOKIE *pdwCookie) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE UnadviseProjectIntellisenseEvents( 
            /* [in] */ VSCOOKIE dwCookie) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetProjectTypeResolutionService( 
            /* [out] */ __RPC__deref_out_opt IUnknown **ppTRS) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsProjectIntellisenseSupportSvcVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsProjectIntellisenseSupportSvc * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsProjectIntellisenseSupportSvc * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsProjectIntellisenseSupportSvc * This);
        
        HRESULT ( STDMETHODCALLTYPE *AdviseProjectIntellisenseEvents )( 
            __RPC__in IVsProjectIntellisenseSupportSvc * This,
            /* [in] */ __RPC__in_opt IVsProjectIntellisenseEvents *pSink,
            /* [out] */ __RPC__out VSCOOKIE *pdwCookie);
        
        HRESULT ( STDMETHODCALLTYPE *UnadviseProjectIntellisenseEvents )( 
            __RPC__in IVsProjectIntellisenseSupportSvc * This,
            /* [in] */ VSCOOKIE dwCookie);
        
        HRESULT ( STDMETHODCALLTYPE *GetProjectTypeResolutionService )( 
            __RPC__in IVsProjectIntellisenseSupportSvc * This,
            /* [out] */ __RPC__deref_out_opt IUnknown **ppTRS);
        
        END_INTERFACE
    } IVsProjectIntellisenseSupportSvcVtbl;

    interface IVsProjectIntellisenseSupportSvc
    {
        CONST_VTBL struct IVsProjectIntellisenseSupportSvcVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsProjectIntellisenseSupportSvc_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsProjectIntellisenseSupportSvc_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsProjectIntellisenseSupportSvc_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsProjectIntellisenseSupportSvc_AdviseProjectIntellisenseEvents(This,pSink,pdwCookie)	\
    ( (This)->lpVtbl -> AdviseProjectIntellisenseEvents(This,pSink,pdwCookie) ) 

#define IVsProjectIntellisenseSupportSvc_UnadviseProjectIntellisenseEvents(This,dwCookie)	\
    ( (This)->lpVtbl -> UnadviseProjectIntellisenseEvents(This,dwCookie) ) 

#define IVsProjectIntellisenseSupportSvc_GetProjectTypeResolutionService(This,ppTRS)	\
    ( (This)->lpVtbl -> GetProjectTypeResolutionService(This,ppTRS) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsProjectIntellisenseSupportSvc_INTERFACE_DEFINED__ */


#ifndef __IVsIntellisenseProjectEventSink2_INTERFACE_DEFINED__
#define __IVsIntellisenseProjectEventSink2_INTERFACE_DEFINED__

/* interface IVsIntellisenseProjectEventSink2 */
/* [uuid][object] */ 


EXTERN_C const IID IID_IVsIntellisenseProjectEventSink2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("07ECC139-FD14-44a9-AAB7-823D1F7DB974")
    IVsIntellisenseProjectEventSink2 : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE OnBeforeWaitForEvent( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE OnAfterWaitForEvent( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE OnOpenHidden( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsIntellisenseProjectEventSink2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsIntellisenseProjectEventSink2 * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsIntellisenseProjectEventSink2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsIntellisenseProjectEventSink2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *OnBeforeWaitForEvent )( 
            __RPC__in IVsIntellisenseProjectEventSink2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *OnAfterWaitForEvent )( 
            __RPC__in IVsIntellisenseProjectEventSink2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *OnOpenHidden )( 
            __RPC__in IVsIntellisenseProjectEventSink2 * This);
        
        END_INTERFACE
    } IVsIntellisenseProjectEventSink2Vtbl;

    interface IVsIntellisenseProjectEventSink2
    {
        CONST_VTBL struct IVsIntellisenseProjectEventSink2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsIntellisenseProjectEventSink2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsIntellisenseProjectEventSink2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsIntellisenseProjectEventSink2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsIntellisenseProjectEventSink2_OnBeforeWaitForEvent(This)	\
    ( (This)->lpVtbl -> OnBeforeWaitForEvent(This) ) 

#define IVsIntellisenseProjectEventSink2_OnAfterWaitForEvent(This)	\
    ( (This)->lpVtbl -> OnAfterWaitForEvent(This) ) 

#define IVsIntellisenseProjectEventSink2_OnOpenHidden(This)	\
    ( (This)->lpVtbl -> OnOpenHidden(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsIntellisenseProjectEventSink2_INTERFACE_DEFINED__ */


/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     __RPC__in unsigned long *, unsigned long            , __RPC__in BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  __RPC__in unsigned long *, __RPC__inout_xcount(0) unsigned char *, __RPC__in BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(__RPC__in unsigned long *, __RPC__in_xcount(0) unsigned char *, __RPC__out BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     __RPC__in unsigned long *, __RPC__in BSTR * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


