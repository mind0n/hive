//////////////////////////////////////////////////////
// MatrixCubeApp.cpp
//////////////////////////////////////////////////////
#include "Deleak.h"
#include <windows.h>
#include <d3d9.h>
#include <d3dx9tex.h>
#include <stdio.h>
// Function prototypes.
LRESULT WINAPI WndProc(HWND hWnd, UINT msg,
    WPARAM wParam, LPARAM lParam);
void RegisterWindowClass(HINSTANCE hInstance);
HWND CreateAppWindow(HINSTANCE hInstance);
WPARAM StartMessageLoop();
extern "C"{
	__declspec(dllexport)
HRESULT InitDirect3D(HWND);
}
extern "C"{
	__declspec(dllexport)
void Render();
}
extern "C"{
	__declspec(dllexport)
		void CleanUpDirect3D();
}
void InitVertices();
void InitMatrices();
BOOL CheckDevice(HWND);

// Global variables.
//HWND g_hWnd;
IDirect3D9* g_pDirect3D = NULL;
IDirect3DDevice9* g_pDirect3DDevice = NULL;
IDirect3DVertexBuffer9* g_pVertexBuf = NULL;
IDirect3DSurface9* g_pBackBuffer = NULL;
IDirect3DSurface9* g_pBitmapSurface = NULL;

struct CUSTOMVERTEX
{
    FLOAT x, y, z;
    DWORD color;
};

//////////////////////////////////////////////////////
// WinMain()
//////////////////////////////////////////////////////
INT WINAPI WinMain(HINSTANCE hInstance, HINSTANCE, LPSTR, INT)
{
    RegisterWindowClass(hInstance);
    HWND g_hWnd = CreateAppWindow(hInstance);
    ShowWindow(g_hWnd, SW_SHOWDEFAULT);
    UpdateWindow(g_hWnd);
    HRESULT hResult = InitDirect3D(g_hWnd);
	if (SUCCEEDED(hResult))
	{
		WPARAM result = StartMessageLoop();
	}
    CleanUpDirect3D();
    return 0;
}

//////////////////////////////////////////////////////
// WndProc()
//////////////////////////////////////////////////////
LRESULT WINAPI WndProc(HWND hWnd, UINT msg, WPARAM wParam, LPARAM lParam)
{
    switch(msg)
    {
    case WM_CREATE:
        return 0;

    case WM_DESTROY:
        PostQuitMessage( 0 );
        return 0;

    case WM_PAINT:
        ValidateRect(hWnd, NULL);
        return 0;

    case WM_KEYDOWN:
        switch(wParam)
        {
        case VK_ESCAPE:
            PostQuitMessage(WM_QUIT);
            break;
        }
    }
    return DefWindowProc(hWnd, msg, wParam, lParam);
}

//////////////////////////////////////////////////////
// RegisterWindowClass()
//////////////////////////////////////////////////////
void RegisterWindowClass(HINSTANCE hInstance)
{
    WNDCLASSEX wc;
    wc.cbSize = sizeof(WNDCLASSEX);
    wc.style = CS_HREDRAW | CS_VREDRAW | CS_OWNDC;
    wc.lpfnWndProc = WndProc;
    wc.cbClsExtra = 0;
    wc.cbWndExtra = 0; 
    wc.hInstance = hInstance;
    wc.hIcon = LoadIcon(NULL, IDI_APPLICATION);
    wc.hCursor = (HCURSOR)LoadCursor(NULL, IDC_ARROW);
    wc.hbrBackground = (HBRUSH)GetStockObject(WHITE_BRUSH);
    wc.lpszMenuName = NULL;
    wc.lpszClassName = "MatrixCubeApp";
    wc.hIconSm = NULL;

    RegisterClassEx(&wc);
}

//////////////////////////////////////////////////////
// CreateAppWindow()
//////////////////////////////////////////////////////
HWND CreateAppWindow(HINSTANCE hInstance)
{
    HWND g_hWnd = CreateWindowEx(
        NULL,
        "MatrixCubeApp",
        "Matrix Cube Application", 
        WS_OVERLAPPEDWINDOW,
        100,
        100,
        648,
        514,
        GetDesktopWindow(),
        NULL,
        hInstance,
        NULL);
	return g_hWnd;
}

