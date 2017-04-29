#ifndef __IFONTSOURCE_H
#define __IFONTSOURCE_H

//---------------------------------------------------------------------------
//
// Copyright (c) Microsoft Corporation.  All rights reserved.
// 
// Description: The FontSourceCollection class represents a collection of font files.
//
// History:  
//  08/25/2005 : mleonov - Created it
//
//---------------------------------------------------------------------------

using namespace System;
using namespace System::IO;


namespace MS { namespace Internal { namespace Text { namespace TextInterface
{
    private interface class IFontSource
    {
        void                                TestFileOpenable();
        System::IO::UnmanagedMemoryStream ^ GetUnmanagedStream();
        System::DateTime                    GetLastWriteTimeUtc();
        property System::Uri^ Uri
        {
            System::Uri^ get();
        }

        property bool IsComposite
        {
            bool get();
        }
       
    };

    private interface class IFontSourceFactory
    {
        IFontSource^ Create(System::String^);
    };

}}}}//MS::Internal::Text::TextInterface

#endif //__IFONTSOURCE_H
