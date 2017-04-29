

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


#ifndef __servexp_h__
#define __servexp_h__

#if defined(_MSC_VER) && (_MSC_VER >= 1020)
#pragma once
#endif

/* Forward Declarations */ 

#ifndef __IVsServerExplorer_FWD_DEFINED__
#define __IVsServerExplorer_FWD_DEFINED__
typedef interface IVsServerExplorer IVsServerExplorer;

#endif 	/* __IVsServerExplorer_FWD_DEFINED__ */


#ifndef __IServersService_FWD_DEFINED__
#define __IServersService_FWD_DEFINED__
typedef interface IServersService IServersService;

#endif 	/* __IServersService_FWD_DEFINED__ */


#ifndef __IDataConnectionsService_FWD_DEFINED__
#define __IDataConnectionsService_FWD_DEFINED__
typedef interface IDataConnectionsService IDataConnectionsService;

#endif 	/* __IDataConnectionsService_FWD_DEFINED__ */


#ifndef __ISEHierarchySerializable_FWD_DEFINED__
#define __ISEHierarchySerializable_FWD_DEFINED__
typedef interface ISEHierarchySerializable ISEHierarchySerializable;

#endif 	/* __ISEHierarchySerializable_FWD_DEFINED__ */


/* header files for imported files */
#include "vsshell.h"

