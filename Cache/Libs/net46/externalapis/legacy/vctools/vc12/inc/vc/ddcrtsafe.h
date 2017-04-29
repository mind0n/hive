/***
*ddcrtsafe.h - helper include file to ease the migration to Secure CRT
*
*       Copyright (c) Microsoft Corporation. All rights reserved.
*
*Purpose:
*       This file completes the Secure CRT contained in the current LKG, fixing some
*       bugs which consistently ease the migration of existing code to Secure CRT.
*
*       This file modifies some of the CRT declarations, so it needs to be included before
*       a set of other include files (see the "check for include order" section below).
*
*       This file will be modified as soon as more fixes come online in the LKG drops.
*
*       Please contact "[....], mcllead" for any issue regarding this file.
*
*       [Internal, Temp]
*
*Revision History:
*       08-24-04  AC    Module created
*       08-25-04  AC    Added missing _mbscpy_s declaration
*                       Added template overload to warn when the size of the string is 1
*       09-16-04  AC    Changed some default values; added some overloads.
*                       Removed some fixes already in LKG8.
*       09-29-04  AC    Added workaround for ecvt_s missing export.
*                       Fixed problem with _DDCRTSAFE_ADD_TRUNCATE_BEHAVIOR switch.
*       10-20-04  AC    Added secure overloads for sprintf family and for tcs functions.
*       01-19-05  AC    Removed workaround for _[w]dupenv_s.
*       01-21-05  AC    Fixed mechanism to turn off ddcrtsafe.h.
*
****/


#pragma once

#if !defined(_INC_DDCRTSAFE)

/* default values for control macros */
#if !defined(_CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES)
#define _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES 1
#endif
#if !defined(_CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES_COUNT)
#define _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES_COUNT 0
#endif
#if !defined(_CRT_SECURE_CPP_OVERLOAD_SECURE_NAMES)
#define _CRT_SECURE_CPP_OVERLOAD_SECURE_NAMES 1
#endif

#include <crtdefs.h>

#if !defined(_CRT_NO_DDCRTSAFE) && defined(__DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_0)
#define _CRT_NO_DDCRTSAFE
#endif

// This was defined conditionally before, and the condition was false
// Moving it here to resolve build break
// ArturL 3/2/2005
#define _CSTRING_BUFFER_SIZE(_CStringObj) ((_CStringObj).GetAllocLength() + 1)

#if defined(_CRT_NO_DDCRTSAFE)

/* check for include order */
#if defined(_INC_MEMORY)
#error ddcrtsafe.h needs to be included before memory.h
#endif
#if defined(_INC_STDIO)
#error ddcrtsafe.h needs to be included before stdio.h
#endif
#if defined(__cplusplus) && defined(_CSTDIO_)
#error ddcrtsafe.h needs to be included before cstdio
#endif
#if defined(_INC_STRING)
#error ddcrtsafe.h needs to be included before string.h
#endif
#if defined(_INC_MBSTRING)
#error ddcrtsafe.h needs to be included before mbstring.h
#endif
#if defined(_INC_STDLIB)
#error ddcrtsafe.h needs to be included before stdlib.h
#endif
#if defined(_INC_WCHAR)
#error ddcrtsafe.h needs to be included before wchar.h
#endif

/* include files */
#include <memory.h>
#include <stdio.h>
#include <string.h>
#include <mbstring.h>
#include <stdlib.h>
#include <wchar.h>
#if defined(__cplusplus)
#include <cstdio>
#endif
#include <errno.h>

#else /* !defined(_CRT_NO_DDCRTSAFE) */

#if !defined(_DDCRTSAFE_ADD_TRUNCATE_BEHAVIOR)
#define _DDCRTSAFE_ADD_TRUNCATE_BEHAVIOR 1
#endif

/* useful macros */

#if !defined(_countof)
#if !defined(__cplusplus) || defined(_DDCRTSAFE_NO_SAFE_COUNTOF)
#define _countof(_Array) (sizeof(_Array) / sizeof(_Array[0]))
#else
template <typename _T, size_t _N>
char (&__countof_helper(_T (&_Array)[_N]))[_N];
#define _countof(_Array) sizeof(__countof_helper(_Array))
#endif
#endif

