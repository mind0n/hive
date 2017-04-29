#ifndef __GDIEXPORTER_PRINTMSG_H__
#define __GDIEXPORTER_PRINTMSG_H__

[SecurityCritical]
void FinePrint(
    IN HDC    hDC,
    IN int    NumColors,
    IN bool   SupportJPEGpassthrough,
    IN bool   SupportPNGpassthrough,
    cli::array<Byte>^ devmode
    );

#endif
