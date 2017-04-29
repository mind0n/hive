//------------------------------------------------------------------------------
// fontsandcolors.h
// Author: Josh Stevens [[....]]
//
// Fonts & Colors helper macros.  See common\idl\vs\vsshell.idl for related
// interfaces and enums.
//------------------------------------------------------------------------------


#pragma once


#ifndef _FONTSANDCOLORS_H_
#define _FONTSANDCOLORS_H_


#include "vsshell.h"
#include "hicolor.h"


// Sometimes includers of this file define INIT_GUID and sometimes they don't,
// so rather than using the DEFINE_GUID macro and hoping it works, we'll just
// define the GUIDs by hand.

// {C34C709B-C855-459e-B38C-3021F162D3B1}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_TextOutputToolWindowsFC = { 0xc34c709b, 0xc855, 0x459e, { 0xb3, 0x8c, 0x30, 0x21, 0xf1, 0x62, 0xd3, 0xb1 } };

// {47724E70-AF55-48fb-A928-BB161C1D0C05}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_PrinterFC = { 0x47724e70, 0xaf55, 0x48fb, { 0xa9, 0x28, 0xbb, 0x16, 0x1c, 0x1d, 0xc, 0x5 } };

// {A9A5637F-B2A8-422e-8FB5-DFB4625F0111}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_TooltipFC = { 0xa9a5637f, 0xb2a8, 0x422e, { 0x8f, 0xb5, 0xdf, 0xb4, 0x62, 0x5f, 0x01, 0x11 } };

// {C1614BB1-734F-4a31-BD42-5AE6275E16D2}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_StatementCompletionFC = { 0xc1614bb1, 0x734f, 0x4a31, { 0xbd, 0x42, 0x5a, 0xe6, 0x27, 0x5e, 0x16, 0xd2 } };

// {9973EFDF-317D-431c-8BC1-5E88CBFD4F7F}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_OutputWindowFC = { 0x9973efdf, 0x317d, 0x431c, { 0x8b, 0xc1, 0x5e, 0x88, 0xcb, 0xfd, 0x4f, 0x7f } };

// {5C48B2CB-0366-4fbf-9786-0BB37E945687}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_FindResultsFC = { 0x5c48b2cb, 0x366, 0x4fbf, { 0x97, 0x86, 0xb, 0xb3, 0x7e, 0x94, 0x56, 0x87 } };

// {EE1BE240-4E81-4beb-8EEA-54322B6B1BF5}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_CommandWindowFC = { 0xee1be240, 0x4e81, 0x4beb, { 0x8e, 0xea, 0x54, 0x32, 0x2b, 0x6b, 0x1b, 0xf5 } };

// {7E50DA68-A928-4d87-BEA4-E9F88AEC218C}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_SystemWindowFC = { 0x7e50da68, 0xa928, 0x4d87, { 0xbe, 0xa4, 0xe9, 0xf8, 0x8a, 0xec, 0x21, 0x8c } };

// {6BB65C5A-2F31-4bde-9F48-8A38DC0C63E7}
EXTERN_C const GUID DECLSPEC_SELECTANY GUID_ImmediateWindowFC = { 0x6bb65c5a, 0x2f31, 0x4bde, { 0x9f, 0x48, 0x8a, 0x38, 0xdc, 0xc, 0x63, 0xe7 } };


// Only font size between these two values (inclusive) are listed in the
// Fonts & Colors page.
#define FONT_SIZE_MIN 6
#define FONT_SIZE_MAX 24


// Macros for managing the FontInfo, ColorableItemInfo, and AllColorableItemInfo structures
// declared in vsshell.idl.  These are no longer necessary now that the helper classes (see
// below) have been created, but I'm leaving them in for compatability with existing code.

#define INITFONTINFO(f) { CFontInfoWrapper::Init(f); }
#define DESTROYFONTINFO(f) { CFontInfoWrapper::Clear(f); }
#define COPYFONTINFO(a, b) { CFontInfoWrapper::Copy(a, b); }

#define INITCOLORABLEITEMINFO(c) { CColorableItemInfoWrapper::Init(c); }
#define COPYCOLORABLEITEMINFO(a, b) { CColorableItemInfoWrapper::Copy(a, b); }

