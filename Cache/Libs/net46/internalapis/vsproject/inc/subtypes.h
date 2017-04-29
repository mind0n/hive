// -------------------------------------------------------------------
// Microsoft Visual Studio
// Copyright (c) Microsoft Corporation. All rights reserved.
// 
// subtypes.h - Recognized file subtype strings set/returned by VSHPROPID_Subtype.
//              The vsproject system modifies the project explorer icon, and 
//              certain behaviours by comparing a files subtype to these strings. These
//              strings are the responsibility of the individual designers to 
//              set. Note that the project system does a case sensitive comparison.
// -------------------------------------------------------------------

#if (_MSC_VER > 1000) && !defined(NO_PRAGMA_ONCE)
#pragma once
#endif
#ifndef __SUBTYPES_H__
#define __SUBTYPES_H__

// These subtypes do not directly support a visual designer.
//
#define VSSUBTYPE_CODE                      L"Code"                 // This is a code file
#define VSSUBTYPE_ASPXCODEBEHIND            L"ASPXCodeBehind"       // A file containing code-behind code; does not support visual design

// But these do.
//
#define VSSUBTYPE_FORM                      L"Form"                 // A file containing a form; supports visual design
#define VSSUBTYPE_CONTROL                   L"UserControl"          // A file containing a custom control; supports visual design
#define VSSUBTYPE_COMPONENT                 L"Component"            // A file containing a component; supports visual design
#define VSSUBTYPE_DESIGNER                  L"Designer"             // A file containing a generic designer; supports visual design

// This subtype can be used with .htm files to force opening the file in the previewer rather than the html editor
#define VSSUBTYPE_PREVIEW                   L"Preview"               // This is a preview file.

#endif
// Enf of subtypes.h

