

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

#ifndef __WebApplicationProject_h__
#define __WebApplicationProject_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IVsWebApplicationProject_FWD_DEFINED__
#define __IVsWebApplicationProject_FWD_DEFINED__
typedef interface IVsWebApplicationProject IVsWebApplicationProject;

#endif 	/* __IVsWebApplicationProject_FWD_DEFINED__ */


#ifndef __IVsWebApplicationProject2_FWD_DEFINED__
#define __IVsWebApplicationProject2_FWD_DEFINED__
typedef interface IVsWebApplicationProject2 IVsWebApplicationProject2;

#endif 	/* __IVsWebApplicationProject2_FWD_DEFINED__ */


#ifndef __IVsCodeBehindEventBinding_FWD_DEFINED__
#define __IVsCodeBehindEventBinding_FWD_DEFINED__
typedef interface IVsCodeBehindEventBinding IVsCodeBehindEventBinding;

#endif 	/* __IVsCodeBehindEventBinding_FWD_DEFINED__ */


#ifndef __IVsCodeBehindEventBinding2_FWD_DEFINED__
#define __IVsCodeBehindEventBinding2_FWD_DEFINED__
typedef interface IVsCodeBehindEventBinding2 IVsCodeBehindEventBinding2;

#endif 	/* __IVsCodeBehindEventBinding2_FWD_DEFINED__ */


#ifndef __IVsCodeEventBinding_FWD_DEFINED__
#define __IVsCodeEventBinding_FWD_DEFINED__
typedef interface IVsCodeEventBinding IVsCodeEventBinding;

#endif 	/* __IVsCodeEventBinding_FWD_DEFINED__ */


#ifndef __IVsCodeBehindCodeGenerator_FWD_DEFINED__
#define __IVsCodeBehindCodeGenerator_FWD_DEFINED__
typedef interface IVsCodeBehindCodeGenerator IVsCodeBehindCodeGenerator;

#endif 	/* __IVsCodeBehindCodeGenerator_FWD_DEFINED__ */


#ifndef __IVsWebApplicationDocument_FWD_DEFINED__
#define __IVsWebApplicationDocument_FWD_DEFINED__
typedef interface IVsWebApplicationDocument IVsWebApplicationDocument;

#endif 	/* __IVsWebApplicationDocument_FWD_DEFINED__ */


/* header files for imported files */
#include "oaidl.h"
#include "ocidl.h"
#include "vsshell.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_WebApplicationProject_0000_0000 */
/* [local] */ 



typedef /* [public][public][uuid] */  DECLSPEC_UUID("3b7eef23-31aa-4c6b-8c40-5f343b558196") 
enum __MIDL___MIDL_itf_WebApplicationProject_0000_0000_0001
    {
        UDC_NoFlags	= 0,
        UDC_Force	= 1,
        UDC_Create	= 2
    } 	UDC_Flags;



extern RPC_IF_HANDLE __MIDL_itf_WebApplicationProject_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_WebApplicationProject_0000_0000_v0_0_s_ifspec;

#ifndef __IVsWebApplicationProject_INTERFACE_DEFINED__
#define __IVsWebApplicationProject_INTERFACE_DEFINED__

/* interface IVsWebApplicationProject */
/* [object][unique][uuid] */ 