/* internal helpers */
#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_Name) __ignore_##_Name

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_BEGIN_SECURE_CRT_DEPRECATION_DISABLE \
    __pragma(warning(push)) \
    __pragma(warning(disable:4996))

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_END_SECURE_CRT_DEPRECATION_DISABLE \
    __pragma(warning(pop))

/* */
#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_VALIDATE_RETURN_ERRCODE(_Condition, _RetValue)

/* fix missing export _ecvt_s, see VSW#371591 */
#if defined(_DLL)
#define _ecvt_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_ecvt_s)
#endif

/* check for include order */
#if defined(_INC_MEMORY)
#error ddcrtsafe.h needs to be included before memory.h
#endif
#if defined(_INC_STDIO)
#error ddcrtsafe.h needs to be included before stdio.h
#endif
#if defined(__cplusplus) && defined(_CSTDIO_)
#error ddcrtsafe.h needs to be included before cstdio
#endif
#if defined(_INC_STRING)
#error ddcrtsafe.h needs to be included before string.h
#endif
#if defined(_INC_MBSTRING)
#error ddcrtsafe.h needs to be included before mbstring.h
#endif
#if defined(_INC_STDLIB)
#error ddcrtsafe.h needs to be included before stdlib.h
#endif
#if defined(_INC_WCHAR)
#error ddcrtsafe.h needs to be included before wchar.h
#endif

/* add _TRUNCATE and STRUNCATE, VSW#320455 */
#if !defined(_TRUNCATE)
#define _TRUNCATE ((size_t)-1)
#endif

#if !defined(STRUNCATE)
#define STRUNCATE 80
#endif

/* add overloads for secure functions, VSW#313364 */
#if defined(__cplusplus) && _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES
#define strcpy _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(strcpy)
#define wcscpy _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(wcscpy)
#define _mbscpy _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbscpy)
#define strcat _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(strcat)
#define wcscat _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(wcscat)
#define _mbscat _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbscat)
#define sprintf _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(sprintf)
#define vsprintf _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(vsprintf)
#define _itoa _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_itoa)
#define _itow _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_itow)
#define _ltoa _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_ltoa)
#define _ltow _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_ltow)
#define _ultoa _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_ultoa)
#define _ultow _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_ultow)
#define _makepath _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_makepath)
#define _wmakepath _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_wmakepath)

#if _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES_COUNT
#define strncpy _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(strncpy)
#define wcsncpy _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(wcsncpy)
#define _mbsncpy _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsncpy)
#define _mbsnbcpy _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsnbcpy)
#define strncat _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(strncat)
#define wcsncat _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(wcsncat)
#define _mbsncat _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsncat)
#define _mbsnbcat _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsnbcat)
#endif

#endif

/* add truncate behavior, VSW#320455 */
#if defined(__cplusplus) && _DDCRTSAFE_ADD_TRUNCATE_BEHAVIOR
#define strncpy_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(strncpy_s)
#define wcsncpy_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(wcsncpy_s)
#define _mbsncpy_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsncpy_s)
#define _mbsnbcpy_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsnbcpy_s)
#define strncat_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(strncat_s)
#define wcsncat_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(wcsncat_s)
#define _mbsncat_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsncat_s)
#define _mbsnbcat_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_mbsnbcat_s)
#define _snprintf_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_snprintf_s)
#define _vsnprintf_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_vsnprintf_s)
#define _snwprintf_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_snwprintf_s)
#define _vsnwprintf_s _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_IGNORE_NAME(_vsnwprintf_s)
#endif

/* include files */
#include <memory.h>
#include <stdio.h>
#include <string.h>
#include <mbstring.h>
#include <stdlib.h>
#include <wchar.h>
#if defined(__cplusplus)
#include <cstdio>
#endif
#include <errno.h>

