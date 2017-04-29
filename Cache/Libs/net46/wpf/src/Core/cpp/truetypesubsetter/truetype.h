// truetype.h

#pragma once
//using namespace System;
using namespace System::IO;
namespace MS { namespace Internal {

typedef System::UInt16  ushort;

private ref class TrueTypeSubsetter abstract sealed
{
public:
    [System::Security::SecurityCritical]
    static array<System::Byte> ^ ComputeSubset(void * fontData, int fileSize, System::Uri ^ sourceUri, int directoryOffset, array<System::UInt16> ^ glyphArray);
};

}} // MS::Internal