EXTERN_C const IID IID_IVsWebApplicationProject;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("6c0237e7-5ce4-4504-bc69-f2f1b7e6db18")
    IVsWebApplicationProject : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetCodeBehindEventBinding( 
            /* [out] */ __RPC__deref_out_opt IVsCodeBehindEventBinding **ppCodeBehindEventBinding) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE UpdateDesignerClass( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in SAFEARRAY * sabstrPublicFields,
            /* [in] */ UDC_Flags dwFlags) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetUrlForItem( 
            /* [in] */ VSITEMID itemid,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrURL) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetOpenedUrl( 
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrURL) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetDataEnvironment( 
            /* [out] */ __RPC__deref_out_opt IUnknown **ppvDataEnv) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE IsBlockingItemTypeResolver( 
            /* [out] */ __RPC__out BOOL *pbIsBlockingItemTypeResolver) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE BlockItemTypeResolver( 
            /* [in] */ BOOL bBlockItemTypeResolver) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsWebApplicationProjectVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsWebApplicationProject * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsWebApplicationProject * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetCodeBehindEventBinding )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [out] */ __RPC__deref_out_opt IVsCodeBehindEventBinding **ppCodeBehindEventBinding);
        
        HRESULT ( STDMETHODCALLTYPE *UpdateDesignerClass )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in SAFEARRAY * sabstrPublicFields,
            /* [in] */ UDC_Flags dwFlags);
        
        HRESULT ( STDMETHODCALLTYPE *GetUrlForItem )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [in] */ VSITEMID itemid,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrURL);
        
        HRESULT ( STDMETHODCALLTYPE *GetOpenedUrl )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrURL);
        
        HRESULT ( STDMETHODCALLTYPE *GetDataEnvironment )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [out] */ __RPC__deref_out_opt IUnknown **ppvDataEnv);
        
        HRESULT ( STDMETHODCALLTYPE *IsBlockingItemTypeResolver )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [out] */ __RPC__out BOOL *pbIsBlockingItemTypeResolver);
        
        HRESULT ( STDMETHODCALLTYPE *BlockItemTypeResolver )( 
            __RPC__in IVsWebApplicationProject * This,
            /* [in] */ BOOL bBlockItemTypeResolver);
        
        END_INTERFACE
    } IVsWebApplicationProjectVtbl;

    interface IVsWebApplicationProject
    {
        CONST_VTBL struct IVsWebApplicationProjectVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsWebApplicationProject_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsWebApplicationProject_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsWebApplicationProject_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsWebApplicationProject_GetCodeBehindEventBinding(This,ppCodeBehindEventBinding)	\
    ( (This)->lpVtbl -> GetCodeBehindEventBinding(This,ppCodeBehindEventBinding) ) 

#define IVsWebApplicationProject_UpdateDesignerClass(This,pszDocument,pszCodeBehind,pszCodeBehindFile,sabstrPublicFields,dwFlags)	\
    ( (This)->lpVtbl -> UpdateDesignerClass(This,pszDocument,pszCodeBehind,pszCodeBehindFile,sabstrPublicFields,dwFlags) ) 

#define IVsWebApplicationProject_GetUrlForItem(This,itemid,pbstrURL)	\
    ( (This)->lpVtbl -> GetUrlForItem(This,itemid,pbstrURL) ) 

#define IVsWebApplicationProject_GetOpenedUrl(This,pbstrURL)	\
    ( (This)->lpVtbl -> GetOpenedUrl(This,pbstrURL) ) 

#define IVsWebApplicationProject_GetDataEnvironment(This,ppvDataEnv)	\
    ( (This)->lpVtbl -> GetDataEnvironment(This,ppvDataEnv) ) 

#define IVsWebApplicationProject_IsBlockingItemTypeResolver(This,pbIsBlockingItemTypeResolver)	\
    ( (This)->lpVtbl -> IsBlockingItemTypeResolver(This,pbIsBlockingItemTypeResolver) ) 

#define IVsWebApplicationProject_BlockItemTypeResolver(This,bBlockItemTypeResolver)	\
    ( (This)->lpVtbl -> BlockItemTypeResolver(This,bBlockItemTypeResolver) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsWebApplicationProject_INTERFACE_DEFINED__ */


#ifndef __IVsWebApplicationProject2_INTERFACE_DEFINED__
#define __IVsWebApplicationProject2_INTERFACE_DEFINED__

/* interface IVsWebApplicationProject2 */
/* [object][unique][uuid] */ 


EXTERN_C const IID IID_IVsWebApplicationProject2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("05DDC9DC-013C-4ea2-AF1B-64E9F912BB62")
    IVsWebApplicationProject2 : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetIWebApplication( 
            /* [out] */ __RPC__deref_out_opt IUnknown **ppvDataEnv) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetBrowseUrl( 
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrURL) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsWebApplicationProject2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsWebApplicationProject2 * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsWebApplicationProject2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsWebApplicationProject2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetIWebApplication )( 
            __RPC__in IVsWebApplicationProject2 * This,
            /* [out] */ __RPC__deref_out_opt IUnknown **ppvDataEnv);
        
        HRESULT ( STDMETHODCALLTYPE *GetBrowseUrl )( 
            __RPC__in IVsWebApplicationProject2 * This,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrURL);
        
        END_INTERFACE
    } IVsWebApplicationProject2Vtbl;

    interface IVsWebApplicationProject2
    {
        CONST_VTBL struct IVsWebApplicationProject2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsWebApplicationProject2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsWebApplicationProject2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsWebApplicationProject2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsWebApplicationProject2_GetIWebApplication(This,ppvDataEnv)	\
    ( (This)->lpVtbl -> GetIWebApplication(This,ppvDataEnv) ) 

#define IVsWebApplicationProject2_GetBrowseUrl(This,pbstrURL)	\
    ( (This)->lpVtbl -> GetBrowseUrl(This,pbstrURL) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsWebApplicationProject2_INTERFACE_DEFINED__ */


#ifndef __IVsCodeBehindEventBinding_INTERFACE_DEFINED__
#define __IVsCodeBehindEventBinding_INTERFACE_DEFINED__

/* interface IVsCodeBehindEventBinding */
/* [object][unique][uuid] */ 


EXTERN_C const IID IID_IVsCodeBehindEventBinding;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("0e686aef-7878-42e2-b58e-ea6857eb9791")
    IVsCodeBehindEventBinding : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Initialize( 
            /* [in] */ __RPC__in_opt IServiceProvider *pServiceProvider,
            /* [in] */ __RPC__in_opt IVsHierarchy *pHierarchy) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CreateUniqueEventHandlerName( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrEventHandlerName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE IsExistingEventHandler( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName,
            /* [out] */ __RPC__out BOOL *pfIsExistingEventHandler) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CreateEventHandler( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE ShowEventHandler( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetCompatibleEventHandlers( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt SAFEARRAY * *ppsaEventHandlerNames) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE IsStaticEventBindingSupported( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [out] */ __RPC__out BOOL *pfIsStaticEventBindingSupported) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetStaticEventBinding( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrEventHandlerName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE RemoveStaticEventBinding( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE AddStaticEventBinding( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsCodeBehindEventBindingVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsCodeBehindEventBinding * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsCodeBehindEventBinding * This);
        
        HRESULT ( STDMETHODCALLTYPE *Initialize )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in_opt IServiceProvider *pServiceProvider,
            /* [in] */ __RPC__in_opt IVsHierarchy *pHierarchy);
        
        HRESULT ( STDMETHODCALLTYPE *Close )( 
            __RPC__in IVsCodeBehindEventBinding * This);
        
        HRESULT ( STDMETHODCALLTYPE *CreateUniqueEventHandlerName )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *IsExistingEventHandler )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName,
            /* [out] */ __RPC__out BOOL *pfIsExistingEventHandler);
        
        HRESULT ( STDMETHODCALLTYPE *CreateEventHandler )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *ShowEventHandler )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *GetCompatibleEventHandlers )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt SAFEARRAY * *ppsaEventHandlerNames);
        
        HRESULT ( STDMETHODCALLTYPE *IsStaticEventBindingSupported )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [out] */ __RPC__out BOOL *pfIsStaticEventBindingSupported);
        
        HRESULT ( STDMETHODCALLTYPE *GetStaticEventBinding )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *RemoveStaticEventBinding )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *AddStaticEventBinding )( 
            __RPC__in IVsCodeBehindEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        END_INTERFACE
    } IVsCodeBehindEventBindingVtbl;

    interface IVsCodeBehindEventBinding
    {
        CONST_VTBL struct IVsCodeBehindEventBindingVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsCodeBehindEventBinding_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsCodeBehindEventBinding_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsCodeBehindEventBinding_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsCodeBehindEventBinding_Initialize(This,pServiceProvider,pHierarchy)	\
    ( (This)->lpVtbl -> Initialize(This,pServiceProvider,pHierarchy) ) 

#define IVsCodeBehindEventBinding_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IVsCodeBehindEventBinding_CreateUniqueEventHandlerName(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName)	\
    ( (This)->lpVtbl -> CreateUniqueEventHandlerName(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName) ) 

#define IVsCodeBehindEventBinding_IsExistingEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName,pfIsExistingEventHandler)	\
    ( (This)->lpVtbl -> IsExistingEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName,pfIsExistingEventHandler) ) 

