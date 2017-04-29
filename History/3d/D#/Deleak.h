#ifdef _DEBUG
	#define DEBUG_CLIENTBLOCK   new( _CLIENT_BLOCK, __FILE__, __LINE__)
#else
	#define DEBUG_CLIENTBLOCK
#endif

#define _CRTDBG_MAP_ALLOC
#include <assert.h>
#include <stdlib.h>
#include <crtdbg.h>

#ifdef _DEBUG
inline void EnableMemLeakCheck()
{
   _CrtSetDbgFlag(_CrtSetDbgFlag(_CRTDBG_REPORT_FLAG) | _CRTDBG_LEAK_CHECK_DF);
#ifdef MemLeakBreakPoint
   if (0 != MemLeakBreakPoint){
	   _CrtSetBreakAlloc(MemLeakBreakPoint);
   }
#endif
}
	#define New DEBUG_CLIENTBLOCK
	int OnExit();

	static onexit_t OnExitHandler = onexit(OnExit);
	void BeginDeleak();
	void EndDeleak();

#endif
