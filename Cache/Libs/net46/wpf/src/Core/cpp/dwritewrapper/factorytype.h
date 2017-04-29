#ifndef __FACTORYTYPE_H
#define __FACTORYTYPE_H

namespace MS { namespace Internal { namespace Text { namespace TextInterface
{
    /// <summary>
    ///The type of the Factory.
    /// </summary>
    private enum class FactoryType
    {
        Shared,
        Isolated
    };

}}}}//MS::Internal::Text::TextInterface

#endif //__FACTORYTYPE_H