#define IVsCodeBehindEventBinding_CreateEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> CreateEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName) ) 

#define IVsCodeBehindEventBinding_ShowEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> ShowEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName) ) 

#define IVsCodeBehindEventBinding_GetCompatibleEventHandlers(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,ppsaEventHandlerNames)	\
    ( (This)->lpVtbl -> GetCompatibleEventHandlers(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,ppsaEventHandlerNames) ) 

#define IVsCodeBehindEventBinding_IsStaticEventBindingSupported(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pfIsStaticEventBindingSupported)	\
    ( (This)->lpVtbl -> IsStaticEventBindingSupported(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pfIsStaticEventBindingSupported) ) 

#define IVsCodeBehindEventBinding_GetStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName)	\
    ( (This)->lpVtbl -> GetStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName) ) 

#define IVsCodeBehindEventBinding_RemoveStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> RemoveStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pszEventHandlerName) ) 

#define IVsCodeBehindEventBinding_AddStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszObjectName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> AddStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszObjectName,pszEventName,pszEventHandlerName) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsCodeBehindEventBinding_INTERFACE_DEFINED__ */


#ifndef __IVsCodeBehindEventBinding2_INTERFACE_DEFINED__
#define __IVsCodeBehindEventBinding2_INTERFACE_DEFINED__

