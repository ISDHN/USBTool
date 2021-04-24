// dllmain.cpp : Defines the entry point for the DLL application.
#include "pch.h"
#include <string>
#include "message.cpp"

#define CONVERT_VERBOSE_FLAG            (0x00000001)
#define CONVERT_NOCHKDSK_FLAG           (0x00000002)
#define CONVERT_FORCE_DISMOUNT_FLAG     (0x00000004)
#define CONVERT_PAUSE_FLAG              (0x00000008)
#define CONVERT_NOSECURITY_FLAG         (0x00000010)

using namespace std;
typedef enum _CONVERT_STATUS {

    CONVERT_STATUS_CONVERTED,
    CONVERT_STATUS_INVALID_FILESYSTEM,
    CONVERT_STATUS_CONVERSION_NOT_AVAILABLE,
    CONVERT_STATUS_CANNOT_LOCK_DRIVE,
    CONVERT_STATUS_ERROR,
    CONVERT_STATUS_INSUFFICIENT_SPACE,
    CONVERT_STATUS_NTFS_RESERVED_NAMES,
    CONVERT_STATUS_DRIVE_IS_DIRTY,
    CONVERT_STATUS_INSUFFICIENT_FREE_SPACE

} CONVERT_STATUS;
typedef bool (__cdecl *ConvertProc)(const wstring*,const wstring*, const wstring*, MESSAGE*,UINT, _CONVERT_STATUS*);

BOOL APIENTRY DllMain( HMODULE hModule,
                       DWORD  ul_reason_for_call,
                       LPVOID lpReserved
                     )
{
    switch (ul_reason_for_call)
    {
    case DLL_PROCESS_ATTACH:
    case DLL_THREAD_ATTACH:
    case DLL_THREAD_DETACH:
    case DLL_PROCESS_DETACH:
        break;
    }
    return TRUE;
}

BOOL Convert(LPCWSTR device) {
    
    HINSTANCE hcnvfat = LoadLibrary(TEXT("cnvfat.dll"));
    ConvertProc procConvert = (ConvertProc)GetProcAddress(hcnvfat, (LPCSTR)"ConvertFAT");
    HINSTANCE hulib = LoadLibrary(TEXT("ulib.dll"));
    MESSAGE m;
    wstring _device = wstring(device);
    wstring _system = wstring(TEXT("NTFS"));
    CONVERT_STATUS status;
    bool r = procConvert(&_device ,&_system,NULL,&m,CONVERT_NOCHKDSK_FLAG,&status);
    return r;
}

