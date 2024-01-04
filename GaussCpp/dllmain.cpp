#include "pch.h"

#define EXPORTED_METHOD extern "C" __declspec(dllexport)
EXPORTED_METHOD
void gauss_filter_cpp(short* original_channel, short* modifiable_channel, int offset, int amount)
{
	for (int i = 0; i < amount; i++)
	{
		modifiable_channel[offset] = ((int)original_channel[offset] * 9 +
			(int)original_channel[offset + 1] * 31 + 
			(int)original_channel[offset + 2] * 48 + 
			(int)original_channel[offset + 3] * 31 + 
			(int)original_channel[offset + 4] * 9) / 128;
		offset++;
	}
}