/* interface IVsCodeBehindEventBinding2 */
/* [object][unique][uuid] */ 


EXTERN_C const IID IID_IVsCodeBehindEventBinding2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("D18CA7EE-EEFE-4895-8B7C-8336979E5351")
    IVsCodeBehindEventBinding2 : public IVsCodeBehindEventBinding
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE CreateEventHandler( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName,
            /* [in] */ BOOL fAddStaticEventBinding) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsCodeBehindEventBinding2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsCodeBehindEventBinding2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsCodeBehindEventBinding2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *Initialize )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in_opt IServiceProvider *pServiceProvider,
            /* [in] */ __RPC__in_opt IVsHierarchy *pHierarchy);
        
        HRESULT ( STDMETHODCALLTYPE *Close )( 
            __RPC__in IVsCodeBehindEventBinding2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *CreateUniqueEventHandlerName )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *IsExistingEventHandler )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName,
            /* [out] */ __RPC__out BOOL *pfIsExistingEventHandler);
        
        HRESULT ( STDMETHODCALLTYPE *CreateEventHandler )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *ShowEventHandler )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *GetCompatibleEventHandlers )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt SAFEARRAY * *ppsaEventHandlerNames);
        
        HRESULT ( STDMETHODCALLTYPE *IsStaticEventBindingSupported )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [out] */ __RPC__out BOOL *pfIsStaticEventBindingSupported);
        
        HRESULT ( STDMETHODCALLTYPE *GetStaticEventBinding )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *RemoveStaticEventBinding )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *AddStaticEventBinding )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName);
        
        HRESULT ( STDMETHODCALLTYPE *CreateEventHandler )( 
            __RPC__in IVsCodeBehindEventBinding2 * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszObjectTypeName,
            /* [in] */ __RPC__in LPCWSTR pszObjectName,
            /* [in] */ __RPC__in LPCWSTR pszEventName,
            /* [in] */ __RPC__in LPCWSTR pszEventHandlerName,
            /* [in] */ BOOL fAddStaticEventBinding);
        
        END_INTERFACE
    } IVsCodeBehindEventBinding2Vtbl;

    interface IVsCodeBehindEventBinding2
    {
        CONST_VTBL struct IVsCodeBehindEventBinding2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsCodeBehindEventBinding2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsCodeBehindEventBinding2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsCodeBehindEventBinding2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsCodeBehindEventBinding2_Initialize(This,pServiceProvider,pHierarchy)	\
    ( (This)->lpVtbl -> Initialize(This,pServiceProvider,pHierarchy) ) 

#define IVsCodeBehindEventBinding2_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IVsCodeBehindEventBinding2_CreateUniqueEventHandlerName(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName)	\
    ( (This)->lpVtbl -> CreateUniqueEventHandlerName(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName) ) 

#define IVsCodeBehindEventBinding2_IsExistingEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName,pfIsExistingEventHandler)	\
    ( (This)->lpVtbl -> IsExistingEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName,pfIsExistingEventHandler) ) 

#define IVsCodeBehindEventBinding2_CreateEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> CreateEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName) ) 

