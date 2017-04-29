

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

#ifndef __containedlanguageprivate_h__
#define __containedlanguageprivate_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IVsIntellisenseEngine2_FWD_DEFINED__
#define __IVsIntellisenseEngine2_FWD_DEFINED__
typedef interface IVsIntellisenseEngine2 IVsIntellisenseEngine2;

#endif 	/* __IVsIntellisenseEngine2_FWD_DEFINED__ */


/* header files for imported files */
#include "textfind.h"
#include "containedlanguage.h"

#ifdef __cplusplus
extern "C"{
#endif 


/* interface __MIDL_itf_containedlanguageprivate_0000_0000 */
/* [local] */ 

#pragma once


extern RPC_IF_HANDLE __MIDL_itf_containedlanguageprivate_0000_0000_v0_0_c_ifspec;
extern RPC_IF_HANDLE __MIDL_itf_containedlanguageprivate_0000_0000_v0_0_s_ifspec;

#ifndef __IVsIntellisenseEngine2_INTERFACE_DEFINED__
#define __IVsIntellisenseEngine2_INTERFACE_DEFINED__

/* interface IVsIntellisenseEngine2 */
/* [uuid][object] */ 


EXTERN_C const IID IID_IVsIntellisenseEngine2;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("d0cc4880-9270-417b-b5d5-314c3642734a")
    IVsIntellisenseEngine2 : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE ContainsReference( 
            __RPC__in LPCWSTR pszToFind,
            VSFINDOPTIONS vsfo,
            __RPC__in BOOL *pfContainsReference) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetCodeBehindOwner( 
            __RPC__deref_in_opt IVsIntellisenseEngine **ppCodeBehindOwner) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsIntellisenseEngine2Vtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsIntellisenseEngine2 * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsIntellisenseEngine2 * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsIntellisenseEngine2 * This);
        
        HRESULT ( STDMETHODCALLTYPE *ContainsReference )( 
            __RPC__in IVsIntellisenseEngine2 * This,
            __RPC__in LPCWSTR pszToFind,
            VSFINDOPTIONS vsfo,
            __RPC__in BOOL *pfContainsReference);
        
        HRESULT ( STDMETHODCALLTYPE *GetCodeBehindOwner )( 
            __RPC__in IVsIntellisenseEngine2 * This,
            __RPC__deref_in_opt IVsIntellisenseEngine **ppCodeBehindOwner);
        
        END_INTERFACE
    } IVsIntellisenseEngine2Vtbl;

    interface IVsIntellisenseEngine2
    {
        CONST_VTBL struct IVsIntellisenseEngine2Vtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsIntellisenseEngine2_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsIntellisenseEngine2_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsIntellisenseEngine2_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsIntellisenseEngine2_ContainsReference(This,pszToFind,vsfo,pfContainsReference)	\
    ( (This)->lpVtbl -> ContainsReference(This,pszToFind,vsfo,pfContainsReference) ) 

#define IVsIntellisenseEngine2_GetCodeBehindOwner(This,ppCodeBehindOwner)	\
    ( (This)->lpVtbl -> GetCodeBehindOwner(This,ppCodeBehindOwner) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsIntellisenseEngine2_INTERFACE_DEFINED__ */


/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