/* begin extern C */
#if defined(__cplusplus)
extern "C" {
#endif

/* fix missing _mbscpy_s declaration */
#if defined(_CRTIMP_ALTERNATIVE)
_CRTIMP_ALTERNATIVE errno_t __cdecl _mbscpy_s(unsigned char *, size_t, const unsigned char *);
#else
_CRTIMP errno_t __cdecl _mbscpy_s(unsigned char *, size_t, const unsigned char *);
#endif

/* fix missing export _ecvt_s, see VSW#371591 */
#if defined(_DLL)

#undef _ecvt_s

__inline
errno_t __cdecl _ecvt_s(char *_Dst, size_t _SizeInBytes, double _Value, int _NDigit, int *_Decpt, int *_Sign)
{
    char *buffer = NULL;
    (_SizeInBytes);
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_BEGIN_SECURE_CRT_DEPRECATION_DISABLE
    buffer = _ecvt(_Value, _NDigit, _Decpt, _Sign);
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_END_SECURE_CRT_DEPRECATION_DISABLE
    if (buffer == NULL)
    {
        *_Dst = 0;
        return errno;
    }
    return strcpy_s(_Dst, _SizeInBytes, buffer);
}

#endif

/* end extern C */
#if defined(__cplusplus)
}
#endif

/* add truncate behavior, VSW#320455 */
#if defined(__cplusplus) && _DDCRTSAFE_ADD_TRUNCATE_BEHAVIOR

#undef strncpy_s
#undef wcsncpy_s
#undef _mbsncpy_s
#undef _mbsnbcpy_s
#undef strncat_s
#undef wcsncat_s
#undef _mbsncat_s
#undef _mbsnbcat_s
#undef _snprintf_s
#undef _vsnprintf_s
#undef _snwprintf_s
#undef _vsnwprintf_s