#ifdef __cplusplus
extern "C"{
#endif 



#ifndef __servexp2_LIBRARY_DEFINED__
#define __servexp2_LIBRARY_DEFINED__

/* library servexp2 */
/* [version][uuid] */ 


EXTERN_C const IID LIBID_servexp2;

#ifndef __IVsServerExplorer_INTERFACE_DEFINED__
#define __IVsServerExplorer_INTERFACE_DEFINED__

/* interface IVsServerExplorer */
/* [uuid][object] */ 


EXTERN_C const IID IID_IVsServerExplorer;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("7494682C-37A0-11d2-A273-00C04F8EF4FF")
    IVsServerExplorer : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE SetHierarchyWindow( 
            /* [in] */ __RPC__in_opt IVsUIHierarchyWindow *pHierWin) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Init( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Show( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE Close( void) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetRootService( 
            /* [in] */ __RPC__in REFGUID guid,
            /* [retval][out] */ __RPC__deref_out_opt IUnknown **serviceObject) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IVsServerExplorerVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IVsServerExplorer * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IVsServerExplorer * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IVsServerExplorer * This);
        
        HRESULT ( STDMETHODCALLTYPE *SetHierarchyWindow )( 
            __RPC__in IVsServerExplorer * This,
            /* [in] */ __RPC__in_opt IVsUIHierarchyWindow *pHierWin);
        
        HRESULT ( STDMETHODCALLTYPE *Init )( 
            __RPC__in IVsServerExplorer * This);
        
        HRESULT ( STDMETHODCALLTYPE *Show )( 
            __RPC__in IVsServerExplorer * This);
        
        HRESULT ( STDMETHODCALLTYPE *Close )( 
            __RPC__in IVsServerExplorer * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetRootService )( 
            __RPC__in IVsServerExplorer * This,
            /* [in] */ __RPC__in REFGUID guid,
            /* [retval][out] */ __RPC__deref_out_opt IUnknown **serviceObject);
        
        END_INTERFACE
    } IVsServerExplorerVtbl;

    interface IVsServerExplorer
    {
        CONST_VTBL struct IVsServerExplorerVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IVsServerExplorer_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IVsServerExplorer_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IVsServerExplorer_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IVsServerExplorer_SetHierarchyWindow(This,pHierWin)	\
    ( (This)->lpVtbl -> SetHierarchyWindow(This,pHierWin) ) 

#define IVsServerExplorer_Init(This)	\
    ( (This)->lpVtbl -> Init(This) ) 

#define IVsServerExplorer_Show(This)	\
    ( (This)->lpVtbl -> Show(This) ) 

#define IVsServerExplorer_Close(This)	\
    ( (This)->lpVtbl -> Close(This) ) 

#define IVsServerExplorer_GetRootService(This,guid,serviceObject)	\
    ( (This)->lpVtbl -> GetRootService(This,guid,serviceObject) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IVsServerExplorer_INTERFACE_DEFINED__ */


#ifndef __IServersService_INTERFACE_DEFINED__
#define __IServersService_INTERFACE_DEFINED__

/* interface IServersService */
/* [uuid][object] */ 


EXTERN_C const IID IID_IServersService;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3F3FE8B0-54F5-11d3-B985-00C04F685DA5")
    IServersService : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetServerCount( 
            /* [retval][out] */ __RPC__out int *serverCount) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetServerName( 
            /* [in] */ int serverIndex,
            /* [retval][out] */ __RPC__deref_out_opt BSTR *bstrServerName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetServerIndex( 
            /* [in] */ __RPC__in BSTR bstrServerName,
            /* [retval][out] */ __RPC__out int *serverIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE AddServer( 
            /* [in] */ __RPC__in BSTR bstrServerName,
            /* [retval][out] */ __RPC__out int *serverIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE RemoveServer( 
            /* [in] */ int serverIndex) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IServersServiceVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IServersService * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IServersService * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IServersService * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetServerCount )( 
            __RPC__in IServersService * This,
            /* [retval][out] */ __RPC__out int *serverCount);
        
        HRESULT ( STDMETHODCALLTYPE *GetServerName )( 
            __RPC__in IServersService * This,
            /* [in] */ int serverIndex,
            /* [retval][out] */ __RPC__deref_out_opt BSTR *bstrServerName);
        
        HRESULT ( STDMETHODCALLTYPE *GetServerIndex )( 
            __RPC__in IServersService * This,
            /* [in] */ __RPC__in BSTR bstrServerName,
            /* [retval][out] */ __RPC__out int *serverIndex);
        
        HRESULT ( STDMETHODCALLTYPE *AddServer )( 
            __RPC__in IServersService * This,
            /* [in] */ __RPC__in BSTR bstrServerName,
            /* [retval][out] */ __RPC__out int *serverIndex);
        
        HRESULT ( STDMETHODCALLTYPE *RemoveServer )( 
            __RPC__in IServersService * This,
            /* [in] */ int serverIndex);
        
        END_INTERFACE
    } IServersServiceVtbl;

    interface IServersService
    {
        CONST_VTBL struct IServersServiceVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IServersService_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IServersService_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IServersService_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IServersService_GetServerCount(This,serverCount)	\
    ( (This)->lpVtbl -> GetServerCount(This,serverCount) ) 

#define IServersService_GetServerName(This,serverIndex,bstrServerName)	\
    ( (This)->lpVtbl -> GetServerName(This,serverIndex,bstrServerName) ) 

#define IServersService_GetServerIndex(This,bstrServerName,serverIndex)	\
    ( (This)->lpVtbl -> GetServerIndex(This,bstrServerName,serverIndex) ) 

#define IServersService_AddServer(This,bstrServerName,serverIndex)	\
    ( (This)->lpVtbl -> AddServer(This,bstrServerName,serverIndex) ) 

#define IServersService_RemoveServer(This,serverIndex)	\
    ( (This)->lpVtbl -> RemoveServer(This,serverIndex) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IServersService_INTERFACE_DEFINED__ */


#ifndef __IDataConnectionsService_INTERFACE_DEFINED__
#define __IDataConnectionsService_INTERFACE_DEFINED__

/* interface IDataConnectionsService */
/* [uuid][object] */ 


EXTERN_C const IID IID_IDataConnectionsService;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("3F3FE8B1-54F5-11d3-B985-00C04F685DA5")
    IDataConnectionsService : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE GetConnectionCount( 
            /* [out] */ __RPC__out int *dataConnectionCount) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetConnectionName( 
            /* [in] */ int ConnectionIndex,
            /* [retval][out] */ __RPC__deref_out_opt BSTR *bstrConnectionName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetConnectionIndex( 
            /* [in] */ __RPC__in BSTR ConnectionName,
            /* [retval][out] */ __RPC__out int *ConnectionIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SetConnectionName( 
            /* [in] */ int ConnectionIndex,
            /* [in] */ __RPC__in BSTR bstrConnectionName) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetConnectionString( 
            /* [in] */ int ConnectionIndex,
            /* [retval][out] */ __RPC__deref_out_opt BSTR *bstrConnectString) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE AddConnection( 
            /* [in] */ __RPC__in BSTR bstrConnectionName,
            /* [in] */ __RPC__in BSTR bstrConnectString,
            /* [retval][out] */ __RPC__out int *connectionIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE PromptAndAddConnection( 
            /* [out] */ __RPC__out int *newConnectionIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE GetConnectionHierarchy( 
            /* [in] */ int ConnectionIndex,
            /* [retval][out] */ __RPC__deref_out_opt IVsHierarchy **vsHierarchy) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE SelectConnection( 
            /* [in] */ int ConnectionIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE RemoveConnection( 
            /* [in] */ int ConnectionIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE FindConnectionByConnectString( 
            /* [in] */ int firstIndex,
            /* [in] */ __RPC__in BSTR bstrConnectionString,
            /* [retval][out] */ __RPC__out int *connectionIndex) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE LoadHierarchyFromMkDocumentPrefix( 
            /* [in] */ __RPC__in BSTR bstrMkDocument,
            /* [retval][out] */ __RPC__out int *statusCode) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct IDataConnectionsServiceVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in IDataConnectionsService * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in IDataConnectionsService * This);
        
        HRESULT ( STDMETHODCALLTYPE *GetConnectionCount )( 
            __RPC__in IDataConnectionsService * This,
            /* [out] */ __RPC__out int *dataConnectionCount);
        
        HRESULT ( STDMETHODCALLTYPE *GetConnectionName )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ int ConnectionIndex,
            /* [retval][out] */ __RPC__deref_out_opt BSTR *bstrConnectionName);
        
        HRESULT ( STDMETHODCALLTYPE *GetConnectionIndex )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ __RPC__in BSTR ConnectionName,
            /* [retval][out] */ __RPC__out int *ConnectionIndex);
        
        HRESULT ( STDMETHODCALLTYPE *SetConnectionName )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ int ConnectionIndex,
            /* [in] */ __RPC__in BSTR bstrConnectionName);
        
        HRESULT ( STDMETHODCALLTYPE *GetConnectionString )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ int ConnectionIndex,
            /* [retval][out] */ __RPC__deref_out_opt BSTR *bstrConnectString);
        
        HRESULT ( STDMETHODCALLTYPE *AddConnection )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ __RPC__in BSTR bstrConnectionName,
            /* [in] */ __RPC__in BSTR bstrConnectString,
            /* [retval][out] */ __RPC__out int *connectionIndex);
        
        HRESULT ( STDMETHODCALLTYPE *PromptAndAddConnection )( 
            __RPC__in IDataConnectionsService * This,
            /* [out] */ __RPC__out int *newConnectionIndex);
        
        HRESULT ( STDMETHODCALLTYPE *GetConnectionHierarchy )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ int ConnectionIndex,
            /* [retval][out] */ __RPC__deref_out_opt IVsHierarchy **vsHierarchy);
        
        HRESULT ( STDMETHODCALLTYPE *SelectConnection )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ int ConnectionIndex);
        
        HRESULT ( STDMETHODCALLTYPE *RemoveConnection )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ int ConnectionIndex);
        
        HRESULT ( STDMETHODCALLTYPE *FindConnectionByConnectString )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ int firstIndex,
            /* [in] */ __RPC__in BSTR bstrConnectionString,
            /* [retval][out] */ __RPC__out int *connectionIndex);
        
        HRESULT ( STDMETHODCALLTYPE *LoadHierarchyFromMkDocumentPrefix )( 
            __RPC__in IDataConnectionsService * This,
            /* [in] */ __RPC__in BSTR bstrMkDocument,
            /* [retval][out] */ __RPC__out int *statusCode);
        
        END_INTERFACE
    } IDataConnectionsServiceVtbl;

    interface IDataConnectionsService
    {
        CONST_VTBL struct IDataConnectionsServiceVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define IDataConnectionsService_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define IDataConnectionsService_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define IDataConnectionsService_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define IDataConnectionsService_GetConnectionCount(This,dataConnectionCount)	\
    ( (This)->lpVtbl -> GetConnectionCount(This,dataConnectionCount) ) 

#define IDataConnectionsService_GetConnectionName(This,ConnectionIndex,bstrConnectionName)	\
    ( (This)->lpVtbl -> GetConnectionName(This,ConnectionIndex,bstrConnectionName) ) 

#define IDataConnectionsService_GetConnectionIndex(This,ConnectionName,ConnectionIndex)	\
    ( (This)->lpVtbl -> GetConnectionIndex(This,ConnectionName,ConnectionIndex) ) 

#define IDataConnectionsService_SetConnectionName(This,ConnectionIndex,bstrConnectionName)	\
    ( (This)->lpVtbl -> SetConnectionName(This,ConnectionIndex,bstrConnectionName) ) 

#define IDataConnectionsService_GetConnectionString(This,ConnectionIndex,bstrConnectString)	\
    ( (This)->lpVtbl -> GetConnectionString(This,ConnectionIndex,bstrConnectString) ) 

#define IDataConnectionsService_AddConnection(This,bstrConnectionName,bstrConnectString,connectionIndex)	\
    ( (This)->lpVtbl -> AddConnection(This,bstrConnectionName,bstrConnectString,connectionIndex) ) 

#define IDataConnectionsService_PromptAndAddConnection(This,newConnectionIndex)	\
    ( (This)->lpVtbl -> PromptAndAddConnection(This,newConnectionIndex) ) 

#define IDataConnectionsService_GetConnectionHierarchy(This,ConnectionIndex,vsHierarchy)	\
    ( (This)->lpVtbl -> GetConnectionHierarchy(This,ConnectionIndex,vsHierarchy) ) 

#define IDataConnectionsService_SelectConnection(This,ConnectionIndex)	\
    ( (This)->lpVtbl -> SelectConnection(This,ConnectionIndex) ) 

#define IDataConnectionsService_RemoveConnection(This,ConnectionIndex)	\
    ( (This)->lpVtbl -> RemoveConnection(This,ConnectionIndex) ) 

#define IDataConnectionsService_FindConnectionByConnectString(This,firstIndex,bstrConnectionString,connectionIndex)	\
    ( (This)->lpVtbl -> FindConnectionByConnectString(This,firstIndex,bstrConnectionString,connectionIndex) ) 

#define IDataConnectionsService_LoadHierarchyFromMkDocumentPrefix(This,bstrMkDocument,statusCode)	\
    ( (This)->lpVtbl -> LoadHierarchyFromMkDocumentPrefix(This,bstrMkDocument,statusCode) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __IDataConnectionsService_INTERFACE_DEFINED__ */


#ifndef __ISEHierarchySerializable_INTERFACE_DEFINED__
#define __ISEHierarchySerializable_INTERFACE_DEFINED__

/* interface ISEHierarchySerializable */
/* [uuid][object] */ 


EXTERN_C const IID IID_ISEHierarchySerializable;

#if defined(__cplusplus) && !defined(CINTERFACE)
    
    MIDL_INTERFACE("A22B83E0-7B7D-4cdc-98D5-23C1309E4FD0")
    ISEHierarchySerializable : public IUnknown
    {
    public:
        virtual HRESULT STDMETHODCALLTYPE SaveHierarchyData( 
            /* [in] */ __RPC__in_opt IUnknown *pHierarchy,
            /* [out] */ __RPC__out int *pHGlobal) = 0;
        
        virtual HRESULT STDMETHODCALLTYPE LoadHierarchyData( 
            /* [in] */ __RPC__in int *hGlobal,
            /* [out] */ __RPC__deref_out_opt IUnknown **ppHierarchy) = 0;
        
    };
    
    
#else 	/* C style interface */

    typedef struct ISEHierarchySerializableVtbl
    {
        BEGIN_INTERFACE
        
        HRESULT ( STDMETHODCALLTYPE *QueryInterface )( 
            __RPC__in ISEHierarchySerializable * This,
            /* [in] */ __RPC__in REFIID riid,
            /* [annotation][iid_is][out] */ 
            _COM_Outptr_  void **ppvObject);
        
        ULONG ( STDMETHODCALLTYPE *AddRef )( 
            __RPC__in ISEHierarchySerializable * This);
        
        ULONG ( STDMETHODCALLTYPE *Release )( 
            __RPC__in ISEHierarchySerializable * This);
        
        HRESULT ( STDMETHODCALLTYPE *SaveHierarchyData )( 
            __RPC__in ISEHierarchySerializable * This,
            /* [in] */ __RPC__in_opt IUnknown *pHierarchy,
            /* [out] */ __RPC__out int *pHGlobal);
        
        HRESULT ( STDMETHODCALLTYPE *LoadHierarchyData )( 
            __RPC__in ISEHierarchySerializable * This,
            /* [in] */ __RPC__in int *hGlobal,
            /* [out] */ __RPC__deref_out_opt IUnknown **ppHierarchy);
        
        END_INTERFACE
    } ISEHierarchySerializableVtbl;

    interface ISEHierarchySerializable
    {
        CONST_VTBL struct ISEHierarchySerializableVtbl *lpVtbl;
    };

    

#ifdef COBJMACROS


#define ISEHierarchySerializable_QueryInterface(This,riid,ppvObject)	\
    ( (This)->lpVtbl -> QueryInterface(This,riid,ppvObject) ) 

#define ISEHierarchySerializable_AddRef(This)	\
    ( (This)->lpVtbl -> AddRef(This) ) 

#define ISEHierarchySerializable_Release(This)	\
    ( (This)->lpVtbl -> Release(This) ) 


#define ISEHierarchySerializable_SaveHierarchyData(This,pHierarchy,pHGlobal)	\
    ( (This)->lpVtbl -> SaveHierarchyData(This,pHierarchy,pHGlobal) ) 

#define ISEHierarchySerializable_LoadHierarchyData(This,hGlobal,ppHierarchy)	\
    ( (This)->lpVtbl -> LoadHierarchyData(This,hGlobal,ppHierarchy) ) 

#endif /* COBJMACROS */


#endif 	/* C style interface */




#endif 	/* __ISEHierarchySerializable_INTERFACE_DEFINED__ */

#endif /* __servexp2_LIBRARY_DEFINED__ */

/* Additional Prototypes for ALL interfaces */

/* end of Additional Prototypes */

#ifdef __cplusplus
}
#endif

#endif


