#ifndef __PRINTSYSTEMJOBENUMS_HPP__
#define __PRINTSYSTEMJOBENUMS_HPP__

namespace System
{
namespace Printing
{
    private enum class JobOperation
    {
        None           = 0,
        JobProduction  = 1,
        JobConsumption = 2
    };

}
}

#endif