//////////////////////////////////////////////////////
// StartMessageLoop()
//////////////////////////////////////////////////////
WPARAM StartMessageLoop()
{
    MSG msg;
    while(1)
    {
        if (PeekMessage(&msg, NULL, 0, 0, PM_REMOVE))
        {
            if (msg.message == WM_QUIT)
                break;
            TranslateMessage(&msg);
            DispatchMessage(&msg);
        }
        else
        {
            // Use idle time here.
            Render();
        }
    }
    return msg.wParam;
}

//////////////////////////////////////////////////////
// InitDirect3D()
//////////////////////////////////////////////////////
extern "C"{
__declspec(dllexport)
HRESULT InitDirect3D(HWND hWnd)
{
	BeginDeleak();
    // Create the application's Direct3D object.
    g_pDirect3D = Direct3DCreate9(D3D_SDK_VERSION);
    if (g_pDirect3D == NULL)
        return E_FAIL;

    // Verify that the hardware can handle the required display mode.
    HRESULT hResult = g_pDirect3D->CheckDeviceType(D3DADAPTER_DEFAULT, 
        D3DDEVTYPE_REF, D3DFMT_X8R8G8B8, D3DFMT_X8R8G8B8, FALSE);
    if (hResult != D3D_OK)
    {
        MessageBox(hWnd,
            "Sorry. This program won't\nrun on your system.",
            "DirectX Error", MB_OK);
        return E_FAIL;
    }

    // Create the application's Direct3D device object.
    D3DPRESENT_PARAMETERS D3DPresentParams;
    ZeroMemory(&D3DPresentParams, sizeof(D3DPRESENT_PARAMETERS));
    D3DPresentParams.Windowed = true;
    D3DPresentParams.BackBufferCount = 1;
    D3DPresentParams.BackBufferWidth = 640;
    D3DPresentParams.BackBufferHeight = 480;
    D3DPresentParams.BackBufferFormat = D3DFMT_X8R8G8B8;
    D3DPresentParams.SwapEffect = D3DSWAPEFFECT_DISCARD;
    D3DPresentParams.hDeviceWindow = hWnd;
    hResult = g_pDirect3D->CreateDevice(D3DADAPTER_DEFAULT, 
        D3DDEVTYPE_HAL, hWnd, D3DCREATE_SOFTWARE_VERTEXPROCESSING,
        &D3DPresentParams, &g_pDirect3DDevice);
    if (FAILED(hResult))
        return E_FAIL;

    // Get a pointer to the backbuffer.
    g_pDirect3DDevice->GetBackBuffer(0, 0, D3DBACKBUFFER_TYPE_MONO, &g_pBackBuffer);

    // Create and load a surface for the background bitmap.
    g_pDirect3DDevice->CreateOffscreenPlainSurface(640, 480, 
        D3DFMT_X8R8G8B8, D3DPOOL_DEFAULT, &g_pBitmapSurface, NULL);
    D3DXLoadSurfaceFromFile(g_pBitmapSurface, NULL, NULL,
        "image.bmp", NULL, D3DX_DEFAULT, 0, NULL);

    // Turn off Direct3D's lighting functions.
    g_pDirect3DDevice->SetRenderState(D3DRS_LIGHTING, FALSE);

    // Turn off Direct3D's culling.
    //g_pDirect3DDevice->SetRenderState(D3DRS_CULLMODE, D3DCULL_NONE);
	InitVertices();
    return D3D_OK;
}
}

//////////////////////////////////////////////////////
// CleanUpDirect3D()
//////////////////////////////////////////////////////
extern "C"{
	__declspec(dllexport)
void CleanUpDirect3D()
{
	if (g_pBitmapSurface)
		g_pBitmapSurface->Release();
	if (g_pVertexBuf)
		g_pVertexBuf->Release();
	if (g_pBackBuffer)
		g_pBackBuffer->Release();
    if (g_pDirect3DDevice)
        g_pDirect3DDevice->Release();
    if (g_pDirect3D)
        g_pDirect3D->Release();
	
	EndDeleak();

}
}

