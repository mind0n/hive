#pragma once

#include <windows.h>

namespace WPFUtils
{
    #if _MANAGED
    /// <SecurityNote>
    /// Critical - Receives a native pointer as parameter.
    ///            Loads a dll from an input path.
    /// </SecurityNote>
    [System::Security::SecurityCritical]
    #endif
    HMODULE LoadDWriteLibraryAndGetProcAddress(const wchar_t *pwszWpftxtPath, void **pfncptrDWriteCreateFactory);
}
