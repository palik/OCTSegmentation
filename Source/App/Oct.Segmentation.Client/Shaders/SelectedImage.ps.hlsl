// ===================================================================================
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//  FITNESS FOR A PARTICULAR PURPOSE.
// ===================================================================================

// This simple pixel shader returns the unmodified vertex color.
// ---

// output from the vertex shader serves as input
// to the pixel shader
sampler2D TexSampler			: register(s0);

struct VertexShaderOutput
{
  float4 Position : POSITION;
  float2 UV		     : TEXCOORD0;
};

// main shader function
float4 main(VertexShaderOutput vertex) : COLOR
{
  //return tex2D(TexSampler, vertex.UV);
  float4 output = tex2D(TexSampler, vertex.UV);


  output.x = 0.9;

  float alpha = 0.1;
  return float4(output.xyz * (2 * alpha), alpha);
}