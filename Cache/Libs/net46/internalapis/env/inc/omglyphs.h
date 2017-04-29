////////////////////////////////////////////////////////////////////////////////
// OMGlyphs.H
//
// This file defines indexes into the imagelist OMStdBmps.bmp

#pragma once

#define OM_GLYPH_NONE                 -1
#define OM_GLYPH_UNKNOWN              -1

// Images are determined by an OM_GLYPH_* + OM_GLYPH_ACC_* value.  There are OM_GLYPH_ACC_TYPE_COUNT versions of each image.
#define OM_GLYPH_ACC_TYPE_COUNT      6

#define OM_GLYPH_CLASS               (OM_GLYPH_ACC_TYPE_COUNT * 0)
#define OM_GLYPH_CONSTANT            (OM_GLYPH_ACC_TYPE_COUNT * 1)
#define OM_GLYPH_DELEGATE            (OM_GLYPH_ACC_TYPE_COUNT * 2)
#define OM_GLYPH_ENUM                (OM_GLYPH_ACC_TYPE_COUNT * 3)
#define OM_GLYPH_ENUMMEMBER          (OM_GLYPH_ACC_TYPE_COUNT * 4)
#define OM_GLYPH_EVENT               (OM_GLYPH_ACC_TYPE_COUNT * 5)
#define OM_GLYPH_EXCEPTION           (OM_GLYPH_ACC_TYPE_COUNT * 6)
#define OM_GLYPH_FIELD               (OM_GLYPH_ACC_TYPE_COUNT * 7)
#define OM_GLYPH_INTERFACE           (OM_GLYPH_ACC_TYPE_COUNT * 8)
#define OM_GLYPH_MACRO               (OM_GLYPH_ACC_TYPE_COUNT * 9)
#define OM_GLYPH_MAP                 (OM_GLYPH_ACC_TYPE_COUNT * 10)
#define OM_GLYPH_MAPITEM             (OM_GLYPH_ACC_TYPE_COUNT * 11)
#define OM_GLYPH_METHOD              (OM_GLYPH_ACC_TYPE_COUNT * 12)
#define OM_GLYPH_OVERLOAD            (OM_GLYPH_ACC_TYPE_COUNT * 13)
#define OM_GLYPH_MODULE              (OM_GLYPH_ACC_TYPE_COUNT * 14)
#define OM_GLYPH_NAMESPACE           (OM_GLYPH_ACC_TYPE_COUNT * 15)
#define OM_GLYPH_OPERATOR            (OM_GLYPH_ACC_TYPE_COUNT * 16)
#define OM_GLYPH_PROPERTY            (OM_GLYPH_ACC_TYPE_COUNT * 17)
#define OM_GLYPH_STRUCT              (OM_GLYPH_ACC_TYPE_COUNT * 18)
#define OM_GLYPH_TEMPLATE            (OM_GLYPH_ACC_TYPE_COUNT * 19)
#define OM_GLYPH_TYPEDEF             (OM_GLYPH_ACC_TYPE_COUNT * 20)
#define OM_GLYPH_TYPE                (OM_GLYPH_ACC_TYPE_COUNT * 21)
#define OM_GLYPH_UNION               (OM_GLYPH_ACC_TYPE_COUNT * 22)
#define OM_GLYPH_VARIABLE            (OM_GLYPH_ACC_TYPE_COUNT * 23)
#define OM_GLYPH_VALUETYPE           (OM_GLYPH_ACC_TYPE_COUNT * 24)
#define OM_GLYPH_INTRINSIC           (OM_GLYPH_ACC_TYPE_COUNT * 25)

#define OM_GLYPH_JSHARP_METHOD       (OM_GLYPH_ACC_TYPE_COUNT * 26)
#define OM_GLYPH_JSHARP_FIELD        (OM_GLYPH_ACC_TYPE_COUNT * 27)
#define OM_GLYPH_JSHARP_CLASS        (OM_GLYPH_ACC_TYPE_COUNT * 28)
#define OM_GLYPH_JSHARP_NAMESPACE    (OM_GLYPH_ACC_TYPE_COUNT * 29)
#define OM_GLYPH_JSHARP_INTERFACE    (OM_GLYPH_ACC_TYPE_COUNT * 30)