//////////////////////////////////////////////////////
// Render()
//////////////////////////////////////////////////////
extern "C"{
	__declspec(dllexport)
	void Render()
{
    // Stop rendering of the app has lost its Direct3D device.
    //if (!CheckDevice()) return;

    // Clear the back buffer to black.
    g_pDirect3DDevice->Clear(0, NULL, D3DCLEAR_TARGET, 
        D3DCOLOR_XRGB(0,0,0), 1.0f, 0 );

    // Set up the Direct3D transformations.
    InitMatrices();

    // Copy the background bitmap to the back buffer.
    g_pDirect3DDevice->StretchRect(g_pBitmapSurface,
        NULL, g_pBackBuffer, NULL, D3DTEXF_NONE);

    // Give Direct3D the vertex buffer and vertex format.
    g_pDirect3DDevice->SetStreamSource(0, g_pVertexBuf, 0,
        sizeof(CUSTOMVERTEX));
    g_pDirect3DDevice->SetFVF(D3DFVF_XYZ | D3DFVF_DIFFUSE);

    // Render the scene.
    g_pDirect3DDevice->BeginScene();
    g_pDirect3DDevice->DrawPrimitive(D3DPT_TRIANGLELIST, 0, 12);
    g_pDirect3DDevice->EndScene();
    g_pDirect3DDevice->Present(NULL, NULL, NULL, NULL);
}
}
//////////////////////////////////////////////////////
// InitVertices()
//////////////////////////////////////////////////////
void InitVertices()
{
CUSTOMVERTEX triangles[] =
{
    // Front of cube.
    { -0.5f, 0.5f, -0.5f, D3DCOLOR_XRGB(255,0,0)},
    {0.5f, -0.5f, -0.5f, D3DCOLOR_XRGB(255,0,0)},
    {-0.5f,  -0.5f, -0.5f, D3DCOLOR_XRGB(255,0,0)},
    { -0.5f, 0.5f, -0.5f, D3DCOLOR_XRGB(0,255,0)},
    {0.5f,  0.5f, -0.5f, D3DCOLOR_XRGB(0,255,0)},
    {0.5f, -0.5f, -0.5f, D3DCOLOR_XRGB(0,255,0)},

    // Right side of cube.
    {0.5f, 0.5f, -0.5f, D3DCOLOR_XRGB(0,0,255)},
    {0.5f, -0.5f, 0.5f, D3DCOLOR_XRGB(0,0,255)},
    {0.5f,  -0.5f, -0.5f, D3DCOLOR_XRGB(0,0,255)},
    {0.5f, 0.5f, -0.5f, D3DCOLOR_XRGB(255,255,0)},
    {0.5f,  0.5f, 0.5f, D3DCOLOR_XRGB(255,255,0)},
    {0.5f, -0.5f, 0.5f, D3DCOLOR_XRGB(255,255,0)},

    // Back of cube.
    {0.5f,  0.5f, 0.5f, D3DCOLOR_XRGB(255,0,255)},
    {-0.5f, -0.5f, 0.5f, D3DCOLOR_XRGB(255,0,255)},
    {0.5f,  -0.5f, 0.5f, D3DCOLOR_XRGB(255,0,255)},
    {0.5f, 0.5f, 0.5f, D3DCOLOR_XRGB(0,255,255)},
    {-0.5f,  0.5f, 0.5f, D3DCOLOR_XRGB(0,255,255)},
    {-0.5f, -0.5f, 0.5f, D3DCOLOR_XRGB(0,255,255)},

    // Left side of cube.
    {-0.5f,  0.5f, 0.5f, D3DCOLOR_XRGB(64,192,64)},
    {-0.5f, -0.5f, -0.5f, D3DCOLOR_XRGB(64,192,64)},
    {-0.5f,  -0.5f, 0.5f, D3DCOLOR_XRGB(64,192,64)},
    {-0.5f, 0.5f, 0.5f, D3DCOLOR_XRGB(64,255,255)},
    {-0.5f,  0.5f, -0.5f, D3DCOLOR_XRGB(64,255,255)},
    {-0.5f, -0.5f, -0.5f, D3DCOLOR_XRGB(64,255,255)},

    // Top of cube.
    {-0.5f,  0.5f, -0.5f, D3DCOLOR_XRGB(3,187,116)},
    {-0.5f, 0.5f, 0.5f, D3DCOLOR_XRGB(3,187,116)},
    {0.5f,  0.5f, -0.5f, D3DCOLOR_XRGB(3,187,116)},
    {-0.5f, 0.5f, 0.5f, D3DCOLOR_XRGB(203,253,42)},
    {0.5f,  0.5f, 0.5f, D3DCOLOR_XRGB(203,253,42)},
    {0.5f, 0.5f, -0.5f, D3DCOLOR_XRGB(203,253,42)},

    // Bottom of cube.
    {-0.5f,  -0.5f, -0.5f, D3DCOLOR_XRGB(255,120,120)},
    {0.5f, -0.5f, -0.5f, D3DCOLOR_XRGB(255,120,120)},
    {-0.5f,  -0.5f, 0.5f, D3DCOLOR_XRGB(255,120,120)},
    {0.5f, -0.5f, -0.5f, D3DCOLOR_XRGB(169,167,245)},
    {0.5f,  -0.5f, 0.5f, D3DCOLOR_XRGB(169,167,245)},
    {-0.5f, -0.5f, 0.5f, D3DCOLOR_XRGB(169,167,245)}
};

	// Create the vertex buffer.
    g_pDirect3DDevice->CreateVertexBuffer(36*sizeof(CUSTOMVERTEX),
        0, D3DFVF_XYZ | D3DFVF_DIFFUSE, D3DPOOL_DEFAULT, &g_pVertexBuf, NULL);

    // Copy the vertices into the buffer.    
    VOID* pVertices;
    g_pVertexBuf->Lock(0, sizeof(triangles), (void**)&pVertices, 0);
    memcpy(pVertices, triangles, sizeof(triangles));
    g_pVertexBuf->Unlock();
}

