    Shader "Hidden/glareFxCheapSM20" {
    Properties {
        _MainTex ("Input", RECT) = "white" {}
        _OrgTex ("Input", RECT) = "white" {}
        _lensDirt ("Input", RECT) = "white" {}
        
        _blurSamples ("", Float) = 5
        _threshold ("", Float) = 0.5
        _int ("", Float) = 1.0
    }
        SubShader {
            Pass {
                ZTest Always Cull Off ZWrite Off
                Fog { Mode off }
           
        Program "vp" {
// Vertex combos: 1
//   opengl - ALU: 8 to 8
//   d3d9 - ALU: 8 to 8
SubProgram "opengl " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
"!!ARBvp1.0
# 8 ALU
PARAM c[9] = { { 0 },
		state.matrix.mvp,
		state.matrix.texture[0] };
TEMP R0;
MOV R0.zw, c[0].x;
MOV R0.xy, vertex.texcoord[0];
DP4 result.texcoord[0].y, R0, c[6];
DP4 result.texcoord[0].x, R0, c[5];
DP4 result.position.w, vertex.position, c[4];
DP4 result.position.z, vertex.position, c[3];
DP4 result.position.y, vertex.position, c[2];
DP4 result.position.x, vertex.position, c[1];
END
# 8 instructions, 1 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [glstate_matrix_texture0]
"vs_2_0
; 8 ALU
def c8, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
mov r0.zw, c8.x
mov r0.xy, v1
dp4 oT0.y, r0, c5
dp4 oT0.x, r0, c4
dp4 oPos.w, v0, c3
dp4 oPos.z, v0, c2
dp4 oPos.y, v0, c1
dp4 oPos.x, v0, c0
"
}

SubProgram "flash " {
Keywords { }
Bind "vertex" Vertex
Bind "texcoord" TexCoord0
Matrix 0 [glstate_matrix_mvp]
Matrix 4 [glstate_matrix_texture0]
"agal_vs
c8 0.0 0.0 0.0 0.0
[bc]
aaaaaaaaaaaaamacaiaaaaaaabaaaaaaaaaaaaaaaaaaaaaa mov r0.zw, c8.x
aaaaaaaaaaaaadacadaaaaoeaaaaaaaaaaaaaaaaaaaaaaaa mov r0.xy, a3
bdaaaaaaaaaaacaeaaaaaaoeacaaaaaaafaaaaoeabaaaaaa dp4 v0.y, r0, c5
bdaaaaaaaaaaabaeaaaaaaoeacaaaaaaaeaaaaoeabaaaaaa dp4 v0.x, r0, c4
bdaaaaaaaaaaaiadaaaaaaoeaaaaaaaaadaaaaoeabaaaaaa dp4 o0.w, a0, c3
bdaaaaaaaaaaaeadaaaaaaoeaaaaaaaaacaaaaoeabaaaaaa dp4 o0.z, a0, c2
bdaaaaaaaaaaacadaaaaaaoeaaaaaaaaabaaaaoeabaaaaaa dp4 o0.y, a0, c1
bdaaaaaaaaaaabadaaaaaaoeaaaaaaaaaaaaaaoeabaaaaaa dp4 o0.x, a0, c0
aaaaaaaaaaaaamaeaaaaaaoeabaaaaaaaaaaaaaaaaaaaaaa mov v0.zw, c0
"
}

}
Program "fp" {
// Fragment combos: 1
//   opengl - ALU: 54 to 54, TEX: 10 to 10
//   d3d9 - ALU: 48 to 48, TEX: 10 to 10
SubProgram "opengl " {
Keywords { }
Float 0 [_threshold]
Float 1 [_int]
SetTexture 0 [_OrgTex] 2D
SetTexture 1 [_MainTex] 2D
SetTexture 2 [_lensDirt] 2D
"!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 54 ALU, 10 TEX
PARAM c[5] = { program.local[0..1],
		{ -0.5, 0, 0.14285715, 0.5 },
		{ 1, 4, 2, 3 },
		{ 5, 6 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
TEMP R6;
TEMP R7;
TEMP R8;
TEMP R9;
ADD R0.xy, fragment.texcoord[0], c[2].x;
MUL R0.xy, R0, c[2].z;
MUL R0.zw, R0.xyxy, R0.xyxy;
ADD R0.z, R0, R0.w;
ADD R1.xy, R0, -fragment.texcoord[0];
ADD R2.zw, R1.xyxy, c[3].x;
MAD R1.xy, R0, c[3].w, -fragment.texcoord[0];
ADD R4.xy, R1, c[3].x;
RSQ R0.z, R0.z;
MUL R0.zw, R0.z, R0.xyxy;
MAD R0.zw, R0, c[2].w, -fragment.texcoord[0].xyxy;
ADD R2.xy, R0.zwzw, c[3].x;
MAD R0.xy, R0, c[4].y, -fragment.texcoord[0];
ADD R5.zw, R0.xyxy, c[3].x;
ADD R0.zw, -fragment.texcoord[0].xyxy, c[2].w;
MUL R0.zw, R0, c[2].z;
MUL R1.zw, R0, c[3].z;
ADD R3.zw, fragment.texcoord[0].xyxy, R1;
MUL R1.zw, R0, c[3].y;
ADD R4.zw, fragment.texcoord[0].xyxy, R1;
MUL R0.zw, R0, c[4].x;
ADD R5.xy, fragment.texcoord[0], R0.zwzw;
ADD R3.xy, -fragment.texcoord[0], c[3].x;
TEX R9.xyz, R5.zwzw, texture[1], 2D;
TEX R8.xyz, R5, texture[1], 2D;
TEX R5.xyz, R3.zwzw, texture[1], 2D;
TEX R7.xyz, R4.zwzw, texture[1], 2D;
TEX R6.xyz, R4, texture[1], 2D;
TEX R4.xyz, R2.zwzw, texture[1], 2D;
TEX R0, fragment.texcoord[0], texture[0], 2D;
TEX R1, fragment.texcoord[0], texture[2], 2D;
TEX R2.xyz, R2, texture[1], 2D;
TEX R3.xyz, R3, texture[1], 2D;
MOV R2.w, c[3].y;
POW R2.w, c[0].x, R2.w;
ADD R3.xyz, -R2.w, R3;
ADD R2.xyz, R2, -R2.w;
ADD R2.xyz, R2, R3;
ADD R3.xyz, -R2.w, R4;
ADD R2.xyz, R2, R3;
ADD R4.xyz, -R2.w, R5;
ADD R2.xyz, R2, R4;
ADD R3.xyz, -R2.w, R6;
ADD R2.xyz, R2, R3;
ADD R4.xyz, -R2.w, R7;
ADD R2.xyz, R2, R4;
ADD R3.xyz, -R2.w, R8;
ADD R4.xyz, -R2.w, R9;
ADD R2.xyz, R2, R3;
ADD R2.xyz, R2, R4;
MUL_SAT R2.xyz, R2, c[2].z;
MOV R2.w, c[2].y;
MUL R1, R2, R1;
MAD result.color, R1, c[1].x, R0;
END
# 54 instructions, 10 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Float 0 [_threshold]
Float 1 [_int]
SetTexture 0 [_OrgTex] 2D
SetTexture 1 [_MainTex] 2D
SetTexture 2 [_lensDirt] 2D
"ps_2_0
; 48 ALU, 10 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c2, -0.50000000, 0.14285715, 0.50000000, 1.00000000
def c3, 4.00000000, 6.00000000, 2.00000000, 3.00000000
def c4, 5.00000000, 0.00000000, 0, 0
dcl t0.xy
add r0.xy, t0, c2.x
mul r1.xy, r0, c2.y
mul_pp r0.xy, r1, r1
add_pp r0.x, r0, r0.y
add r4.xy, r1, -t0
add r8.xy, r4, c2.w
mad r4.xy, r1, c3.w, -t0
add r6.xy, r4, c2.w
rsq_pp r0.x, r0.x
mul_pp r0.xy, r0.x, r1
mad r0.xy, r0, c2.z, -t0
add r0.xy, r0, c2.w
add_pp r2.xy, -t0, c2.z
mul r2.xy, r2, c2.y
mul r3.xy, r2, c3.z
add_pp r7.xy, t0, r3
mul r3.xy, r2, c3.x
add_pp r5.xy, t0, r3
mul r2.xy, r2, c4.x
add_pp r4.xy, t0, r2
mad r1.xy, r1, c3.y, -t0
add r3.xy, r1, c2.w
add r9.xy, -t0, c2.w
mov r0.w, c4.y
texld r10, r0, s1
texld r2, t0, s2
texld r1, t0, s0
texld r3, r3, s1
texld r4, r4, s1
texld r5, r5, s1
texld r6, r6, s1
texld r7, r7, s1
texld r8, r8, s1
texld r9, r9, s1
mov_pp r0.x, c3
pow_pp r11.x, c0.x, r0.x
mov_pp r0.x, r11.x
add r9.xyz, -r0.x, r9
add r10.xyz, r10, -r0.x
add r8.xyz, -r0.x, r8
add r9.xyz, r10, r9
add r7.xyz, -r0.x, r7
add r8.xyz, r9, r8
add r6.xyz, -r0.x, r6
add r7.xyz, r8, r7
add r5.xyz, -r0.x, r5
add r6.xyz, r7, r6
add r4.xyz, -r0.x, r4
add r0.xyz, -r0.x, r3
add r5.xyz, r6, r5
add r3.xyz, r5, r4
add r0.xyz, r3, r0
mul_sat r0.xyz, r0, c2.y
mul r0, r0, r2
mad r0, r0, c1.x, r1
mov_pp oC0, r0
"
}

}

#LINE 108

            }
        }
    }