#define OM_GLYPH_ERROR               (OM_GLYPH_ACC_TYPE_COUNT * 31)       // Error glyph (NOTE:  Do not add OM_GLYPH_ACC_ to this!!!)
#define OM_GLYPH_BSCFILE             (OM_GLYPH_ERROR + 5)
#define OM_GLYPH_ASSEMBLY	         (OM_GLYPH_ERROR + 6)
#define OM_GLYPH_LIBRARY             (OM_GLYPH_ERROR + 7)
#define OM_GLYPH_VBPROJECT           (OM_GLYPH_ERROR + 8)
#define OM_GLYPH_COOLPROJECT         (OM_GLYPH_ERROR + 10)
#define OM_GLYPH_CPPPROJECT          (OM_GLYPH_ERROR + 13)
#define OM_GLYPH_DIALOG_ID           (OM_GLYPH_ERROR + 14)
#define OM_GLYPH_OPEN_FOLDER         (OM_GLYPH_ERROR + 15)
#define OM_GLYPH_CLOSED_FOLDER       (OM_GLYPH_ERROR + 16)
#define OM_GLYPH_ARROW               (OM_GLYPH_ERROR + 17)
#define OM_GLYPH_CSHARPFILE          (OM_GLYPH_ERROR + 18)
#define OM_GLYPH_CSHARPEXPANSION     (OM_GLYPH_ERROR + 19)
#define OM_GLYPH_KEYWORD             (OM_GLYPH_ERROR + 20)
#define OM_GLYPH_INFORMATION         (OM_GLYPH_ERROR + 21)
#define OM_GLYPH_REFERENCE           (OM_GLYPH_ERROR + 22)
#define OM_GLYPH_RECURSION           (OM_GLYPH_ERROR + 23)
#define OM_GLYPH_XMLITEM             (OM_GLYPH_ERROR + 24)
#define OM_GLYPH_JSHARP_PROJECT      (OM_GLYPH_ERROR + 25)
#define OM_GLYPH_JSHARP_DOCUMENT     (OM_GLYPH_ERROR + 26)
#define OM_GLYPH_FORWARDTYPE         (OM_GLYPH_ERROR + 27)
#define OM_GLYPH_CALLERSGRAPH        (OM_GLYPH_ERROR + 28)
#define OM_GLYPH_CALLGRAPH           (OM_GLYPH_ERROR + 29)
#define OM_GLYPH_WARNING             (OM_GLYPH_ERROR + 30)
#define OM_GLYPH_MAYBE_REFERENCE     (OM_GLYPH_ERROR + 31)
#define OM_GLYPH_MAYBE_CALLER        (OM_GLYPH_ERROR + 32)
#define OM_GLYPH_MAYBE_CALL          (OM_GLYPH_ERROR + 33)

#define OM_GLYPH_EXTENSIONMETHOD              (OM_GLYPH_ERROR + 34)
#define OM_GLYPH_EXTENSIONMETHOD_INTERNAL     (OM_GLYPH_ERROR + 35)	//not used in code specifically but presetn to reserve this ID
#define OM_GLYPH_EXTENSIONMETHOD_FRIEND       (OM_GLYPH_ERROR + 36) //not used in code specifically but presetn to reserve this ID
#define OM_GLYPH_EXTENSIONMETHOD_PROTECTED    (OM_GLYPH_ERROR + 37) //not used in code specifically but presetn to reserve this ID
#define OM_GLYPH_EXTENSIONMETHOD_PRIVATE      (OM_GLYPH_ERROR + 38) //not used in code specifically but presetn to reserve this ID
#define OM_GLYPH_EXTENSIONMETHOD_SHORTCUT     (OM_GLYPH_ERROR + 39) //not used in code specifically but presetn to reserve this ID

#define OM_GLYPH_XMLATTRIBUTE               (OM_GLYPH_ERROR + 40)
#define OM_GLYPH_XMLCHILD                   (OM_GLYPH_ERROR + 41)
#define OM_GLYPH_XMLDESCENDANT              (OM_GLYPH_ERROR + 42)
#define OM_GLYPH_XMLNAMESPACE               (OM_GLYPH_ERROR + 43)
#define OM_GLYPH_XMLATTRIBUTE_QUESTION      (OM_GLYPH_ERROR + 44)
#define OM_GLYPH_XMLATTRIBUTE_CHECK         (OM_GLYPH_ERROR + 45)
#define OM_GLYPH_XMLCHILD_QUESTION          (OM_GLYPH_ERROR + 46)
#define OM_GLYPH_XMLCHILD_CHECK             (OM_GLYPH_ERROR + 47)
#define OM_GLYPH_XMLDESCENDANT_QUESTION     (OM_GLYPH_ERROR + 48)
#define OM_GLYPH_XMLDESCENDANT_CHECK        (OM_GLYPH_ERROR + 49)

#define OM_GLYPH_ACC_PUBLIC          0
#define OM_GLYPH_ACC_INTERNAL        1
#define OM_GLYPH_ACC_FRIEND          2
#define OM_GLYPH_ACC_PROTECTED       3
#define OM_GLYPH_ACC_PRIVATE         4
#define OM_GLYPH_ACC_SHORTCUT        5

#define OM_GLYPH_IMGLIST_WIDTH           16
#define OM_GLYPH_IMGLIST_BACKGROUND      RGB(255,0,255)

// Given a current glyph, returns its base glyph type. If given a glyph outside
// the range of those with different access classes, it return OM_GLYPH_UNKNOWN.
#define OM_GLYPH_GET_BASE_GLYPH(x) (((x)<OM_GLYPH_ERROR)?(x) - ((x)%OM_GLYPH_ACC_TYPE_COUNT):OM_GLYPH_UNKNOWN)
