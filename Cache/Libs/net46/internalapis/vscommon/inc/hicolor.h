#pragma once

//------------------------------------------------------------------------------
// hicolor.h
// Author: Dylan Lingelbach [[....]]
//
// Helpers for hicolors and IVsHiColorItem
//------------------------------------------------------------------------------

#ifndef _HICOLOR_H_
#define _HICOLOR_H_

enum HICOLOR
{
    HC_FIRST_HICOLOR        = 0,
    HC_BLACK                = HC_FIRST_HICOLOR,
    HC_WHITE                = 1,
    HC_MAROON               = 2,
    HC_DARKGREEN            = 3,
    HC_BROWN                = 4,
    HC_DARKBLUE             = 5, 
    HC_PURPLE               = 6,
    HC_AQUAMARINE           = 7,
    HC_LIGHTGRAY            = 8,
    HC_DARKGRAY             = 9,
    HC_RED                  = 10,
    HC_GREEN                = 11,
    HC_YELLOW               = 12,
    HC_BLUE                 = 13,
    HC_MAGENTA              = 14,
    HC_CYAN                 = 15,     
    HC_FOREGROUND_TEAL      = 16,
    HC_BOOKMARK_BLUE        = 17,
    HC_BRACEMATCH_BEIGE     = 18,
    HC_BACKGROUND_MAROON    = 19,
    HC_FOREGROUND_YELLOW    = 20,
    HC_BACKGROUND_GREEN     = 21,
    HC_FOREGROUND_MAROON    = 22,
    HC_BACKGROUND_YELLOW    = 23,
    HC_FOREGROUND_NAVY      = 24,
    HC_BACKGROUND_SILVER    = 25,
    HC_FOREGROUND_PURPLE    = 26,
    HC_TRACKCHANGES_GREEN   = 27,
    HC_TRACKCHANGES_YELLOW  = 28,
    HC_LIGHTBLUE            = 29,
    HC_LIGHTGREEN           = 30,
    HC_LIGHTMAROON          = 31,
    HC_LIGHTRED             = 32,
    HC_LIGHTBLACK           = 33,
    HC_LIGHTYELLOW          = 34,
    HC_LAST_HICOLOR = HC_LIGHTYELLOW
};

static const size_t HICOLOR_PALETTE_SIZE = HC_LAST_HICOLOR + 1;

extern const COLORREF EncodeHiColor(HICOLOR hcColor);

#endif // _HICOLORS_H_