//////////////////////////////////////////////////////
// InitMatrices()
//////////////////////////////////////////////////////
void InitMatrices()
{
    // Set up the world transformation.
    D3DXMATRIX worldMatrix;
    double radians = timeGetTime() / 1000.0f;
	//double radians = 0.0;
 D3DXMATRIX m_Rotation;
 D3DXMatrixRotationY(&m_Rotation, radians);
 D3DXMATRIX m_Rotation2;
 D3DXMatrixRotationZ(&m_Rotation2, radians);
 D3DXMATRIX m_Translation;
 D3DXMatrixTranslation(&m_Translation,1,1,0);

	//D3DXMatrixRotationY(&worldMatrix, radians);
 D3DXMatrixMultiply(&worldMatrix, &m_Rotation, &m_Translation);
 D3DXMatrixMultiply(&worldMatrix, &worldMatrix, &m_Rotation2);
 
    g_pDirect3DDevice->SetTransform(D3DTS_WORLD, &worldMatrix);
    // Set up the view transformation.
    D3DXVECTOR3 vEyePt(0.0f, 0.0f, -5.0f);
    D3DXVECTOR3 vLookatPt(0.0f, 0.0f, 0.0f);
    D3DXVECTOR3 vUpVec(0.0f, 1.0f, 0.0f);
    D3DXMATRIX viewMatrix;
    D3DXMatrixLookAtLH(&viewMatrix, &vEyePt, &vLookatPt, &vUpVec);
    g_pDirect3DDevice->SetTransform(D3DTS_VIEW, &viewMatrix);

    // Set up the projection transformation.
    D3DXMATRIX projectionMatrix;
    D3DXMatrixPerspectiveFovLH(&projectionMatrix, 
        D3DX_PI/4, 1.0f, 1.0f, 550.0f);
    g_pDirect3DDevice->SetTransform(D3DTS_PROJECTION, &projectionMatrix);
}

//////////////////////////////////////////////////////
// CheckDevice()
//////////////////////////////////////////////////////
BOOL CheckDevice(HWND hWnd)
{
    HRESULT hResult = g_pDirect3DDevice->TestCooperativeLevel();
    if (hResult == D3DERR_DEVICELOST)
        return FALSE;
    else if (hResult == D3DERR_DEVICENOTRESET)
    {
        CleanUpDirect3D();
        InitDirect3D(hWnd);
        InitVertices();
        return TRUE;
    }
    else
        return TRUE;
}
