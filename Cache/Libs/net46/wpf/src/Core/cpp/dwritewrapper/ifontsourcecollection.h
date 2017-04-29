#ifndef __IFONTSOURCECOLLECTION_H
#define __IFONTSOURCECOLLECTION_H

#include "IFontSource.h"

using namespace System;
namespace MS { namespace Internal { namespace Text { namespace TextInterface
{
    private interface class IFontSourceCollection : IEnumerable<IFontSource^>
    {
    };

    private interface class IFontSourceCollectionFactory
    {
        IFontSourceCollection^ Create(String^);
    };
    
}}}}//MS::Internal::Text::TextInterface

#endif //__IFONTSOURCECOLLECTION_H
