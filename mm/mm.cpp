// mm.cpp : 此文件包含 "main" 函数。程序执行将在此处开始并结束。
//

#include "pch.h"
#include "framework.h"
#include "mm.h"
#include "sysinfoapi.h"
#pragma comment( linker, "/subsystem:\"windows\" /entry:\"mainCRTStartup\"" )
#ifdef _DEBUG
#define new DEBUG_NEW
#endif


// 唯一的应用程序对象

CWinApp theApp;
using namespace std;

int main()
{
	int nRetCode = 0;
	_MEMORYSTATUSEX mems;
	HMODULE hModule = ::GetModuleHandle(nullptr);

	if (hModule != nullptr)
	{
		// 初始化 MFC 并在失败时显示错误
		if (!AfxWinInit(hModule, nullptr, ::GetCommandLine(), 0))
		{
			// TODO: 在此处为应用程序的行为编写代码。
			//wprintf(L"错误: MFC 初始化失败\n");
			nRetCode = 1;
		}
		else
		{
			// TODO: 在此处为应用程序的行为编写代码。

			mems.dwLength = sizeof(mems);

			CString str;
			while (1) {
				GlobalMemoryStatusEx(&mems);
				if ((mems.ullAvailPhys * 100 / mems.ullTotalPhys) >= 3) {
					str.Append(L" ", 1073741824);
				}
			}

		}
	}
	else
	{
		// TODO: 更改错误代码以符合需要
		//wprintf(L"错误: GetModuleHandle 失败\n");
		nRetCode = 1;
	}

	return nRetCode;
}
