    Shader "Hidden/glareFxClassic" {
    Properties {
        _MainTex ("Input", RECT) = "white" {}
        _OrgTex ("Input", RECT) = "white" {}
        _lensDirt ("Input", RECT) = "white" {}
        
        _blurSamples ("", Float) = 5
        _threshold ("", Float) = 0.5
        _int ("", Float) = 1.0
        _haloint ("", Float) = 1.0
        
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
"3.0-!!ARBvp1.0
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
"vs_3_0
; 8 ALU
dcl_position o0
dcl_texcoord0 o1
def c8, 0.00000000, 0, 0, 0
dcl_position0 v0
dcl_texcoord0 v1
mov r0.zw, c8.x
mov r0.xy, v1
dp4 o1.y, r0, c5
dp4 o1.x, r0, c4
dp4 o0.w, v0, c3
dp4 o0.z, v0, c2
dp4 o0.y, v0, c1
dp4 o0.x, v0, c0
"
}

}
Program "fp" {
// Fragment combos: 1
//   opengl - ALU: 136 to 136, TEX: 13 to 13
//   d3d9 - ALU: 123 to 123, TEX: 13 to 13
SubProgram "opengl " {
Keywords { }
Float 0 [_threshold]
Float 1 [_int]
Float 2 [_haloint]
SetTexture 0 [_OrgTex] 2D
SetTexture 1 [_MainTex] 2D
SetTexture 2 [_lensDirt] 2D
"3.0-!!ARBfp1.0
OPTION ARB_precision_hint_fastest;
# 136 ALU, 13 TEX
PARAM c[7] = { program.local[0..2],
		{ -0.5, 0, 0.1, 0.5 },
		{ 1, 0.29890001, 0.58660001, 0.1145 },
		{ 2, 3, 4, 5 },
		{ 6, 7, 8, 9 } };
TEMP R0;
TEMP R1;
TEMP R2;
TEMP R3;
TEMP R4;
TEMP R5;
ADD R0.xy, fragment.texcoord[0], c[3].x;
MUL R5.xy, R0, c[3].z;
MUL R1.xy, R5, R5;
ADD R0.w, R1.x, R1.y;
ADD R0.xy, R5, -fragment.texcoord[0];
ADD R0.xy, R0, c[4].x;
TEX R0.xyz, R0, texture[1], 2D;
MUL R1.x, R0.y, c[4].z;
MAD R1.z, R0.x, c[4].y, R1.x;
RSQ R0.w, R0.w;
MUL R1.xy, R0.w, R5;
MAD R2.xy, R1, c[3].w, -fragment.texcoord[0];
MAD_SAT R0.w, R0.z, c[4], R1.z;
ADD R1.xyz, R0.w, -R0;
MAD R1.xyz, R1, c[0].x, R0;
ADD R2.xy, R2, c[4].x;
TEX R0.xyz, R2, texture[1], 2D;
ADD R2.xyz, R1, -c[0].x;
MUL R0.w, R0.y, c[4].z;
MAD R0.w, R0.x, c[4].y, R0;
MAD_SAT R0.w, R0.z, c[4], R0;
ADD R3.xyz, R0.w, -R0;
MAD R0.xyz, R3, c[0].x, R0;
ADD R1.xy, -fragment.texcoord[0], c[4].x;
TEX R1.xyz, R1, texture[1], 2D;
MUL R1.w, R1.y, c[4].z;
MAD R0.w, R1.x, c[4].y, R1;
MAD_SAT R0.w, R1.z, c[4], R0;
ADD R3.xyz, R0.w, -R1;
MAD R1.xyz, R3, c[0].x, R1;
ADD R3.xy, -fragment.texcoord[0], c[3].w;
MOV R0.w, c[4].x;
ADD R0.w, R0, -c[0].x;
RCP R0.w, R0.w;
ADD R0.xyz, R0, -c[0].x;
ADD R1.xyz, R1, -c[0].x;
MUL R5.zw, R3.xyxy, c[3].z;
MUL_SAT R1.xyz, R0.w, R1;
MUL_SAT R0.xyz, R0, R0.w;
MAD R0.xyz, R0, c[2].x, R1;
MUL_SAT R1.xyz, R0.w, R2;
ADD R2.xyz, R0, R1;
MAD R0.xy, R5, c[5].x, -fragment.texcoord[0];
MUL R1.xy, R5.zwzw, c[5].y;
ADD R0.xy, R0, c[4].x;
TEX R0.xyz, R0, texture[1], 2D;
MUL R1.w, R0.y, c[4].z;
MAD R1.w, R0.x, c[4].y, R1;
MAD_SAT R1.w, R0.z, c[4], R1;
ADD R3.xyz, R1.w, -R0;
MAD R0.xyz, R3, c[0].x, R0;
ADD R1.xy, fragment.texcoord[0], R1;
TEX R1.xyz, R1, texture[1], 2D;
MUL R2.w, R1.y, c[4].z;
MAD R2.w, R1.x, c[4].y, R2;
MAD_SAT R2.w, R1.z, c[4], R2;
ADD R4.xyz, R2.w, -R1;
MAD R1.xyz, R4, c[0].x, R1;
ADD R0.xyz, R0, -c[0].x;
MUL_SAT R0.xyz, R0.w, R0;
ADD R1.xyz, R1, -c[0].x;
MUL_SAT R1.xyz, R0.w, R1;
ADD R0.xyz, R2, R0;
ADD R2.xyz, R0, R1;
MAD R0.xy, R5, c[5].z, -fragment.texcoord[0];
MUL R1.xy, R5.zwzw, c[5].w;
ADD R0.xy, R0, c[4].x;
TEX R0.xyz, R0, texture[1], 2D;
MUL R1.w, R0.y, c[4].z;
MAD R1.w, R0.x, c[4].y, R1;
MAD_SAT R1.w, R0.z, c[4], R1;
ADD R3.xyz, R1.w, -R0;
MAD R0.xyz, R3, c[0].x, R0;
ADD R1.xy, fragment.texcoord[0], R1;
TEX R1.xyz, R1, texture[1], 2D;
MUL R2.w, R1.y, c[4].z;
MAD R2.w, R1.x, c[4].y, R2;
MAD_SAT R2.w, R1.z, c[4], R2;
ADD R4.xyz, R2.w, -R1;
MAD R1.xyz, R4, c[0].x, R1;
ADD R0.xyz, R0, -c[0].x;
MUL_SAT R0.xyz, R0.w, R0;
ADD R1.xyz, R1, -c[0].x;
MUL_SAT R1.xyz, R0.w, R1;
ADD R0.xyz, R2, R0;
ADD R2.xyz, R0, R1;
MUL R0.xy, R5.zwzw, c[6].x;
MAD R1.xy, R5, c[6].y, -fragment.texcoord[0];
ADD R0.xy, fragment.texcoord[0], R0;
TEX R0.xyz, R0, texture[1], 2D;
MUL R1.w, R0.y, c[4].z;
MAD R1.w, R0.x, c[4].y, R1;
MAD_SAT R1.w, R0.z, c[4], R1;
ADD R3.xyz, R1.w, -R0;
MAD R0.xyz, R3, c[0].x, R0;
ADD R1.xy, R1, c[4].x;
TEX R1.xyz, R1, texture[1], 2D;
MUL R2.w, R1.y, c[4].z;
MAD R2.w, R1.x, c[4].y, R2;
MAD_SAT R2.w, R1.z, c[4], R2;
ADD R4.xyz, R2.w, -R1;
MAD R1.xyz, R4, c[0].x, R1;
ADD R0.xyz, R0, -c[0].x;
MUL_SAT R0.xyz, R0.w, R0;
ADD R1.xyz, R1, -c[0].x;
MUL_SAT R1.xyz, R0.w, R1;
ADD R0.xyz, R2, R0;
ADD R0.xyz, R0, R1;
MAD R1.zw, R5.xyxy, c[6].w, -fragment.texcoord[0].xyxy;
ADD R2.xy, R1.zwzw, c[4].x;
TEX R2.xyz, R2, texture[1], 2D;
MUL R2.w, R2.y, c[4].z;
MAD R2.w, R2.x, c[4].y, R2;
MAD_SAT R2.w, R2.z, c[4], R2;
ADD R4.xyz, R2.w, -R2;
MUL R1.xy, R5.zwzw, c[6].z;
ADD R1.xy, fragment.texcoord[0], R1;
TEX R1.xyz, R1, texture[1], 2D;
MUL R1.w, R1.y, c[4].z;
MAD R1.w, R1.x, c[4].y, R1;
MAD_SAT R1.w, R1.z, c[4], R1;
ADD R3.xyz, R1.w, -R1;
MAD R1.xyz, R3, c[0].x, R1;
MAD R2.xyz, R4, c[0].x, R2;
ADD R1.xyz, R1, -c[0].x;
MUL_SAT R1.xyz, R0.w, R1;
ADD R0.xyz, R0, R1;
ADD R2.xyz, R2, -c[0].x;
MUL_SAT R2.xyz, R0.w, R2;
ADD R0.xyz, R0, R2;
MUL_SAT R2.xyz, R0, c[3].z;
TEX R0, fragment.texcoord[0], texture[0], 2D;
TEX R1, fragment.texcoord[0], texture[2], 2D;
MOV R2.w, c[3].y;
MUL R1, R2, R1;
MAD result.color, R1, c[1].x, R0;
END
# 136 instructions, 6 R-regs
"
}

SubProgram "d3d9 " {
Keywords { }
Float 0 [_threshold]
Float 1 [_int]
Float 2 [_haloint]
SetTexture 0 [_OrgTex] 2D
SetTexture 1 [_MainTex] 2D
SetTexture 2 [_lensDirt] 2D
"ps_3_0
; 123 ALU, 13 TEX
dcl_2d s0
dcl_2d s1
dcl_2d s2
def c3, -0.50000000, 0.10000000, 0.50000000, 1.00000000
def c4, 0.58660001, 0.29890001, 0.11450000, 2.00000000
def c5, 3.00000000, 4.00000000, 5.00000000, 6.00000000
def c6, 7.00000000, 8.00000000, 9.00000000, 0.00000000
dcl_texcoord0 v0.xy
add r0.xy, v0, c3.x
mul r5.xy, r0, c3.y
mul_pp r1.xy, r5, r5
add_pp r0.w, r1.x, r1.y
add r0.xy, r5, -v0
add r0.xy, r0, c3.w
texld r0.xyz, r0, s1
mul r1.x, r0.y, c4
mad r1.z, r0.x, c4.y, r1.x
rsq_pp r0.w, r0.w
mul_pp r1.xy, r0.w, r5
mad r2.xy, r1, c3.z, -v0
mad_sat r0.w, r0.z, c4.z, r1.z
add r1.xyz, r0.w, -r0
mad r1.xyz, r1, c0.x, r0
add r2.xy, r2, c3.w
texld r0.xyz, r2, s1
add r2.xyz, r1, -c0.x
mul r0.w, r0.y, c4.x
mad r0.w, r0.x, c4.y, r0
mad_sat r0.w, r0.z, c4.z, r0
add r3.xyz, r0.w, -r0
mad r0.xyz, r3, c0.x, r0
add r1.xy, -v0, c3.w
texld r1.xyz, r1, s1
mul r1.w, r1.y, c4.x
mad r0.w, r1.x, c4.y, r1
mad_sat r0.w, r1.z, c4.z, r0
add r3.xyz, r0.w, -r1
mad r1.xyz, r3, c0.x, r1
add_pp r3.xy, -v0, c3.z
mov r0.w, c0.x
add r0.w, c3, -r0
rcp r0.w, r0.w
add r0.xyz, r0, -c0.x
add r1.xyz, r1, -c0.x
mul r5.zw, r3.xyxy, c3.y
mul_sat r1.xyz, r0.w, r1
mul_sat r0.xyz, r0, r0.w
mad r0.xyz, r0, c2.x, r1
mul_sat r1.xyz, r0.w, r2
add r2.xyz, r0, r1
mad r0.xy, r5, c4.w, -v0
mul r1.xy, r5.zwzw, c5.x
add r0.xy, r0, c3.w
texld r0.xyz, r0, s1
mul r1.w, r0.y, c4.x
mad r1.w, r0.x, c4.y, r1
mad_sat r1.w, r0.z, c4.z, r1
add r3.xyz, r1.w, -r0
mad r0.xyz, r3, c0.x, r0
add_pp r1.xy, v0, r1
texld r1.xyz, r1, s1
mul r2.w, r1.y, c4.x
mad r2.w, r1.x, c4.y, r2
mad_sat r2.w, r1.z, c4.z, r2
add r4.xyz, r2.w, -r1
mad r1.xyz, r4, c0.x, r1
add r0.xyz, r0, -c0.x
mul_sat r0.xyz, r0.w, r0
add r1.xyz, r1, -c0.x
mul_sat r1.xyz, r0.w, r1
add r0.xyz, r2, r0
add r2.xyz, r0, r1
mad r0.xy, r5, c5.y, -v0
mul r1.xy, r5.zwzw, c5.z
add r0.xy, r0, c3.w
texld r0.xyz, r0, s1
mul r1.w, r0.y, c4.x
mad r1.w, r0.x, c4.y, r1
mad_sat r1.w, r0.z, c4.z, r1
add r3.xyz, r1.w, -r0
mad r0.xyz, r3, c0.x, r0
add_pp r1.xy, v0, r1
texld r1.xyz, r1, s1
mul r2.w, r1.y, c4.x
mad r2.w, r1.x, c4.y, r2
mad_sat r2.w, r1.z, c4.z, r2
add r4.xyz, r2.w, -r1
mad r1.xyz, r4, c0.x, r1
add r0.xyz, r0, -c0.x
mul_sat r0.xyz, r0.w, r0
add r1.xyz, r1, -c0.x
mul_sat r1.xyz, r0.w, r1
add r0.xyz, r2, r0
add r2.xyz, r0, r1
mul r0.xy, r5.zwzw, c5.w
mad r1.xy, r5, c6.x, -v0
add_pp r0.xy, v0, r0
texld r0.xyz, r0, s1
mul r1.w, r0.y, c4.x
mad r1.w, r0.x, c4.y, r1
mad_sat r1.w, r0.z, c4.z, r1
add r3.xyz, r1.w, -r0
mad r0.xyz, r3, c0.x, r0
add r1.xy, r1, c3.w
texld r1.xyz, r1, s1
mul r2.w, r1.y, c4.x
mad r2.w, r1.x, c4.y, r2
mad_sat r2.w, r1.z, c4.z, r2
add r4.xyz, r2.w, -r1
mad r1.xyz, r4, c0.x, r1
add r0.xyz, r0, -c0.x
mul_sat r0.xyz, r0.w, r0
add r1.xyz, r1, -c0.x
mul_sat r1.xyz, r0.w, r1
add r0.xyz, r2, r0
add r0.xyz, r0, r1
mad r1.zw, r5.xyxy, c6.z, -v0.xyxy
add r2.xy, r1.zwzw, c3.w
texld r2.xyz, r2, s1
mul r2.w, r2.y, c4.x
mad r2.w, r2.x, c4.y, r2
mad_sat r2.w, r2.z, c4.z, r2
add r4.xyz, r2.w, -r2
mul r1.xy, r5.zwzw, c6.y
add_pp r1.xy, v0, r1
texld r1.xyz, r1, s1
mul r1.w, r1.y, c4.x
mad r1.w, r1.x, c4.y, r1
mad_sat r1.w, r1.z, c4.z, r1
add r3.xyz, r1.w, -r1
mad r1.xyz, r3, c0.x, r1
mad r2.xyz, r4, c0.x, r2
add r1.xyz, r1, -c0.x
mul_sat r1.xyz, r0.w, r1
add r0.xyz, r0, r1
add r2.xyz, r2, -c0.x
mul_sat r2.xyz, r0.w, r2
add r0.xyz, r0, r2
mul_sat r2.xyz, r0, c3.y
texld r1, v0, s0
texld r0, v0, s2
mov r2.w, c6
mul r0, r2, r0
mad oC0, r0, c1.x, r1
"
}

}

#LINE 131

            }
        }
    }