#define INITALLCOLORABLEITEMINFO(a) { CAllColorableItemInfoWrapper::Init(a); }
#define DESTROYALLCOLORABLEITEMINFO(a) { CAllColorableItemInfoWrapper::Clear(a); }
#define COPYALLCOLORABLEITEMINFO(a, b) { CAllColorableItemInfoWrapper::Copy(a, b); }


// This constant means that bits 0-23 are an index into the following color table:
// 0  - generic text (foreground)
// 1  - generic text (background)
// 2  - black
// 3  - white
// 4  - maroon
// 5  - dark green
// 6  - brown
// 7  - dark blue
// 8  - purple
// 9  - aquamarine
// 10 - light gray
// 11 - dark gray
// 12 - red
// 13 - green
// 14 - yellow
// 15 - blue
// 16 - magenta
// 17 - cyan
// 18 - selected text (foreground) == ::GetSysColor(COLOR_HIGHLIGHTTEXT)
// 19 - selected text (background) == ::GetSysColor(COLOR_HIGHLIGHT)
// 20 - inactive selection (foreground) == ::GetSysColor(COLOR_INACTIVECAPTIONTEXT)
// 21 - inactive selection (background) == ::GetSysColor(COLOR_INACTIVECAPTION)
// 22 - indicator margin (background) == ::GetSysColor(COLOR_3DFACE)
// 23 - plain text (foreground) == ::GetSysColor(COLOR_WINDOWTEXT)
// 24 - plain text (background) == ::GetSysColor(COLOR_WINDOW)
// Entries 0-1 and 18-24 are tracking (i.e., dynamic)
#define _COLORREF_INDEXED (0x01000000)

// This indicates that bits 0-23 are a COLOR_* value, the raw RGB equivalent of which
// is returned from the GetSysColor() function.
#define _COLORREF_SYS (0x10000000)

// This indicates that bits 0-23 are a VSCOLOR_* value, the raw RGB equivalent of which
// is returned from IVsUIShell::GetVSSysColor().
#define _COLORREF_VS (0x20000000)

// These three constants define the range of "real" colors in our palette--that is,
// colors that correspond to raw RGB constants and don't "track" system color
// settings.
#define FIRST_REAL_COLOR (2)
#define LAST_REAL_COLOR  (17)
#define REAL_COLOR_COUNT (LAST_REAL_COLOR - FIRST_REAL_COLOR + 1)

// Call these to create COLORREF that represents the color specified by the given
// index.  The value returned is NOT a raw RGB value.
#define INDEXED_COLORREF(c)     (_COLORREF_INDEXED | (c))
#define INDEXED_SYS_COLORREF(c) (_COLORREF_SYS     | (c))
#define INDEXED_VS_COLORREF(c)  (_COLORREF_VS      | (c))

// Call this to extract the index from an indexed COLORREF (e.g., a value returned
// by INDEXED_COLORREF().  The value returned is NOT a raw RGB value, nor is it
// an index-coded COLORREF; it is simply an index.
#define GET_COLORREF_INDEX(c) ((c) & 0x00ffffff)

// Returns whether the specified value is an index-coded COLORREF.
#define IS_INDEXED_COLORREF(c) ((c) & (_COLORREF_INDEXED | _COLORREF_SYS | _COLORREF_VS))

// Returns whether the given COLORREF is an index-coded value that corresponds to
// one of the system color values (i.e., not a "real" color value).
#define IS_AUTO_COLORREF(c) (IS_INDEXED_COLORREF(c) && (GET_COLORREF_INDEX(c) < FIRST_REAL_COLOR || GET_COLORREF_INDEX(c) > LAST_REAL_COLOR))

// This constant means that the color tracks the 'automatic' color specified
// in the crAutoForeground or crAutoBackground member.
#define COLORREF_AUTO (0x02000000)

// This constant means that the color is invalid, or not set.
#define COLORREF_INVALID (0xff000000)

// This constant means that the color tracks the foreground color of another
// item in the list, specified by an index in bits 0-23.
#define COLORREF_TRACK_FG_BY_INDEX (0x04000000)

#define COLORREF_TRACK_BG_BY_INDEX (0x08000000)

#define PALETTE_SIZE (16 + 2 + 7)
#define ACTUAL_COLOR(Color, Auto) GetRGBValue(Color, Auto)