#define IVsCodeBehindEventBinding2_ShowEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> ShowEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,pszEventHandlerName) ) 

#define IVsCodeBehindEventBinding2_GetCompatibleEventHandlers(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,ppsaEventHandlerNames)	\
    ( (This)->lpVtbl -> GetCompatibleEventHandlers(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszEventName,ppsaEventHandlerNames) ) 

#define IVsCodeBehindEventBinding2_IsStaticEventBindingSupported(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pfIsStaticEventBindingSupported)	\
    ( (This)->lpVtbl -> IsStaticEventBindingSupported(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pfIsStaticEventBindingSupported) ) 

#define IVsCodeBehindEventBinding2_GetStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName)	\
    ( (This)->lpVtbl -> GetStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pbstrEventHandlerName) ) 

#define IVsCodeBehindEventBinding2_RemoveStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> RemoveStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectName,pszEventName,pszEventHandlerName) ) 

#define IVsCodeBehindEventBinding2_AddStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszObjectName,pszEventName,pszEventHandlerName)	\
    ( (This)->lpVtbl -> AddStaticEventBinding(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszObjectName,pszEventName,pszEventHandlerName) ) 


#define IVsCodeBehindEventBinding2_CreateEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszObjectName,pszEventName,pszEventHandlerName,fAddStaticEventBinding)	\
    ( (This)->lpVtbl -> CreateEventHandler(This,pszDocument,pszCodeBehind,pszCodeBehindFile,pszClassName,pszObjectTypeName,pszObjectName,pszEventName,pszEventHandlerName,fAddStaticEventBinding) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsCodeBehindEventBinding2_INTERFACE_DEFINED__ */


#ifndef __IVsCodeEventBinding_INTERFACE_DEFINED__
#define __IVsCodeEventBinding_INTERFACE_DEFINED__

/* interface IVsCodeEventBinding */
/* [object][unique][uuid] */ 


EXTERN_C const IID IID_IVsCodeEventBinding;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A61CAE96-2429-4304-BBE5-698581E93935")
    IVsCodeEventBinding : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetCompatibleMethods( 
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszSignature,
            /* [out] */ __RPC__deref_out_opt SAFEARRAY * *ppsaMethodNames) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CreateUniqueMethodName( 
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszComponentID,
            /* [in] */ __RPC__in LPCWSTR pszMethodNameBase,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrUniqueMethodName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE CreateMethod( 
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszMethodName,
            /* [in] */ __RPC__in LPCWSTR pszContent) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsCodeEventBindingVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsCodeEventBinding * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsCodeEventBinding * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsCodeEventBinding * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetCompatibleMethods )( 
            __RPC__in IVsCodeEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszSignature,
            /* [out] */ __RPC__deref_out_opt SAFEARRAY * *ppsaMethodNames);
        
        HRESULT ( STDMETHODCALLTYPE *CreateUniqueMethodName )( 
            __RPC__in IVsCodeEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszComponentID,
            /* [in] */ __RPC__in LPCWSTR pszMethodNameBase,
            /* [out] */ __RPC__deref_out_opt BSTR *pbstrUniqueMethodName);
        
        HRESULT ( STDMETHODCALLTYPE *CreateMethod )( 
            __RPC__in IVsCodeEventBinding * This,
            /* [in] */ __RPC__in LPCWSTR pszClassName,
            /* [in] */ __RPC__in LPCWSTR pszMethodName,
            /* [in] */ __RPC__in LPCWSTR pszContent);
        
        END_INTERFACE
    } IVsCodeEventBindingVtbl;

    interface IVsCodeEventBinding
    {
        CONST_VTBL struct IVsCodeEventBindingVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsCodeEventBinding_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsCodeEventBinding_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsCodeEventBinding_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsCodeEventBinding_GetCompatibleMethods(This,pszClassName,pszSignature,ppsaMethodNames)	\
    ( (This)->lpVtbl -> GetCompatibleMethods(This,pszClassName,pszSignature,ppsaMethodNames) ) 

#define IVsCodeEventBinding_CreateUniqueMethodName(This,pszClassName,pszComponentID,pszMethodNameBase,pbstrUniqueMethodName)	\
    ( (This)->lpVtbl -> CreateUniqueMethodName(This,pszClassName,pszComponentID,pszMethodNameBase,pbstrUniqueMethodName) ) 

#define IVsCodeEventBinding_CreateMethod(This,pszClassName,pszMethodName,pszContent)	\
    ( (This)->lpVtbl -> CreateMethod(This,pszClassName,pszMethodName,pszContent) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsCodeEventBinding_INTERFACE_DEFINED__ */


#ifndef __IVsCodeBehindCodeGenerator_INTERFACE_DEFINED__
#define __IVsCodeBehindCodeGenerator_INTERFACE_DEFINED__

/* interface IVsCodeBehindCodeGenerator */
/* [object][unique][uuid] */ 


EXTERN_C const IID IID_IVsCodeBehindCodeGenerator;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("76893cea-de06-4ac7-aa3e-6e21dbe1102a")
    IVsCodeBehindCodeGenerator : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE Initialize( 
            /* [in] */ __RPC__in_opt IVsHierarchy *pHierarchy) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE IsGenerateAllowed( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ BOOL fCreate,
            /* [retval][out] */ __RPC__out BOOL *pfIsGenerateAllowed) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE BeginGenerate( 
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName_Full,
            /* [in] */ BOOL fCreate) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE EnsureStronglyTypedProperty( 
            /* [in] */ __RPC__in LPCWSTR pszPropertyName,
            /* [in] */ __RPC__in LPCWSTR pszPropertyTypeName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE EnsureControlDeclaration( 
            /* [in] */ __RPC__in LPCWSTR pszName,
            /* [in] */ __RPC__in LPCWSTR pszTypeName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Generate( void) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsCodeBehindCodeGeneratorVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsCodeBehindCodeGenerator * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsCodeBehindCodeGenerator * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsCodeBehindCodeGenerator * This);
        
        HRESULT ( STDMETHODCALLTYPE *Initialize )( 
            __RPC__in IVsCodeBehindCodeGenerator * This,
            /* [in] */ __RPC__in_opt IVsHierarchy *pHierarchy);
        
        HRESULT ( STDMETHODCALLTYPE *Close )( 
            __RPC__in IVsCodeBehindCodeGenerator * This);
        
        HRESULT ( STDMETHODCALLTYPE *IsGenerateAllowed )( 
            __RPC__in IVsCodeBehindCodeGenerator * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ BOOL fCreate,
            /* [retval][out] */ __RPC__out BOOL *pfIsGenerateAllowed);
        
        HRESULT ( STDMETHODCALLTYPE *BeginGenerate )( 
            __RPC__in IVsCodeBehindCodeGenerator * This,
            /* [in] */ __RPC__in LPCWSTR pszDocument,
            /* [in] */ __RPC__in LPCWSTR pszCodeBehindFile,
            /* [in] */ __RPC__in LPCWSTR pszClassName_Full,
            /* [in] */ BOOL fCreate);
        
        HRESULT ( STDMETHODCALLTYPE *EnsureStronglyTypedProperty )( 
            __RPC__in IVsCodeBehindCodeGenerator * This,
            /* [in] */ __RPC__in LPCWSTR pszPropertyName,
            /* [in] */ __RPC__in LPCWSTR pszPropertyTypeName);
        
        HRESULT ( STDMETHODCALLTYPE *EnsureControlDeclaration )( 
            __RPC__in IVsCodeBehindCodeGenerator * This,
            /* [in] */ __RPC__in LPCWSTR pszName,
            /* [in] */ __RPC__in LPCWSTR pszTypeName);
        
        HRESULT ( STDMETHODCALLTYPE *Generate )( 
            __RPC__in IVsCodeBehindCodeGenerator * This);
        
        END_INTERFACE
    } IVsCodeBehindCodeGeneratorVtbl;

    interface IVsCodeBehindCodeGenerator
    {
        CONST_VTBL struct IVsCodeBehindCodeGeneratorVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsCodeBehindCodeGenerator_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsCodeBehindCodeGenerator_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsCodeBehindCodeGenerator_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsCodeBehindCodeGenerator_Initialize(This,pHierarchy)	\
    ( (This)->lpVtbl -> Initialize(This,pHierarchy) ) 

#define IVsCodeBehindCodeGenerator_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IVsCodeBehindCodeGenerator_IsGenerateAllowed(This,pszDocument,pszCodeBehindFile,fCreate,pfIsGenerateAllowed)	\
    ( (This)->lpVtbl -> IsGenerateAllowed(This,pszDocument,pszCodeBehindFile,fCreate,pfIsGenerateAllowed) ) 

#define IVsCodeBehindCodeGenerator_BeginGenerate(This,pszDocument,pszCodeBehindFile,pszClassName_Full,fCreate)	\
    ( (This)->lpVtbl -> BeginGenerate(This,pszDocument,pszCodeBehindFile,pszClassName_Full,fCreate) ) 

#define IVsCodeBehindCodeGenerator_EnsureStronglyTypedProperty(This,pszPropertyName,pszPropertyTypeName)	\
    ( (This)->lpVtbl -> EnsureStronglyTypedProperty(This,pszPropertyName,pszPropertyTypeName) ) 

#define IVsCodeBehindCodeGenerator_EnsureControlDeclaration(This,pszName,pszTypeName)	\
    ( (This)->lpVtbl -> EnsureControlDeclaration(This,pszName,pszTypeName) ) 

#define IVsCodeBehindCodeGenerator_Generate(This)	\
    ( (This)->lpVtbl -> Generate(This) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsCodeBehindCodeGenerator_INTERFACE_DEFINED__ */


#ifndef __IVsWebApplicationDocument_INTERFACE_DEFINED__
#define __IVsWebApplicationDocument_INTERFACE_DEFINED__

/* interface IVsWebApplicationDocument */
/* [object][unique][uuid] */ 


EXTERN_C const IID IID_IVsWebApplicationDocument;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("d997d023-0de4-48e0-b3b3-98d768369d77")
    IVsWebApplicationDocument : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE OnCodeBehindRenamed( 
            /* [in] */ __RPC__in LPCWSTR pszOldCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszNewCodeBehind) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsWebApplicationDocumentVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsWebApplicationDocument * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsWebApplicationDocument * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsWebApplicationDocument * This);
        
        HRESULT ( STDMETHODCALLTYPE *OnCodeBehindRenamed )( 
            __RPC__in IVsWebApplicationDocument * This,
            /* [in] */ __RPC__in LPCWSTR pszOldCodeBehind,
            /* [in] */ __RPC__in LPCWSTR pszNewCodeBehind);
        
        END_INTERFACE
    } IVsWebApplicationDocumentVtbl;

    interface IVsWebApplicationDocument
    {
        CONST_VTBL struct IVsWebApplicationDocumentVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsWebApplicationDocument_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsWebApplicationDocument_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsWebApplicationDocument_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsWebApplicationDocument_OnCodeBehindRenamed(This,pszOldCodeBehind,pszNewCodeBehind)	\
    ( (This)->lpVtbl -> OnCodeBehindRenamed(This,pszOldCodeBehind,pszNewCodeBehind) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsWebApplicationDocument_INTERFACE_DEFINED__ */


/* Additional Prototypes for ALL interfaces */

unsigned long             __RPC_USER  BSTR_UserSize(     __RPC__in unsigned long *, unsigned long            , __RPC__in BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserMarshal(  __RPC__in unsigned long *, __RPC__inout_xcount(0) unsigned char *, __RPC__in BSTR * ); 
unsigned char * __RPC_USER  BSTR_UserUnmarshal(__RPC__in unsigned long *, __RPC__in_xcount(0) unsigned char *, __RPC__out BSTR * ); 
void                      __RPC_USER  BSTR_UserFree(     __RPC__in unsigned long *, __RPC__in BSTR * ); 

unsigned long             __RPC_USER  LPSAFEARRAY_UserSize(     __RPC__in unsigned long *, unsigned long            , __RPC__in LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserMarshal(  __RPC__in unsigned long *, __RPC__inout_xcount(0) unsigned char *, __RPC__in LPSAFEARRAY * ); 
unsigned char * __RPC_USER  LPSAFEARRAY_UserUnmarshal(__RPC__in unsigned long *, __RPC__in_xcount(0) unsigned char *, __RPC__out LPSAFEARRAY * ); 
void                      __RPC_USER  LPSAFEARRAY_UserFree(     __RPC__in unsigned long *, __RPC__in LPSAFEARRAY * ); 

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


