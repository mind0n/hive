#include "Deleak.h"

_CrtMemState Sh1,Sh2,Sh_Diff;

void BeginDeleak(){
	_CrtMemCheckpoint(&Sh1);
}
void EndDeleak(){
	_CrtMemCheckpoint(&Sh2);
	int rlt = _CrtMemDifference(&Sh_Diff, &Sh1, &Sh2);
	_CrtMemDumpAllObjectsSince(&Sh_Diff);
	//_CrtDumpMemoryLeaks();
	assert(rlt==0);
}

int OnExit()
{
	//int i = _CrtDumpMemoryLeaks();
	//assert(i == 0);
	//return i;
	return 0;
}