extern int ShellAutoToWin32Constant(int index);
extern COLORREF GetRGBValue(COLORREF crColor, COLORREF crAuto = COLORREF_INVALID, IServiceProvider *pSP = NULL, const GUID *pguidCategory = NULL);
extern void SetPaletteColor(int index, COLORREF color);
extern void SetUserTextColors(REFGUID rguidCategory, COLORREF crForeground, COLORREF crBackground);
extern COLORREF GetPaletteColor(int index);
extern BOOL GetUserTextColors(REFGUID rguidCategory, COLORREF *pcrForeground, COLORREF *pcrBackground);
extern COLORREF* GetColorPalette();


//-------------------------------------------------------------------------------------------------
// class CFontInfoWrapper
//
// Wraps the FontInfo structure (through inheritance) to provide automatic memory management
// features such as cleanup-on-destruct.
class CFontInfoWrapper : public FontInfo
{
public:

    // Static utility methods that may be called at any time.

    static void __stdcall Init(FontInfo &fiSource);
    static void __stdcall Clear(FontInfo &fiSource);
    static void __stdcall Copy(FontInfo &fiDest, const FontInfo &fiSource);
    static bool __stdcall Equal(const FontInfo &fiFirst, const FontInfo &fiSecond);

    // Constructors and destructor

    CFontInfoWrapper();
    CFontInfoWrapper(const FontInfo &fiSource);
    CFontInfoWrapper(const CFontInfoWrapper &fiSource);
    virtual ~CFontInfoWrapper();

    // Operators and additional non-static utility functions.

    void SetName(LPCWSTR wszName);
    CFontInfoWrapper &operator = (const FontInfo &fiSource);
    CFontInfoWrapper &operator = (const CFontInfoWrapper &fiSource);
    bool operator == (const FontInfo &fiSource) const;
    bool operator != (const FontInfo &fiSource) const;
};


//-------------------------------------------------------------------------------------------------
// class CColorableItemInfoWrapper
//
// Wraps the ColorableItemInfo structure (through inheritance) to provide automatic memory
// management features such as cleanup-on-destruct.
class CColorableItemInfoWrapper : public ColorableItemInfo
{
public:

    // Static utility methods that may be called at any time.

    static void __stdcall Init(ColorableItemInfo &ciiSource);
    static bool __stdcall Equal(const ColorableItemInfo &ciiFirst, const ColorableItemInfo &ciiSecond);
    static void __stdcall Copy(ColorableItemInfo &ciiDest, const ColorableItemInfo &ciiSource);

    // Constructors

    CColorableItemInfoWrapper();
    CColorableItemInfoWrapper(const ColorableItemInfo &ciiSource);
    CColorableItemInfoWrapper(const CColorableItemInfoWrapper &ciiSource);

    // Operators and additional non-static utility functions.

    bool operator == (const ColorableItemInfo &ciiSource) const;
    bool operator != (const ColorableItemInfo &info) const;
};


//-------------------------------------------------------------------------------------------------
// class CAllColorableItemInfoWrapper
//
// Wraps the AllColorableItemInfo structure (through inheritance) to provide automatic memory
// management features such as cleanup-on-destruct.
class CAllColorableItemInfoWrapper : public AllColorableItemInfo
{
public:

    // Static utility methods that may be called at any time.

    static void __stdcall Init(AllColorableItemInfo &aciiSource);
    static void __stdcall Clear(AllColorableItemInfo &aciiSource);
    static void __stdcall Copy(AllColorableItemInfo &aciiDest, const AllColorableItemInfo &aciiSource);
    static bool __stdcall Equal(const AllColorableItemInfo &aciiFirst, const AllColorableItemInfo &aciiSecond);

    // Constructors and destructor

    CAllColorableItemInfoWrapper();
    CAllColorableItemInfoWrapper(const AllColorableItemInfo &aciiSource);
    CAllColorableItemInfoWrapper(const CAllColorableItemInfoWrapper &aciiSource);
    virtual ~CAllColorableItemInfoWrapper();

    // Operators and additional non-static utility functions.

    void SetName(LPCWSTR wszName);
    void SetLocalizedName(LPCWSTR wszName);
    void SetDescription(LPCWSTR wszDesc);
    CAllColorableItemInfoWrapper &operator = (const AllColorableItemInfo &aciiSource);
    CAllColorableItemInfoWrapper &operator = (const CAllColorableItemInfoWrapper &aciiSource);
    bool operator == (const AllColorableItemInfo &aciiSource) const;
    bool operator != (const AllColorableItemInfo &aciiSource) const;
};


#endif // _FONTSANDCOLORS_H_