/* some helpers */
#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CPY_OVERLOAD(_FuncName, _LengthFuncName, _CharType, _CastCharType) \
    extern "C" \
    { \
        __inline \
        errno_t __cdecl __notrunc_##_FuncName(_CharType *_Dst, size_t _Size, const _CharType *_Src, size_t _Count) \
        { \
            errno_t __cdecl _FuncName(_CharType *_Dst, size_t _Size, const _CharType *_Src, size_t _Count); \
            return _FuncName(_Dst, _Size, _Src, _Count); \
        } \
    } \
    template <typename _T> \
    inline \
    errno_t __cdecl _FuncName(_CharType *_Dst, _T _Size, const _CharType *_Src, size_t _Count) \
    { \
        if (_Count == _TRUNCATE) \
        { \
            errno_t _Ret = __notrunc_##_FuncName(_Dst, _Size, _Src, _Size - 1); \
            if (_LengthFuncName((const _CastCharType *)_Src) >= (size_t)_Size) \
            { \
                _Ret = STRUNCATE; \
            } \
            return _Ret; \
        } \
        else \
        { \
            return __notrunc_##_FuncName(_Dst, _Size, _Src, _Count); \
        } \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CAT_OVERLOAD(_FuncName, _LengthFuncName, _CharType, _CastCharType) \
    extern "C" \
    { \
        __inline \
        errno_t __cdecl __notrunc_##_FuncName(_CharType *_Dst, size_t _Size, const _CharType *_Src, size_t _Count) \
        { \
            errno_t __cdecl _FuncName(_CharType *_Dst, size_t _Size, const _CharType *_Src, size_t _Count); \
            return _FuncName(_Dst, _Size, _Src, _Count); \
        } \
    } \
    template <typename _T> \
    inline \
    errno_t __cdecl _FuncName(_CharType *_Dst, _T _Size, const _CharType *_Src, size_t _Count) \
    { \
        if (_Count == _TRUNCATE) \
        { \
            size_t _DstLength = _LengthFuncName((const _CastCharType *)_Dst); \
            errno_t _Ret = __notrunc_##_FuncName(_Dst, _Size, _Src, _Size - _DstLength - 1); \
            if ((_DstLength + _LengthFuncName((const _CastCharType *)_Src)) >= (size_t)_Size) \
            { \
                _Ret = STRUNCATE; \
            } \
            return _Ret; \
        } \
        else \
        { \
            return __notrunc_##_FuncName(_Dst, _Size, _Src, _Count); \
        } \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_OVERLOAD(_FuncName, _AlternativeFuncName, _CharType) \
    extern "C" \
    { \
        __inline \
        errno_t __cdecl __notrunc_##_FuncName(_CharType *_Dst, size_t _Size, const _CharType *_Src, size_t _Count) \
        { \
            errno_t __cdecl _FuncName(_CharType *_Dst, size_t _Size, const _CharType *_Src, size_t _Count); \
            return _FuncName(_Dst, _Size, _Src, _Count); \
        } \
    } \
    template <typename _T> \
    inline \
    errno_t __cdecl _FuncName(_CharType *_Dst, _T _Size, const _CharType *_Src, size_t _Count) \
    { \
        if (_Count == _TRUNCATE) \
        { \
            return _AlternativeFuncName(_Dst, _Size, _Src, _TRUNCATE); \
        } \
        else \
        { \
            return __notrunc_##_FuncName(_Dst, _Size, _Src, _Count); \
        } \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_OVERLOAD_PRINTF(_FuncName, _VFuncName, _CharType) \
    extern "C" \
    { \
    __inline \
    int __cdecl __notrunc_##_VFuncName(_CharType * _Dst, size_t _Size, size_t _Count, const _CharType *_Format, va_list _ArgList) \
    { \
        int __cdecl _VFuncName(_CharType *, size_t, size_t, const _CharType *, va_list); \
        return _VFuncName(_Dst, _Size, _Count, _Format, _ArgList); \
    } \
    } \
    template <typename _T> \
    inline \
    int __cdecl _FuncName(_CharType *_Dst, _T _Size, size_t _Count, const _CharType *_Format, ...) \
    { \
        va_list _ArgList; \
        int _Ret; \
        if (_Count == _TRUNCATE) \
        { \
            _Count = ((size_t)_Size) - 1; \
        } \
        _crt_va_start(_ArgList, _Format); \
        _Ret = __notrunc_##_VFuncName(_Dst, _Size, _Count, _Format, _ArgList); \
        _crt_va_end(_ArgList); \
        return _Ret; \
    } \
    template <typename _T> \
    inline \
    int __cdecl _VFuncName(_CharType *_Dst, _T _Size, size_t _Count, const _CharType *_Format, va_list _ArgList) \
    { \
        if (_Count == _TRUNCATE) \
        { \
            _Count = ((size_t)_Size) - 1; \
        } \
        return __notrunc_##_VFuncName(_Dst, _Size, _Count, _Format, _ArgList); \
    }

/* overload definitions */
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CPY_OVERLOAD(strncpy_s, strlen, char, char)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CPY_OVERLOAD(wcsncpy_s, wcslen, wchar_t, wchar_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CPY_OVERLOAD(_mbsnbcpy_s, strlen, unsigned char, char)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_OVERLOAD(_mbsncpy_s, _mbsnbcpy_s, unsigned char)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CAT_OVERLOAD(strncat_s, strlen, char, char)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CAT_OVERLOAD(wcsncat_s, wcslen, wchar_t, wchar_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_CAT_OVERLOAD(_mbsnbcat_s, strlen, unsigned char, char)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_OVERLOAD(_mbsncat_s, _mbsnbcat_s, unsigned char)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_OVERLOAD_PRINTF(_snprintf_s, _vsnprintf_s, char)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_TRUNCATE_OVERLOAD_PRINTF(_snwprintf_s, _vsnwprintf_s, wchar_t)

#endif /* defined(__cplusplus) && _DDCRTSAFE_ADD_TRUNCATE_BEHAVIOR */

/* add overloads for secure functions, VSW#313364 */
#if defined(__cplusplus) && _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES

/* restore function names */
#undef strcpy
#undef wcscpy
#undef _mbscpy
#undef sprintf
#undef vsprintf
#undef strcat
#undef wcscat
#undef _mbscat
#undef _itoa
#undef _itow
#undef _ltoa
#undef _ltow
#undef _ultoa
#undef _ultow
#undef _makepath
#undef _wmakepath

#if _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES_COUNT
#undef strncpy
#undef wcsncpy
#undef _mbsncpy
#undef _mbsnbcpy
#undef strncat
#undef wcsncat
#undef _mbsncat
#undef _mbsnbcat
#endif

/* some helpers */
#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1(_ReturnType, _FuncName, _DstType, _TType1) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1) \
    { \
        return (_FuncName##_s(_Dst, _Size, _TArg1) == 0 ? _Dst : NULL); \
    } \
    template <> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName<1>(_DstType (&_Dst)[1], _TType1 _TArg1) \
    { \
        return (_FuncName##_s(_Dst, 1, _TArg1) == 0 ? _Dst : NULL); \
    } \
    extern "C" \
    { \
    __inline \
    _ReturnType __cdecl __unsecure_##_FuncName(_DstType * _Dst, _TType1 _TArg1) \
    { \
        _ReturnType __cdecl _FuncName(_DstType *, _TType1); \
        return _FuncName(_Dst, _TArg1); \
    } \
    } \
    template <typename _T> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName(_T _Dst, _TType1 _TArg1) \
    { \
        return __unsecure_##_FuncName(_Dst, _TArg1); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(_ReturnType, _FuncName, _DstType, _TType1, _TType2) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, _TType2 _TArg2) \
    { \
        return (_FuncName##_s(_Dst, _Size, _TArg1, _TArg2) == 0 ? _Dst : NULL); \
    } \
    template <> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName<1>(_DstType (&_Dst)[1], _TType1 _TArg1, _TType2 _TArg2) \
    { \
        return (_FuncName##_s(_Dst, 1, _TArg1, _TArg2) == 0 ? _Dst : NULL); \
    } \
    extern "C" \
    { \
    __inline \
    _ReturnType __cdecl __unsecure_##_FuncName(_DstType * _Dst, _TType1 _TArg1, _TType2 _TArg2) \
    { \
        _ReturnType __cdecl _FuncName(_DstType *, _TType1, _TType2); \
        return _FuncName(_Dst, _TArg1, _TArg2); \
    } \
    } \
    template <typename _T> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName(_T _Dst, _TType1 _TArg1, _TType2 _TArg2) \
    { \
        return __unsecure_##_FuncName(_Dst, _TArg1, _TArg2); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_4_VOID(_FuncName, _DstType, _TType1, _TType2, _TType3, _TType4) \
    template <size_t _Size> \
    inline \
    void __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, _TType2 _TArg2, _TType3 _TArg3, _TType4 _TArg4) \
    { \
        _FuncName##_s(_Dst, _Size, _TArg1, _TArg2, _TArg3, _TArg4); \
    } \
    template <> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    void __cdecl _FuncName<1>(_DstType (&_Dst)[1], _TType1 _TArg1, _TType2 _TArg2, _TType3 _TArg3, _TType4 _TArg4) \
    { \
        _FuncName##_s(_Dst, 1, _TArg1, _TArg2, _TArg3, _TArg4); \
    } \
    extern "C" \
    { \
    __inline \
    void __cdecl __unsecure_##_FuncName(_DstType * _Dst, _TType1 _TArg1, _TType2 _TArg2, _TType3 _TArg3, _TType4 _TArg4) \
    { \
        void __cdecl _FuncName(_DstType *, _TType1, _TType2, _TType3, _TType4); \
        _FuncName(_Dst, _TArg1, _TArg2, _TArg3, _TArg4); \
    } \
    } \
    template <typename _T> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    void __cdecl _FuncName(_T _Dst, _TType1 _TArg1, _TType2 _TArg2, _TType3 _TArg3, _TType4 _TArg4) \
    { \
        __unsecure_##_FuncName(_Dst, _TArg1, _TArg2, _TArg3, _TArg4); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_1_1(_ReturnType, _FuncName, _HType1, _DstType, _TType1) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_HType1 _HArg1, _DstType (&_Dst)[_Size], _TType1 _TArg1) \
    { \
        return (_FuncName##_s(_HArg1, _Dst, _Size, _TArg1) == 0 ? _Dst : NULL); \
    } \
    template <> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName<1>(_HType1 _HArg1, _DstType (&_Dst)[1], _TType1 _TArg1) \
    { \
        return (_FuncName##_s(_HArg1, _Dst, 1, _TArg1) == 0 ? _Dst : NULL); \
    } \
    extern "C" \
    { \
    __inline \
    _ReturnType __cdecl __unsecure_##_FuncName(_HType1 _HArg1, _DstType * _Dst, _TType1 _TArg1) \
    { \
        _ReturnType __cdecl _FuncName(_HType1, _DstType *, _TType1); \
        return _FuncName(_HArg1, _Dst, _TArg1); \
    } \
    } \
    template <typename _T> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName(_HType1 _HArg1, _T _Dst, _TType1 _TArg1) \
    { \
        return __unsecure_##_FuncName(_HArg1, _Dst, _TArg1); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1_ARGLIST(_ReturnType, _FuncName, _VFuncName, _DstType, _TType1) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, ...) \
    { \
        va_list _ArgList; \
        _ReturnType _Ret; \
        _crt_va_start(_ArgList, _TArg1); \
        _Ret = _VFuncName##_s(_Dst, _Size, _TArg1, _ArgList); \
        _crt_va_end(_ArgList); \
        return _Ret; \
    } \
    template <> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName<1>(_DstType (&_Dst)[1], _TType1 _TArg1, ...) \
    { \
        va_list _ArgList; \
        _ReturnType _Ret; \
        _crt_va_start(_ArgList, _TArg1); \
        _Ret = _VFuncName##_s(_Dst, 1, _TArg1, _ArgList); \
        _crt_va_end(_ArgList); \
        return _Ret; \
    } \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _VFuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, va_list _ArgList) \
    { \
        return _VFuncName##_s(_Dst, _Size, _TArg1, _ArgList); \
    } \
    template <> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _VFuncName<1>(_DstType (&_Dst)[1], _TType1 _TArg1, va_list _ArgList) \
    { \
        return _VFuncName##_s(_Dst, 1, _TArg1, _ArgList); \
    } \
    extern "C" \
    { \
    __inline \
    _ReturnType __cdecl __unsecure_##_VFuncName(_DstType * _Dst, _TType1 _TArg1, va_list _ArgList) \
    { \
        _ReturnType __cdecl _VFuncName(_DstType *, _TType1, va_list); \
        return _VFuncName(_Dst, _TArg1, _ArgList); \
    } \
    } \
    template <typename _T> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _FuncName(_T _Dst, _TType1 _TArg1, ...) \
    { \
        va_list _ArgList; \
        _ReturnType _Ret; \
        _crt_va_start(_ArgList, _TArg1); \
        _Ret = __unsecure_##_VFuncName(_Dst, _TArg1, _ArgList); \
        _crt_va_end(_ArgList); \
        return _Ret; \
    } \
    template <typename _T> \
    inline \
    _CRT_INSECURE_DEPRECATE \
    _ReturnType __cdecl _VFuncName(_T _Dst, _TType1 _TArg1, va_list _ArgList) \
    { \
        return __unsecure_##_VFuncName(_Dst, _TArg1, _ArgList); \
    }

_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1(char *, strcpy, char, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1(wchar_t *, wcscpy, wchar_t, const wchar_t *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1(unsigned char *, _mbscpy, unsigned char, const unsigned char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1(char *, strcat, char, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1(wchar_t *, wcscat, wchar_t, const wchar_t *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1(unsigned char *, _mbscat, unsigned char, const unsigned char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_1_ARGLIST(int, sprintf, vsprintf, char, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_1_1(char *, _itoa, int, char, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_1_1(wchar_t *, _itow, int, wchar_t, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_1_1(char *, _ltoa, long, char, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_1_1(wchar_t *, _ltow, long, wchar_t, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_1_1(char *, _ultoa, unsigned long, char, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_1_1(wchar_t *, _ultow, unsigned long, wchar_t, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_4_VOID(_makepath, char, const char *, const char *, const char *, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_4_VOID(_wmakepath, wchar_t, const wchar_t *, const wchar_t *, const wchar_t *, const wchar_t *)

#if _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES_COUNT
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(char *, strncpy, char, const char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(wchar_t *, wcsncpy, wchar_t, const wchar_t *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(unsigned char *, _mbsncpy, unsigned char, const unsigned char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(unsigned char *, _mbsnbcpy, unsigned char, const unsigned char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(char *, strncat, char, const char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(wchar_t *, wcsncat, wchar_t, const wchar_t *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(unsigned char *, _mbsncat, unsigned char, const unsigned char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_STANDARD_FUNC_0_2(unsigned char *, _mbsnbcat, unsigned char, const unsigned char *, size_t)
#endif

#endif /* defined(__cplusplus) && _CRT_SECURE_CPP_OVERLOAD_STANDARD_NAMES */

/* add overloads for secure functions, VSW#313364 */
#if defined(__cplusplus) && _CRT_SECURE_CPP_OVERLOAD_SECURE_NAMES

/* some helpers */
#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1(_ReturnType, _FuncName, _DstType, _TType1) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1) \
    { \
        return _FuncName(_Dst, _Size, _TArg1); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(_ReturnType, _FuncName, _DstType, _TType1, _TType2) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, _TType2 _TArg2) \
    { \
        return _FuncName(_Dst, _Size, _TArg1, _TArg2); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_4(_ReturnType, _FuncName, _DstType, _TType1, _TType2, _TType3, _TType4) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, _TType2 _TArg2, _TType3 _TArg3, _TType4 _TArg4) \
    { \
        return _FuncName(_Dst, _Size, _TArg1, _TArg2, _TArg3, _TArg4); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_1_1(_ReturnType, _FuncName, _HType1, _DstType, _TType1) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_HType1 _HArg1, _DstType (&_Dst)[_Size], _TType1 _TArg1) \
    { \
        return _FuncName(_HArg1, _Dst, _Size, _TArg1); \
    }

#define _DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1_ARGLIST(_ReturnType, _FuncName, _VFuncName, _DstType, _TType1) \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _FuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, ...) \
    { \
        va_list _ArgList; \
        _ReturnType _Ret; \
        _crt_va_start(_ArgList, _TArg1); \
        _Ret = _VFuncName(_Dst, _Size, _TArg1, _ArgList); \
        _crt_va_end(_ArgList); \
        return _Ret; \
    } \
    template <size_t _Size> \
    inline \
    _ReturnType __cdecl _VFuncName(_DstType (&_Dst)[_Size], _TType1 _TArg1, va_list _ArgList) \
    { \
        return _VFuncName(_Dst, _Size, _TArg1, _ArgList); \
    }

/* overload definitions */
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1(errno_t, strcpy_s, char, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1(errno_t, wcscpy_s, wchar_t, const wchar_t *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1(errno_t, _mbscpy_s, unsigned char, unsigned const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1(errno_t, strcat_s, char, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1(errno_t, wcscat_s, wchar_t, const wchar_t *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1(errno_t, _mbscat_s, unsigned char, unsigned const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, strncpy_s, char, const char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, wcsncpy_s, wchar_t, const wchar_t *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, _mbsncpy_s, unsigned char, const unsigned char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, _mbsnbcpy_s, unsigned char, const unsigned char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, strncat_s, char, const char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, wcsncat_s, wchar_t, const wchar_t *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, _mbsncat_s, unsigned char, const unsigned char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, _mbsnbcat_s, unsigned char, const unsigned char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_1_1(errno_t, _itoa_s, int, char, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_1_1(errno_t, _itow_s, int, wchar_t, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_1_1(errno_t, _ltoa_s, long, char, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_1_1(errno_t, _ltow_s, long, wchar_t, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_1_1(errno_t, _ultoa_s, unsigned long, char, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_1_1(errno_t, _ultow_s, unsigned long, wchar_t, int)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_4(errno_t, _makepath_s, char, const char *, const char *, const char *, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_4(errno_t, _wmakepath_s, wchar_t, const wchar_t *, const wchar_t *, const wchar_t *, const wchar_t *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1_ARGLIST(int, sprintf_s, vsprintf_s, char, const char *)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_1_ARGLIST(int, swprintf_s, vswprintf_s, char, const char *)

#if !defined(_MB_MAP_DIRECT)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, _tcsncpy_s, char, const char *, size_t)
_DDCRTSAFE_DO_NO_USE_OUTSIDE_THIS_FILE_DEFINE_CPP_OVERLOAD_SECURE_FUNC_0_2(errno_t, _tcsncat_s, char, const char *, size_t)
#endif

#endif /* defined(__cplusplus) && _CRT_SECURE_CPP_OVERLOAD_SECURE_NAMES */

#endif /* _CRT_NO_DDCRTSAFE */

#define _INC_DDCRTSAFE

#endif /* _INC_DDCRTSAFE */
