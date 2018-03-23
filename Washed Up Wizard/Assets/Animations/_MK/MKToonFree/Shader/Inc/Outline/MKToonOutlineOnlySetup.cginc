//outline setup
#ifndef MK_TOON_OUTLINE_ONLY
	#define MK_TOON_OUTLINE_ONLY

	#ifndef MKTOON_OUTLINE_ONLY_PASS
		#define MKTOON_OUTLINE_ONLY_PASS 1
	#endif

	#include "UnityCG.cginc"

	#include "../Common/MKToonDef.cginc"
	#include "../Common/MKToonV.cginc"
	#include "../Common/MKToonInc.cginc"
	#include "MKToonOutlineOnlyIO.cginc"
#endif