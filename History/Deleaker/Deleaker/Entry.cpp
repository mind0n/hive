#include "Deleak.h"
#include <stdio.h>
void main()
{
	char * pstr;
	BeginDeleak();
	pstr = New char[5];
	pstr = New char[9];
	EndDeleak();
	getchar();
}