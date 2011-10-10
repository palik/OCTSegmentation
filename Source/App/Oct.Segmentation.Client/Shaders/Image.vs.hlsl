// ===================================================================================
//  Copyright (c) Microsoft Corporation.  All rights reserved.
//  THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
//  FITNESS FOR A PARTICULAR PURPOSE.
// ===================================================================================

// This simple vertex shader transforms input vertices to screen space.
// ---

// transformation matrix provided by the application
float4x4 WorldViewProj : register(c0);

// vertex input to the shader matching the structure
// defined in the application
struct VertexData
{
  float3 Position : POSITION;
  float2 UV		     : TEXCOORD0;
};

// vertex shader output passed through to geometry 
// processing and a pixel shader
struct VertexShaderOutput
{
  float4 Position : POSITION;
  float2 UV		     : TEXCOORD0;
};

// main shader function
VertexShaderOutput main(VertexData vertex)
{
  VertexShaderOutput output;

  // apply standard transformation for rendering
  output.Position = mul(float4(vertex.Position,1), WorldViewProj);

  // pass the color through to the next stage
  output.UV = vertex.UV.xy;
  
  return output;
}