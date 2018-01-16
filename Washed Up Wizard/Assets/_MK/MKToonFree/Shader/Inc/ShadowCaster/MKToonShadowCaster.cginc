//shadow rendering input and output
#ifndef MK_TOON_SHADOWCASTER
	#define MK_TOON_SHADOWCASTER

	/////////////////////////////////////////////////////////////////////////////////////////////
	// VERTEX SHADER
	/////////////////////////////////////////////////////////////////////////////////////////////
	void vertShadowCaster (VertexInputShadowCaster v, out VertexOutputShadowCaster o, out float4 pos : SV_POSITION)
	{
		UNITY_SETUP_INSTANCE_ID(v);
		UNITY_INITIALIZE_OUTPUT(VertexOutputShadowCaster, o);
		UNITY_TRANSFER_INSTANCE_ID(v,o);
		UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

		#ifdef SHADOWS_CUBE //point light shadows
			pos = UnityObjectToClipPos(v.vertex);
			o.sv = mul(unity_ObjectToWorld, v.vertex).xyz - _LightPositionRange.xyz;
		#else //other shadows
			//pos with unity macros
			pos = UnityClipSpaceShadowCasterPos(v.vertex.xyz, v.normal);
			pos = UnityApplyLinearShadowBias(pos);
		#endif
	}

	/////////////////////////////////////////////////////////////////////////////////////////////
	// FRAGMENT SHADER
	/////////////////////////////////////////////////////////////////////////////////////////////
	half4 fragShadowCaster 
		(
			VertexOutputShadowCaster o
			#if MKTOON_DITHER_MASK
				, UNITY_VPOS_TYPE vpos : VPOS
			#endif
		) : SV_Target
	{	
		UNITY_SETUP_INSTANCE_ID(o);
		#ifdef SHADOWS_CUBE
			return UnityEncodeCubeShadowDepth ((length(o.sv) + unity_LightShadowBias.x) * _LightPositionRange.w);
		#else
			return 0;
		#endif
	}			
#endif